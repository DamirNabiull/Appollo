using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Question12 : Page, IPageManagerHandler
    {
        private int _needed = 1, _correct = 0;
        private PageManager _pageManager;
        public Question12()
        {
            InitializeComponent();
        }

        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        public void PreNavigate(NavigationToArgs args)
        {
            _needed = 1;
            _correct = 0;
            Set_Default();
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

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            _pageManager.Navigate(typeof(Start));
        }

        private void Correct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_correct != _needed)
            {
                Set_Default();
                Button? button = (sender as Button);
                ((TextBlock)((Grid)button.Content).Children[1]).Foreground = App.Current.Resources["pressedButton"] as SolidColorBrush;
                ((Image)((Grid)button.Content).Children[0]).Source = new BitmapImage(new Uri(@$"images/GreenButton.png", UriKind.Relative));
                _correct++;
                if (_correct == _needed)
                {
                    Definition.Visibility = Visibility.Visible;
                    next_page_button.IsEnabled = true;
                    ((TextBlock)next_page_button.Content).Foreground = App.Current.Resources["nextButtonEnable"] as SolidColorBrush;
                }
            }
        }

        private void Next_Page_Click(object sender, RoutedEventArgs e)
        {

            _pageManager.Navigate(typeof(End));
        }

        private void Incorrect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button? button = (sender as Button);

            var text_block = ((TextBlock)((Grid)button.Content).Children[1]);
            bool pressed = text_block.Foreground.ToString() == "#FFFFFFFF" ? true : false;

            if (_correct != _needed)
            {
                Set_Default();
                next_page_button.IsEnabled = false;
                ((TextBlock)next_page_button.Content).Foreground = App.Current.Resources["nextButtonDisable"] as SolidColorBrush;

                SolidColorBrush? new_color = pressed ? App.Current.Resources["pressedButton"] as SolidColorBrush : App.Current.Resources["unpressedButton"] as SolidColorBrush;
                ((TextBlock)((Grid)button.Content).Children[1]).Foreground = new_color;

                BitmapImage? background = pressed ? new BitmapImage(new Uri(@$"images/RedButton.png", UriKind.Relative)) : new BitmapImage(new Uri(@$"images/DefaultButton.png", UriKind.Relative));
                ((Image)((Grid)button.Content).Children[0]).Source = background;
                Definition.Visibility = Visibility.Visible;
            }
        }

        private void Set_Default()
        {
            ((TextBlock)((Grid)button_1.Content).Children[1]).Foreground = App.Current.Resources["unpressedButton"] as SolidColorBrush;
            ((TextBlock)((Grid)button_2.Content).Children[1]).Foreground = App.Current.Resources["unpressedButton"] as SolidColorBrush;
            ((TextBlock)((Grid)button_3.Content).Children[1]).Foreground = App.Current.Resources["unpressedButton"] as SolidColorBrush;

            ((Image)((Grid)button_1.Content).Children[0]).Source = new BitmapImage(new Uri(@$"images/DefaultButton.png", UriKind.Relative));
            ((Image)((Grid)button_2.Content).Children[0]).Source = new BitmapImage(new Uri(@$"images/DefaultButton.png", UriKind.Relative));
            ((Image)((Grid)button_3.Content).Children[0]).Source = new BitmapImage(new Uri(@$"images/DefaultButton.png", UriKind.Relative));

            ((TextBlock)next_page_button.Content).Foreground = App.Current.Resources["nextButtonDisable"] as SolidColorBrush;
            next_page_button.IsEnabled = false;
            Definition.Visibility = Visibility.Hidden;
        }
    }
}
