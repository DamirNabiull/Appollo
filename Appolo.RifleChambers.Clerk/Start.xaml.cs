using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
    public partial class Start : Page, IPageManagerHandler
    {
        private PageManager _pageManager;
        public Start()
        {
            InitializeComponent();
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
            if (MainWindow._available)
            {
                HttpClient _client = new HttpClient();
                _client.GetAsync($"{Config<AppConfig>.Value.SecondSensor}?available=false");
                _client.GetAsync($"{Config<AppConfig>.Value.Player}?state=1");
                _pageManager.Navigate(typeof(Name));
            }
        }
    }
}
