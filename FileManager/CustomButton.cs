using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public class CustomButton : Button
    {
        public CustomButton(string text, Action action) : base()
        {
            AutoSize = true;
            Text = text;
            Click += (sender, args) => action();
        }
    }
}