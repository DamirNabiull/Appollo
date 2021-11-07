using Gma.System.MouseKeyHook;
using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Vlc.DotNet.Forms;
using W2021.Helpers;

namespace W2021.NoExpirationDate
{
    class AppConfig
    {
        public string ComPort { get; set; }
        public string VideoPath { get; set; }
    }

    public partial class MainWindow : Window
    {
        private SerialPort _serialDevice;
        private VlcControl _vlcControl;

        public MainWindow()
        {
            InitializeComponent();

            // _serialDevice = new SerialPort(Config<AppConfig>.Value.ComPort);
            // _serialDevice.BaudRate = 9600;
            // _serialDevice.DataReceived += _serialDevice_DataReceived;
            // _serialDevice.Open();
            // 
            var vlcLibPath = Path.Combine(PathHelpers.ExecutableDirectory(), "libvlc", "win-x64");
            var vlcLib = new DirectoryInfo(vlcLibPath);

            _vlcControl = new VlcControl();
            WindowsFormsHost.Child = _vlcControl;

            _vlcControl.BeginInit();
            _vlcControl.VlcLibDirectory = vlcLib;
            _vlcControl.EndInit();
            _vlcControl.SetMedia(new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), Config<AppConfig>.Value.VideoPath)), new string[] { "input-repeat=65535" });

            SizeChanged += MainWindow_SizeChanged;

            var hook = Hook.GlobalEvents();

            hook.MouseDown += Hook_MouseDown;
            hook.MouseUp += Hook_MouseUp;
        }

        private void Hook_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                _vlcControl.Play();
            }));
        }

        private void Hook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                _vlcControl.Stop();
            }));
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _vlcControl.Video.CropGeometry = $"{WindowsFormsHost.ActualWidth}:{WindowsFormsHost.ActualHeight}";
        }

        // private void _serialDevice_DataReceived(object sender, SerialDataReceivedEventArgs e)
        // {
        //     int rawValue = _serialDevice.ReadByte();
        // 
        //     ThreadPool.QueueUserWorkItem(_  =>
        //     {
        //         if (rawValue == 0)
        //         {
        //             _vlcControl.Stop();
        //         }
        //         else
        //         {
        //             _vlcControl.Play();
        //         }
        //     });
        // }
    }
}
