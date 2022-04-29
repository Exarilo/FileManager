using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{

    public static class Tools
    {
        public static string OpenFile()
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "All files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                //WriteLastFilePath(open.FileName);
                return open.FileName;
            }
            return null;
        }
        public static void WriteLastFilePath(string text)
        {
            string path = "LastFilePath.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(text);
            }

        }
    }
}
