using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ColorBookCS
{
    class AboutDialog : Form
    {
        public AboutDialog()
        {
            Size = new Size(400, 300);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Visible = false;

            m_OkBtn = new Button();
            m_OkBtn.Text = "OK";
            m_OkBtn.Parent = this;
            m_OkBtn.DialogResult = DialogResult.OK;
            m_OkBtn.Location = new Point(260, 230);
            m_OkBtn.Anchor = AnchorStyles.Left;

            m_TabCtrl = new TabControl();
            m_TabCtrl.Size = new Size(370, 210);
            m_TabCtrl.Location = new Point(10, 10);
            m_TabAbout = new TabPage();
            m_TabAbout.Size = new Size(370, 210);
            m_TabAbout.BackColor = Color.FromKnownColor(KnownColor.Control);
            m_TabAbout.Text = "About... ";
            m_TabLicence = new TabPage();
            m_TabLicence.BackColor = Color.FromKnownColor(KnownColor.Control);
            m_TabLicence.Size = new Size(370, 210);
            m_TabLicence.Text = "License";
            m_TabCtrl.TabPages.Add(m_TabAbout);
            m_TabCtrl.TabPages.Add(m_TabLicence);
            m_TabCtrl.Parent = this;

            m_LicenceBox = new TextBox();
            m_LicenceBox.Multiline = true;
            m_LicenceBox.Location = new Point(10, 10);
            m_LicenceBox.Size = new Size(340, 160);
            m_LicenceBox.ReadOnly = true;
            m_LicenceBox.WordWrap = false;
            m_LicenceBox.ScrollBars = ScrollBars.Both;
            m_TabLicence.Controls.Add(m_LicenceBox);

            m_IconBox = new PictureBox();
            m_IconBox.Size = new Size(64, 64);
            m_IconBox.Location = new Point(10, 10);
            m_IconBox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_TabAbout.Controls.Add(m_IconBox);

            m_NameLabel = new Label();
            m_NameLabel.Location = new Point(90, 10);
            m_NameLabel.Size = new Size(250, 25);
            m_NameLabel.Font = new Font("Courier", 10.0f, FontStyle.Bold);
            m_TabAbout.Controls.Add(m_NameLabel);

            m_VerLabel = new Label();
            m_VerLabel.Location = new Point(90, 35);
            m_VerLabel.Size = new Size(250, 25);
            m_TabAbout.Controls.Add(m_VerLabel);

            m_CopyLabel = new Label();
            m_CopyLabel.Location = new Point(90, 60);
            m_CopyLabel.Size = new Size(250, 25);
            m_TabAbout.Controls.Add(m_CopyLabel);

            m_DescLabel = new Label();
            m_DescLabel.Location = new Point(90, 85);
            m_DescLabel.Size = new Size(250, 75);
            m_TabAbout.Controls.Add(m_DescLabel);
        }

        public Icon AppIcon
        {
            set { Icon = value; m_IconBox.Image = value.ToBitmap(); }
        }
        public String AppName
        {
            set { Text = value; m_NameLabel.Text = value; }
        }
        public String AppVersion
        {
            set { m_VerLabel.Text = value; }
        }
        public String AppCopyright
        {
            set { m_CopyLabel.Text = value; }
        }
        public String AppDescription
        {
            set { m_DescLabel.Text = value; }
        }
        public String Licence
        {
            set { m_LicenceBox.Text = value; }
        }

        private TabControl m_TabCtrl;
        private TabPage m_TabAbout, m_TabLicence;
        private PictureBox m_IconBox;
        private Label m_NameLabel, m_VerLabel, m_CopyLabel, m_DescLabel;
        private TextBox m_LicenceBox;
        private Button m_OkBtn;
    }
}
