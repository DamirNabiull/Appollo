using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Windows.Media.Animation;
using System.IO;

namespace Appolo.RifleChambers.Map_Projector
{
    /// <summary>
    /// Логика взаимодействия для MapPage.xaml
    /// </summary>
    public partial class MapPage : Page, IPageManagerHandler
    {
        private PageManager _pageManager;
        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }
        public MapPage()
        {
            InitializeComponent();
            Photo.ImageSource = new BitmapImage(new Uri(@$"images/default.png", UriKind.Relative));
            new Thread(RunServer).Start();
        }

        private static bool _isInProgress = false;
        private HttpListener _listener;
        private bool _server = true;
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

            ChangeBackground(vars["map"]);

            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        private void ChangeBackground(string photoPath)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                _isInProgress = !_isInProgress;

                if (_isInProgress)
                {

                    New_Photo.Opacity = 0.0;
                    New_Photo.Source = new BitmapImage(new Uri(@$"pack://application:,,,/Appolo.RifleChambers.Map_Projector;component/images/{photoPath}.png"));

                    Trace.WriteLine(@$"images/{photoPath}.png");

                    var animation = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        BeginTime = TimeSpan.FromSeconds(0),
                        Duration = TimeSpan.FromSeconds(2),
                    };

                    animation.Completed += (s, a) => New_Photo.Opacity = 1.0;

                    New_Photo.BeginAnimation(UIElement.OpacityProperty, animation);
                } 
                else
                {
                    var animation = new DoubleAnimation
                    {
                        From = 1,
                        To = 0,
                        BeginTime = TimeSpan.FromSeconds(0),
                        Duration = TimeSpan.FromSeconds(2),
                    };

                    animation.Completed += (s, a) => New_Photo.Opacity = 0.0;
                    New_Photo.BeginAnimation(UIElement.OpacityProperty, animation);
                }
            }));
        }

        public void PreNavigate(NavigationToArgs args)
        {

        }

        public void AfterNavigate(NavigationToArgs args)
        {

        }

        public void AfterNavigateFrom(NavigationFromArgs args)
        {

        }

        public void PreNavigateFrom(NavigationFromArgs args)
        {

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
