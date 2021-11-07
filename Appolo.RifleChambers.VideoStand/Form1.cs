using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Forms;

namespace Appolo.RifleChambers.VideoStand
{
    public partial class Form1 : Form
    {
        public VlcControl vlcController;
        public string strWorkPath;
        public Form1()
        {
            InitializeComponent();

            strWorkPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var vlcLibPath = Path.Combine(strWorkPath, "libvlc", "win-x64");
            var vlcLib = new DirectoryInfo(vlcLibPath);

            vlcController = new VlcControl();
            vlcController.BeginInit();
            vlcController.VlcLibDirectory = vlcLib;
            vlcController.EndInit();

            vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", "0.mp4")));

            vlcController.EndReached += VlcController_EndReached;

            Controls.Add(vlcController);
        }

        private void VlcController_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            Task.Run(() =>
            {
                vlcController.Play(new FileInfo(Path.Combine(strWorkPath, "Videos", "0.mp4")));
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (vlcController == null)
            {
                return;
            }
            vlcController.Size = Size;
            vlcController.Video.CropGeometry = $"{Width}:{Height}";
        }
    }
}
