using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ValveFormat;


namespace TF2Items.Dialogs
{
    public partial class ReferenceWindow : Form
    {
        private EnglishReference ret;
        private DataNode s;
        private bool Reset = true;
        public ReferenceWindow()
        {
            InitializeComponent();
        }
        public EnglishReference ShowWindow(string label)
        {
            s = MainWindow.FindEnglishTokens();
            if (s == null)
            {
                MessageBox.Show("tf_english.txt hasn't been opened yet!",
                                "TF2 Items Editor",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Close();
                return new EnglishReference();
            }
            txtReference.Text = label;
            ShowDialog();
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var win = new CreateReferenceWindow())
            {
                ret = win.ShowWindow(txtReference.Text);
            }
            if (ret.Label != null)
            {
                if (gridReferences.Rows.Cast<DataGridViewRow>().Any(r => r.Cells[0].Value.ToString() == ret.Label))
                {
                    MessageBox.Show("This label already exists!",
                                    "TF2 Items Editor",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                s.SubNodes.Add(new DataNode(ret.Label, ret.Value));
                txtReference.Text = ret.Label;
                LoadReferences();
            }
        }

        private void ReferenceWindow_Load(object sender, EventArgs e)
        {
            s = MainWindow.FindEnglishTokens();
            LoadReferences();   
        }

        private void ReferenceWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Reset) ret = new EnglishReference();
        }
        private void LoadReferences()
        {
            gridReferences.Rows.Clear();
            var rows = new List<DataGridViewRow>();
            foreach (DataNode n in s.SubNodes)
            {
                if (n.Key.ToLower().Contains(txtReference.Text.ToLower()) || n.Value.ToLower().Contains(txtReference.Text.ToLower()))
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(gridReferences, new[] { n.Key, n.Value });
                    rows.Add(row);
                }
            }
            rows.Sort((x, y) => x.Cells[0].Value.ToString().CompareTo(y.Cells[0].Value.ToString()));
            gridReferences.Rows.AddRange(rows.ToArray());
        }
        private void txtReference_TextChanged(object sender, EventArgs e)
        {
            LoadReferences();
        }

        private void gridReferences_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            ret = new EnglishReference
                      {
                          Label = gridReferences.Rows[e.RowIndex].Cells[0].Value.ToString(),
                          Value = gridReferences.Rows[e.RowIndex].Cells[1].Value.ToString()
                      };
            Reset = false;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset = true;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (gridReferences.SelectedCells.Count == 0) return;
            ret = new EnglishReference
                      {
                          Label = gridReferences.SelectedCells[0].OwningRow.Cells[0].Value.ToString(),
                          Value = gridReferences.SelectedCells[0].OwningRow.Cells[1].Value.ToString()
                      };
            Reset = false;
            Close();
        }
    }
}
