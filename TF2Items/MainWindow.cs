using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TF2Items.Dialogs;
using TF2Items.Properties;
using ValveFormat;


namespace TF2Items
{
    public partial class MainWindow : Form
    {
        private AttributesWindow attr;
        public static ValveFormatParser itemsParser;
        public static ValveFormatParser englishParser;

        public static int AttribOffsetX;
        public static int AttribOffsetY;
        public static bool Moving;

        public TextBox[] tipBoxes;

        public bool itemSetup;
        public bool wait;

        public MainWindow()
        {
            InitializeComponent();
            tipBoxes = new[] {txtItemName, txtItemTypeName, txtSetName};
        }

        private const int SW_SHOWNOACTIVATE = 4;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOACTIVATE = 0x0010;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,           // window handle
             int hWndInsertAfter,    // placement-order handle
             int X,          // horizontal position
             int Y,          // vertical position
             int cx,         // width
             int cy,         // height
             uint uFlags);       // window positioning flags

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        void ShowInactiveTopmost(Form frm)
        {
            ShowWindow(frm.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST,
            Right + AttribOffsetX, Top + AttribOffsetY, frm.Width, frm.Height,
            SWP_NOACTIVATE);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Settings.Default.Upgrade();
            attr = new AttributesWindow();
            attr.inst = this;
            groupSettings.Enabled = false;
            groupAttributes.Enabled = false;
            Text = "TF2 Items Editor v2 build #" +
                   System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision;

            AttribOffsetX = Width - ClientSize.Width;
            AttribOffsetY = 0;
            string str = "";
            stripTxt.Image = null;
            if (tabControl1.SelectedTab == tabItemsGame)
            {
                str = "File->Open->items_game.txt to open items_game.txt!";
                stripTxt.Image = images.Images["Warning"];
            }
            else if(tabControl1.SelectedTab == tabEnglish)
            {
                str = "File->Open->tf_english.txt to open tf_english.txt!";
                stripTxt.Image = images.Images["Warning"];
            }
            stripTxt.Text = str;
            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                using (var form = new Changes())
                {
                    form.ShowDialog();
                }
            }
        }

        private void attributesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (attributesToolStripMenuItem.Checked)
            {
                //attr.Location = new Point(Right + AttribOffsetX, Top + AttribOffsetY);
                Moving = true;
                ShowInactiveTopmost(attr);
                attr.AttributesWindow_Load(null, null);
                Moving = false;
                if (!attr.Visible)
                {
                    attributesToolStripMenuItem.Checked = false;
                }
                if (itemsParser != null)
                {
                    stripTxt.Text = "Drag and drop attributes to add them. Edit->Attributes to close the attributes window!";
                    stripTxt.Image = images.Images["Info"];
                }
            }
            else
            {
                attr.Hide();
                if (itemsParser != null)
                {
                    stripTxt.Text = "Edit->Attributes to open the attributes window!";
                    stripTxt.Image = images.Images["Info"];
                }
            }
        }

        #region Load files
        private void itemsgametxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ret;
            using (var diag = new OpenFileWindow())
            {
                Cursor = Cursors.WaitCursor;
                ret = diag.ShowWindow("items_game.txt", @"\tf\scripts\items\");
            }
            Cursor = Cursors.Arrow;
            if (!String.IsNullOrEmpty(ret))
            {
                Cursor.Current = Cursors.WaitCursor;
                itemsParser = new ValveFormatParser(ret);
                itemsParser.LoadFile();
                Cursor.Current = Cursors.Arrow;
                if (itemsParser.RootNode.Key != "items_game")
                {
                    MessageBox.Show("That is not items_game.txt.\r\nPlease select items_game.txt!",
                                    "TF2 Items Editor",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    itemsParser = null;
                    return;
                }
                LoadItems();
                stripTxt.Text = "Edit->Attributes to open the attributes window!";
                stripTxt.Image = images.Images["Info"];
            }
        }

        private void tfenglishtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ret = "";
            using (var diag = new OpenFileWindow())
            {
                Cursor = Cursors.WaitCursor;
                ret = diag.ShowWindow("tf_english.txt", @"\tf\resource\");
            }
            Cursor = Cursors.Arrow;
            if (!String.IsNullOrEmpty(ret))
            {
                wait = true;
                Cursor.Current = Cursors.WaitCursor;
                englishParser = new ValveFormatParser(ret);
                englishParser.LoadFile();
                Cursor.Current = Cursors.Arrow;
                if (englishParser.RootNode.Key != "lang")
                {
                    MessageBox.Show("That is not tf_english.txt.\r\nPlease select tf_english.txt!",
                                    "TF2 Items Editor",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    englishParser = null;
                    wait = false;
                    return;
                }
                stripTxt.Text = "";
                stripTxt.Image = null;
                groupTips.Enabled = true;
                comboTipClasses.SelectedIndex = 0;
                wait = false;
            }
        }
        #endregion
        #region Find nodes functions
        
        public DataNode FindItemsNode()
        {
            return itemsParser == null ? null : itemsParser.RootNode.SubNodes.Find(n => n.Key == "items");
        }
        public DataNode FindAttributesNode()
        {
            return itemsParser == null ? null : itemsParser.RootNode.SubNodes.Find(n => n.Key == "attributes");
        }

        private DataNode FindSetsNode()
        {
            return itemsParser == null ? null : itemsParser.RootNode.SubNodes.Find(n => n.Key == "item_sets");
        }

        private DataNode FindItemNode(string name, string key)
        {
            foreach (DataNode n in FindItemsNode().SubNodes)
            {
                foreach (DataNode no in n.SubNodes)
                {
                    if (no.Key == "name" && no.Value != name) break;
                    if (no.Key == key) return no;
                }
            }
            return null;
        }
        private DataNode FindItem(string name)
        {
            foreach (DataNode n in FindItemsNode().SubNodes)
            {
                foreach (DataNode no in n.SubNodes)
                {
                    if (no.Key == "name" && no.Value == name) return n;
                }
            }
            return null;
        }

        private DataNode FindSet(string name)
        {
            foreach (DataNode n in FindSetsNode().SubNodes)
            {
                if (n.Key == name) return n;
            }
            return null;
        }

        public static DataNode FindEnglishTokens()
        {
            return (englishParser == null || englishParser.RootNode.SubNodes == null)
                       ? null
                       : englishParser.RootNode.SubNodes.Find(n => n.Key == "Tokens");
        }
        #endregion
        private void LoadItems()
        {
            //Items
            foreach (DataNode node in FindItemsNode().SubNodes)
            {
                string item = node.SubNodes.Find(n => n.Key == "name").Value;
                comboItems.Items.Add(item);
                comboSetItems.Items.Add(item);
            }
            comboItems.SelectedIndex = 0;
            groupItems.Enabled = true;
            groupSettings.Enabled = true;

            //Sets
            foreach (DataNode node in FindSetsNode().SubNodes)
            {
                comboSets.Items.Add(node.Key);
            }
            comboSets.SelectedIndex = 0;
            groupSets.Enabled = true;

        }

        private void comboItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemSetup = true;
            txtItemName.Text = "";
            txtItemTypeName.Text = "";
            groupAttributes.Enabled = comboItems.SelectedIndex > 31
                && !comboItems.SelectedItem.ToString().Contains("Supply Crate")//Protect breaking "force_gc_to_generate" "1" in attributes of crates and paint cans
                && !comboItems.SelectedItem.ToString().Contains("Winter Crate")
                && !comboItems.SelectedItem.ToString().Contains("Summer Crate")
                && !comboItems.SelectedItem.ToString().Contains("Paint Can");

            string item = comboItems.SelectedItem.ToString();
            DataNode dnode = FindItemNode(item, "item_name");
            if (dnode != null) txtItemName.Text = dnode.Value;
            dnode = FindItemNode(item, "item_type_name");
            if (dnode != null) txtItemTypeName.Text = dnode.Value;
            grid_attribs.Rows.Clear();
            if (FindItemNode(item, "attributes") != null)
            {
                foreach (DataNode node in FindItemNode(item, "attributes").SubNodes)
                {
                    grid_attribs.Rows.Add(new[]
                                              {
                                                  node.Key,
                                                  node.SubNodes.Find(n => n.Key == "attribute_class").Value,
                                                  node.SubNodes.Find(n => n.Key == "value").Value
                                              });
                }
            }
            if (englishParser == null) return;
            List<DataNode> tokens = FindEnglishTokens().SubNodes;
            foreach (TextBox box in tipBoxes)
            {
                DataNode token = tokens.Find(n => n.Key == box.Text.TrimStart(new[] { '#' }));
                if (token != null) tipReference.SetToolTip(box, token.Value);
            }
            itemSetup = false;
        }

        private void btnEditItemName_Click_1(object sender, EventArgs e)
        {
            using (var refWindow = new ReferenceWindow())
            {
                var eng = refWindow.ShowWindow(txtItemName.Text.TrimStart(new[] {'#'}));
                if (eng.Label != null) txtItemName.Text = "#" + eng.Label;
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            if (itemSetup) return;
            DataNode node = FindItemNode(comboItems.SelectedItem.ToString(), "item_name");
            if(node != null) node.Value = txtItemName.Text;
        }

        private void txtItemTypeName_TextChanged(object sender, EventArgs e)
        {
            if (itemSetup) return;
            DataNode node = FindItemNode(comboItems.SelectedItem.ToString(), "item_type_name");
            if(node != null) node.Value = txtItemTypeName.Text;
        }

        private void btnEditItemTypeName_Click_1(object sender, EventArgs e)
        {
            using (var refWindow = new ReferenceWindow())
            {
                var eng = refWindow.ShowWindow(txtItemTypeName.Text.TrimStart(new[] {'#'}));
                if (eng.Label != null) txtItemTypeName.Text = "#" + eng.Label;
            }
        }

        private void btnEditSets_Click(object sender, EventArgs e)
        {
            using (var win = new EditSetsWindow())
            {
                win.ShowDialog();
            }
        }

        private void comboSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataNode setNode = FindSetsNode().SubNodes.Find(n => n.Key == comboSets.SelectedItem.ToString());
            txtSetName.Text = setNode.SubNodes.Find(n => n.Key == "name").Value;
            DataNode itemsNode = setNode.SubNodes.Find(n => n.Key == "items");
            listSetItems.Items.Clear();
            foreach (DataNode node in itemsNode.SubNodes)
            {
                listSetItems.Items.Add(node.Key);
            }
            gridSetAttributes.Rows.Clear();
            DataNode attribsNode = setNode.SubNodes.Find(n => n.Key == "attributes");
            foreach (DataNode node in attribsNode.SubNodes)
            {
                gridSetAttributes.Rows.Add(new[]
                                               {
                                                   node.Key, 
                                                   node.SubNodes.Find(n => n.Key == "attribute_class").Value,
                                                   node.SubNodes.Find(n => n.Key == "value").Value
                                               });
            }
            if (englishParser == null) return;
            List<DataNode> tokens = FindEnglishTokens().SubNodes;
            foreach (TextBox box in tipBoxes)
            {
                DataNode token = tokens.Find(n => n.Key == box.Text.TrimStart(new[] { '#' }));
                if (token != null) tipReference.SetToolTip(box, token.Value);
            }

        }

        private void btnEditSetName_Click(object sender, EventArgs e)
        {
            using (var refWindow = new ReferenceWindow())
            {
                var eng = refWindow.ShowWindow(txtSetName.Text.TrimStart(new[] {'#'}));
                if (eng.Label != null) txtSetName.Text = "#" + eng.Label;
            }
        }

        private void txtSetName_TextChanged(object sender, EventArgs e)
        {
            FindSetsNode().SubNodes.Find(n => n.Key == comboSets.SelectedItem.ToString()).SubNodes.Find(
                n => n.Key == "name").Value = txtSetName.Text;

        }

        private void gridSetAttributes_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataNode attribsNode = FindSetsNode().SubNodes.Find(n => n.Key == comboSets.SelectedItem.ToString()).SubNodes.Find(n => n.Key == "attributes");
             attribsNode.SubNodes.Clear();
             foreach (DataGridViewRow r in gridSetAttributes.Rows)
             {
                 var node = new DataNode(r.Cells[0].Value.ToString());
                 node.SubNodes.Add(new DataNode("attribute_class", r.Cells[1].Value.ToString(), node));
                 node.SubNodes.Add(new DataNode("value", r.Cells[2].Value.ToString(), node));
                 attribsNode.SubNodes.Add(node);
             }
        }

        private bool CheckSteamappsFolder()
        {
            if (Settings.Default.SteamFolder == null || Settings.Default.SteamFolder.ToString() == "")
            {
            showDia:
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(folderBrowserDialog1.SelectedPath + @"\team fortress 2 content.gcf"))
                    {
                        var res = MessageBox.Show(
                            "team fortress 2 content.gcf could not be found in that folder!\r\nPlease select your steamapps folder!",
                            "TF2 Items Editor",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error);
                        if (res == DialogResult.OK) goto showDia;
                        return false;
                    }
                    Settings.Default.SteamFolder = folderBrowserDialog1.SelectedPath;
                    Settings.Default.Save();
                }
                else return false;

            }
            string[] dirs = Directory.GetDirectories(Settings.Default.SteamFolder);
            if (string.IsNullOrEmpty(Settings.Default.SteamUser))
            {
                var match = new List<string>();
                foreach (string _dir in dirs)
                {
                    string dir = (new DirectoryInfo(_dir)).Name;
                    if (dir != "common" && dir != "media" && dir != "SourceMods") match.Add(dir);
                }
                if (match.Count > 1)
                {
                    using (var win = new SteamFolderSelectWindow())
                    {
                        string ret = win.ShowWindow(match);
                        if (ret == "") return false;
                        Settings.Default.SteamUser = ret;
                        Settings.Default.Save();
                    }
                }
                else if (match.Count == 1)
                {
                    Settings.Default.SteamUser = match[0];
                    Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("No user folders found in steamapps!",
                                    "TF2 Items Editor",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void itemsgametxtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (itemsParser == null)
            {
                MessageBox.Show("items_game.txt hasn't been opened yet!",
                                "TF2 Items Editor",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            if (!CheckSteamappsFolder()) return;
            string p = Settings.Default.SteamFolder + @"\" + Settings.Default.SteamUser +
                           @"\team fortress 2\tf\scripts\items\items_game.txt";
            Cursor.Current = Cursors.WaitCursor;
            itemsParser.SaveFile(p);
            Cursor.Current = Cursors.Arrow;
        }

        private void tfenglishtxtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (englishParser == null)
            {
                MessageBox.Show("tf_english.txt hasn't been opened yet!",
                                "TF2 Items Editor",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            if (!CheckSteamappsFolder()) return;
            string p = Settings.Default.SteamFolder + @"\" + Settings.Default.SteamUser +
                           @"\team fortress 2\tf\resource\tf_english.txt";
            Cursor.Current = Cursors.WaitCursor;
            englishParser.SaveFile(p, false); //tf_english.txt somehow doesn't like indenting :/
            Cursor.Current = Cursors.Arrow;
        }

        private void itemsgametxtToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (itemsParser == null)
            {
                MessageBox.Show("items_game.txt hasn't been opened yet!",
                                "TF2 Items Editor",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            Cursor.Current = Cursors.WaitCursor;
            File.Delete(saveFileDialog1.FileName);
            itemsParser.SaveFile(saveFileDialog1.FileName);
            Cursor.Current = Cursors.Arrow;
        }

        private void tfenglishtxtToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (englishParser == null)
            {
                MessageBox.Show("tf_english.txt hasn't been opened yet!",
                                "TF2 Items Editor",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            Cursor.Current = Cursors.WaitCursor;
            File.Delete(saveFileDialog1.FileName);
            englishParser.SaveFile(saveFileDialog1.FileName, false);
            Cursor.Current = Cursors.Arrow;
        }

        private void grid_attribs_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.StringFormat) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void grid_attribs_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                DataNode attribsNode = FindAttributesNode();
                DataNode attrib = null;
                foreach (DataNode dNode in attribsNode.SubNodes)
                {
                    string data = e.Data.GetData(DataFormats.StringFormat).ToString();
                    if (dNode.SubNodes.Find(n => n.Key == "name").Value == data) attrib = dNode;
                }

                if (attrib == null) return;
                string aClass = attrib.SubNodes.Find(n => n.Key == "attribute_class").Value;
                grid_attribs.Rows.Add(new[] { attrib.SubNodes.Find(n => n.Key == "name").Value, aClass, "0" });
                DataNode item = FindItem(comboItems.SelectedItem.ToString());
                DataNode itemAttribs = new DataNode("attributes", item);
                bool found = false;
                foreach (DataNode no in item.SubNodes)
                {
                    if (no.Key == "attributes")
                    {
                        itemAttribs = no;
                        found = true;
                    }
                }
                var node = new DataNode(attrib.SubNodes.Find(n => n.Key == "name").Value, itemAttribs);
                node.SubNodes.Add(new DataNode("attribute_class", aClass, node));
                node.SubNodes.Add(new DataNode("value", "0", node));
                itemAttribs.SubNodes.Add(node);
                if (!found) item.SubNodes.Add(itemAttribs);
            }
        }

        private void gridSetAttributes_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.StringFormat) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void gridSetAttributes_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                DataNode attribsNode = FindAttributesNode();
                DataNode attrib = null;
                foreach (DataNode dNode in attribsNode.SubNodes)
                {
                    string data = e.Data.GetData(DataFormats.StringFormat).ToString();
                    if (dNode.SubNodes.Find(n => n.Key == "name").Value == data) attrib = dNode;
                }

                if (attrib == null) return;
                string aClass = attrib.SubNodes.Find(n => n.Key == "attribute_class").Value;
                gridSetAttributes.Rows.Add(new[] { attrib.SubNodes.Find(n => n.Key == "name").Value, aClass, "0" });
                DataNode item = FindSet(comboSets.SelectedItem.ToString());
                var itemAttribs = new DataNode("attributes", item);
                bool found = false;
                foreach (DataNode no in item.SubNodes)
                {
                    if (no.Key == "attributes")
                    {
                        itemAttribs = no;
                        found = true;
                    }
                }
                var node = new DataNode(attrib.SubNodes.Find(n => n.Key == "name").Value, itemAttribs);
                node.SubNodes.Add(new DataNode("attribute_class", aClass, node));
                node.SubNodes.Add(new DataNode("value", "0", node));
                itemAttribs.SubNodes.Add(node);
                if (!found) item.SubNodes.Add(itemAttribs);
            }
        }

        private void gridSetAttributes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (comboSets.SelectedItem == null) return;
            DataNode set = FindSet(comboSets.SelectedItem.ToString());
            DataNode attrNode = set.SubNodes.Find(n => n.Key == "attributes");
            attrNode.SubNodes.Clear();
            foreach (DataGridViewRow r in gridSetAttributes.Rows)
            {
                var main = new DataNode(r.Cells[0].Value.ToString(), attrNode);
                main.SubNodes.Add(new DataNode("attribute_class", r.Cells[1].Value.ToString(), main));
                main.SubNodes.Add(new DataNode("value", r.Cells[2].Value.ToString(), main));
                attrNode.SubNodes.Add(main);
            }
        }

        private void grid_attribs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (comboItems.SelectedItem == null) return;
            DataNode item = FindItem(comboItems.SelectedItem.ToString());
            DataNode attrNode = item.SubNodes.Find(n => n.Key == "attributes");
            attrNode.SubNodes.Clear();
            foreach (DataGridViewRow r in grid_attribs.Rows)
            {
                var main = new DataNode(r.Cells[0].Value.ToString(), attrNode);
                main.SubNodes.Add(new DataNode("attribute_class", r.Cells[1].Value.ToString(), main));
                main.SubNodes.Add(new DataNode("value", r.Cells[2].Value.ToString(), main));
                attrNode.SubNodes.Add(main);
                
            }
        }

        private void MainWindow_Move(object sender, EventArgs e)
        {
            if (attributesToolStripMenuItem.Checked)
            {
                Moving = true;
                attr.Location = new Point(Right + AttribOffsetX, Top + AttribOffsetY);
                Moving = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var w = new SettingsWindow())
            {
                w.ShowDialog();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            stripTxt.Image = null;
            if (tabControl1.SelectedTab == tabItemsGame) 
            {
                if (itemsParser == null)
                {
                    str = "File->Open->items_game.txt to open items_game.txt!";
                    stripTxt.Image = images.Images["Warning"];
                }
                else if(!attr.Visible)
                {
                    str = "Edit->Attributes to open the attributes window!";
                    stripTxt.Image = images.Images["Info"];
                }
                else
                {
                    stripTxt.Text = "Drag and drop attributes to add them. Edit->Attributes to close the attributes window!";
                    stripTxt.Image = images.Images["Info"];
                }
            }
            else if (tabControl1.SelectedTab == tabEnglish && englishParser == null)
            {
                str = "File->Open->tf_english.txt to open tf_english.txt!";
                stripTxt.Image = images.Images["Warning"];
            }
            stripTxt.Text = str;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var win = new AboutWindow())
            {
                win.ShowDialog();
            }
        }

        private void btnAddSetItem_Click(object sender, EventArgs e)
        {
            DataNode setNode = FindSet(comboSets.SelectedItem.ToString());
            var itemsNode = setNode.SubNodes.Find(n => n.Key == "items");
            itemsNode.SubNodes.Add(new DataNode(comboSetItems.SelectedItem.ToString(), "1", itemsNode));
            listSetItems.Items.Add(comboSetItems.SelectedItem.ToString());
        }

        private void btnRemoveSetItem_Click(object sender, EventArgs e)
        {
            if (listSetItems.SelectedItem == null) return;
            DataNode setNode = FindSet(comboSets.SelectedItem.ToString());
            var itemsNode = setNode.SubNodes.Find(n => n.Key == "items");
            itemsNode.SubNodes.RemoveAt(itemsNode.SubNodes.FindIndex(n => n.Key == listSetItems.SelectedItem.ToString()));
            listSetItems.Items.RemoveAt(listSetItems.SelectedIndex);
        }

        private void comboTipClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            wait = true;
            int sClass = comboTipClasses.SelectedIndex + 1;
            txtTips.Clear();
            foreach (DataNode node in FindEnglishTokens().SubNodes)
            {
                var reg = new Regex(@"Tip_"+sClass+@"_\d+");
                if (reg.IsMatch(node.Key))
                {
                    txtTips.AppendText(node.Value + "\r\n");
                }
            }
            wait = false;
        }

        private void txtTips_TextChanged(object sender, EventArgs e)
        {
            if (wait) return;
            int sClass = comboTipClasses.SelectedIndex + 1;
            DataNode eNode = FindEnglishTokens();
            foreach (DataNode node in eNode.SubNodes.ToArray())
            {
                var reg = new Regex(@"Tip_" + sClass + @"_\d+");
                if (reg.IsMatch(node.Key) || node.Key == "Tip_" + sClass + "_Count")
                {
                    eNode.SubNodes.Remove(node);
                }
            }
            eNode.SubNodes.Add(new DataNode("Tip_" + sClass + "_Count", txtTips.Lines.Length.ToString(), eNode));
            int i = 1;
            foreach (string line in txtTips.Lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                eNode.SubNodes.Add(new DataNode("Tip_" + sClass + "_" + i, line, eNode));
                i++;
            }
        }
    }
}

