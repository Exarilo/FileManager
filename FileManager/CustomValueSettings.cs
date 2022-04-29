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
    public partial class CustomValueSettings : Settings
    {
        private Action<double> action;
        private TrackBar trackBar;


        public CustomValueSettings() : base()
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Location = new Point(0, 5);
            label.Text = "Value :";

            trackBar = new TrackBar();

            trackBar.Location = new Point(label.Location.X, label.Location.Y+30);
            trackBar.Size = new Size(this.Size.Width -10, trackBar.Size.Height);
            trackBar.Maximum = 100;
            trackBar.Minimum = -100;
            trackBar.Value = 0;
            //trackBar.Value = trackBar.Maximum / 2;
            trackBar.TickStyle = TickStyle.None;


            this.Controls.Add(label);
            this.Controls.Add(trackBar);
        }
        public void addAction(Action<double> action)
        {
            Visible = true;
            this.action = action;
            trackBar.MouseCaptureChanged += (sender, args) => action(trackBar.Value);

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
