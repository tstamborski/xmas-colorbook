﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ColorBookCS
{
    class ColorBook : Form
    {
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new ColorBook());
        }

        ColorBook()
        {
            Size = new Size(800, 600);
            BackColor = Color.Lavender;
            Icon = Resource.ColorBookIcon;
            Text = "TS Xmas ColorBook ";
            FormClosing += ColorBook_FormClosing;

            currentPencilWidth = 8;
            currentZoom = 1.0;

            aboutDialog = new AboutDialog();
            aboutDialog.AppIcon = Resource.ColorBookIcon;
            aboutDialog.AppName = "TS Xmas ColorBook";
            aboutDialog.AppVersion = "ver. 0.8 (beta1)";
            aboutDialog.AppCopyright = "Copyright (c) 2017 by Tobiasz Stamborski";
            aboutDialog.AppDescription = "Free computer colorbook in the christmas mood!";
            aboutDialog.Licence = license;

            colorDialog = new ColorDialog();

            menuBar = new MenuStrip();

            colorBookMenu = (ToolStripMenuItem)menuBar.Items.Add("&ColorBook");
            nextPageMenuItem = (ToolStripMenuItem)colorBookMenu.DropDownItems.Add("&Next page ",
                Resource.NextIcon.ToBitmap(), new EventHandler(nextPageMenuItem_Click));
            nextPageMenuItem.ShortcutKeys = Keys.Control | Keys.Right;
            prevPageMenuItem = (ToolStripMenuItem)colorBookMenu.DropDownItems.Add("&Previous page ",
                Resource.PreviousIcon.ToBitmap(), new EventHandler(prevPageMenuItem_Click));
            prevPageMenuItem.ShortcutKeys = Keys.Control | Keys.Left;
            prevPageMenuItem.Enabled = false;
            colorBookMenu.DropDownItems.Add("-");
            saveAsMenuItem = (ToolStripMenuItem)colorBookMenu.DropDownItems.Add("&Save As... ",
                Resource.SaveIcon.ToBitmap(), new EventHandler(saveAsMenuItem_Click));
            saveAsMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            colorBookMenu.DropDownItems.Add("-");
            exitMenuItem = (ToolStripMenuItem)colorBookMenu.DropDownItems.Add("&Exit ",
                Resource.ExitIcon.ToBitmap(), new EventHandler(exitMenuItem_Click));
            exitMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;

            pencilMenu = (ToolStripMenuItem)menuBar.Items.Add("&Pencil");
            pastelMenu = (ToolStripMenuItem)pencilMenu.DropDownItems.Add("&Pastel ");
            yellowMenuItem = new ColorMenuItem("Yellow", new ColorIcon(Color.Yellow),
                new EventHandler(colorMenuItem_Click));
            orangeMenuItem = new ColorMenuItem("Orange", new ColorIcon(Color.Orange),
                new EventHandler(colorMenuItem_Click));
            lightPinkMenuItem = new ColorMenuItem("Light Pink", new ColorIcon(Color.LightPink),
                new EventHandler(colorMenuItem_Click));
            pinkMenuItem = new ColorMenuItem("Pink", new ColorIcon(Color.Pink),
                new EventHandler(colorMenuItem_Click));
            redMenuItem = new ColorMenuItem("Red", new ColorIcon(Color.Red),
                new EventHandler(colorMenuItem_Click));
            purpleMenuItem = new ColorMenuItem("Purple", new ColorIcon(Color.Purple),
                new EventHandler(colorMenuItem_Click));
            violetMenuItem = new ColorMenuItem("Violet", new ColorIcon(Color.BlueViolet),
                new EventHandler(colorMenuItem_Click));
            pastelMenu.DropDownItems.Add((ToolStripItem)yellowMenuItem);
            pastelMenu.DropDownItems.Add((ToolStripItem)orangeMenuItem);
            pastelMenu.DropDownItems.Add((ToolStripItem)lightPinkMenuItem);
            pastelMenu.DropDownItems.Add((ToolStripItem)pinkMenuItem);
            pastelMenu.DropDownItems.Add((ToolStripItem)redMenuItem);
            pastelMenu.DropDownItems.Add((ToolStripItem)purpleMenuItem);
            pastelMenu.DropDownItems.Add((ToolStripItem)violetMenuItem);
            marineMenu = (ToolStripMenuItem)pencilMenu.DropDownItems.Add("&Marine ");
            skyBlueMenuItem = new ColorMenuItem("Sky Blue", new ColorIcon(Color.SkyBlue),
                new EventHandler(colorMenuItem_Click));
            blueMenuItem = new ColorMenuItem("Blue", new ColorIcon(Color.Blue),
                new EventHandler(colorMenuItem_Click));
            darkBlueMenuItem = new ColorMenuItem("Dark Blue", new ColorIcon(Color.DarkBlue),
                new EventHandler(colorMenuItem_Click));
            yellowGreenMenuItem = new ColorMenuItem("Yellow Green", new ColorIcon(Color.YellowGreen),
                new EventHandler(colorMenuItem_Click));
            darkGreenMenuItem = new ColorMenuItem("Dark Green", new ColorIcon(Color.DarkGreen),
                new EventHandler(colorMenuItem_Click));
            seledineMenuItem = new ColorMenuItem("Slate Blue", new ColorIcon(Color.SlateBlue),
                new EventHandler(colorMenuItem_Click));
            marineMenu.DropDownItems.Add((ToolStripItem)skyBlueMenuItem);
            marineMenu.DropDownItems.Add((ToolStripItem)blueMenuItem);
            marineMenu.DropDownItems.Add((ToolStripItem)darkBlueMenuItem);
            marineMenu.DropDownItems.Add((ToolStripItem)yellowGreenMenuItem);
            marineMenu.DropDownItems.Add((ToolStripItem)darkGreenMenuItem);
            marineMenu.DropDownItems.Add((ToolStripItem)seledineMenuItem);
            groundMenu = (ToolStripMenuItem)pencilMenu.DropDownItems.Add("&Ground ");
            lightGrayMenuItem = new ColorMenuItem("Light Gray", new ColorIcon(Color.LightGray),
                new EventHandler(colorMenuItem_Click));
            slateGrayMenuItem = new ColorMenuItem("Slate Gray", new ColorIcon(Color.SlateGray),
                new EventHandler(colorMenuItem_Click));
            blackMenuItem = new ColorMenuItem("Black", new ColorIcon(Color.Black),
                new EventHandler(colorMenuItem_Click));
            goldMenuItem = new ColorMenuItem("Gold", new ColorIcon(Color.Gold),
                new EventHandler(colorMenuItem_Click));
            brownMenuItem = new ColorMenuItem("Brown", new ColorIcon(Color.Brown),
                new EventHandler(colorMenuItem_Click));
            groundMenu.DropDownItems.Add((ToolStripItem)lightGrayMenuItem);
            groundMenu.DropDownItems.Add((ToolStripItem)slateGrayMenuItem);
            groundMenu.DropDownItems.Add((ToolStripItem)blackMenuItem);
            groundMenu.DropDownItems.Add((ToolStripItem)goldMenuItem);
            groundMenu.DropDownItems.Add((ToolStripItem)brownMenuItem);
            currentColorIcon = redMenuItem.ColorIcon; //kolor na poczatek
            customColorMenuItem = (ToolStripMenuItem)pencilMenu.DropDownItems.Add("&Custom color... ",
                Resource.CustomColorIcon.ToBitmap(), new EventHandler(customColorMenuItem_Click));
            customColorMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            pencilMenu.DropDownItems.Add("-");
            rubberMenuItem = (ToolStripMenuItem)pencilMenu.DropDownItems.Add("&Rubber ",
                Resource.RubberIcon.ToBitmap(), new EventHandler(rubberMenuItem_Click));
            rubberMenuItem.ShortcutKeys = Keys.Control | Keys.R;

            optionsMenu = (ToolStripMenuItem)menuBar.Items.Add("&Options");
            pencilSizeMenu = (ToolStripMenuItem)optionsMenu.DropDownItems.Add("Pencil &size ");
            smallPencilMenuItem = (ToolStripMenuItem)pencilSizeMenu.DropDownItems.Add("&Small",
                null, new EventHandler(pencilSizeMenu_Click));
            smallPencilMenuItem.ShortcutKeys = Keys.None | Keys.F5;
            mediumPencilMenuItem = (ToolStripMenuItem)pencilSizeMenu.DropDownItems.Add("&Medium",
                null, new EventHandler(pencilSizeMenu_Click));
            mediumPencilMenuItem.ShortcutKeys = Keys.None | Keys.F6;
            mediumPencilMenuItem.Checked = true;
            bigPencilMenuItem = (ToolStripMenuItem)pencilSizeMenu.DropDownItems.Add("&Big",
                null, new EventHandler(pencilSizeMenu_Click));
            bigPencilMenuItem.ShortcutKeys = Keys.None | Keys.F7;
            largePencilMenuItem = (ToolStripMenuItem)pencilSizeMenu.DropDownItems.Add("&Large",
                null, new EventHandler(pencilSizeMenu_Click));
            largePencilMenuItem.ShortcutKeys = Keys.None | Keys.F8;
            zoomMenu = (ToolStripMenuItem)optionsMenu.DropDownItems.Add("&Zoom ");
            zoom50MenuItem = (ToolStripMenuItem)zoomMenu.DropDownItems.Add("&50%",
                null, new EventHandler(zoomMenu_Click));
            zoom100MenuItem = (ToolStripMenuItem)zoomMenu.DropDownItems.Add("&100%",
                null, new EventHandler(zoomMenu_Click));
            zoom100MenuItem.Checked = true;
            zoom200MenuItem = (ToolStripMenuItem)zoomMenu.DropDownItems.Add("&200%",
                null, new EventHandler(zoomMenu_Click));
            zoom400MenuItem = (ToolStripMenuItem)zoomMenu.DropDownItems.Add("&400%",
                null, new EventHandler(zoomMenu_Click));

            helpMenu = (ToolStripMenuItem)menuBar.Items.Add("&Help");
            aboutMenuItem = (ToolStripMenuItem)helpMenu.DropDownItems.Add("&About... ", Resource.ColorBookIcon.ToBitmap(),
                new EventHandler(aboutMenuItem_Click));

            toolBar = new ToolStrip();
            yellowButton = new ColorBarButton(new ColorIcon(Color.Yellow), new EventHandler(colorBarButton_Click));
            orangeButton = new ColorBarButton(new ColorIcon(Color.Orange), new EventHandler(colorBarButton_Click));
            pinkButton = new ColorBarButton(new ColorIcon(Color.Pink), new EventHandler(colorBarButton_Click));
            redButton = new ColorBarButton(new ColorIcon(Color.Red), new EventHandler(colorBarButton_Click));
            skyBlueButton = new ColorBarButton(new ColorIcon(Color.SkyBlue), new EventHandler(colorBarButton_Click));
            blueButton = new ColorBarButton(new ColorIcon(Color.Blue), new EventHandler(colorBarButton_Click));
            yellowGreenButton = new ColorBarButton(new ColorIcon(Color.YellowGreen), new EventHandler(colorBarButton_Click));
            darkGreenButton = new ColorBarButton(new ColorIcon(Color.DarkGreen), new EventHandler(colorBarButton_Click));
            lightGrayButton = new ColorBarButton(new ColorIcon(Color.LightGray), new EventHandler(colorBarButton_Click));
            slateGrayButton = new ColorBarButton(new ColorIcon(Color.SlateGray), new EventHandler(colorBarButton_Click));
            brownButton = new ColorBarButton(new ColorIcon(Color.Brown), new EventHandler(colorBarButton_Click));
            blackButton = new ColorBarButton(new ColorIcon(Color.Black), new EventHandler(colorBarButton_Click));
            rubberButton = new ToolStripButton(Resource.RubberIcon.ToBitmap());
            rubberButton.Click += RubberButton_Click;
            toolBar.Items.Add(yellowButton);
            toolBar.Items.Add(orangeButton);
            toolBar.Items.Add(pinkButton);
            toolBar.Items.Add(redButton);
            toolBar.Items.Add(skyBlueButton);
            toolBar.Items.Add(blueButton);
            toolBar.Items.Add(yellowGreenButton);
            toolBar.Items.Add(darkGreenButton);
            toolBar.Items.Add(slateGrayButton);
            toolBar.Items.Add(lightGrayButton);
            toolBar.Items.Add(brownButton);
            toolBar.Items.Add(blackButton);
            toolBar.Items.Add("-");
            toolBar.Items.Add(rubberButton);
            
            area = new PagesArea();
            page = new Page[7];
            page[0] = new Page(Resource.Page1, "Santa with bag");
            page[1] = new Page(Resource.Page2, "Santa smiling");
            page[2] = new ColorBookCS.Page(Resource.Page3, "Birth of Christ");
            page[3] = new Page(Resource.Page4, "Gifts");
            page[4] = new Page(Resource.Page5, "Rudolph reindeer");
            page[5] = new Page(Resource.Page6, "Christmas tree");
            page[6] = new Page(Resource.Page7, "Guru meditation");
            for (int i = 0; i < 7; i++)
            {
                page[i].MouseMove += ColorBook_MouseMove;
                page[i].ColorChanged += ColorBook_ColorChanged;
                area.AddPage(page[i]);
            }
            area.ActualPage.PencilWidth = currentPencilWidth;
            area.ActualPage.Zoom = currentZoom;
            area.ActualPage.PencilColor = currentColorIcon.Color;

            statusBar = new StatusBar();
            colorPanel = new StatusBarPanel();
            colorPanel.MinWidth = 20;
            colorPanel.Width = 20;
            colorPanel.Icon = redMenuItem.ColorIcon.Icon;
            pagePanel = new StatusBarPanel();
            pagePanel.AutoSize = StatusBarPanelAutoSize.Spring;
            pagePanel.Text = String.Format("Page {0} of {1}: {2}",
                area.Index + 1, area.PageCount, area.ActualPage.Name);
            coordPanel = new StatusBarPanel();
            coordPanel.Text = "x:    0, y:    0";
            coordPanel.MinWidth = 100;
            coordPanel.Width = 100;
            statusBar.Panels.Add(colorPanel);
            statusBar.Panels.Add(pagePanel);
            statusBar.Panels.Add(coordPanel);
            statusBar.ShowPanels = true;

            MainMenuStrip = menuBar;
            Controls.Add(area);
            Controls.Add(toolBar);
            Controls.Add(menuBar);
            Controls.Add(statusBar);
        }

        private void ColorBook_ColorChanged()
        {
            customColorIcon = new ColorIcon(area.ActualPage.PencilColor);
            currentColorIcon = customColorIcon;

            colorPanel.Icon = currentColorIcon.Icon;
        }

        private void ColorBook_MouseMove(object sender, MouseEventArgs e)
        {
            coordPanel.Text = String.Format("x: {0}, y: {1}",
                area.ActualPage.MouseX, area.ActualPage.MouseY);
        }

        private void RubberButton_Click(object sender, EventArgs e)
        {
            customColorIcon = new ColorIcon(Color.White);
            currentColorIcon = customColorIcon;
            colorPanel.Icon = Resource.RubberIcon;

            area.ActualPage.PencilColor = currentColorIcon.Color;
        }

        private void ColorBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure wish to quit?", "Closing... ",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void nextPageMenuItem_Click(object sender, EventArgs e)
        {
            area.FlipNext();
            if (area.Index >= area.PageCount - 1)
                nextPageMenuItem.Enabled = false;
            prevPageMenuItem.Enabled = true;

            area.ActualPage.Zoom = currentZoom;
            area.ActualPage.PencilColor = currentColorIcon.Color;
            area.ActualPage.PencilWidth = currentPencilWidth;

            pagePanel.Text = String.Format("Page {0} of {1}: {2}",
                area.Index + 1, area.PageCount, area.ActualPage.Name);
        }
        private void prevPageMenuItem_Click(object sender, EventArgs e)
        {
            area.FlipPrevious();
            if (area.Index == 0)
                prevPageMenuItem.Enabled = false;
            nextPageMenuItem.Enabled = true;

            area.ActualPage.Zoom = currentZoom;
            area.ActualPage.PencilColor = currentColorIcon.Color;
            area.ActualPage.PencilWidth = currentPencilWidth;

            pagePanel.Text = String.Format("Page {0} of {1}: {2}",
                area.Index + 1, area.PageCount, area.ActualPage.Name);
        }
        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.OverwritePrompt = true;
            dlg.Title = "Save As... ";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            dlg.Filter = "Bitmap file (*.bmp)|*.bmp";
            dlg.DefaultExt = "Bitmap file (*.bmp)|*.bmp";
            dlg.AddExtension = true;

            if (dlg.ShowDialog() == DialogResult.OK)
                area.ActualPage.Save(dlg.FileName);
        }
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void colorMenuItem_Click(object sender, EventArgs e)
        {
            ColorMenuItem item = sender as ColorMenuItem;

            currentColorIcon = item.ColorIcon;
            colorPanel.Icon = currentColorIcon.Icon;

            area.ActualPage.PencilColor = currentColorIcon.Color;
        }
        private void customColorMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                customColorIcon = new ColorIcon(colorDialog.Color);
                currentColorIcon = customColorIcon;
                colorPanel.Icon = currentColorIcon.Icon;
            }

            area.ActualPage.PencilColor = currentColorIcon.Color;
        }
        private void rubberMenuItem_Click(object sender, EventArgs e)
        {
            customColorIcon = new ColorIcon(Color.White);
            currentColorIcon = customColorIcon;
            colorPanel.Icon = Resource.RubberIcon;

            area.ActualPage.PencilColor = currentColorIcon.Color;
        }
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            aboutDialog.ShowDialog(this);
        }
        private void pencilSizeMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem mi in pencilSizeMenu.DropDownItems)
            {
                if (mi == item)
                    mi.Checked = true;
                else
                    mi.Checked = false;
            }

            if (item == smallPencilMenuItem)
                currentPencilWidth = 4;
            else if (item == mediumPencilMenuItem)
                currentPencilWidth = 8;
            else if (item == bigPencilMenuItem)
                currentPencilWidth = 16;
            else if (item == largePencilMenuItem)
                currentPencilWidth = 32;

            area.ActualPage.PencilWidth = currentPencilWidth;
        }
        private void zoomMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem mi in zoomMenu.DropDownItems)
            {
                if (mi == item)
                    mi.Checked = true;
                else
                    mi.Checked = false;
            }

            if (item == zoom50MenuItem)
                currentZoom = 0.5;
            else if (item == zoom100MenuItem)
                currentZoom = 1.0;
            else if (item == zoom200MenuItem)
                currentZoom = 2.0;
            else if (item == zoom400MenuItem)
                currentZoom = 4.0;

            area.ActualPage.Zoom = currentZoom;
        }

        private void colorBarButton_Click(object sender, EventArgs e)
        {
            ColorBarButton item = sender as ColorBarButton;

            currentColorIcon = item.ColorIcon;
            colorPanel.Icon = currentColorIcon.Icon;

            area.ActualPage.PencilColor = currentColorIcon.Color;
        }

        ColorIcon customColorIcon, currentColorIcon;
        double currentZoom;
        int currentPencilWidth;

        MenuStrip menuBar;
        ToolStripMenuItem colorBookMenu, pencilMenu, optionsMenu, helpMenu;
        ToolStripMenuItem nextPageMenuItem, prevPageMenuItem, saveAsMenuItem, exitMenuItem;
        ToolStripMenuItem pastelMenu, marineMenu, groundMenu;
        ColorMenuItem yellowMenuItem, orangeMenuItem, lightPinkMenuItem,
            pinkMenuItem, redMenuItem, purpleMenuItem, violetMenuItem;
        ColorMenuItem skyBlueMenuItem, blueMenuItem, darkBlueMenuItem, yellowGreenMenuItem,
            darkGreenMenuItem, seledineMenuItem;
        ColorMenuItem lightGrayMenuItem, slateGrayMenuItem, blackMenuItem, goldMenuItem, brownMenuItem;
        ToolStripMenuItem customColorMenuItem, rubberMenuItem;
        ToolStripMenuItem pencilSizeMenu, zoomMenu;
        ToolStripMenuItem smallPencilMenuItem, mediumPencilMenuItem, bigPencilMenuItem, largePencilMenuItem;
        ToolStripMenuItem zoom50MenuItem, zoom100MenuItem, zoom200MenuItem, zoom400MenuItem;
        ToolStripMenuItem aboutMenuItem;
        ToolStrip toolBar;
        ColorBarButton yellowButton, pinkButton, redButton, blueButton, darkGreenButton, lightGrayButton,
            brownButton, blackButton, orangeButton, skyBlueButton, yellowGreenButton, slateGrayButton;
        ToolStripButton rubberButton;
        StatusBar statusBar;
        StatusBarPanel colorPanel, pagePanel, coordPanel;

        PagesArea area;
        Page[] page;

        AboutDialog aboutDialog;
        ColorDialog colorDialog;

        String license = "The MIT License (MIT)\r\nCopyright (c) 2017 Tobiasz Stamborski\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the\r\nSoftware without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and / or sell copies of the Software,\r\nand to permit persons to whom the Software is furnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF\r\nMERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR\r\nANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH\r\nTHE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";
    }
}