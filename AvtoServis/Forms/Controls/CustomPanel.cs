using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AvtoServis.Forms.Screens
{
    public class CustomPanel : Panel
    {
        private int _cornerRadius = 15;

        [Category("Appearance")]
        [Description("Radius of the panel's corners")]
        [Browsable(true)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = Math.Max(0, value); // Salbiy qiymatlarni oldini olish
                UpdateRegion();
                Invalidate(); // Qayta chizish
            }
        }

        public CustomPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                int radius = _cornerRadius * 2;
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90); // Yuqori chap
                path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90); // Yuqori o‘ng
                path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90); // Pastki o‘ng
                path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90); // Pastki chap
                path.CloseAllFigures();

                this.Region = new Region(path);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                int radius = _cornerRadius * 2;
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();

                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion(); // O‘lcham o‘zgarganda Region ni yangilash
        }
    }
}