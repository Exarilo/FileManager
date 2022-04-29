using Microsoft.VisualBasic;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{

    class BatManager : ManageFile, IFile
    {
            private string[] originalBatTxt;
    
        public BatManager(string[] originalBatTxt, Form form) : base(form)
        {
            this.originalBatTxt = originalBatTxt;
            this.form = form;
        }
        public void BackUpFolder()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string copy = $"xcopy /s/i \"{ dialog.FileName}\" \"{dialog.FileName}{"-Copy"}\"";
                InsertTextAtGoodPosition(copy);
            }
        }
        public void BackUpFile()
        {
            string fileName = Tools.OpenFile();
            string copy = $"xcopy /s/i \"{fileName}\" \"{fileName}{"-Copy"}\"";
            InsertTextAtGoodPosition(copy);
        }

        public void CreateFolder()
        {
            string FolderName = Interaction.InputBox("Choose Folder Name :", "Choose Folder Name", "FolderName");
            InsertTextAtGoodPosition($"MKDIR {FolderName}");
        }
        public void RunBatFile()
        {
            Process cmd = new Process();
            string strCmdText;
            strCmdText = $@"/C {(mainComponent as TextBox).Text}";
            Process.Start("CMD.exe", strCmdText);
        }

        public void Save()
        {
            Panel panel = form.Controls.Find("panelMid", true).FirstOrDefault() as Panel;

            foreach (Control control in panel.Controls)
            {
                if (!control.GetType().Equals(typeof(TextBox)))
                    continue;
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = DateTime.Now.ToString("programme");
                save.Filter = "Bat File|*.bat";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(save.OpenFile());
                    writer.Write((control as TextBox).Text);
                    writer.Dispose();
                    writer.Close();
                }
                break;

            }
        }
        public override void createButtons(Panel panel)
        {
            base.createButtons(panel);
            
            CustomButton btBackUpFolder = new CustomButton("Back Up Folder", BackUpFolder);
            CustomButton btBackUpFile = new CustomButton("Back Up File", BackUpFile);
            CustomButton btCreateFolder = new CustomButton("Create Folder", CreateFolder);
            CustomButton btRunBatFile = new CustomButton("Run", RunBatFile);
            CustomButton btSave = new CustomButton("Save", Save);
            listButtons.Add(btBackUpFolder);
            listButtons.Add(btBackUpFile);
            listButtons.Add(btCreateFolder);
            listButtons.Add(btRunBatFile);
            listButtons.Add(btSave);
            base.setButtonLocation(panel);

        }
        public override void FillPanelWithControl(Panel panel, object o)
        {
            base.FillPanelWithControl(panel, o);

            if (o.GetType().Equals(typeof(string[])))
            {
                TextBox textbox = new TextBox();
                textbox.Font = new Font(textbox.Font.FontFamily, 14);
                textbox.Multiline = true;
                textbox.Dock = DockStyle.Fill;

                foreach (string line in (string[])o)
                {
                    textbox.Text += line;
                    textbox.Text += Environment.NewLine;
                }
                panel.Controls.Add(textbox);
                mainComponent = textbox;
            }
        }
        public void InsertTextAtGoodPosition(string text)
        {
            int cursorPos = (mainComponent as TextBox).SelectionStart;
            int lineCount = (mainComponent as TextBox).GetLineFromCharIndex(cursorPos) + 1; // zero based
            List<string> lines = (mainComponent as TextBox).Lines.ToList();
            lines.Insert(lineCount-1, text);
            (mainComponent as TextBox).Lines = lines.ToArray();
            (mainComponent as TextBox).Select(cursorPos, 0);
        }
    }
}
