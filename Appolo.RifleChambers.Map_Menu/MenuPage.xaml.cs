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
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Appolo.RifleChambers.Map_Menu
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page, IPageManagerHandler
    {
        private PageManager _pageManager;
        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }
        public MenuPage()
        {
            InitializeComponent();
        }

        public void PreNavigate(NavigationToArgs args)
        {

        }

        public void AfterNavigate(NavigationToArgs args)
        {

        }

        public void PreNavigateFrom(NavigationFromArgs args)
        {

        }

        public void AfterNavigateFrom(NavigationFromArgs args)
        {

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var _button = sender as Button;
            String _name = _button.Name.ToString();
            _pageManager.Navigate(typeof(OrderPage), new object[1] { _name });
        }
    }
}
