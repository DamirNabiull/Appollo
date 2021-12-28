using System;
using System.Collections.Generic;
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

namespace VlcFormsApp
{
    public partial class Form2 : Form
    {
        public VlcControl vlcController;
        public string strWorkPath;

        private HttpListener _listener;
        private bool _server = true;

        public Form2()
        {
            InitializeComponent();

            strWorkPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var vlcLibPath = Path.Combine(strWorkPath, "libvlc", "win-x64");
            var vlcLib = new DirectoryInfo(vlcLibPath);

            vlcController = new VlcControl();
            vlcController.BeginInit();
            vlcController.VlcLibDirectory = vlcLib;
            vlcController.EndInit();
            
            vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", "0.mp4")));

            vlcController.EndReached += VlcController_EndReached;

            Controls.Add(vlcController);

            new Thread(RunServer).Start();
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

            if (vars["video"] == "stop")
            {
                StopVideo();
            }
            else if (vars["video"] == "resume")
            {
                ResumeVideo();
            }
            else
            {
                PlayVideo(vars["video"]);
            }

            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void VlcController_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            Task.Run(() =>
            {
                vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", "0.mp4")));
            });
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            if (vlcController == null)
            {
                return;
            }
            vlcController.Size = Size;
            vlcController.Video.CropGeometry = $"{Width}:{Height}";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void PlayVideo(string videoPath)
        {
            this.Invoke(new Action(() =>
            {
                vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", videoPath)));
            }));
        }

        private void StopVideo()
        {
            this.Invoke(new Action(() =>
            {
                vlcController.Pause();
            }));
        }

        private void ResumeVideo()
        {
            this.Invoke(new Action(() =>
            {
                vlcController.Play();
            }));
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
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
