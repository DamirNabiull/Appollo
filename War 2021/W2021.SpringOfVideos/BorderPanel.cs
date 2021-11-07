using System.Drawing;
using System.Windows.Forms;
using W2021.Helpers;

namespace W2021.SpringOfVideos
{
    public class BorderPanel : Panel
    {
        public int EditIndex;
        public bool EditMode = false;

        private void DrawBorder(Graphics graphics, Rectangle rect, Color color)
        {
            ControlPaint.DrawBorder(graphics, rect, color, 10, ButtonBorderStyle.Solid, color, 10, ButtonBorderStyle.Solid, color, 10, ButtonBorderStyle.Solid, color, 10, ButtonBorderStyle.Solid); ;
        }

        public void Update(bool editMode, int editIndex)
        {
            EditIndex = editIndex;
            EditMode = editMode;

            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!EditMode)
            {
                return;
            }

            e.Graphics.Clear(Color.Black);

            for (int i = 0; i <= 2; i++)
            {
                Rectangle borderRectangle = new Rectangle(Config<AppConfig>.Value.PlayerLocations[i], Config<AppConfig>.Value.PlayerSizes[i]);
                DrawBorder(e.Graphics, borderRectangle, EditIndex == i ? Color.Green : Color.Red);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }
    }
}
