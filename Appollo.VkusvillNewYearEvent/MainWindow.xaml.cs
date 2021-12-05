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
        protected static byte[] byteData = new byte[1024];
        public string[] names = new string[3];
        public int[] indexes = { 1, 2, 3 };
        public int[] indexes_rand = new int[3];
        public string my_choise = "-";
        public string another_choise = "-";
        public Random rand = new Random();

        private string _mjpeg_url;
        readonly MjpegDecoder _mjpeg;

        public MainWindow()
        {
            InitializeComponent();

            Start.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Hidden;
            Choose.Visibility = Visibility.Hidden;

            SendButton.IsEnabled = false;

            using (StreamReader reader = new StreamReader("names.txt"))
            {
                for (int i = 0; i < 3; i++)
                {
                    names[i] = reader.ReadLine();
                }
            }

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
            indexes_rand = indexes.OrderBy(x => rand.Next()).ToArray();

            ImageItem1.Source = new BitmapImage(new Uri($@"{PathHelpers.ExecutableDirectory()}/images/Item{indexes_rand[0]}.png"));
            ImageItem2.Source = new BitmapImage(new Uri($@"{PathHelpers.ExecutableDirectory()}/images/Item{indexes_rand[1]}.png"));
            ImageItem3.Source = new BitmapImage(new Uri($@"{PathHelpers.ExecutableDirectory()}/images/Item{indexes_rand[2]}.png"));

            TextItem1.Text = names[indexes_rand[0] - 1];
            TextItem2.Text = names[indexes_rand[1] - 1];
            TextItem3.Text = names[indexes_rand[2] - 1];

            Start.Visibility = Visibility.Hidden;
            HomeButton.Visibility = Visibility.Visible;
            Choose.Visibility = Visibility.Visible;
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            my_choise = names[indexes_rand[Int32.Parse(but.Name.Substring(10)) - 1] - 1];
            SendButton.IsEnabled = true;

            Shadow1.Visibility = Visibility.Visible;
            Shadow2.Visibility = Visibility.Visible;
            Shadow3.Visibility = Visibility.Visible;

            ((Image)((Grid)but.Content).Children[4]).Visibility = Visibility.Hidden;
        }

        private void Send_Item_Click(object sender, RoutedEventArgs e)
        {
            Shadow1.Visibility = Visibility.Hidden;
            Shadow2.Visibility = Visibility.Hidden;
            Shadow3.Visibility = Visibility.Hidden;
            Trace.WriteLine(my_choise);
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Start.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Hidden;
            Choose.Visibility = Visibility.Hidden;
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
