using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace Appollo.RifleChambers.Baraban_Menu
{
    public partial class MainWindow : Window
    {
        private string _email, host = "http://localhost:3001";
        private bool _shift = false;

        private bool _server = true;
        public static HttpClient _client = new HttpClient();
        private HttpListener _listener;
        private string _ended = "0";
        public MainWindow()
        {
            InitializeComponent();

            new Thread(RunServer).Start();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _server = false;
            Environment.Exit(0);
        }

        private void Start_1(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"http://localhost:3001/play/?state=1&email=123");
            Choose_Label.Visibility = Visibility.Hidden;
            Window_1.Visibility = Visibility.Visible;
        }

        private void Start_2(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"http://localhost:3001/play/?state=2&email=123");
            Choose_Label.Visibility = Visibility.Hidden;
            Window_2.Visibility = Visibility.Visible;
        }

        private void Start_3(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"http://localhost:3001/play/?state=3&email=123");
            Choose_Label.Visibility = Visibility.Hidden;
            Window_3.Visibility = Visibility.Visible;
        }

        private void Start_4(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"http://localhost:3001/play/?state=4&email=123");
            Choose_Label.Visibility = Visibility.Hidden;
            Window_4.Visibility = Visibility.Visible;
        }

        private void Start_5(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"http://localhost:3001/play/?state=5&email=123");
            Choose_Label.Visibility = Visibility.Hidden;
            Window_5.Visibility = Visibility.Visible;
        }

        private void Start_6(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"http://localhost:3001/play/?state=6&email=123");
            Choose_Label.Visibility = Visibility.Hidden;
            Window_6.Visibility = Visibility.Visible;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"{host}/play/?state=777&email={_email}");
            Email.Visibility = Visibility.Hidden;
            End.Visibility = Visibility.Visible;
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"http://localhost:3001/play/?state=0&email=123");
            Window_1.Visibility = Visibility.Hidden;
            Window_2.Visibility = Visibility.Hidden;
            Window_3.Visibility = Visibility.Hidden;
            Window_4.Visibility = Visibility.Hidden;
            Window_5.Visibility = Visibility.Hidden;
            Window_6.Visibility = Visibility.Hidden;
            Email.Visibility = Visibility.Hidden;
            End.Visibility = Visibility.Hidden;
            Choose_Label.Visibility = Visibility.Hidden;
            Start_Label.Visibility = Visibility.Visible;
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            Email.Visibility = Visibility.Hidden;
            End.Visibility = Visibility.Hidden;
            Choose_Label.Visibility = Visibility.Hidden;
            Start_Label.Visibility = Visibility.Visible;
        }

        private void RunServer()
        {
            try
            {
                _listener = new HttpListener();

                NetAclChecker.AddAddress("http://*:3003/play/");
                NetAclChecker.AddAddress("http://+:3003/play/");

                _listener.Prefixes.Add("http://*:3003/play/");
                _listener.Prefixes.Add("http://+:3003/play/");

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

            _ended = vars["end"];

            if (_ended == "1")
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Start_Label.Visibility = Visibility.Hidden;
                    Choose_Label.Visibility = Visibility.Hidden;
                    Window_1.Visibility = Visibility.Hidden;
                    Window_2.Visibility = Visibility.Hidden;
                    Window_3.Visibility = Visibility.Hidden;
                    Email.Visibility = Visibility.Visible;
                    _ended = "0";
                }));
            }

            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void Keyboard_Shift_Clcik(object sender, RoutedEventArgs e)
        {
            _shift = true;
        }

        private void Keyboard_Clcik(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            String text = ((TextBlock)button.Content).Text.ToString();
            if (_shift)
            {
                _email += text.ToUpper();
                Email_Field.Text = _email;
                _shift = false;
            }
            else
            {
                _email += text;
                Email_Field.Text = _email;
            }

        }

        private void Start_Button(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"http://localhost:3001/play/?state=123&email=123");
            Start_Label.Visibility = Visibility.Hidden;
            Choose_Label.Visibility = Visibility.Visible;
        }

        private void Keyboard_Backspace_Clcik(object sender, RoutedEventArgs e)
        {
            if (_email.Length > 0)
            {
                _email = _email.Remove(_email.Length - 1);
                Email_Field.Text = _email;
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
