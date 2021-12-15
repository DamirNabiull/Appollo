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

namespace Appollo.MercedesEQS
{
    public partial class Form1 : Form
    {
        public VlcControl vlcController;
        public string strWorkPath;

        private HttpListener _listener;
        private bool _server = true;
        private string _state = "idle";
        private string video_name = "idle.mp4";
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

            vlcController.EndReached += VlcController_EndReached;

            Controls.Add(vlcController);

            new Thread(RunServer).Start();
        }


        private void PlayVideo()
        {
            this.Invoke(new Action(() =>
            {
                vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", video_name)));
            }));
        }

        private void VlcController_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            Task.Run(() =>
            {
                vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", video_name)));
            });
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
            }
        }

        void Process(object o)
        {
            var context = o as HttpListenerContext;
            var vars = context.Request.Url.ParseQueryString();

            Trace.WriteLine(vars["video"]);
            _state = vars["video"];
            video_name = _state + ".mp4";
            Trace.WriteLine(video_name);
            PlayVideo();

            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            _server = false;
            Environment.Exit(0);
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            if (vlcController == null)
            {
                return;
            }
            vlcController.Size = Size;
            vlcController.Video.CropGeometry = $"{1920}:{1944}";
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            PlayVideo();
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
