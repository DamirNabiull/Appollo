using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using W2021.Helpers;

namespace W2021.WasNotAwarded
{
    public partial class MainPage : Page, IPageManagerHandler, INotifyPropertyChanged
    {
        public ObservableCollection<Hero> Tabs { get; set; }
        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        private bool _isChecked = false;
        private PageManager _pageManager;

        public MainPage()
        {
            InitializeComponent();

            Tabs = (Application.Current.MainWindow as MainWindow).CurrentHeroes;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tabs"));

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Hero hero = TabBehavior.GetHero(sender as RadioButton);
            Title.Text = hero.Title;

            if (hero.Date != null)
            {
                Title.Text = hero.Date + " - " + Title.Text;
            }

            Description.Text = hero.Description;
            Photo.Source = new BitmapImage(new Uri(@$"Photos/{hero.Photo}", UriKind.Relative));
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
