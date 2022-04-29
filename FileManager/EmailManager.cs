using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{

    class EmailManager : ManageFile, IFile
    {
        private string from;
        private string to;
        private string subject;
        private string date;
        private string message;
        private string htmlBody;
        private List<string> listColumnName = new List<string>();
        private List<string> listContent = new List<string>();

        public EmailManager(Form form) : base(form)
        {
            this.form = form;
        }

        public void ReadMessage(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            switch (extension)
            {
                case ".eml":
                    ReadEML(filePath);
                    break;
                case ".msg":
                    ReadMSG(filePath);
                    break;
                default:
                    break;
            }
        }
        public void ReadMSG(string emlFileName)
        {
            using (var msg = new MsgReader.Outlook.Storage.Message(emlFileName))
            {
                from = msg.Sender.DisplayName; listContent.Add(from); listColumnName.Add("From");
                to = msg.GetEmailRecipients(MsgReader.Outlook.RecipientType.To, false, false); listContent.Add(to); listColumnName.Add("To");
                subject = msg.Subject; listContent.Add(subject); listColumnName.Add("Subject");
                date = msg.SentOn.ToString(); listContent.Add(date); listColumnName.Add("Date");
                message = msg.BodyText; listContent.Add(message); listColumnName.Add("Message");
                htmlBody = msg.BodyHtml; 
            }
        }
        public void ReadEML(string msgFileName)
        {

            var fileInfo = new FileInfo(msgFileName);
            var eml = MsgReader.Mime.Message.Load(fileInfo);
            
            if (eml.Headers.From != null)
            {
                from = eml.Headers.From.ToString();
                listColumnName.Add("From");
                listContent.Add(from);
            }

            if (eml.Headers != null)
            {
                if (eml.Headers.To != null)
                {
                    foreach (var recipient in eml.Headers.To)
                    {
                        to += recipient.Address;
                    }
                    listColumnName.Add("To");
                    listContent.Add(to);
                }
            }

            subject = eml.Headers.Subject;
            listColumnName.Add("Subject");
            listContent.Add(subject);

            if (eml.TextBody != null)
            {
                message= Encoding.UTF8.GetString(eml.TextBody.Body);
                listColumnName.Add("Message");
                listContent.Add(message);
            }

            if (eml.HtmlBody != null)
            {
                htmlBody = Encoding.UTF8.GetString(eml.HtmlBody.Body);
            }
        }

        public override void createButtons(Panel panel)
        {
            base.createButtons(panel);
            base.setButtonLocation(panel);

        }
        public override void FillPanelWithControl(Panel panel)
        {
            base.FillPanelWithControl(panel);

            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for(int i = 0; i < listColumnName.Count; i++)
            {
                dataGridView.Columns.Add(listColumnName[i], listColumnName[i]);
                if (listColumnName[i] != "Message")
                    dataGridView.Columns[listColumnName[i]].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                else
                    dataGridView.Columns[listColumnName[i]].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView.Rows[0].Cells[i].Value=listContent[i];
            } 

            panel.Controls.Add(dataGridView);
            mainComponent = dataGridView;

        }
    }
}