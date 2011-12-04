using System;
using System.Windows.Forms;


namespace TF2Items.Dialogs
{
    public partial class CreateReferenceWindow : Form
    {
        private EnglishReference ret = new EnglishReference();

        public CreateReferenceWindow()
        {
            InitializeComponent();
        }
        public EnglishReference ShowWindow(string label)
        {
            txtLabel.Text = label;
            ShowDialog();
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MainWindow.englishParser
            ret.Label = txtLabel.Text;
            ret.Value = txtValue.Text;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ret.Label = null;
            ret.Value = null;
            Close();
        }
    }
}
