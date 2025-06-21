using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public class CustomProgressBar : ProgressBar
    {
        public CustomProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Fon
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(230, 230, 230)))
            {
                g.FillRoundedRectangle(bgBrush, rect, 8);
            }

            // Progress
            int progressWidth = (int)((Value / (double)Maximum) * rect.Width);
            Rectangle progressRect = new Rectangle(0, 0, progressWidth, rect.Height);
            Color startColor = Value < 30 ? Color.FromArgb(231, 76, 60) :
                              Value < 70 ? Color.FromArgb(241, 196, 15) :
                              Color.FromArgb(46, 204, 113);
            Color endColor = Color.FromArgb(100, startColor);
            using (LinearGradientBrush progressBrush = new LinearGradientBrush(progressRect, startColor, endColor, 45f))
            {
                g.FillRoundedRectangle(progressBrush, progressRect, 8);
            }

            // Chegara
            using (Pen borderPen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                g.DrawRoundedRectangle(borderPen, rect, 8);
            }
        }
    }

    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics g, Brush brush, Rectangle rect, int radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.Width - radius * 2, rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseFigure();
                g.FillPath(brush, path);
            }
        }

        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle rect, int radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.Width - radius * 2, rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseFigure();
                g.DrawPath(pen, path);
            }
        }
    }
}