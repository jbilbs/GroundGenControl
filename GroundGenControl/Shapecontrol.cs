using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeControl
{
    /// <summary>
    /// Defines the shape types available for the ShapeControl.
    /// </summary>
    public enum ShapeType
    {
        Rectangle,
        Ellipse
    }

    /// <summary>
    /// A custom WinForms control that draws colored shapes with optional gradient fills.
    /// Used as status indicators throughout the GroundGenControl UI.
    /// Replacement for the original ShapeControl.dll dependency.
    /// </summary>
    public class ShapeControl : Control
    {
        // ── Private backing fields ────────────────────────────────────────────

        private ShapeType _shape = ShapeType.Ellipse;
        private Color _centerColor = Color.White;
        private Color _surroundColor = Color.Gray;
        private Color _borderColor = Color.Black;
        private DashStyle _borderStyle = DashStyle.Solid;
        private int _borderWidth = 1;
        private bool _useGradient = false;

        // ── Constructor ───────────────────────────────────────────────────────

        public ShapeControl()
        {
            // Enable custom painting and reduce flicker
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            BackColor = Color.Transparent;
            Size = new Size(24, 24);
        }

        // ── Public Properties ─────────────────────────────────────────────────

        /// <summary>
        /// The shape to draw — Rectangle or Ellipse.
        /// </summary>
        public ShapeType Shape
        {
            get => _shape;
            set { _shape = value; Invalidate(); }
        }

        /// <summary>
        /// The center (inner) color of the shape, used in gradient mode.
        /// In flat mode this is the fill color.
        /// </summary>
        public Color CenterColor
        {
            get => _centerColor;
            set { _centerColor = value; Invalidate(); }
        }

        /// <summary>
        /// The surround (outer) color used in gradient mode.
        /// </summary>
        public Color SurroundColor
        {
            get => _surroundColor;
            set { _surroundColor = value; Invalidate(); }
        }

        /// <summary>
        /// The color of the shape's border.
        /// </summary>
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        /// <summary>
        /// The dash style of the border (Solid, Dash, Dot, etc).
        /// </summary>
        public new DashStyle BorderStyle
        {
            get => _borderStyle;
            set { _borderStyle = value; Invalidate(); }
        }

        /// <summary>
        /// The width in pixels of the border.
        /// </summary>
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>
        /// When true, fills the shape with a radial gradient from CenterColor to SurroundColor.
        /// When false, fills with CenterColor as a flat fill.
        /// </summary>
        public bool UseGradient
        {
            get => _useGradient;
            set { _useGradient = value; Invalidate(); }
        }

        // ── Painting ──────────────────────────────────────────────────────────

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Inset the draw rect slightly so the border isn't clipped
            int inset = Math.Max(1, _borderWidth / 2);
            Rectangle rect = new Rectangle(
                inset, inset,
                Width - inset * 2 - 1,
                Height - inset * 2 - 1);

            if (rect.Width <= 0 || rect.Height <= 0)
                return;

            // ── Fill ──────────────────────────────────────────────────────────
            if (_useGradient)
            {
                // Radial-style gradient: use a path gradient brush
                using GraphicsPath path = MakePath(rect);
                using PathGradientBrush brush = new PathGradientBrush(path)
                {
                    CenterColor = _centerColor,
                    SurroundColors = new[] { _surroundColor }
                };
                FillShape(g, brush, rect);
            }
            else
            {
                using SolidBrush brush = new SolidBrush(_centerColor);
                FillShape(g, brush, rect);
            }

            // ── Border ────────────────────────────────────────────────────────
            if (_borderWidth > 0)
            {
                using Pen pen = new Pen(_borderColor, _borderWidth)
                {
                    DashStyle = _borderStyle
                };
                DrawShape(g, pen, rect);
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private void FillShape(Graphics g, Brush brush, Rectangle rect)
        {
            if (_shape == ShapeType.Ellipse)
                g.FillEllipse(brush, rect);
            else
                g.FillRectangle(brush, rect);
        }

        private void DrawShape(Graphics g, Pen pen, Rectangle rect)
        {
            if (_shape == ShapeType.Ellipse)
                g.DrawEllipse(pen, rect);
            else
                g.DrawRectangle(pen, rect);
        }

        private GraphicsPath MakePath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            if (_shape == ShapeType.Ellipse)
                path.AddEllipse(rect);
            else
                path.AddRectangle(rect);
            return path;
        }

        // ── Overrides ─────────────────────────────────────────────────────────

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}