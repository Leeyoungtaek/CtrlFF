using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace CtrlFF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string path = dialog.SelectedPath;

            tbFolderPath.Text = path;
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbFolderPath.Text))
            {
                MessageBox.Show("Folder Path Error");
                return;
            }

            if(string.IsNullOrEmpty(tbBefore.Text) || string.IsNullOrEmpty(tbAfter.Text))
            {
                MessageBox.Show("Text Error");
                return;
            }

            ChangeTextAtFolder(tbFolderPath.Text, tbBefore.Text, tbAfter.Text);
        }

        private void ChangeTextAtFolder(string path, string beforeText, string afterText)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach(var file in di.GetFiles())
            {
                if (file.Name.Contains(beforeText))
                {
                    file.MoveTo(file.Directory.FullName + "\\" + file.Name.Replace(beforeText, afterText));
                }
            }

            foreach(var directory in di.GetDirectories())
            {
                ChangeTextAtFolder(directory.FullName, beforeText, afterText);
            }

            MessageBox.Show("SUCCESSFUL");

            tbBefore.Text = "";
            tbAfter.Text = "";
            tbFolderPath.Text = "";

            return;
        }
    }
}
