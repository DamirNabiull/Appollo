using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using W2021.Helpers;

namespace W2021.EveryoneShouldKnow
{
    public partial class MainPage : Page, IPageManagerHandler, INotifyPropertyChanged
    {
        private bool _isChecked = false;
        private PageManager _pageManager;

        public ObservableCollection<Hero> Cells { get; set; }
        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        public MainPage()
        {
            InitializeComponent();

            Cells = MainWindow.Heroes;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cells"));

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Hero hero = CellBehavior.GetHero(sender as RadioButton);
            Title.Text = hero.Title;
            Age.Text = hero.Age;
            Quote.Text = hero.Quote;

            Description.Text = hero.Description;
            Photo.Source = new BitmapImage(new Uri(@$"{hero.Photo}", UriKind.Relative));

            MainWindow.VlcForm.PlayVideo(hero.Video);
        }

        private void RadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_isChecked)
            {
                _isChecked = true;
                (sender as RadioButton).IsChecked = true;
            }
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

        private void ScrollViewer_ManipulationBoundaryFeedback(object sender, System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
