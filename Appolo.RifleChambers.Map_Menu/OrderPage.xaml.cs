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

namespace Appolo.RifleChambers.Map_Menu
{
    /// <summary>
    /// Логика взаимодействия для OrderPage1.xaml
    /// </summary>
    public partial class OrderPage : Page, IPageManagerHandler
    {
        private PageManager _pageManager;
        private HttpClient _client = new HttpClient();
        private String _order;
        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        public OrderPage()
        {
            InitializeComponent();
        }

        public void PreNavigate(NavigationToArgs args)
        {
            _order = args.Args[0] as String;

            String _image_num = _order[_order.Length - 2].ToString() + _order[_order.Length - 1].ToString();
            ImageBox.Source = new BitmapImage(new Uri(@$"images/orders/{_order}.png", UriKind.Relative));
            Trace.WriteLine(_image_num);
            _client.GetAsync($"{Config<AppConfig>.Value.Projector}?map={_image_num}");
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

        public void Exit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _client.GetAsync($"{Config<AppConfig>.Value.Projector}?map=default");
            _pageManager.Navigate(typeof(MenuPage));
        }
    }
}
