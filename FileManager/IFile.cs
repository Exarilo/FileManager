using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    internal interface IFile
    {
        void createButtons(Panel panel);
        void FillPanelWithControl(Panel panel, object o);
        void FillPanelWithControl(Panel panel);
    }
}
