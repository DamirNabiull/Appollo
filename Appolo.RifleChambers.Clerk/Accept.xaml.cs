using Aspose.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Appolo.RifleChambers.Clerk
{
    public partial class Accept : Page, IPageManagerHandler
    {
        private PageManager _pageManager;
        private HttpClient _client = new HttpClient();
        private String _name;
        public Accept()
        {
            InitializeComponent();
        }

        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        public void PreNavigate(NavigationToArgs args)
        {
            _name = args.Args[0] as String;
            NameField.Text = _name;
            NameField.FontSize = _name.Length > 7 ? 55 : 65;
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

        private void Retart_Click(object sender, RoutedEventArgs e)
        {
            _pageManager.Navigate(typeof(Name));
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            DrawingVisual dr = new DrawingVisual();
            DrawingContext dc = dr.RenderOpen();

            double _font_size = _name.Length > 7 ? 55 : 65;

            FormattedText text = new FormattedText(_name,
                    new CultureInfo("ru-RU"),
                    FlowDirection.LeftToRight,
                    new Typeface(App.Current.Resources["Kremlin"] as FontFamily, FontStyles.Normal, FontWeights.Bold, new FontStretch()),
                    _font_size,
                    App.Current.Resources["imageTextColor"] as SolidColorBrush);

            text.MaxTextWidth = 351;
            text.MaxTextHeight = 79;
            text.TextAlignment = TextAlignment.Center;

            var background = new BitmapImage(new Uri(@$"images/pergament2.png", UriKind.Relative));

            Rect rect = new Rect();
            rect.X = 0;
            rect.Y = 0;
            rect.Width = 602;
            rect.Height = 864;

            dc.DrawImage(background, rect);
            dc.DrawText(text, new Point(140.5, 417));

            dc.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap(602, 864, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(dr);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(bmp));

            using (Stream stm = File.Create("image.png"))
            {
                png.Save(stm);
            }

            _client.GetAsync($"{Config<AppConfig>.Value.Player}?state=2");

            _pageManager.Navigate(typeof(Email));
        }

        private void Exit_Button(object sStarter, RoutedEventArgs e)
        {
            _client.GetAsync($"{Config<AppConfig>.Value.SecondSensor}?available=true");
            _client.GetAsync($"{Config<AppConfig>.Value.Player}?state=0");
            _pageManager.Navigate(typeof(Start));
        }
    }
}
