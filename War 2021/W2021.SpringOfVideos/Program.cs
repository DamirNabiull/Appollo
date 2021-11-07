using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using W2021.Helpers;

namespace W2021.SpringOfVideos
{
    public class AppConfig
    {
        public List<Point> PlayerLocations { get; set; }
        public List<Size> PlayerSizes { get; set; }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
