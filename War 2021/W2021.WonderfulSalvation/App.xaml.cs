using System.Linq;
using System.Windows;
using System.Windows.Forms;
using W2021.Helpers;

namespace W2021.WonderfulSalvation
{
    public partial class AppConfig
    {
        public int StandIndex { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var firstWindow = new MainWindow(Config<AppConfig>.Value.StandIndex);

            var screen = Screen.AllScreens.First(s => s.Primary);

            firstWindow.Left = screen.Bounds.Left;
            firstWindow.Top = screen.Bounds.Top;
            firstWindow.Width = screen.Bounds.Width;
            firstWindow.Height = screen.Bounds.Height;

            firstWindow.Show();
        }
    }
}
