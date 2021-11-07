using System.Collections.Generic;
using System.IO;
using System.Windows;
using W2021.Helpers;
using Vlc.DotNet.Forms;
using Emgu.CV;
using ZXing;
using System.Drawing;
using System;
using System.Threading;
using TIS.Imaging;
using Emgu.CV.Structure;
using FlaUI.Core.AutomationElements;
using System.Diagnostics;
using FlaUI.UIA3;
using System.Linq;

namespace W2021.AwardFoundHero
{
    class Rect
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    class AppConfig
    {
        public List<string> Videos { get; set; }
        public Rect CropRect { get; set; }
        public bool Debug { get; set; }
        public int FailsToStop { get; set; }
    }

    public partial class MainWindow : System.Windows.Window
    {
        private VlcControl _vlcControl;
        private BarcodeReader _barcodeReader;
        private int _lastIndex = 66;
        private ICImagingControl _icControl;
        private int _fails = 0;

        public MainWindow()
        {
            InitializeComponent();

            var vlcLibPath = Path.Combine(PathHelpers.ExecutableDirectory(), "libvlc", "win-x86");
            var vlcLib = new DirectoryInfo(vlcLibPath);
            
            _vlcControl = new VlcControl();
            WindowsFormsHost.Child = _vlcControl;
            _vlcControl.BeginInit();
            _vlcControl.VlcLibDirectory = vlcLib;
            _vlcControl.EndInit();

            _icControl = new ICImagingControl();
            _icControl.Sink = new FrameQueueSink(ProccessBuffer, MediaSubtypes.RGB8, 5);
            
            _icControl.LiveStop();
            new Thread(() =>
            {
                Thread.Sleep(300);

                var app = FlaUI.Core.Application.Attach(Process.GetCurrentProcess().Id);

                using (var automation = new UIA3Automation())
                {
                    var icWindow = app.GetAllTopLevelWindows(automation).First(window => window.Name == "Device Settings");

                    var deviceCombo = icWindow.FindFirstDescendant(cf => cf.ByAutomationId("1004")).AsComboBox();
                    deviceCombo.Select(0).Click();

                    Thread.Sleep(300);

                    var fpsCombo = icWindow.FindFirstDescendant(cf => cf.ByAutomationId("1007")).AsComboBox();
                    fpsCombo.Select(0).Click();

                    Thread.Sleep(300);

                    var okButton = icWindow.FindFirstDescendant(cf => cf.ByAutomationId("1")).AsButton();
                    okButton.Click();
                }
            }).Start();
            _icControl.ShowDeviceSettingsDialog(Process.GetCurrentProcess().MainWindowHandle);
            _icControl.LiveStart();

            _icControl.LiveStop();
            new Thread(() =>
            {
                Thread.Sleep(300);

                var app = FlaUI.Core.Application.Attach(Process.GetCurrentProcess().Id);

                using (var automation = new UIA3Automation())
                {
                    var icWindow = app.GetAllTopLevelWindows(automation).First(window => window.Name == "Device Settings");

                    var deviceCombo = icWindow.FindFirstDescendant(cf => cf.ByAutomationId("1004")).AsComboBox();
                    deviceCombo.Select(0).Click();

                    Thread.Sleep(300);

                    var fpsCombo = icWindow.FindFirstDescendant(cf => cf.ByAutomationId("1007")).AsComboBox();
                    fpsCombo.Select(1).Click();

                    Thread.Sleep(300);

                    var okButton = icWindow.FindFirstDescendant(cf => cf.ByAutomationId("1")).AsButton();
                    okButton.Click();
                }
            }).Start();
            _icControl.ShowDeviceSettingsDialog(Process.GetCurrentProcess().MainWindowHandle);
            _icControl.LiveStart();

            _barcodeReader = new BarcodeReader();
            _barcodeReader.Options.TryHarder = true;
            _barcodeReader.Options.PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.DATA_MATRIX };

            SizeChanged += MainWindow_SizeChanged;
        }

        private FrameQueuedResult ProccessBuffer(IFrameQueueBuffer buffer)
        {
            // var frameBuffer = _frameSink.SnapSingle(TimeSpan.FromSeconds(1));
            var frame = buffer.CreateBitmapWrap();

            var cropRect = Config<AppConfig>.Value.CropRect;
            var imageCV = frame.ToImage<Rgb, Byte>();
            imageCV.ROI = new Rectangle(cropRect.Left, cropRect.Top, cropRect.Width, cropRect.Height);

            var frameBitmap = imageCV.ToBitmap();

            try
            {
                if (Config<AppConfig>.Value.Debug)
                {
                    imageCV.Save("fuck.png");
                }
            }
            catch (Exception ex) { }

            var result = _barcodeReader.Decode(frameBitmap);
            
            if (result != null)
            {
                _fails = 0;
                int decodedIndex = int.Parse(result.ToString()) - 1;
            
                if (decodedIndex != _lastIndex)
                {
                    _lastIndex = decodedIndex;
                    ChangePage(decodedIndex);
                }
            }// else
            // {
            //     _fails += 1;
            // 
            //     if (_fails == Config<AppConfig>.Value.FailsToStop && _vlcControl.IsPlaying) {
            //         ThreadPool.QueueUserWorkItem(_ =>
            //         {
            //             _vlcControl.Stop();
            //         });
            //     }
            // }


            return FrameQueuedResult.ReQueue;
        }

        private void ChangePage(int decodedIndex)
        {
            // Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            // {
            //     Test.Text = decodedIndex.ToString();
            // }));

            ThreadPool.QueueUserWorkItem(_ =>
            {
                _vlcControl.Play(new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), Config<AppConfig>.Value.Videos[decodedIndex])), new string[] { "input-repeat=65535" });
            });
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _vlcControl.Video.CropGeometry = $"{WindowsFormsHost.ActualWidth}:{WindowsFormsHost.ActualHeight}";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _icControl.LiveStop();
            _icControl.Dispose();
        }
    }
}
