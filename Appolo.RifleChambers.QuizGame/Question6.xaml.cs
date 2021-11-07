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
    public partial class Question6 : Page, IPageManagerHandler
    {
        private int _answered = 0, _needed = 4, correct=0;
        private PageManager _pageManager;
        public Question6()
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

        private void Correct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_answered != _needed)
            {
                Button? button = (sender as Button);
                var text_block = ((TextBlock)((Grid)button.Content).Children[1]);
                bool pressed = text_block.Foreground.ToString() == "#FFFFFFFF" ? true : false;
                if (pressed)
                {
                    ((TextBlock)((Grid)button.Content).Children[1]).Foreground = App.Current.Resources["pressedButton"] as SolidColorBrush;
                    ((Image)((Grid)button.Content).Children[0]).Source = new BitmapImage(new Uri(@$"images/GreenButton.png", UriKind.Relative));
                    _answered++;
                    correct++;
                }
                if (correct == _needed)
                {
                    next_page_button.IsEnabled = true;
                    ((TextBlock)next_page_button.Content).Foreground = App.Current.Resources["nextButtonEnable"] as SolidColorBrush;
                }
            }
        }

        private void Next_Page_Click(object sender, RoutedEventArgs e)
        {
            _pageManager.Navigate(typeof(Question7));
        }

        private void Incorrect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button? button = (sender as Button);

            var text_block = ((TextBlock)((Grid)button.Content).Children[1]);
            bool pressed = text_block.Foreground.ToString() == "#FFFFFFFF" ? true : false;

            if (_answered != _needed)
            {
                _answered = pressed ? _answered + 1 : _answered - 1;

                SolidColorBrush? new_color = pressed ? App.Current.Resources["pressedButton"] as SolidColorBrush : App.Current.Resources["unpressedButton"] as SolidColorBrush;
                ((TextBlock)((Grid)button.Content).Children[1]).Foreground = new_color;

                BitmapImage? background = pressed ? new BitmapImage(new Uri(@$"images/RedButton.png", UriKind.Relative)) : new BitmapImage(new Uri(@$"images/DefaultButton.png", UriKind.Relative));
                ((Image)((Grid)button.Content).Children[0]).Source = background;
            }
            else if (_answered == _needed && pressed == false)
            {
                ((Image)((Grid)button.Content).Children[0]).Source = new BitmapImage(new Uri(@$"images/DefaultButton.png", UriKind.Relative));
                ((TextBlock)((Grid)button.Content).Children[1]).Foreground = App.Current.Resources["unpressedButton"] as SolidColorBrush;
                _answered--;
            }
        }
    }
}
