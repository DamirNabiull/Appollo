using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Vlc.DotNet.Forms;
using W2021.Helpers;

namespace W2021.SpringOfVideos
{
    public static class UriExtensions
    {
        private static readonly Regex _regex = new Regex(@"[?&](\w[\w.]*)=([^?&]+)");

        public static IReadOnlyDictionary<string, string> ParseQueryString(this System.Uri uri)
        {
            var match = _regex.Match(uri.PathAndQuery);
            var paramaters = new Dictionary<string, string>();
            while (match.Success)
            {
                paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }
            return paramaters;
        }
    }

    public static class NetAclChecker
    {
        public static void AddAddress(string address)
        {
            AddAddress(address, Environment.UserDomainName, Environment.UserName);
        }

        public static void AddAddress(string address, string domain, string user)
        {
            string args = string.Format(@"http add urlacl url={0} user={1}\{2}", address, domain, user);

            ProcessStartInfo psi = new ProcessStartInfo("netsh", args);
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.UseShellExecute = true;

            Process.Start(psi).WaitForExit();
        }
    }

    public partial class Form1 : Form
    {
        public VlcControl LeftPlayer;
        public VlcControl CenterPlayer;
        public VlcControl RightPlayer;

        private BorderPanel _borderPanel;
        private VideoPanel _videoPanel;
        private HttpListener _listener;
        private bool _server = true;

        public int EditIndex;
        public bool EditMode = false;


