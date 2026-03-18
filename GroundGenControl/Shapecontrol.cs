using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeControl
{
    public enum ShapeType
    {
        Rectangle,
        Ellipse
    }

    public class ShapeControl : Control
    {
        private ShapeType _shape = ShapeType.Ellipse;
        private Color _centerColor = Color.FromArgb(100, 255, 0, 0);
        private Color _surroundColor = Color.FromArgb(100, 0, 255, 255);
        private Color _borderColor = Color.FromArgb(255, 255, 0, 0);
        private DashStyle _borderStyle = DashStyle.Solid;
        private int _borderWidth = 3;
        private bool _useGradient = false;
        private GraphicsPath _outline = new GraphicsPath();

        public ShapeControl()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.DoubleBuffer, true);
            BackColor = Color.FromArgb(0, 255, 255, 255);
            Size = new Size(24, 24);
        }

        public ShapeType Shape
        {
            get => _shape;
            set { _shape = value; OnResize(null); }
        }

        public Color CenterColor
        {
            get => _centerColor;
            set { _centerColor = value; Refresh(); }
        }

        public Color SurroundColor
        {
            get => _surroundColor;
            set { _surroundColor = value; Refresh(); }
        }

        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Refresh(); }
        }

        public new DashStyle BorderStyle
        {
            get => _borderStyle;
            set { _borderStyle = value; Refresh(); }
        }

        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = Math.Max(0, value); Refresh(); }
        }

        public bool UseGradient
        {
            get => _useGradient;
            set { _useGradient = value; Refresh(); }
        }

        protected override void OnResize(EventArgs e)
        {
            if (Width >= 0 && Height > 0)
            {
                _outline = new GraphicsPath();
                if (_shape == ShapeType.Ellipse)
                    _outline.AddEllipse(0, 0, Width, Height);
                else
                    _outline.AddRectangle(new Rectangle(0, 0, Width, Height));

                this.Region = new Region(_outline);
                Refresh();
                if (e != null) base.OnResize(e);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (_useGradient)
            {
                PathGradientBrush brush = new PathGradientBrush(_outline);
                brush.CenterColor = _centerColor;
                brush.SurroundColors = new Color[] { _surroundColor };
                pe.Graphics.FillPath(brush, _outline);
                brush.Dispose();
            }

            if (_borderWidth > 0)
            {
                Pen pen = new Pen(_borderColor, _borderWidth * 2);
                pen.DashStyle = _borderStyle;
                pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                pe.Graphics.DrawPath(pen, _outline);
                pen.Dispose();
            }

            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            pe.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor),
                new Rectangle(0, 0, Width, Height), sf);

            base.OnPaint(pe);
        }
    }
}