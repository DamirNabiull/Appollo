using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Appolo.RifleChambers.Clerk
{
    /// <summary>
    /// Логика взаимодействия для Name.xaml
    /// </summary>
    public partial class Name : Page, IPageManagerHandler
    {
        private PageManager _pageManager;
        private String _name;
        private Timer _timer, new_timer, _long;
        private bool _time = false;
        private HttpClient _client = new HttpClient();

        public Name()
        {
            InitializeComponent();
            InputLanguageManager.SetInputLanguage(Field, CultureInfo.CreateSpecificCulture("ru-RU"));
        }

        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        public void PreNavigate(NavigationToArgs args)
        {
            Field.Text = "";
            _name = "";

            Field.Focus();

            _long = new Timer(60000);
            _long.Elapsed += Exit_Function;
            _long.Start();
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Start_Click(object sStarter, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                _long.Stop();
                string[] subs = Field.Text.ToString().Split(' ');
                _name = subs[0];
                if (_name.Length > 0)
                {
                    Trace.WriteLine(_name);
                    _pageManager.Navigate(typeof(Wait), new object[1] { _name });
                }
            }));
        }

        private void Exit_Button(object sStarter, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                _long.Stop();
                _client.GetAsync($"{Config<AppConfig>.Value.SecondSensor}?available=true");
                _client.GetAsync($"{Config<AppConfig>.Value.Player}?state=0");
                _pageManager.Navigate(typeof(Start));
            }));
        }

        private void Exit_Function(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                _long.Stop();
                _client.GetAsync($"{Config<AppConfig>.Value.SecondSensor}?available=true");
                _client.GetAsync($"{Config<AppConfig>.Value.Player}?state=0");
                _pageManager.Navigate(typeof(Start));
            }));
        }

        private void Keyboard_Clcik(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _name += ((TextBlock)button.Content).Text.ToString();
            Field.Text = _name;
        }

        private void Keyboard_Backspace_Clcik(object sender, RoutedEventArgs e)
        {
            if (_name.Length > 0)
            {
                _name = _name.Remove(_name.Length - 1);
                Field.Text = _name;
            }
        }
    }
}
