using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class HomeForm : Form
    {
        static ManageFile fileManager ;
        
        //static ImageManager imageManager ;

        public HomeForm()
        {
            InitializeComponent();
        }


        private void btBrowse_Click(object sender, EventArgs e)
        {
            string filePath = Tools.OpenFile();
            Tools.WriteLastFilePath(filePath);
            string extension = Path.GetExtension(filePath);
            tbFilePath.Text = filePath;
            
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".png":
                    Bitmap bitmap = new Bitmap(filePath);
                    fileManager = new ImageManager(bitmap,this);
                    fileManager.FillPanelWithControl(panelMid, bitmap);
                    break;
                case ".doc":
                case ".docx":
                case ".txt":
                case ".text":
                case ".pdf":
                case ".rtf":
                    string[] text = File.ReadAllLines(filePath);
                    fileManager = new TextManager(text,this);
                    fileManager.FillPanelWithControl(panelMid, text);
                    break;
                case ".eml":
                case ".msg":
                    fileManager = new EmailManager(this);
                    (fileManager as EmailManager).ReadMessage(filePath);
                    fileManager.FillPanelWithControl(panelMid);
                    break;
                case ".bat":
                    string[] textBat = File.ReadAllLines(filePath);
                    fileManager = new BatManager(textBat,this);
                    fileManager.FillPanelWithControl(panelMid, textBat);
                    break;
                default:
                    MessageBox.Show("Not yet implemented");
                    break;
            }
            fileManager.createButtons(panelBot);
        }



        private void HomeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
