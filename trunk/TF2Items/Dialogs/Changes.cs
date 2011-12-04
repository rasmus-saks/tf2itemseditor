using System;
using System.Net;
using System.Windows.Forms;


namespace TF2Items.Dialogs
{
    public partial class Changes : Form
    {
        public Changes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Changes_Load(object sender, EventArgs e)
        {
            var cl = new WebClient();
            txtChanges.Text = cl.DownloadString(new Uri("http://tf2itemseditor.googlecode.com/svn/trunk/Updater/changes.txt"));
        }
    }
}
