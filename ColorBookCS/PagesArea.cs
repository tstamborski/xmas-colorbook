using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace ColorBookCS
{
    class PagesArea : ContainerControl
    {
        public PagesArea()
        {
            BackColor = Color.Lavender;
            AutoScroll = true;

            Anchor = AnchorStyles.Top | AnchorStyles.Left;
            Dock = DockStyle.Fill;
            

            _pages = new List<Page>();
            _index = 0;

            //SizeChanged += PagesArea_SizeChanged;
        }

        /*
        private void PagesArea_SizeChanged(object sender, EventArgs e)
        {
            if (_pages.Count == 0)
                return;

            Page page = _pages[_index];

            if (page.Width < Width)
                page.Left = Width / 2 - page.Width / 2;

            if (page.Height < Height)
                page.Top = Height / 2 - page.Height / 2;

            ScrollToControl(page);
        }
        */

        public void AddPage(Page p)
        {
            _pages.Add(p);
            //p.SizeChanged += Page_SizeChanged;
            Controls.Add(p);

            Index = 0;
        }

        public void FlipNext()
        {
            if (_index < _pages.Count - 1)
                Index += 1;
        }

        public void FlipPrevious()
        {
            if (_index > 0)
                Index -= 1;
        }

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                for (int i = 0; i < _pages.Count; i++)
                {
                    if (i == value)
                    {
                        _pages[i].Visible = true;
                        _pages[i].Focus();
                    }
                    else
                        _pages[i].Visible = false;
                }

                //PagesArea_SizeChanged(null, null);
            }
        }
        public Page ActualPage
        {
            get { return _pages[_index]; }
        }
        public int PageCount
        {
            get { return _pages.Count; }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //base.OnMouseWheel(e);
        }

        /*
        private void Page_SizeChanged(object sender, EventArgs e)
        {
            Page page = sender as Page;
            
            if (page.Width < Width)
                page.Left = Width / 2 - page.Width / 2;

            if (page.Height < Height)
                page.Top = Height / 2 - page.Height / 2;
        }
        */
    

        private int _index;
        private List<Page> _pages;
    }
}
