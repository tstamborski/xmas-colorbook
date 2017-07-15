using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ColorBookCS
{
    class ColorIcon
    {
        public ColorIcon(Color colr)
        {
            _bitmap = new Bitmap(16, 16);
            Color = colr;
        }

        public Icon Icon
        {
            get { return Icon.FromHandle(_bitmap.GetHicon()); }
        }
        public Bitmap Bitmap
        {
            get { return _bitmap; }
        }
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Graphics g = Graphics.FromImage(_bitmap);
                g.FillRectangle(new SolidBrush(value), 0, 0, 15, 15);
                g.DrawRectangle(Pens.Black, 0, 0, 15, 15);
            }
        }

        private Bitmap _bitmap;
        private Color _color;
    }

    class ColorMenuItem : ToolStripMenuItem
    {
        public ColorMenuItem(String text, ColorIcon color_icon, EventHandler e)
            : base()
        {
            Text = text;
            _colrIcon = color_icon;
            Image = _colrIcon.Bitmap;
            Click += e;
        }

        public ColorIcon ColorIcon
        {
            get
            {
                return _colrIcon;
            }
        }

        private ColorIcon _colrIcon;
    }

    class ColorBarButton : ToolStripButton
    {
        public ColorBarButton(ColorIcon color_icon, EventHandler e)
            : base()
        {
            _colrIcon = color_icon;
            Image = _colrIcon.Bitmap;
            Click += e;
        }

        public ColorIcon ColorIcon
        {
            get
            {
                return _colrIcon;
            }
        }

        private ColorIcon _colrIcon;
    }
}
