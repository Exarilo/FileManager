using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class Settings : Panel 
    {
        public Settings()
        {
            InitializeComponent();
            Dock = DockStyle.Right;
            Visible = false;
            BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        public void addToPanel(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Panel)
                    panel.Controls.Remove(control);
            }
            panel.Controls.Add(this);
        }
    }
}
