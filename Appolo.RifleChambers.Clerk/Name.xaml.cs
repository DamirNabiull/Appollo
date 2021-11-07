using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
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

namespace Appolo.RifleChambers.Clerk
{
    /// <summary>
    /// Логика взаимодействия для Name.xaml
    /// </summary>
    public partial class Name : Page, IPageManagerHandler
    {
        private PageManager _pageManager;
        private String _name;
        private HttpClient _client = new HttpClient();

        public Name()
        {
            InitializeComponent();
            InputLanguageManager.SetInputLanguage(Field, CultureInfo.CreateSpecificCulture("ru-RU"));
        }

        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

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

        private void Start_Click(object sStarter, RoutedEventArgs e)
        {
            string[] subs = Field.Text.ToString().Split(' ');
            _name = subs[0];
            Trace.WriteLine(_name);
            _pageManager.Navigate(typeof(Wait), new object[1] {_name});
        }

        private void Exit_Button(object sStarter, RoutedEventArgs e)
        {
            _client.GetAsync($"{Config<AppConfig>.Value.SecondSensor}?available=true");
            _client.GetAsync($"{Config<AppConfig>.Value.Player}?state=0");
            _pageManager.Navigate(typeof(Start));
        }
    }
}
