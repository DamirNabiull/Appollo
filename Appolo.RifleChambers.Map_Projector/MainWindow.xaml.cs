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
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Media.Animation;
using System.Net;
using System.ComponentModel;

namespace Appolo.RifleChambers.Map_Projector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PageManager _pageManager;
        private static bool _isInProgress = false;
        private HttpListener _listener;
        private bool _server = true;
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

            //Photo.ImageSource = new BitmapImage(new Uri(@$"images/default.png", UriKind.Relative));
            new Thread(RunServer).Start();
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
                        Duration = TimeSpan.FromSeconds(1),
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
                        Duration = TimeSpan.FromSeconds(1),
                    };
                     
                    animation.Completed += (s, a) => New_Photo.Opacity = 0.0;
                    New_Photo.BeginAnimation(UIElement.OpacityProperty, animation);
                }
            }));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            //_pageManager.Navigate(typeof(MapPage));
        }
    }
}
