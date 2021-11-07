using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using W2021.Helpers;

namespace W2021.MassHeroism
{
    public partial class HeroPage : Page, IPageManagerHandler
    {
        private PageManager _pageManager;

        public HeroPage()
        {
            InitializeComponent(); 
        }

        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        public void AfterNavigate(NavigationToArgs args)
        {

        }

        public void AfterNavigateFrom(NavigationFromArgs args)
        {

        }

        public void PreNavigate(NavigationToArgs args)
        {
            Hero hero = args.Args[0] as Hero;

            Photo.Source = new BitmapImage(new Uri(@$"{hero.Photo}", UriKind.Relative));
            Title.Text = hero.Title;
            Description.Text = hero.Description;
            ScrollViewer.ScrollToTop();
        }

        public void PreNavigateFrom(NavigationFromArgs args)
        {

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _pageManager.Navigate(typeof(MainPage));
        }

        private void ScrollViewer_ManipulationBoundaryFeedback(object sender, System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
