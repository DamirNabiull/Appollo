using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Threading;
using MjpegProcessor;

namespace Appollo.VkusvillNewYearEvent
{
    public partial class MainWindow : Window
    {
        private HttpListener _listener;
        private bool _server = true;
        BitmapImage bi;
        protected static byte[] byteData = new byte[1024];
        public static string text = "";
        public static int check = 0;
        public static int got = 0;

        private string _mjpeg_url = "http://25.61.239.215:7777/mjpegfeed?1080x1920";
        readonly MjpegDecoder _mjpeg;

        public MainWindow()
        {
            InitializeComponent();

            _mjpeg_url = $"http://{Config<AppConfig>.Value.Ip}:{Config<AppConfig>.Value.Port}/mjpegfeed?{Config<AppConfig>.Value.WidthHeight}";
            Trace.WriteLine(_mjpeg_url);
            _mjpeg = new MjpegDecoder();
            _mjpeg.FrameReady += mjpeg_FrameReady;
            _mjpeg.ParseStream(new Uri(_mjpeg_url));

            new Thread(RunServer).Start();
        }

        private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {
            ImageWebcam.Source = e.BitmapImage;
        }
        
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            Trace.WriteLine("I am here");
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _server = false;
            Environment.Exit(0);
        }

        private void RunServer()
        {
            try
            {
                _listener = new HttpListener();

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
            var request = context.Request;

            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.Visibility = Visibility.Hidden;
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

    public class AppConfig
    {
        public string Port { get; set; }
        public string Ip { get; set; }
        public string WidthHeight {  get; set; }
    }
}
