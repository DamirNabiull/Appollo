using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using W2021.Helpers;

namespace W2021.SpringOfVictory
{

    public partial class MainPage : Page, INotifyPropertyChanged, IPageManagerHandler
    {
        private PageManager _pageManager;


        public ObservableCollection<Hero> HeroesLeft { get; set; }
        public ObservableCollection<Hero> HeroesFront { get; set; }
        public ObservableCollection<Hero> HeroesRight { get; set; }
        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        public MainPage()
        {
            InitializeComponent();

            HeroesLeft = MainWindow.HeroesLeft;
            HeroesFront = MainWindow.HeroesFront;
            HeroesRight = MainWindow.HeroesRight;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HeroesLeft"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HeroesFront"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HeroesRight"));

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hero hero = CellBehavior.GetHero(sender as Button);

            _pageManager.Navigate(typeof(HeroPage), new object[1] { hero });
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
    }
}
