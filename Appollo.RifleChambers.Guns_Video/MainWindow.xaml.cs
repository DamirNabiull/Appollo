using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MjpegProcessor;

namespace Appollo.RifleChambers.Guns_Video
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient _client = new HttpClient();
        private HttpListener _listener;
        private bool _server = true;

        private double _time = 20;
        private int photo_ind = 0;
        private bool make_photo = false;

        private string _state = "0";
        private string video;
        private string strWorkPath;

        private string _email;

        private string _mjpeg_url = "http://127.0.0.1:7777/camera/mjpeg";
        private string _jpeg_url = "http://127.0.0.1:7777/camera/jpeg";
        readonly MjpegDecoder _mjpeg;
        public DispatcherTimer timer, photo_timer;
        public MainWindow()
        {
            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
            if (secondaryScreen != null)
            {
                Trace.WriteLine("True");
                if (!this.IsLoaded)
                    this.WindowStartupLocation = WindowStartupLocation.Manual;

                var workingArea = secondaryScreen.WorkingArea;
                this.Left = workingArea.Left;
                this.Top = workingArea.Top;
                this.Width = workingArea.Width;
                this.Height = workingArea.Height;
            }

            InitializeComponent();

            cam.Visibility = Visibility.Hidden;

            Trace.WriteLine(_mjpeg_url);
            _mjpeg = new MjpegDecoder();
            _mjpeg.FrameReady += mjpeg_FrameReady;
            _mjpeg.ParseStream(new Uri(_mjpeg_url));
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(15) };
            timer.Tick += UpdateStream;
            timer.Start();

            photo_timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(_time) };
            photo_timer.Tick += MakePhoto;

            strWorkPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            video = System.IO.Path.Combine(strWorkPath, "Videos", "0.mp4");
            player.Source = new Uri(video);
            player.Play();

            new Thread(RunServer).Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {
            if (_state != "0")
            {
                cam.Source = e.BitmapImage;

                if (make_photo)
                {
                    make_photo = false;

                    if (photo_ind < 10)
                    {
                        PngBitmapEncoder png = new PngBitmapEncoder();

                        TransformedBitmap TempImage = new TransformedBitmap();

                        TempImage.BeginInit();
                        TempImage.Source = e.BitmapImage; // MyImageSource of type BitmapImage

                        RotateTransform transform = new RotateTransform(270);
                        TempImage.Transform = transform;
                        TempImage.EndInit();
                        png.Frames.Add(BitmapFrame.Create(TempImage));

                        using (Stream stm = File.Create($"{photo_ind}.png"))
                        {
                            png.Save(stm);
                        }

                        photo_ind += 1;
                    }
                }
            }
        }

        private void MakePhoto(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                photo_timer.Stop();

                make_photo = true;

                photo_timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(_time) };
                photo_timer.Tick += MakePhoto;
                photo_timer.Start();
            }));
        }

        private void UpdateStream(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                timer.Stop();
                _mjpeg.StopStream();
                _mjpeg.ParseStream(new Uri(_mjpeg_url));
                timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(15) };
                timer.Tick += UpdateStream;
                timer.Start();
            }));
        }

        private void SendMessage()
        {
            Trace.WriteLine(_email);
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "mail.nic.ru";
                client.Port = 587; // Обратите внимание что порт 587
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("strelets@museum-vf.ru", "1928Gfkfns"); // Ваши логин и пароль

                LinkedResource img1 = new LinkedResource($"{strWorkPath}/0.png");
                img1.ContentId = Guid.NewGuid().ToString();
                LinkedResource img2 = new LinkedResource($"{strWorkPath}/1.png");
                img2.ContentId = Guid.NewGuid().ToString();
                LinkedResource img3 = new LinkedResource($"{strWorkPath}/2.png");
                img3.ContentId = Guid.NewGuid().ToString();
                LinkedResource img4 = new LinkedResource($"{strWorkPath}/3.png");
                img4.ContentId = Guid.NewGuid().ToString();
                LinkedResource img5 = new LinkedResource($"{strWorkPath}/4.png");
                img5.ContentId = Guid.NewGuid().ToString();
                LinkedResource img6 = new LinkedResource($"{strWorkPath}/5.png");
                img6.ContentId = Guid.NewGuid().ToString();
                LinkedResource img7 = new LinkedResource($"{strWorkPath}/6.png");
                img7.ContentId = Guid.NewGuid().ToString();
                LinkedResource img8 = new LinkedResource($"{strWorkPath}/7.png");
                img8.ContentId = Guid.NewGuid().ToString();
                LinkedResource img9 = new LinkedResource($"{strWorkPath}/8.png");
                img9.ContentId = Guid.NewGuid().ToString();
                LinkedResource img10 = new LinkedResource($"{strWorkPath}/9.png");
                img10.ContentId = Guid.NewGuid().ToString();


                string htmlBody = @"<img src='cid:" + img1.ContentId + @"'/><img src='cid:" + img2.ContentId + @"'/><img src='cid:" + img3.ContentId + @"'/><img src='cid:" + img4.ContentId + @"'/><img src='cid:" + img5.ContentId + @"'/><img src='cid:" + img6.ContentId + @"'/><img src='cid:" + img7.ContentId + @"'/><img src='cid:" + img8.ContentId + @"'/><img src='cid:" + img9.ContentId + @"'/><img src='cid:" + img10.ContentId + @"'/>";
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(img1);
                alternateView.LinkedResources.Add(img2);
                alternateView.LinkedResources.Add(img3);
                alternateView.LinkedResources.Add(img4);
                alternateView.LinkedResources.Add(img5);
                alternateView.LinkedResources.Add(img6);
                alternateView.LinkedResources.Add(img7);
                alternateView.LinkedResources.Add(img8);
                alternateView.LinkedResources.Add(img9);
                alternateView.LinkedResources.Add(img10);

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.AlternateViews.Add(alternateView);
                mail.From = new MailAddress("strelets@museum-vf.ru");
                mail.To.Add(_email);
                mail.Subject = "Фото";

                client.Send(mail);
            }
            catch (Exception e)
            {

            }
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

            Trace.WriteLine(vars["state"]);
            _state = vars["state"];


            _email = vars["email"];
            Trace.WriteLine(vars["email"]);


            this.Dispatcher.Invoke(new Action(() =>
            {
                photo_ind = 0;
                photo_timer.Stop();
                if (_state == "777")
                {
                    _state = "0";
                    new Thread(SendMessage).Start();
                }
                else if (_state == "1")
                {
                    player.Stop();
                    photo_timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(_time) };
                    photo_timer.Tick += MakePhoto;
                    photo_timer.Start();

                    cam.Visibility = Visibility.Visible;
                    video = System.IO.Path.Combine(strWorkPath, "Videos", "1.mp4");
                    player.Source = new Uri(video);
                    player.Play();
                }
                else if (_state == "2")
                {
                    player.Stop();
                    photo_timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(_time) };
                    photo_timer.Tick += MakePhoto;
                    photo_timer.Start();

                    cam.Visibility = Visibility.Visible;
                    video = System.IO.Path.Combine(strWorkPath, "Videos", "2.mp4");
                    player.Source = new Uri(video);
                    player.Play();
                }
                else if (_state == "3")
                {
                    player.Stop();
                    photo_timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(_time) };
                    photo_timer.Tick += MakePhoto;
                    photo_timer.Start();

                    cam.Visibility = Visibility.Visible;
                    video = System.IO.Path.Combine(strWorkPath, "Videos", "3.mp4");
                    player.Source = new Uri(video);
                    player.Play();
                }
                else if (_state == "123")
                {
                    player.Stop();
                    video = System.IO.Path.Combine(strWorkPath, "Videos", "vybor.mp4");
                    player.Source = new Uri(video);
                    player.Play();
                }
                else
                {
                    player.Stop();
                    cam.Visibility = Visibility.Hidden;
                    video = System.IO.Path.Combine(strWorkPath, "Videos", "0.mp4");
                    player.Source = new Uri(video);
                    player.Play();
                }
            }));

            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _server = false;
            Environment.Exit(0);
        }

        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            photo_timer.Stop();

            if (_state != "0" && _state != "123")
            {
                _client.GetAsync($"http://localhost:3003/play/?end=1");
            }

            _state = "0";
            cam.Visibility = Visibility.Hidden;
            player.Stop();
            video = System.IO.Path.Combine(strWorkPath, "Videos", "0.mp4");
            player.Source = new Uri(video);
            player.Play();
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
