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

namespace Appolo.RifleChambers.QuizGame
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
            _pageManager.Navigate(typeof(Start));
        }
    }
}
