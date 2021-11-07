using System.Windows;
using System.IO.Ports;
using System;
using System.IO;
using W2021.Helpers;
using Vlc.DotNet.Forms;
using System.Linq;
using Vlc.DotNet.Core.Interops.Signatures;
using System.Threading;
using System.Windows.Forms.Integration;

namespace W2021.BattleOfMoscow
{
    class AppConfig
    {
        public string ComPort { get; set; }
        public string VideoPath { get; set; }
        public int FilterStep { get; set; }
        public int UpdateRate { get; set; }
    }

    public partial class MainWindow : Window
    {
        private SerialPort _serialDevice;
        private VlcControl _vlcControl;
        private double _scrollWidth;
        private double _ratio;
        private float _currentValue = 9999;

        public MainWindow()
        {
            InitializeComponent();

            _serialDevice = new SerialPort(Config<AppConfig>.Value.ComPort);
            _serialDevice.BaudRate = 9600;
            _serialDevice.Parity = Parity.None;
            _serialDevice.StopBits = StopBits.One;
            _serialDevice.DataBits = 8;
            _serialDevice.Handshake = Handshake.None;
            _serialDevice.RtsEnable = false;

            _serialDevice.Open();

            var vlcLibPath = Path.Combine(PathHelpers.ExecutableDirectory(), "libvlc", "win-x64");
            var vlcLib = new DirectoryInfo(vlcLibPath);

            // _vlcControl = new VlcControl();
            // WindowsFormsHost.Child = _vlcControl;
            // 
            // _vlcControl.BeginInit();
            // _vlcControl.VlcLibDirectory = vlcLib;
            // _vlcControl.EndInit();
            // 
            // _vlcControl.Play(new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), Config<AppConfig>.Value.VideoPath)), new string[] { "input-repeat=65535" });

            // Player.LoadedBehavior;
            // Player.Source = new Uri(Path.Combine(PathHelpers.ExecutableDirectory(), Config<AppConfig>.Value.VideoPath), UriKind.Absolute);
            // Player.Play();
            // Player.MediaEnded += Player_MediaEnded;

            DataContext = this;

            SizeChanged += MainWindow_SizeChanged;

            new Thread(ReadLoop).Start();

            // var uiTimer = new System.Timers.Timer(1000 / Config<AppConfig>.Value.UpdateRate);
            // uiTimer.AutoReset = true;
            // uiTimer.Elapsed += UiTimer_Elapsed;
            // uiTimer.Start();
        }

        private void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Player.Position = TimeSpan.Zero;
            // Player.Play();
        }

        private void UiTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                // Test.Text = rawValue;
                ScrollView.ScrollToHorizontalOffset((1 - _currentValue) * _scrollWidth);
            }));
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = 6144.0; //  (double)_vlcControl.GetCurrentMedia().TracksInformations.First(track => track.Type == MediaTrackTypes.Video).Video.Width;
            var height = 2500.0; // (double)_vlcControl.GetCurrentMedia().TracksInformations.First(track => track.Type == MediaTrackTypes.Video).Video.Height;

            _ratio = width / height;

            WindowsFormsHost.Height = ScrollView.ActualHeight;
            WindowsFormsHost.Width = ScrollView.ActualHeight * _ratio;

            // Player.Height = ScrollView.ActualHeight;
            // Player.Width = ScrollView.ActualHeight * _ratio;

            // _vlcControl.Video.CropGeometry = $"{ScrollView.ActualWidth}:{ScrollView.ActualHeight}";
            _scrollWidth = WindowsFormsHost.Width - ScrollView.ActualWidth;
            // _scrollWidth = Player.Width - ScrollView.ActualWidth;
        }

        private void ReadLoop()
        {
            while (_serialDevice.IsOpen)
            {
                string rawValue = _serialDevice.ReadLine();

                try
                {
                    int newValue = int.Parse(rawValue);
                    float value = (float)newValue / 9999.0F;

                    _currentValue = value;

                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        // Test.Text = rawValue;
                        ScrollView.ScrollToHorizontalOffset((1 - _currentValue) * _scrollWidth);
                    }));
                }
                catch (Exception) { }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _serialDevice.Close();
            _serialDevice.Dispose();
        }
    }
}
