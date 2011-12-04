using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TF2Items.Dialogs
{
    public partial class SteamFolderSelectWindow : Form
    {
        private string ret = "";
        List<string> Items = new List<string>();
        public SteamFolderSelectWindow()
        {
            InitializeComponent();
        }
        public string ShowWindow(List<string> items)
        {
            Items = items;
            ShowDialog();
            return ret;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0) return;
            ret = listBox1.SelectedItems[0].ToString();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0) return;
            ret = listBox1.SelectedItems[0].ToString();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ret = "";
            Close();
        }

        private void SteamFolderSelectWindow_Load(object sender, EventArgs e)
        {
            foreach (string item in Items)
            {
                listBox1.Items.Add(item);
            }
                
        }
    }
}
