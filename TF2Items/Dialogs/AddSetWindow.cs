using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TF2Items.Dialogs
{
    public partial class AddSetWindow : Form
    {
        private string[] ret = new string[2];
        public AddSetWindow()
        {
            InitializeComponent();
        }
        public string[] ShowWindow()
        {
            ret[0] = "";
            ret[1] = "";
            ShowDialog();
            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ret[0] = txtName.Text;
            ret[1] = txtNameReference.Text;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ret[0] = "";
            ret[1] = "";
            Close();
        }

        private void AddSetWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ret[0] = "";
            ret[1] = "";
        }

        private void btnEditNameReference_Click(object sender, EventArgs e)
        {
            using (var refWindow = new ReferenceWindow())
            {
                var eng = refWindow.ShowWindow("");
                if (eng.Label != null) txtNameReference.Text = "#" + eng.Label;
            }
        }
    }
}
