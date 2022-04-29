using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    class TextManager : ManageFile,IFile
    {
        string[] originalText;

        public TextManager(string[] originalText,Form form):base(form)
        {
            this.form = form;
            this.originalText = originalText;
        }
        public void Save()
        {
            Panel panel = form.Controls.Find("panelMid", true).FirstOrDefault() as Panel;

            foreach (Control control in panel.Controls)
            {
                if (!control.GetType().Equals(typeof(RichTextBoxEx.RichTextBoxEx)))
                    continue;
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = DateTime.Now.ToString("yyyy-MM-dd");
                save.Filter = "Word Documents|*.doc;*.docx" +
                        "|Text Files|*.txt;" +
                        "|Rich Text Files|*.rtf;" +
                        "|Archives Files|*.zip;*.rar" +
                        "|All Files|*.*";
                //"Portable Document Format|*.pdf;" +

                if (save.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(save.OpenFile());
                    writer.Write((control as RichTextBoxEx.RichTextBoxEx).Rtf);
                    writer.Dispose();
                    writer.Close();
                }
                break;

            }
        }
        public override void FillPanelWithControl(Panel panel, object o)
        {
            base.FillPanelWithControl(panel, o);

            if (o.GetType().Equals(typeof(string[])))
            {
                RichTextBoxEx.RichTextBoxEx textBox = new RichTextBoxEx.RichTextBoxEx();
                textBox.AllowSpellCheck = false;
                textBox.AllowHyphenation = false;
                textBox.Dock = DockStyle.Fill;

                foreach (string line in (string[])o)
                {
                    textBox.Text += line;
                    textBox.Text += Environment.NewLine;
                }
                panel.Controls.Add(textBox);
                mainComponent = textBox;
            }
        }
        public override void createButtons(Panel panel)
        {
            base.createButtons(panel);
            CustomButton btSave = new CustomButton("Save", Save);

            listButtons.Add(btSave);
            base.setButtonLocation(panel);
        }
    }
}
