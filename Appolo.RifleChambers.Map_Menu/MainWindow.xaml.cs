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

namespace Appolo.RifleChambers.Map_Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PageManager _pageManager;
        public MainWindow()
        {
            InitializeComponent();
            _pageManager = new PageManager(NavFrame);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _pageManager.Navigate(typeof(MenuPage));
        }

        public static Dictionary<string, Order> _orders = new Dictionary<string, Order>()
        {
            {"left_0",  new Order(){
                    Photo = "images/orders/04.png",
                    Description = "Description",
                    Map = "01",
                }
            },
        };
    }
}
