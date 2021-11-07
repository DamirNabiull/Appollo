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

namespace Appolo.RifleChambers.Map_Projector
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
                // If window isn't loaded then maxmizing will result in the window displaying on the primary monitor
                if (this.IsLoaded)
                    this.WindowState = WindowState.Maximized;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            _pageManager.Navigate(typeof(MapPage));
        }
    }

    static public class WindowExt
    {

        // NB : Best to call this function from the windows Loaded event or after showing the window
        // (otherwise window is just positioned to fill the secondary monitor rather than being maximised).
        public static void MaximizeToSecondaryMonitor(this Window window)
        {
            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();

            if (secondaryScreen != null)
            {
                if (!window.IsLoaded)
                    window.WindowStartupLocation = WindowStartupLocation.Manual;

                var workingArea = secondaryScreen.WorkingArea;
                window.Left = workingArea.Left;
                window.Top = workingArea.Top;
                window.Width = workingArea.Width;
                window.Height = workingArea.Height;
                // If window isn't loaded then maxmizing will result in the window displaying on the primary monitor
                if (window.IsLoaded)
                    window.WindowState = WindowState.Maximized;
            }
        }
    }
}
