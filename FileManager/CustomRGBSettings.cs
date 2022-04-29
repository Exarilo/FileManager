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
    public partial class CustomRGBSettings : Settings 
    {
        private Action<int,string> action;
        private TrackBar trackBarR;
        private TrackBar trackBarG;
        private TrackBar trackBarB;


        public CustomRGBSettings(): base()
        {
            Label labelR = new Label();
            labelR.Location = new Point(2,5);
            labelR.Text = "Red :";
            labelR.AutoSize = true; 

            Label labelG = new Label();
            labelG.Location = new Point(labelR.Location.X, (Size.Height / 2)-(labelG.Height/2));
            labelG.Text = "Green :";
            labelG.AutoSize = true;

            Label labelB = new Label();
            labelB.Location = new Point(labelR.Location.X, this.Height - labelB.Height+10);
            labelB.Text = "Blue :";
            labelB.AutoSize = true;

            trackBarR = new TrackBar();
            trackBarG = new TrackBar();
            trackBarB = new TrackBar();
            trackBarR.Tag = "Red";
            trackBarG.Tag = "Green";
            trackBarB.Tag = "Blue";

            trackBarR.Location = new Point(labelR.Location.X +40, labelR.Location.Y);
            trackBarR.Size= new Size(this.Size.Width- labelR.Size.Width+45, trackBarR.Size.Height);
            trackBarR.Maximum = 255;
            trackBarR.Value = trackBarR.Maximum / 2;
            trackBarR.TickStyle = TickStyle.None;


            trackBarG.Location = new Point(labelG.Location.X + 40, labelG.Location.Y);
            trackBarG.Size = new Size(this.Size.Width - labelG.Size.Width + 45, trackBarG.Size.Height);
            trackBarG.Maximum = 255;
            trackBarG.Value = trackBarG.Maximum / 2;
            trackBarG.TickStyle = TickStyle.None;


            trackBarB.Location = new Point(labelB.Location.X + 40, labelB.Location.Y);
            trackBarB.Size = new Size(this.Size.Width - labelB.Size.Width + 45, trackBarB.Size.Height);
            trackBarB.Maximum = 255;
            trackBarB.Value = trackBarB.Maximum / 2;
            trackBarB.TickStyle = TickStyle.None;

            this.Controls.Add(labelR);
            this.Controls.Add(labelG);
            this.Controls.Add(labelB);
            this.Controls.Add(trackBarR);
            this.Controls.Add(trackBarG);
            this.Controls.Add(trackBarB);
        }
        public void addAction(Action<int,string> action)
        {
            Visible = true;
            this.action = action;
            trackBarR.MouseCaptureChanged += (sender, args) => action(trackBarR.Value, (string)trackBarR.Tag);
            trackBarG.MouseCaptureChanged += (sender, args) => action(trackBarG.Value, (string)trackBarG.Tag);
            trackBarB.MouseCaptureChanged += (sender, args) => action(trackBarB.Value, (string)trackBarB.Tag);

        }

    }
}
