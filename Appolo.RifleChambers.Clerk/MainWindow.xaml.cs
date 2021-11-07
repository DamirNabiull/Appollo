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
using WPFTabTipMixedHardware;

namespace Appolo.RifleChambers.Clerk
{
    public partial class MainWindow : Window
    {
        public static bool _available = true;
        public static HttpClient _client = new HttpClient();
        private HttpListener _listener;
        private bool _server = true;

        public PageManager _pageManager;
        public MainWindow()
        {
            InitializeComponent();
            _pageManager = new PageManager(NavFrame);
            TabTipAutomation.IgnoreHardwareKeyboard = WPFTabTipMixedHardware.Models.HardwareKeyboardIgnoreOptions.IgnoreAll;
            TabTipAutomation.AutomationTriggers = TabTipAutomationTrigger.OnFocus;
            TabTipAutomation.BindTo<TextBox>();

            new Thread(RunServer).Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _client.GetAsync($"{Config<AppConfig>.Value.Player}?state=0");
            _pageManager.Navigate(typeof(Start));
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
                //NetAclChecker.AddAddress("http://*:3001/play/");
                //NetAclChecker.AddAddress("http://+:3001/play/");
                _listener.Prefixes.Add($"http://*:{Config<AppConfig>.Value.Port}/play/");
                _listener.Prefixes.Add($"http://+:{Config<AppConfig>.Value.Port}/play/");

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

            _available = vars["available"] == "false" ? false : true;

            Trace.WriteLine(_available);

            context.Response.StatusCode = 200;
            context.Response.Close();
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
        public string SecondSensor { get; set; }
        public string Player { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
