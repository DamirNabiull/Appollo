using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Forms;

namespace Appolo.RifleChambers.Clerk_VideoStand
{
    public partial class Form1 : Form
    {
        public VlcControl vlcController;
        public string strWorkPath;

        private HttpListener _listener;
        private bool _server = true;
        private string _state = "0";

        private Random rand = new Random();
        public string _video_name = "video_name";
        public string[] _idles = { "idle.avi", "idle2.avi" };
        public string[] _scans = { "scan_left_1.avi", "scan_left_2.avi", "scan_right_1.avi", "scan_right_2.avi", "scan_3.avi" };
        public string _exec = "exec.avi";
        public Form1()
        {
            InitializeComponent();

            strWorkPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var vlcLibPath = Path.Combine(strWorkPath, "libvlc", "win-x64");
            var vlcLib = new DirectoryInfo(vlcLibPath);

            vlcController = new VlcControl();
            vlcController.BeginInit();
            vlcController.VlcLibDirectory = vlcLib;
            vlcController.EndInit();

            PlayIdle();

            vlcController.EndReached += VlcController_EndReached;

            //vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", "idle.avi")), new string[] { "input-repeat=65535" });

            //vlcController.EndReached += VlcController_EndReached

            Controls.Add(vlcController);

            new Thread(RunServer).Start();
        }

        private void PlayIdle()
        {
            _video_name = _idles[rand.Next(2)];
            Trace.WriteLine(_video_name);
            vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", _video_name)));
        }

        private void PlayScan()
        {
            _video_name = _scans[rand.Next(5)];
            Trace.WriteLine(_video_name);
            vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", _video_name)));
        }

        private void PlayExec()
        {
            Trace.WriteLine(_exec);
            vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", _exec)));
        }

        private void CheckState()
        {
            if (_state == "1")
            {
                PlayScan();
            }
            else if (_state == "2")
            {
                PlayExec();
            }
            else
            {
                PlayIdle();
            }
        }

        private void VlcController_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            Task.Run(() =>
            {
                CheckState();
            });
        }

        private void RunServer()
        {
            try
            {
                _listener = new HttpListener();

                NetAclChecker.AddAddress("http://*:3000/play/");
                NetAclChecker.AddAddress("http://+:3000/play/");

                _listener.Prefixes.Add("http://*:3000/play/");
                _listener.Prefixes.Add("http://+:3000/play/");

                _listener.Start();

                while (_server)
                {
                    ThreadPool.QueueUserWorkItem(Process, _listener.GetContext());
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        void Process(object o)
        {
            var context = o as HttpListenerContext;
            var vars = context.Request.Url.ParseQueryString();

            Trace.WriteLine(vars["state"]);
            _state = vars["state"];
            CheckState();
            //PlayVideo(vars["video"]);

            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (vlcController == null)
            {
                return;
            }
            vlcController.Size = Size;
            vlcController.Video.CropGeometry = $"{Width}:{Height}";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server = false;
            Environment.Exit(0);
        }
    }

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

            Debug.WriteLine(args);

            ProcessStartInfo psi = new ProcessStartInfo("netsh", args);
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.UseShellExecute = true;

            Process.Start(psi).WaitForExit();
        }
    }
}
