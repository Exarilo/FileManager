using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RichTextBoxEx;
namespace FileManager
{
    class ManageFile 
    {
        protected object mainComponent;
        protected Form form;
        protected List<Button> listButtons = new List<Button>();

        public ManageFile(Form form) 
        {
            this.form = form;
        }
        public ManageFile()
        {
        }

        public void UpdateMainComponent(object o)
        {
            if (mainComponent.GetType().Equals(typeof(PictureBox)) && o.GetType().Equals(typeof(Bitmap)))
            {
                (mainComponent as PictureBox).Image = (o as Bitmap);
            }
        }
        public virtual void FillPanelWithControl(Panel panel, object o)
        {
            if (panel.Controls.Count > 0)
            {
                foreach (Control control in panel.Controls)
                    panel.Controls.Remove(control);
            }
        }
        public virtual void FillPanelWithControl(Panel panel)
        {
            if (panel.Controls.Count > 0)
            {
                foreach (Control control in panel.Controls)
                    panel.Controls.Remove(control);
            }
        }
        public virtual void createButtons(Panel panel)
        {
            if (panel.Controls.Count > 0)
            {
                foreach (Control control in panel.Controls)
                    panel.Controls.Remove(control);
            }
        }
        public virtual void setButtonLocation(Panel panel)
        {
            if (listButtons == null)
                return;
            if (listButtons.Count <= 0)
                return;
            int interval = 10;
            for (int i = 0; i < listButtons.Count; i++)
            {
                if (i == 0)
                    listButtons[i].Location = new Point(20, panel.Height / 2);

                else
                {
                    listButtons[i].Location = new Point(listButtons[i - 1].Location.X + listButtons[i - 1].Width + interval, panel.Height / 2);
                }
                if (listButtons[i].Location.X + listButtons[i].Width > panel.Width)
                    return;
                panel.Controls.Add(listButtons[i]);
            }
        }
    }
}
