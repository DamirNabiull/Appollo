using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using W2021.Helpers;

namespace W2021.MassHeroism
{
    public partial class MainPage : Page, INotifyPropertyChanged, IPageManagerHandler
    {
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
