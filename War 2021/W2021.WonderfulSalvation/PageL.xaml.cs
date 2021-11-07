using System;
using System.Collections.Generic;
using System.Linq;
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

namespace W2021.WonderfulSalvation
{
    public partial class PageL : Page
    {
        public PageL()
        {
            InitializeComponent();

            _interactionTimer = new Timer();
            _interactionTimer.Interval = 120000;
            _interactionTimer.Elapsed += _interactionTimer_Elapsed;

            _placeholderImage = new BitmapImage(new Uri(@"Photos_R/0-0.png", UriKind.Relative));
            DescriptionPhoto.Source = _placeholderImage;
        }

        private RadioButton _lastChecked;
        private Timer _interactionTimer;
        private BitmapImage _placeholderImage;

        private void _interactionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_lastChecked == null)
            {
                return;
            }

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                _lastChecked.IsChecked = false;
                DescriptionPhoto.Source = _placeholderImage;
            }));
        }

        private void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _lastChecked = (RadioButton)sender;
            string image3 = CellBehavior.GetImage3(_lastChecked);
            DescriptionPhoto.Source = new BitmapImage(new Uri(@$"{image3}", UriKind.Relative));

            _interactionTimer.Stop();
            _interactionTimer.Start();
        }
    }
}
