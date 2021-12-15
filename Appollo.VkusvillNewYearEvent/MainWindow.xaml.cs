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
using System.Net.Http;
using WpfAnimatedGif;

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
        public int my_choise = 0;
        public int another_choise = 0;
        public int copy_choise = 0;
        public int state = 0;
        public bool received = false;
        public bool sended = false;
        public bool in_success = false;
        public Random rand = new Random();
        public int animation_index = 0;
        public string[] animation_names = { "Морква_00", "Круасан_00", "Чипос_00"};
        public DispatcherTimer timer;
        bool in_menu = false;
        private HttpClient _client = new HttpClient();

        private string _mjpeg_url;
        readonly MjpegDecoder _mjpeg;

        public MainWindow()
        {
            InitializeComponent();

            Start.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Hidden;
            Choose.Visibility = Visibility.Hidden;
            SuccessGrid.Visibility = Visibility.Hidden;
            FailGrid.Visibility = Visibility.Hidden;
            MenuGrid.Visibility = Visibility.Hidden;

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

            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(15) };
            timer.Tick += UpdateStream;
            timer.Start();

            new Thread(Animate).Start();
            new Thread(RunServer).Start();
        }

        private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {
            ImageWebcam.Source = e.BitmapImage;
        }

        private void UpdateStream(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Trace.WriteLine("Update Stream");
                timer.Stop();
                _mjpeg.StopStream();
                _mjpeg.ParseStream(new Uri(_mjpeg_url));
                timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(15) };
                timer.Tick += UpdateStream;
                timer.Start();
            }));
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
            another_choise = Int32.Parse(vars["item"]);
            if (sended && another_choise != 0)
            {
                if (another_choise == my_choise && !in_menu)
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        Success();
                    }));
                }
                else if (!in_menu)
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        Fail();
                    }));
                }
            }
            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void Animate()
        {
            while (true)
            {
                if (in_success)
                {
                    if (animation_index < 10)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            ImageAnimation.Source = new BitmapImage(new Uri($@"{PathHelpers.ExecutableDirectory()}/animations/item{copy_choise}/{animation_names[copy_choise - 1]}00{animation_index}.png"));
                        }));
                    }
                    else if (animation_index < 100)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            ImageAnimation.Source = new BitmapImage(new Uri($@"{PathHelpers.ExecutableDirectory()}/animations/item{copy_choise}/{animation_names[copy_choise - 1]}0{animation_index}.png"));
                        }));
                    }
                    else
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            ImageAnimation.Source = new BitmapImage(new Uri($@"{PathHelpers.ExecutableDirectory()}/animations/item{copy_choise}/{animation_names[copy_choise - 1]}{animation_index}.png"));
                        }));
                    }
                    animation_index = (animation_index + 1) % 250;
                    Task.WaitAll(new Task[] {Task.Delay(40)});
                }
            }
        }

        private void Success()
        {
            animation_index = 0;
            in_success = true;
            state = 3;
            ImageWait.Visibility = Visibility.Hidden;
            Choose.Visibility = Visibility.Hidden;
            Border.Visibility = Visibility.Hidden;
            Lights.Visibility = Visibility.Hidden;
            SuccessGrid.Visibility = Visibility.Visible;
            copy_choise = my_choise;
            another_choise = 0;
            my_choise = 0;
            sended = false;
        }

        private void Fail()
        {
            state = 4;
            ImageWait.Visibility = Visibility.Hidden;
            Choose.Visibility = Visibility.Hidden;
            FailGrid.Visibility = Visibility.Visible;
            TextFinalItem1.Text = names[my_choise - 1];
            TextFinalItem2.Text = names[another_choise - 1];
            ImageFinalItem1.Source = new BitmapImage(new Uri($@"{PathHelpers.ExecutableDirectory()}/images/Item{my_choise}.png"));
            ImageFinalItem2.Source = new BitmapImage(new Uri($@"{PathHelpers.ExecutableDirectory()}/images/Item{another_choise}.png"));
            another_choise = 0;
            my_choise = 0;
            sended = false;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            state = 1;
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
            my_choise = indexes_rand[Int32.Parse(but.Name.Substring(10)) - 1];
            SendButton.IsEnabled = true;

            Shadow1.Visibility = Visibility.Visible;
            Shadow2.Visibility = Visibility.Visible;
            Shadow3.Visibility = Visibility.Visible;

            ((Image)((Grid)but.Content).Children[4]).Visibility = Visibility.Hidden;
        }

        private void Send_Item_Click(object sender, RoutedEventArgs e)
        {
            sended = true;
            _client.GetAsync($"http://{Config<AppConfig>.Value.Ip}:3001/play/?item={my_choise}");
            ButtonItem1.IsEnabled = false;
            ButtonItem2.IsEnabled = false;
            ButtonItem3.IsEnabled = false;
            SendButton.IsEnabled = false;
            SendButton.Visibility = Visibility.Hidden;
            if (another_choise == 0)
            {
                ImageWait.Visibility = Visibility.Visible;
                state = 2;
            }
            else if (another_choise == my_choise)
            {
                Success();
            }
            else
            {
                Fail();
            }
        }

        private void Home_Grid_Click(object sender, RoutedEventArgs e)
        {
            in_menu = true;
            MenuGrid.Visibility = Visibility.Visible;
            ImageWait.Visibility = Visibility.Hidden;
            SuccessGrid.Visibility = Visibility.Hidden;
            FailGrid.Visibility = Visibility.Hidden;
            Choose.Visibility = Visibility.Hidden;
            Border.Visibility = Visibility.Visible;
            Lights.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Hidden;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            in_success = false;
            in_menu = false;
            state = 0;
            my_choise = 0;

            _client.GetAsync($"http://{Config<AppConfig>.Value.Ip}:3001/play/?item={my_choise}");
            received = false;
            Start.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Hidden;

            ButtonItem1.IsEnabled = true;
            ButtonItem2.IsEnabled = true;
            ButtonItem3.IsEnabled = true;
            SendButton.IsEnabled = false;
            SendButton.Visibility = Visibility.Visible;
            Shadow1.Visibility = Visibility.Hidden;
            Shadow2.Visibility = Visibility.Hidden;
            Shadow3.Visibility = Visibility.Hidden;

            ImageWait.Visibility = Visibility.Hidden;
            SuccessGrid.Visibility = Visibility.Hidden;
            FailGrid.Visibility = Visibility.Hidden;
            Choose.Visibility = Visibility.Hidden;
            MenuGrid.Visibility = Visibility.Hidden;

            Border.Visibility = Visibility.Visible;
            Lights.Visibility = Visibility.Visible;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            in_menu = false;
            HomeButton.Visibility = Visibility.Visible;
            MenuGrid.Visibility = Visibility.Hidden;
            if (state == 1) {
                Choose.Visibility = Visibility.Visible;
            }
            if (state == 2)
            {
                Choose.Visibility = Visibility.Visible;
                if (another_choise == 0)
                {
                    ImageWait.Visibility = Visibility.Visible;
                    state = 2;
                }
                else if (another_choise == my_choise)
                {
                    Success();
                }
                else
                {
                    Fail();
                }
            }
            if (state == 3)
            {
                Border.Visibility = Visibility.Hidden;
                Lights.Visibility = Visibility.Hidden;
                SuccessGrid.Visibility = Visibility.Visible;
            }
            if (state == 4)
            {
                FailGrid.Visibility = Visibility.Visible;
            }
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