        public Form1()
        {
            InitializeComponent();

            var vlcLibPath = Path.Combine(PathHelpers.ExecutableDirectory(), "libvlc", "win-x64");
            var vlcLib = new DirectoryInfo(vlcLibPath);
            
            BackColor = Color.Black;
            
            LeftPlayer = new VlcControl();
            LeftPlayer.BeginInit();
            LeftPlayer.VlcLibDirectory = vlcLib;
            LeftPlayer.EndInit();
            LeftPlayer.SetMedia(new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), "Videos/Left.mp4")), new string[] { "input-repeat=65535" });
            
            CenterPlayer = new VlcControl();
            CenterPlayer.BeginInit();
            CenterPlayer.VlcLibDirectory = vlcLib;
            CenterPlayer.EndInit();
            CenterPlayer.SetMedia(new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), "Videos/Center.mp4")), new string[] { "input-repeat=65535" });
            
            RightPlayer = new VlcControl();
            RightPlayer.BeginInit();
            RightPlayer.VlcLibDirectory = vlcLib;
            RightPlayer.EndInit();
            RightPlayer.SetMedia(new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), "Videos/Right.mp4")), new string[] { "input-repeat=65535" });
            
            _borderPanel = new BorderPanel();
            _borderPanel.Enabled = false;
            
            _videoPanel = new VideoPanel();
            _videoPanel.FormBorderStyle = FormBorderStyle.None;
            _videoPanel.WindowState = FormWindowState.Maximized;
            _videoPanel.Bounds = Screen.AllScreens.First(s => s.Primary).Bounds;
            _videoPanel.Show(this);
            _videoPanel.Hide();

            this.Focus();
            
            Controls.Add(LeftPlayer);
            Controls.Add(CenterPlayer);
            Controls.Add(RightPlayer);

            Controls.Add(_borderPanel);
            _borderPanel.BringToFront();
            _borderPanel.Visible = false;

            SavePositions();
            
            LeftPlayer.Play();
            CenterPlayer.Play();
            RightPlayer.Play();

            new Thread(RunServer).Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _server = false;
        }

        private void RunServer()
        {
            try
            {
                _listener = new HttpListener();

                NetAclChecker.AddAddress("http://*:3001/play/");
                NetAclChecker.AddAddress("http://+:3001/play/");

                _listener.Prefixes.Add("http://*:3001/play/");
                _listener.Prefixes.Add("http://+:3001/play/");
                
                _listener.Start();

                while (_server)
                {
                    ThreadPool.QueueUserWorkItem(Process, _listener.GetContext());
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.ToString());
                // NativeWinAlert.Error(e.ToString(), "What the fuck wrong with you, man");
            }
        }

        void Process(object o)
        {
            var context = o as HttpListenerContext;
            var vars = context.Request.Url.ParseQueryString();

            PlayVideo(int.Parse(vars["pos"]), vars["video"]);

            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void SavePositions()
        {
            Config<AppConfig>.Save();

            LeftPlayer.Location = Config<AppConfig>.Value.PlayerLocations[0];
            LeftPlayer.Size = Config<AppConfig>.Value.PlayerSizes[0];

            CenterPlayer.Location = Config<AppConfig>.Value.PlayerLocations[1];
            CenterPlayer.Size = Config<AppConfig>.Value.PlayerSizes[1];

            RightPlayer.Location = Config<AppConfig>.Value.PlayerLocations[2];
            RightPlayer.Size = Config<AppConfig>.Value.PlayerSizes[2];

            LeftPlayer.Video.CropGeometry = $"{LeftPlayer.Width}:{LeftPlayer.Height}";
            CenterPlayer.Video.CropGeometry = $"{CenterPlayer.Width}:{CenterPlayer.Height}";
            RightPlayer.Video.CropGeometry = $"{RightPlayer.Width}:{RightPlayer.Height}";
        }

        private void PlayVideo(int videoPos, string videoPath)
        {
            this.Invoke(() =>
            {
                _videoPanel.Hide();
                _videoPanel.Show(this);

                this.Focus();

                _videoPanel.PlayVideo(videoPos, videoPath);
            });
        }

        private void CloseVideo()
        {
            _videoPanel.Hide();
            _videoPanel.CloseVideo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Focus();
            this.KeyPreview = true;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            var value = (ModifierKeys & Keys.Control) == Keys.Control ? -1 : 1;

            if (e.KeyCode == Keys.E)
            {
                ToggleEditMode();
            }
            else if (e.KeyCode == Keys.D1 && EditMode)
            {
                EditIndex = 0;
                _borderPanel.Update(EditMode, EditIndex);
            }
            else if (e.KeyCode == Keys.D2 && EditMode)
            {
                EditIndex = 1;
                _borderPanel.Update(EditMode, EditIndex);
            }
            else if (e.KeyCode == Keys.D3 && EditMode)
            {
                EditIndex = 2;
                _borderPanel.Update(EditMode, EditIndex);
            }
            else if (e.KeyCode == Keys.Left && EditMode)
            {
                Point temp = Config<AppConfig>.Value.PlayerLocations[EditIndex];
                temp.X += value;

                Size temp2 = Config<AppConfig>.Value.PlayerSizes[EditIndex];
                temp2.Width -= value;

                Config<AppConfig>.Value.PlayerLocations[EditIndex] = temp;
                Config<AppConfig>.Value.PlayerSizes[EditIndex] = temp2;

                _borderPanel.Update(EditMode, EditIndex);
            }
            else if (e.KeyCode == Keys.Right && EditMode)
            {
                Size temp2 = Config<AppConfig>.Value.PlayerSizes[EditIndex];
                temp2.Width -= value;

                Config<AppConfig>.Value.PlayerSizes[EditIndex] = temp2;

                _borderPanel.Update(EditMode, EditIndex);
            }
            else if (e.KeyCode == Keys.Up && EditMode)
            {
                Point temp = Config<AppConfig>.Value.PlayerLocations[EditIndex];
                temp.Y += value;

                Size temp2 = Config<AppConfig>.Value.PlayerSizes[EditIndex];
                temp2.Height -= value;

                Config<AppConfig>.Value.PlayerLocations[EditIndex] = temp;
                Config<AppConfig>.Value.PlayerSizes[EditIndex] = temp2;

                _borderPanel.Update(EditMode, EditIndex);
            }
            else if (e.KeyCode == Keys.Down && EditMode)
            {
                Size temp2 = Config<AppConfig>.Value.PlayerSizes[EditIndex];
                temp2.Height -= value;

                Config<AppConfig>.Value.PlayerSizes[EditIndex] = temp2;

                _borderPanel.Update(EditMode, EditIndex);
            } else if (e.KeyCode == Keys.T && !EditMode)
            {
                PlayVideo(1, "Videos/Test.m4v");
            }
            else if (e.KeyCode == Keys.Y && !EditMode)
            {
                CloseVideo();
            }
        }

        private void ToggleEditMode()
        { 
            EditMode = !EditMode;

            if (EditMode)
            {
                EditIndex = 0;

                LeftPlayer.Stop();
                CenterPlayer.Stop();
                RightPlayer.Stop();

                _borderPanel.Visible = true;
            } else {
                _borderPanel.Visible = false;

                LeftPlayer.Play();
                CenterPlayer.Play();
                RightPlayer.Play();

                SavePositions();
            }

            _borderPanel.Update(EditMode, EditIndex);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            _borderPanel.Size = Size;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
            {
                object sender = Control.FromHandle(msg.HWnd);
                KeyEventArgs e = new KeyEventArgs(keyData);
                Form1_KeyDown(sender, e);

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
