
namespace FileManager
{
    partial class HomeForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btBrowse = new System.Windows.Forms.Button();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBot = new System.Windows.Forms.Panel();
            this.panelMid = new System.Windows.Forms.Panel();
            this.settings1 = new FileManager.Settings();
            this.panelTop.SuspendLayout();
            this.panelMid.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btBrowse);
            this.panelTop.Controls.Add(this.tbFilePath);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1011, 71);
            this.panelTop.TabIndex = 7;
            // 
            // btBrowse
            // 
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.Location = new System.Drawing.Point(903, 25);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(65, 23);
            this.btBrowse.TabIndex = 6;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(109, 25);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(773, 20);
            this.tbFilePath.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Image file :";
            // 
            // panelBot
            // 
            this.panelBot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBot.Location = new System.Drawing.Point(0, 452);
            this.panelBot.Name = "panelBot";
            this.panelBot.Size = new System.Drawing.Size(1011, 125);
            this.panelBot.TabIndex = 8;
            // 
            // panelMid
            // 
            this.panelMid.Controls.Add(this.settings1);
            this.panelMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMid.Location = new System.Drawing.Point(0, 71);
            this.panelMid.Name = "panelMid";
            this.panelMid.Size = new System.Drawing.Size(1011, 381);
            this.panelMid.TabIndex = 9;
            // 
            // settings1
            // 
            this.settings1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.settings1.Dock = System.Windows.Forms.DockStyle.Right;
            this.settings1.Location = new System.Drawing.Point(811, 0);
            this.settings1.Name = "settings1";
            this.settings1.Size = new System.Drawing.Size(200, 381);
            this.settings1.TabIndex = 0;
            this.settings1.Visible = false;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 577);
            this.Controls.Add(this.panelMid);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBot);
            this.Name = "HomeForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBot;
        private System.Windows.Forms.Panel panelMid;
        private Settings settings1;
    }
}

