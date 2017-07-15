using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ColorBookCS
{
    class Page : Control
    {
        public Page(Bitmap bmp, String name = "Default page")
        {
            Name = name;

            _bmp = bmp;
            _bmp.MakeTransparent(Color.White);
            _canvas = new Bitmap(bmp.Width, bmp.Height);
            Graphics g = Graphics.FromImage(_canvas);
            g.Clear(Color.White);

            PencilWidth = 16;
            PencilColor = Color.Blue;
            Zoom = 1.0;
            _mousebtn = false;
        }

        public event ColorChanged ColorChanged;
        
        public int MouseX { get { return (int)_mousex; } }
        public int MouseY { get { return (int)_mousey; } }
        public Color PencilColor
        {
            get
            {
                return _pencilcolor;
            }
            set
            {
                _pencilcolor = value;
                Graphics g = Graphics.FromImage(_brushbmp);
                g.Clear(value);
            }
        }
        public int PencilWidth
        {
            get
            {
                return _pencilwidth;
            }
            set
            {
                _pencilwidth = value;
                _brushbmp = new Bitmap(value, value);

                Graphics g = Graphics.FromImage(_brushbmp);
                g.Clear(_pencilcolor);
            }
        }
        public double Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                Width = (int)(_bmp.Width * value);
                Height = (int)(_bmp.Height * value);

                Refresh();
            }
        }

        public bool Save(String filepath)
        {
            Bitmap bmp = new Bitmap(_bmp.Width, _bmp.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(_canvas, 0, 0);
            g.DrawImage(_bmp, 0, 0);

            try
            {
                bmp.Save(filepath);
            }
            catch (AccessViolationException)
            {
                MessageBox.Show(null, "Error! Cannot save to file.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.DrawImage(_canvas,
                new Rectangle(0, 0, Width, Height),
                new Rectangle(0, 0, _canvas.Width, _canvas.Height),
                GraphicsUnit.Pixel
                );
            g.DrawImage(_bmp,
                new Rectangle(0, 0, Width, Height),
                new Rectangle(0, 0, _bmp.Width, _bmp.Height),
                GraphicsUnit.Pixel
                );
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            _oldmousex = _mousex;
            _oldmousey = _mousey;

            _mousex = e.X / _zoom;
            _mousey = e.Y / _zoom;

            if (_mousebtn)
            {
                Graphics g = Graphics.FromImage(_canvas);

                double stepx = (_mousex - _oldmousex) / (_pencilwidth);
                double stepy = (_mousey - _oldmousey) / (_pencilwidth);

                double x = _oldmousex, y = _oldmousey;
                while (x != _mousex || y != _mousey)
                {
                    x += stepx;
                    y += stepy;

                    g.DrawImage(_brushbmp, (float)x, (float)y);
                    Invalidate(new Rectangle((int)(x * _zoom), (int)(y * _zoom),
                            (int)(_pencilwidth * _zoom), (int)(_pencilwidth * _zoom)));
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _mousex = e.X / _zoom;
            _mousey = e.Y / _zoom;

            if (e.Button == MouseButtons.Left)
            {
                _mousebtn = true;

                Graphics g = Graphics.FromImage(_canvas);
                g.DrawImage(_brushbmp, (float)_mousex, (float)_mousey);

                Invalidate(new Rectangle((int)(_mousex * _zoom), (int)(_mousey * _zoom),
                        (int)(_pencilwidth * _zoom), (int)(_pencilwidth * _zoom)));
            }
            else if (e.Button == MouseButtons.Right)
            {
                PencilColor = _canvas.GetPixel((int)_mousex, (int)_mousey);

                ColorChanged?.Invoke();
            }

            _oldmousex = _mousex;
            _oldmousey = _mousey;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _mousebtn = false;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            Color c = PencilColor;
            int r = c.R, g = c.G, b = c.B;

            r += e.Delta / 16;
            g += e.Delta / 16;
            b += e.Delta / 16;

            if (r > 255 || r < 0 ||
                g > 255 || g < 0 ||
                b > 255 || b < 0)
                return;

            PencilColor = Color.FromArgb(r, g, b);

            ColorChanged?.Invoke();
        }

        private Bitmap _brushbmp;
        private Bitmap _bmp, _canvas;

        private double _zoom;
        private int _pencilwidth;
        private Color _pencilcolor;
        private double _mousex, _mousey;
        private double _oldmousex, _oldmousey;
        private bool _mousebtn;
    }

    delegate void ColorChanged();
}
