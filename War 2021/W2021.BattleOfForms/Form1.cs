using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using Vlc.DotNet.Forms;
using W2021.Helpers;

namespace W2021.BattleOfForms
{
    class AppConfig
    {
        public string ComPort { get; set; }
        public string VideoPath { get; set; }
        public int FilterStep { get; set; }
        public int UpdateRate { get; set; }
    }

    public partial class Form1 : Form
    {
        private SerialPort _serialDevice;
        private VlcControl _vlcControl;
        private int _scrollWidth;

        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

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

            _vlcControl = new VlcControl();
            _vlcControl.BeginInit();
            _vlcControl.VlcLibDirectory = vlcLib;
            _vlcControl.EndInit();

            _vlcControl.Play(new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), Config<AppConfig>.Value.VideoPath)), new string[] { "input-repeat=65535" });

            Controls.Add(_vlcControl);

            new Thread(ReadLoop).Start();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var width = 6144.0; //  (double)_vlcControl.GetCurrentMedia().TracksInformations.First(track => track.Type == MediaTrackTypes.Video).Video.Width;
            var height = 2500.0; // (double)_vlcControl.GetCurrentMedia().TracksInformations.First(track => track.Type == MediaTrackTypes.Video).Video.Height;

            var ratio = width / height;

            _vlcControl.Height = Height;
            _vlcControl.Width = (int)Math.Round(Height * ratio);

            _vlcControl.Video.CropGeometry = $"{Width}:{Height}";
            _scrollWidth = _vlcControl.Width - Width;
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

                    this.Invoke(new Action(() => {
                        var temp = _vlcControl.Location;
                        temp.X = (int)Math.Round((value - 1) * _scrollWidth);

                        _vlcControl.Location = temp;
                    }));
                }
                catch (Exception) { }
            }
        }
    }
}
