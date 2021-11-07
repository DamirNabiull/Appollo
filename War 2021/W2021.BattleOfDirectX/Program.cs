using System;
using System.Threading;
using System.Windows.Forms;

using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.MediaFoundation;

using DXDevice = SharpDX.Direct3D11.Device;
using SharpDX.DXGI;
using SharpDX.Windows;
using System.IO.Ports;
using W2021.Helpers;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Media3D;


namespace W2021.BattleOfDirectX
{
    class AppConfig
    {
        public string ComPort { get; set; }
        public string VideoPath { get; set; }
    }

    static class Program
    {
        private static SerialPort _serialDevice;
        private static float _value = 0f;

        private static int _videoWidth;
        private static int _videoHeight;

        private static MediaEngine _mediaEngine;
        private static MediaEngineEx _mediaEngineEx;
        private static DXDevice _device;
        private static SwapChain _swapChain;
        private static DXGIDeviceManager _dxgiManager;
        private static Surface _surface;
        private static ManualResetEvent _readyToPlay = new ManualResetEvent(false);

        private static VideoNormalizedRect _rect = new VideoNormalizedRect();

        private static void MediaEngine_PlaybackEvent(MediaEngineEvent mediaEvent, long param1, int param2)
        {
            switch (mediaEvent)
            {
                case MediaEngineEvent.CanPlay:
                    _readyToPlay.Set();
                    break;
                default:
                    break;
            }
        }

        private static DXDevice CreateDeviceForVideo(out DXGIDeviceManager manager)
        {
            var device = new DXDevice(DriverType.Hardware, DeviceCreationFlags.BgraSupport | DeviceCreationFlags.VideoSupport);

            DeviceMultithread mt = device.QueryInterface<DeviceMultithread>();
            mt.SetMultithreadProtected(true);
            mt.Dispose();

            manager = new DXGIDeviceManager();
            manager.ResetDevice(device);

            return device;
        }

        private static SwapChain CreateSwapChain(DXDevice dxdevice, IntPtr handle)
        {
            var dxgidevice = dxdevice.QueryInterface<SharpDX.DXGI.Device>();
            var adapter = dxgidevice.Adapter.QueryInterface<Adapter>();
            var factory = adapter.GetParent<Factory1>();

            SwapChainDescription sd = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                IsWindowed = false,
                OutputHandle = handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput,
                Flags = SwapChainFlags.None
            };

            return new SwapChain(factory, dxdevice, sd);
        }

        private static void ReadLoop()
        {
            while (_serialDevice.IsOpen && !_device.IsDisposed)
            {
                string rawValue = _serialDevice.ReadLine();

                try
                {
                    int newValue = int.Parse(rawValue);
                    float value = (float)newValue / 9999.0F;

                    _value = 1 - value;
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _serialDevice = new SerialPort(Config<AppConfig>.Value.ComPort);
            _serialDevice.BaudRate = 9600;
            _serialDevice.Parity = Parity.None;
            _serialDevice.StopBits = StopBits.One;
            _serialDevice.DataBits = 8;
            _serialDevice.Handshake = Handshake.None;
            _serialDevice.RtsEnable = false;

            _serialDevice.Open();

            MediaManager.Startup();

            var renderForm = new SharpDX.Windows.RenderForm();

            renderForm.FormBorderStyle = FormBorderStyle.None;
            renderForm.WindowState = FormWindowState.Maximized;

            renderForm.Show();


            _device = CreateDeviceForVideo(out _dxgiManager);

            var mediaEngineFactory = new MediaEngineClassFactory();

            var attr = new MediaEngineAttributes();
            attr.VideoOutputFormat = (int)Format.B8G8R8A8_UNorm;
            attr.DxgiManager = _dxgiManager;
            attr.Set(MediaEngineAttributeKeys.PlaybackHwnd, renderForm.Handle);

            _mediaEngine = new MediaEngine(mediaEngineFactory, attr, MediaEngineCreateFlags.None);
            _mediaEngine.PlaybackEvent += MediaEngine_PlaybackEvent;
            _mediaEngine.Loop = true;

            _mediaEngineEx = _mediaEngine.QueryInterface<MediaEngineEx>();

            var fileStream = new FileStream(Path.Combine(PathHelpers.ExecutableDirectory(), Config<AppConfig>.Value.VideoPath), FileMode.Open);
            var stream = new ByteStream(fileStream);

            var url = new Uri(Path.Combine(PathHelpers.ExecutableDirectory(), Config<AppConfig>.Value.VideoPath), UriKind.RelativeOrAbsolute);
            _mediaEngineEx.SetSourceFromByteStream(stream, url.AbsoluteUri);

            _readyToPlay.WaitOne(1000);

            _swapChain = CreateSwapChain(_device, renderForm.Handle);

            var texture = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);
            _surface = texture.QueryInterface<SharpDX.DXGI.Surface>();

            new Thread(ReadLoop).Start();

            _mediaEngine.GetNativeVideoSize(out _videoWidth, out _videoHeight);

            _mediaEngineEx.Play();

            var videoRatio = (float)_videoHeight / (float)_videoWidth;
            var ratio = (float)renderForm.Width / (float)renderForm.Height;
            ratio = ratio * videoRatio;

            var width = renderForm.Width;
            var height = renderForm.Height;

            RenderLoop.Run(renderForm, () =>
            {
                //Transfer frame if a new one is available
                if (_mediaEngine.OnVideoStreamTick(out _))
                {
                    var left = (1 - ratio) * _value;
                    
                    _rect.Left = left;
                    _rect.Top = 0;
                    _rect.Right = left + ratio;
                    _rect.Bottom = 1;

                    _mediaEngine.TransferVideoFrame(_surface, _rect, new SharpDX.Rectangle(0, 0, width, height), null);
                }

                _swapChain.Present(1, SharpDX.DXGI.PresentFlags.None);
            });

            _serialDevice.Close();
            _mediaEngine.Shutdown();
            _swapChain.Dispose();
            _device.Dispose();
        }
    }
}
