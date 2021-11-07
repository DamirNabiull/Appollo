using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Vlc.DotNet.Forms;
using W2021.Helpers;

namespace W2021.EveryoneShouldKnow
{
    public class VlcForm: Form
    {
        private VlcControl _vlcControl;

        public VlcForm()
        {
            var vlcLibPath = Path.Combine(PathHelpers.ExecutableDirectory(), "libvlc", "win-x64");
            var vlcLib = new DirectoryInfo(vlcLibPath);

            _vlcControl = new VlcControl();

            _vlcControl.BeginInit();
            _vlcControl.VlcLibDirectory = vlcLib;
            _vlcControl.EndInit();

            Controls.Add(_vlcControl);

            BackColor = Color.Black;

            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            Bounds = Screen.AllScreens.First(screen => !screen.Primary).Bounds;
        }

        public void PlayVideo(string name)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var path = new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), name));
                _vlcControl.Play(path, new string[] { "input-repeat=65535" });
            });
        }

        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);

            _vlcControl.Size = Size;
            _vlcControl.Video.CropGeometry = $"{Width}:{Height}";
        }
    }
}
