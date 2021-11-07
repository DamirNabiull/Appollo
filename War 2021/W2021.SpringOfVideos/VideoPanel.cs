using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Forms;
using W2021.Helpers;

namespace W2021.SpringOfVideos
{
    class VideoPanel: Form
    {
        internal enum AccentState
        {
            ACCENT_DISABLED,
            ACCENT_ENABLE_GRADIENT,
            ACCENT_ENABLE_TRANSPARENTGRADIENT,
            ACCENT_ENABLE_BLURBEHIND,
            ACCENT_INVALID_STATE,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            // 省略其他未使用的字段
            WCA_ACCENT_POLICY = 19,
            // 省略其他未使用的字段
        }

        private PictureBox _popup;
        private VlcControl _videoControl;
        private int _currentIndex;

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        [DllImport("gdi32")]
        private static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("dwmapi")]
        private static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, ref DwmBlurbehind pBlurBehind);
        public struct DwmBlurbehind
        {
            public int DwFlags;
            public bool FEnable;
            public IntPtr HRgnBlur;
            public bool FTransitionOnMaximized;
        }

        /// ХЕЛБ МИ

        private List<Size> _popupSizes = new List<Size>()
        {
            new Size(1558, 1080),
            new Size(1306, 1080),
            new Size(1558, 1080),
        };
        private List<Size> _videoSizes = new List<Size>()
        {
            new Size(1365, 766),
            new Size(1013, 766),
            new Size(1365, 766),
        };
        private List<int> _videoMargins = new List<int>()
        {
            218,
            207,
            218,
        };
        private List<Image> _popupBackgrounds;

        public VideoPanel()
        {
            var vlcLibPath = Path.Combine(PathHelpers.ExecutableDirectory(), "libvlc", "win-x64");
            var vlcLib = new DirectoryInfo(vlcLibPath);

            // BackColor = Color.Transparent;
            // .FromArgb(144, 16, 16, 16);

            _popup = new PictureBox();
            _popup.BackColor = Color.Transparent;
            _popup.SizeMode = PictureBoxSizeMode.StretchImage;
            _popupBackgrounds = new List<Image>() {
                (Image)new Bitmap(Path.Join(PathHelpers.ExecutableDirectory(), "Images/PanelLeftRight.png")),
                (Image)new Bitmap(Path.Join(PathHelpers.ExecutableDirectory(), "Images/PanelCenter.png")),
                (Image)new Bitmap(Path.Join(PathHelpers.ExecutableDirectory(), "Images/PanelLeftRight.png"))
            };

            _videoControl = new VlcControl();
            _videoControl.BeginInit();
            _videoControl.VlcLibDirectory = vlcLib;
            _videoControl.BackColor = Color.DarkGray;
            _videoControl.EndInit();

            Controls.Add(_popup);
            Controls.Add(_videoControl);

            _videoControl.BringToFront();

            _videoControl.VlcMediaPlayer.EndReached += VlcMediaPlayer_EndReached;
            _videoControl.VlcMediaPlayer.Playing += VlcMediaPlayer_Playing;
        }

        private void VlcMediaPlayer_Playing(object? sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
        {
            Size videoSize = _videoSizes[_currentIndex];
            _videoControl.Video.CropGeometry = $"{videoSize.Width}:{videoSize.Height}";
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            WindowUtils.EnableAcrylic(this, Color.FromArgb(114, 16, 16, 16));
        }

        public void PlayVideo(int videoPos, string videoPath)
        {
            Point bgPos = Config<AppConfig>.Value.PlayerLocations[videoPos];
            Size bgSize = Config<AppConfig>.Value.PlayerSizes[videoPos];

            Size popupSize = _popupSizes[videoPos];
            Size videoSize = _videoSizes[videoPos];

            Point popupPos = new Point();
            popupPos.Y = bgPos.Y + bgSize.Height - popupSize.Height;
            popupPos.X = bgPos.X + (bgSize.Width - popupSize.Width) / 2;

            _popup.Image = _popupBackgrounds[videoPos];
            _popup.ClientSize = popupSize;
            _popup.Location = popupPos;

            Point vlcPos = new Point();
            vlcPos.Y = bgPos.Y + bgSize.Height - popupSize.Height + _videoMargins[videoPos];
            vlcPos.X = bgPos.X + (bgSize.Width - videoSize.Width) / 2;

            _videoControl.Stop();

            _videoControl.Video.CropGeometry = $"{videoSize.Width}:{videoSize.Height}";
            _videoControl.Size = videoSize;
            _videoControl.Video.CropGeometry = $"{videoSize.Width}:{videoSize.Height}";
            _videoControl.Location = vlcPos;
            _videoControl.Video.CropGeometry = $"{videoSize.Width}:{videoSize.Height}";

            _videoControl.Play(new FileInfo(Path.Combine(PathHelpers.ExecutableDirectory(), videoPath)));

            _videoControl.Video.CropGeometry = $"{videoSize.Width}:{videoSize.Height}";

            Task.Run(async () =>
            {
                await Task.Delay(100);
                _videoControl.Video.CropGeometry = $"{videoSize.Width}:{videoSize.Height}";
            });

            _currentIndex = videoPos;
        }

        private void VlcMediaPlayer_EndReached(object? sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            this.Invoke(() =>
            {
                Hide();
            });
        }

        public void CloseVideo()
        {
            _videoControl.Stop();
        }
    }
}
