using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ValveFormat;


namespace TF2Items.Dialogs
{
    public partial class EditSetsWindow : Form
    {
        private DataNode sets;
        public EditSetsWindow()
        {
            InitializeComponent();
        }

        private DataNode FindSetsNode()
        {
            return MainWindow.itemsParser == null ? null : MainWindow.itemsParser.RootNode.SubNodes.Find(n => n.Key == "item_sets");
        }
        private void EditSetsWindow_Load(object sender, EventArgs e)
        {
            sets = FindSetsNode();
            foreach (DataNode n in sets.SubNodes)
            {
                listSets.Items.Add(n.Key);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var win = new AddSetWindow())
            {
                string[] ret = win.ShowWindow();
                var node = new DataNode(ret[0], sets);
                /*node.SubNodes.Add(new DataNode());
                sets.SubNodes.Add();
                */
            }
        }

    }
}
