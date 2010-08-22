using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
//using Windows7.DesktopIntegration;
//using Windows7.DesktopIntegration.WindowsForms;

namespace TF2Items
{
    public partial class Form1 : Form
    {
        const int number_of_items = 220;
        string[] name = new string[number_of_items];
        string[] item_class = new string[number_of_items];
        string[] craft_class = new string[number_of_items];
        string[] item_type_name = new string[number_of_items];
        string[] item_name = new string[number_of_items];
        string[] item_slot = new string[number_of_items];
        string[] item_quality = new string[number_of_items];
        string[] baseitem = new string[number_of_items];
        string[] min_ilevel = new string[number_of_items];
        string[] max_ilevel = new string[number_of_items];
        string[] image_inventory = new string[number_of_items];
        string[] image_inventory_size_w = new string[number_of_items];
        string[] image_inventory_size_h = new string[number_of_items];
        string[] model_player = new string[number_of_items];
        string[] attach_to_hands = new string[number_of_items];
        int[,] used_by_classes = new int[number_of_items, 9];

        const int number_of_attribs = 150;
        string[] attrib_name = new string[number_of_attribs];
        string[] attrib_class = new string[number_of_attribs];
        string[] attrib_aname = new string[number_of_attribs];
        double[] attrib_minvalue = new double[number_of_attribs];

        string[,] item_attribs = new string[number_of_items, number_of_attribs];
        double[,] item_attribs_value = new double[number_of_items, number_of_attribs];

        #region Clipboard vars
        string c_item_class;
        string c_item_type_name;
        string c_item_name;
        string c_item_slot;
        string c_item_quality;
        string c_baseitem;
        string c_min_ilevel;
        string c_max_ilevel;
        string c_image_inventory;
        string c_image_inventory_size_w;
        string c_image_inventory_size_h;
        string c_model_player;
        string c_attach_to_hands;
        int[] c_used_by_classes = new int[9];

        string[] c_item_attribs = new string[number_of_attribs];
        double[] c_item_attribs_value = new double[number_of_attribs];
        #endregion

        bool firstSetup = false;
        double percent;

        string fileName;

        bool[] saved = new bool[14];

        public Form1()
        {
            InitializeComponent();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            filediagOpen.Title = "Select items_game.txt or team fortress 2 content.gcf";
            
            filediagOpen.Filter = "items_game.txt|items_game.txt|Team Fortress 2 content|team fortress 2 content.gcf";
            filediagOpen.RestoreDirectory = false;
            DialogResult result =  filediagOpen.ShowDialog();
            if(result != DialogResult.OK) return;
            if(filediagOpen.FileName.Contains(".gcf"))
            {
                Process extract = new Process();
                extract.StartInfo.FileName = "HLExtract.exe";
                extract.StartInfo.Arguments = "-c -p \"" + filediagOpen.FileName + "\" -e \"root\\tf\\scripts\\items\\items_game.txt\"";
                if (!File.Exists("HLExtract.exe"))
                {
                    MessageBox.Show("HLExtract.exe is missing from the program folder!", "Who send all these babies to fight?!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists("HLLib.dll"))
                {
                    MessageBox.Show("HLLib.dll is missing from the program folder!", "Who send all these babies to fight?!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    
                    //extract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    extract.Start();
                   /* System.IO.WaitForChangedResult resultt;
                    string directory = fileName.Replace("items_game.txt", "");
                    System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher(directory, "items_game.txt");
                    resultt = watcher.WaitForChanged(System.IO.WatcherChangeTypes.All);*/
                }
                catch
                {
                    MessageBox.Show("Something went wrong when extracting :(", "Who send all these babies to fight?!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Extracted successfully to " + filediagOpen.FileName.Replace("team fortress 2 content.gcf", "items_game.txt") + "\nAfter saving, remember to place this in your \\steamapps\\username\\team fortress 2\\tf\\scripts\\items\\ folder!", "Listen up!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fileName = filediagOpen.FileName.Replace("team fortress 2 content.gcf", "items_game.txt");
                extract.Kill();
                extract.Close();
            }
            else fileName = filediagOpen.FileName;
            firstSetup = true;
            comboName.Items.Clear();
            grid_attribs.Rows.Clear();
            list_all_attribs.Items.Clear();
            list_available_classes.Items.Clear();
            list_used_by_classes.Items.Clear();
            txt_attach_to_hands.Clear();
            txt_baseitem.Clear();
            txt_craft_class.Clear();
            txt_image_inventory.Clear();
            txt_image_inventory_size_h.Clear();
            txt_image_inventory_size_w.Clear();
            txt_item_class.Clear();
            txt_item_name.Clear();
            txt_item_quality.Clear();
            txt_item_slot.Clear();
            txt_item_type_name.Clear();
            txt_max_ilevel.Clear();
            txt_min_ilevel.Clear();
            txt_model_player.Clear();
            comboName.SelectedIndex = -1;
            comboName.Text = "Select an item";
            for (int jj = 0; jj < number_of_items; jj++)
            {
                for (int kk = 0; kk < 9; kk++)
                {
                    used_by_classes[jj, kk] = 0;
                }
            }
            using (StreamReader sReader = new StreamReader(fileName))
            {
                StreamReader file = null;
                string line;
                string current = "";
                int i = 0;
                file = new StreamReader(fileName);
                percent = file.BaseStream.Length/(double)100;
                bool usedby = false;
                bool inAttribs = false;
                int itemAtr = -1;
                string lastline = "";
                int aN = 0;
                while ((line = file.ReadLine()) != null)
                {
                    if (line == null) continue;
                    progressReading.Value = (int)file.BaseStream.Position / (int)percent;
                   // if (Osinfo.MajorVersion.ToString() + "." + Osinfo.MinorVersion.ToString() == "6.1") progressReading.SetTaskbarProgress(); //Only show progress bar on the taskbar if using Windows 7
                    if (!inAttribs)
                    {
                        if (line.Contains("\"name\"")) //Parsing new item
                        {
                            string tmp = line.Replace("\"name\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            current = tmp;
                            comboName.Items.Insert(i, tmp + "\r\n");
                            i++;
                            aN = 0;
                        }
                        if (line.Contains("\"attributes\""))
                        {
                            itemAtr++;
                            continue;
                        }
                        if (line.Contains("{") && itemAtr > 0) itemAtr++;
                        if (line.Contains("}") && itemAtr > 0) itemAtr--;
                        if (line.Contains("\"attribute_class\"") && itemAtr > 0) continue;
                        if (line.Contains("}") && lastline.Contains("}")) itemAtr = 0;
                        if (line.Contains("\"value\"") && itemAtr > 0)
                        {
                            item_attribs_value[i - 1, aN] = Converter.ToDouble(line.Replace("\"", "").Replace("	", "").Replace("value", ""));
                            aN++;
                        }
                        if (line.Contains("\"force_gc_to_generate\"") && itemAtr > 0) continue;
                        if (line.Contains("\"use_custom_logic\"") && itemAtr > 0) continue;
                        if (line.Contains("\"") && itemAtr > 0 && !line.Contains("\"value\""))
                        {
                            item_attribs[i-1, aN] = line.Replace("\"", "").Replace("	", "");
                        }

                        #region Assign arrays
                        if (line.Contains("\"item_class\""))
                        {
                            string tmp = line.Replace("\"item_class\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            item_class[i - 1] = tmp;
                        }
                        if (line.Contains("\"craft_class\""))
                        {
                            string tmp = line.Replace("\"craft_class\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            craft_class[i - 1] = tmp;
                        }
                        if (line.Contains("\"item_type_name\""))
                        {
                            string tmp = line.Replace("\"item_type_name\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            item_type_name[i - 1] = tmp;
                        }
                        if (line.Contains("\"item_name\""))
                        {
                            string tmp = line.Replace("\"item_name\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            item_name[i - 1] = tmp;
                        }
                        if (line.Contains("\"item_slot\""))
                        {
                            string tmp = line.Replace("\"item_slot\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            item_slot[i - 1] = tmp;
                        }
                        if (line.Contains("\"item_quality\""))
                        {
                            string tmp = line.Replace("\"item_quality\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            item_quality[i - 1] = tmp;
                        }
                        if (line.Contains("\"baseitem\""))
                        {
                            string tmp = line.Replace("\"baseitem\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            baseitem[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"min_ilevel\""))
                        {
                            string tmp = line.Replace("\"min_ilevel\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            min_ilevel[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"max_ilevel\""))
                        {
                            string tmp = line.Replace("\"max_ilevel\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            max_ilevel[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"image_inventory\""))
                        {
                            string tmp = line.Replace("\"image_inventory\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            image_inventory[i - 1] = tmp;
                        }
                        if (line.Contains("\"image_inventory_size_w\""))
                        {
                            string tmp = line.Replace("\"image_inventory_size_w\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            image_inventory_size_w[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"image_inventory_size_h\""))
                        {
                            string tmp = line.Replace("\"image_inventory_size_h\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            image_inventory_size_h[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"model_player\""))
                        {
                            string tmp = line.Replace("\"model_player\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            model_player[i - 1] = tmp;
                        }
                        if (line.Contains("\"attach_to_hands\""))
                        {
                            string tmp = line.Replace("\"attach_to_hands\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            attach_to_hands[i - 1] = tmp.Replace(" ", "");
                        }
                        #endregion

                        if (line.Contains("\"used_by_classes\"")) usedby = true;
                        if (line.Contains("}") && usedby == true) usedby = false;
                        if (line.Contains("\"1\"") && usedby == true)
                        {
                            string tmp = line.Replace("\"1\"", "");
                            int res = 0;
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            tmp = tmp.Replace(" ", "");
                            #region Classes switch
                            switch (tmp)
                            {
                                case "scout":
                                    res = 0;
                                    break;
                                case "soldier":
                                    res = 1;
                                    break;
                                case "pyro":
                                    res = 2;
                                    break;
                                case "demoman":
                                    res = 3;
                                    break;
                                case "heavy":
                                    res = 4;
                                    break;
                                case "engineer":
                                    res = 5;
                                    break;
                                case "medic":
                                    res = 6;
                                    break;
                                case "sniper":
                                    res = 7;
                                    break;
                                case "spy":
                                    res = 8;
                                    break;
                                default:
                                    //MessageBox.Show("Invalid class in " + current + "/used_by_classes! Line = " + line);
                                    res = 0;
                                    break;
                            }
                            #endregion
                            used_by_classes[i - 1, res] = 1;
                        }
                    }
                    else
                    {
                        if (line.Contains("\"name\""))
                        {
                            string tmp = line.Replace("\"name\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            current = tmp;
                            attrib_name[i] = tmp;
                            list_all_attribs.Items.Add(tmp);
                            i++;
                        }
                        else if (line.Contains("\"attribute_class\""))
                        {
                            attrib_class[i - 1] = line.Replace("\"attribute_class\"", "").Replace("\"", "").Replace("\t", "");
                        }
                        else if (line.Contains("\"attribute_name\""))
                        {
                            attrib_aname[i - 1] = line.Replace("\"attribute_name\"", "").Replace("\"", "").Replace("\t", "");
                        }
                        else if (line.Contains("\"attribute_name\""))
                        {
                            attrib_minvalue[i - 1] = Converter.ToDouble(line.Replace("\"min_value\"", "").Replace("\"", "").Replace("\t", ""));
                        }
                    }
                    if (current.Contains("Slot Token - Head") && line.Contains("slot_token_id"))
                    {
                        inAttribs = true;
                        i = 0;
                    }
                    lastline = line;
                }
                file.Close();
                sReader.Close();
                attrib_aname[113] = "mod mini-crit airborne"; //Somehow the mini-crit airborne doesn't have attribute_name, here's a workaround
                comboName.SelectedIndex = comboName.SelectedIndex;
                progressReading.Value = 100;
                //if(Osinfo.MajorVersion.ToString() + "." + Osinfo.MinorVersion.ToString() == "6.1") Windows7.DesktopIntegration.Windows7Taskbar.SetProgressState(this.Handle, Windows7Taskbar.ThumbnailProgressState.NoProgress);
            }
            firstSetup = false;
            comboName.SelectedIndex = 0;
        }
        /// <summary>Returns a TF2 class name based on the classid</summary>
        /// <param name="classid">The class id, starting from 0</param>
        /// <returns>The class name, lowercase or "Invalid classid!" on error</returns>
        static public string GetClassName(int classid)
        {
            switch (classid)
            {
                case 0: return "scout";
                case 1: return "soldier";
                case 2: return "pyro";
                case 3: return "demoman";
                case 4: return "heavy";
                case 5: return "engineer";
                case 6: return "medic";
                case 7: return "sniper";
                case 8: return "spy";
                default: return "Invalid classid!";
            }
        }
        /// <summary>Returns a TF2 class id based on the name</summary>
        /// <param name="classname">The class name, lowercase</param>
        /// <returns>The class id, starting from 0 or -1 on error</returns>
        static public int GetClassId(string classname)
        {
            switch (classname)
            {
                case "scout": return 0;
                case "soldier": return 1;
                case "pyro": return 2;
                case "demoman": return 3;
                case "heavy": return 4;
                case "engineer": return 5;
                case "medic": return 6;
                case "sniper": return 7;
                case "spy": return 8;
                default: return -1;
            }
        }
        /// <summary>
        /// Returns a bool indicating if the item has attributes
        /// </summary>
        /// <param name="itemid">The id of the item</param>
        public bool DoesItemHaveAttribs(int itemid)
        {
            if (itemid < 0 || itemid >= number_of_items) return false;
            for (int i = 0; i < number_of_attribs; i++)
            {
                if (item_attribs[itemid, i] != null) return true;
            }
            return false;
        }
        private static Regex _isNumber = new Regex(@"^\d+$");

        public static bool isNumeric(string theValue)
        {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }

        private string GetAttribClass(string attribname)
        {
            for (int k = 0; k < number_of_attribs; k++)
            {
                if (attrib_name[k] == attribname) return attrib_class[k];
            }
            return "";
        }
        private string GetAttribAname(string attribname)
        {
            for (int k = 0; k < number_of_attribs; k++)
            {
                if (attrib_name[k] == attribname) return attrib_aname[k];
            }
            return "";
        }
        public string ReturnSettingVal(int item, int sID)
        {
            if (item < 0 || item >= number_of_items || sID < 0 || sID > 13) return null;
            switch (sID)
            {
                case 0: return item_class[item];
                case 1: return craft_class[item];
                case 2: return item_type_name[item];
                case 3: return item_name[item];
                case 4: return item_slot[item];
                case 5: return item_quality[item];
                case 6: return baseitem[item];
                case 7: return min_ilevel[item];
                case 8: return max_ilevel[item];
                case 9: return image_inventory[item];
                case 10: return image_inventory_size_w[item];
                case 11: return image_inventory_size_h[item];
                case 12: return model_player[item];
                case 13: return attach_to_hands[item];
            }
            return "";
        }
        public string ReturnSettingStr(int sID)
        {
            if (sID < 0 || sID > 13) return null;
            switch (sID)
            {
                case 0: return "item_class";
                case 1: return "craft_class";
                case 2: return "item_type_name";
                case 3: return "item_name";
                case 4: return "item_slot";
                case 5: return "item_quality";
                case 6: return "baseitem";
                case 7: return "min_ilevel";
                case 8: return "max_ilevel";
                case 9: return "image_inventory";
                case 10: return "image_inventory_size_w";
                case 11: return "image_inventory_size_h";
                case 12: return "model_player";
                case 13: return "attach_to_hands";
            }

            return "";
        }
        private void comboName_SelectedIndexChanged(object sender, EventArgs e) //When user selects an item
        {
            if (comboName.SelectedIndex == -1) return;
            firstSetup = true;

            txt_item_class.Text = item_class[comboName.SelectedIndex];
            txt_craft_class.Text = craft_class[comboName.SelectedIndex];
            txt_item_type_name.Text = item_type_name[comboName.SelectedIndex];
            txt_item_name.Text = item_name[comboName.SelectedIndex];
            txt_item_slot.Text = item_slot[comboName.SelectedIndex];
            txt_item_quality.Text = item_quality[comboName.SelectedIndex];
            txt_baseitem.Text = baseitem[comboName.SelectedIndex];
            txt_min_ilevel.Text = min_ilevel[comboName.SelectedIndex];
            txt_max_ilevel.Text = max_ilevel[comboName.SelectedIndex];
            txt_image_inventory.Text = image_inventory[comboName.SelectedIndex];
            txt_image_inventory_size_w.Text = image_inventory_size_w[comboName.SelectedIndex];
            txt_image_inventory_size_h.Text = image_inventory_size_h[comboName.SelectedIndex];
            txt_model_player.Text = model_player[comboName.SelectedIndex];
            txt_attach_to_hands.Text = attach_to_hands[comboName.SelectedIndex];

            txt_item_class.Enabled = comboName.SelectedIndex > 30;
            txt_craft_class.Enabled = comboName.SelectedIndex > 30;
            txt_item_type_name.Enabled = comboName.SelectedIndex > 30;
            txt_item_name.Enabled = comboName.SelectedIndex > 30;
            txt_item_slot.Enabled = comboName.SelectedIndex > 30;
            txt_item_quality.Enabled = comboName.SelectedIndex > 30;
            txt_baseitem.Enabled = comboName.SelectedIndex > 30;
            txt_min_ilevel.Enabled = comboName.SelectedIndex > 30;
            txt_max_ilevel.Enabled = comboName.SelectedIndex > 30;
            txt_image_inventory.Enabled = comboName.SelectedIndex > 30;
            txt_image_inventory_size_w.Enabled = comboName.SelectedIndex > 30;
            txt_image_inventory_size_h.Enabled = comboName.SelectedIndex > 30;
            txt_model_player.Enabled = comboName.SelectedIndex > 30;
            txt_attach_to_hands.Enabled = comboName.SelectedIndex > 30;
            list_all_attribs.Enabled = comboName.SelectedIndex > 30;
            list_used_by_classes.Enabled = comboName.SelectedIndex > 30;
            list_available_classes.Enabled = comboName.SelectedIndex > 30;
            move_left_btn.Enabled = comboName.SelectedIndex > 30;
            move_right_btn.Enabled = comboName.SelectedIndex > 30;
            grid_attribs.Enabled = comboName.SelectedIndex > 30;

            list_used_by_classes.Items.Clear();
            list_available_classes.Items.Clear();

            for (int i = 0; i < 9; i++)
            {
                if (used_by_classes[comboName.SelectedIndex, i] == 1)
                    list_used_by_classes.Items.Add(GetClassName(i));
                else
                    list_available_classes.Items.Add(GetClassName(i));
            }
            int n = 0;
            int c = 0;
            grid_attribs.Rows.Clear();
            for (int j = 0; j < number_of_attribs; j++)
            {
                if (item_attribs[comboName.SelectedIndex, j] != null)
                {
                    n = grid_attribs.Rows.Add();
                    grid_attribs.Rows[n].Cells[0].Value = item_attribs[comboName.SelectedIndex, j];
                    grid_attribs.Rows[n].Cells[1].Value = GetAttribClass(item_attribs[comboName.SelectedIndex, j]);
                    grid_attribs.Rows[n].Cells[2].Value = item_attribs_value[comboName.SelectedIndex, j];
                    if(!item_attribs[comboName.SelectedIndex, j].Contains("custom employee number")) c++;
                }
            }
            //list_all_attribs.Enabled = c != 0;
            firstSetup = false;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (list_available_classes.SelectedItem == null) return;
            list_used_by_classes.Items.Add(list_available_classes.SelectedItem);
            list_available_classes.Items.Remove(list_available_classes.SelectedItem);
            for (int i = 0; i < 9; i++)
            {
                if (list_used_by_classes.Items.Contains(GetClassName(i))) used_by_classes[comboName.SelectedIndex, i] = 1;
                else used_by_classes[comboName.SelectedIndex, i] = 0;
            }
        }

        private void move_right_btn_Click(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (list_used_by_classes.SelectedItem == null) return;
            list_available_classes.Items.Add(list_used_by_classes.SelectedItem);
            list_used_by_classes.Items.Remove(list_used_by_classes.SelectedItem);
            for (int i = 0; i < 9; i++)
            {
                if (list_used_by_classes.Items.Contains(GetClassName(i))) used_by_classes[comboName.SelectedIndex, i] = 1;
                else used_by_classes[comboName.SelectedIndex, i] = 0;
            }
        }
        
        private void list_all_attribs_DoubleClick(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            int n = grid_attribs.Rows.Add();
            grid_attribs.Rows[n].Cells[0].Value = list_all_attribs.SelectedItem.ToString();
            grid_attribs.Rows[n].Cells[1].Value = GetAttribClass(list_all_attribs.SelectedItem.ToString());
            item_attribs[comboName.SelectedIndex, n] = list_all_attribs.SelectedItem.ToString();
        }

        #region Save textboxes

        private void txt_item_class_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            item_class[comboName.SelectedIndex] = txt_item_class.Text;
        }

        private void txt_craft_class_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            craft_class[comboName.SelectedIndex] = txt_craft_class.Text;
        }

        private void txt_item_type_name_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            item_type_name[comboName.SelectedIndex] = txt_item_class.Text;
        }

        private void txt_item_name_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            item_name[comboName.SelectedIndex] = txt_item_name.Text;
        }

        private void txt_item_slot_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            item_slot[comboName.SelectedIndex] = txt_item_slot.Text;
        }

        private void txt_item_quality_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            item_quality[comboName.SelectedIndex] = txt_item_quality.Text;
        }

        private void txt_baseitem_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            baseitem[comboName.SelectedIndex] = txt_baseitem.Text;
        }

        private void txt_min_ilevel_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            min_ilevel[comboName.SelectedIndex] = txt_min_ilevel.Text;
        }

        private void txt_max_ilevel_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            max_ilevel[comboName.SelectedIndex] = txt_max_ilevel.Text;
        }

        private void txt_image_inventory_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            image_inventory[comboName.SelectedIndex] = txt_image_inventory.Text;
        }

        private void txt_image_inventory_size_w_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            image_inventory_size_w[comboName.SelectedIndex] = txt_image_inventory_size_w.Text;
        }

        private void txt_image_inventory_size_h_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            image_inventory_size_h[comboName.SelectedIndex] = txt_image_inventory_size_h.Text;
        }

        private void txt_model_player_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            model_player[comboName.SelectedIndex] = txt_model_player.Text;
        }

        private void txt_attach_to_hands_TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            attach_to_hands[comboName.SelectedIndex] = txt_attach_to_hands.Text;
        }
        #endregion
        
        private void grid_attribs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (e == null) return;
            int i = 0;
            if (e.ColumnIndex == 0 || firstSetup) return;
            foreach (DataGridViewRow roo in grid_attribs.Rows)
            {
                if (roo.Cells[2].Value == null) continue;
                item_attribs_value[comboName.SelectedIndex, i] = Converter.ToDouble(roo.Cells[2].Value.ToString());
                item_attribs[comboName.SelectedIndex, i] = roo.Cells[0].Value.ToString();
                i++;
            }
        }
        private void grid_attribs_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (e == null || firstSetup) return;
            int i = 0;
            item_attribs[comboName.SelectedIndex, e.RowIndex] = null;
            item_attribs_value[comboName.SelectedIndex, e.RowIndex] = 0;
            foreach (DataGridViewRow roo in grid_attribs.Rows)
            {
                if (roo.Index == e.RowIndex) continue;
                item_attribs_value[comboName.SelectedIndex, i] = Converter.ToDouble(roo.Cells[2].Value.ToString());
                item_attribs[comboName.SelectedIndex, i] = roo.Cells[0].Value.ToString();
                i++;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (fileName == null) return;
            StringBuilder newFile = new StringBuilder();
            if(!File.Exists(@fileName))
            {
                FileStream rofl = File.Create(@fileName);
                rofl.Close();
            }
            string[] file = File.ReadAllLines(@fileName);

            int i = 0;
            string temp = "";
            string current = null;
            int itemAtr = 0;
            string lastline = "";
            bool don = false;
            bool usedBy = false;
            int level = 0;
            for (int ii = 0; ii < 14; ii++)
                saved[ii] = false;
            foreach (string line in file)
            {
                progressReading.Value = (int)newFile.Length / (int)percent > 100 ? (100) : ((int)newFile.Length / (int)percent);
                //if (Osinfo.MajorVersion.ToString() + "." + Osinfo.MinorVersion.ToString() == "6.1") progressReading.SetTaskbarProgress(); //Only show progress bar on the taskbar if using Windows 7
                temp = line;
                if (line.Contains("{")) level++;
                if (line.Contains("}")) level--;
                if (line.Contains("\"name\"")) //Parsing new item
                {
                    i++;
                    current = line.Replace("name", "").Replace("\"", "").Replace("\t", "");
                    newFile.Append(line + "\r\n");
                    don = false;
                    for (int ii = 0; ii < 14; ii++)
                        saved[ii] = false;
                    continue;
                }
                #region Write from arrays
                if (line.Contains("\"item_class\""))
                {
                    temp = line.Replace("\"item_class\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, item_class[i - 1]);
                    saved[0] = true;
                    goto end;
                }
                if (line.Contains("\"craft_class\""))
                {
                    temp = line.Replace("\"craft_class\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, craft_class[i - 1]);
                    saved[1] = true;
                    goto end;
                }
                if (line.Contains("\"item_type_name\""))
                {
                    temp = line.Replace("\"item_type_name\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, item_type_name[i - 1]);
                    saved[2] = true;
                    goto end;
                }
                if (line.Contains("\"item_name\""))
                {
                    temp = line.Replace("\"item_name\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, item_name[i - 1]);
                    saved[3] = true;
                    goto end;
                }
                if (line.Contains("\"item_slot\""))
                {
                    temp = line.Replace("\"item_slot\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, item_slot[i - 1]);
                    saved[4] = true;
                    goto end;
                }
                if (line.Contains("\"item_quality\""))
                {
                    temp = line.Replace("\"item_quality\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, item_quality[i - 1]);
                    saved[5] = true;
                    goto end;
                }
                if (line.Contains("\"baseitem\""))
                {
                    temp = line.Replace("\"baseitem\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "").Replace(" ", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, baseitem[i - 1]);
                    saved[6] = true;
                    goto end;
                }
                if (line.Contains("\"min_ilevel\""))
                {
                    temp = line.Replace("\"min_ilevel\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, min_ilevel[i - 1]);
                    saved[7] = true;
                    goto end;
                }
                if (line.Contains("\"max_ilevel\""))
                {
                    temp = line.Replace("\"max_ilevel\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, max_ilevel[i - 1]);
                    saved[8] = true;
                    goto end;
                }
                if (line.Contains("\"image_inventory\""))
                {
                    temp = line.Replace("\"image_inventory\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, image_inventory[i - 1]);
                    saved[9] = true;
                    goto end;
                }
                if (line.Contains("\"image_inventory_size_w\""))
                {
                    temp = line.Replace("\"image_inventory_size_w\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, image_inventory_size_w[i - 1]);
                    saved[10] = true;
                    goto end;
                }
                if (line.Contains("\"image_inventory_size_h\""))
                {
                    temp = line.Replace("\"image_inventory_size_h\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, image_inventory_size_h[i - 1]);
                    saved[11] = true;
                    goto end;
                }
                if (line.Contains("\"model_player\""))
                {
                    temp = line.Replace("\"model_player\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, model_player[i - 1]);
                    saved[12] = true;
                    goto end;
                }
                if (line.Contains("\"attach_to_hands\""))
                {
                    temp = line.Replace("\"attach_to_hands\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0) { temp = line; goto end; }
                    temp = line.Replace(temp, attach_to_hands[i - 1]);
                    saved[13] = true;
                    goto end;
                }
                #endregion
                if (current == null) goto end;
                if (current == "Slot Token - Head") goto end;
                if (line.Contains("\"attributes\"") && !current.Contains("Badge"))
                {
                    itemAtr++;
                    don = false;
                    temp += "\r\n\t\t\t{";
                    goto end;
                }
                if (line.Contains("{") && itemAtr > 0)
                {
                    itemAtr++;
                    continue;
                }
                if (line.Contains("}") && itemAtr > 0)
                {
                    itemAtr--;
                    continue;
                }
                if (line.Contains("}") && itemAtr > 1) continue;
                if (line.Contains("{") && itemAtr > 1) continue;
                if ((line.Contains("\"attribute_class\"") || line.Contains("\"value\"")) && itemAtr > 0) continue;
                if (line.Contains("}") && lastline.Contains("}")) itemAtr = 0;
                if (line.Contains("\"") && don == true && itemAtr > 0) continue;
                if (line.Contains("\"") && itemAtr > 0 && !don)
                {
                    temp = "";
                    for (int j = 0; j < number_of_attribs; j++)
                    {
                        if (i >= number_of_items-1) break;
                        if (item_attribs[i - 1, j] == "custom employee number")
                        {
                            //Badges have weird attributes, here's a dirty workaround...
                            temp = "\t\t\t\t\"custom employee number\"\r\n\t\t\t\t{\r\n\t\t\t\t\t\"attribute_class\"\t\"set_employee_number\"\r\n\t\t\t\t\t\"force_gc_to_generate\"\t\"1\"\r\n\t\t\t\t\t\"use_custom_logic\"\t\"employee_number\"\r\n\t\t\t\t}\r\n";
                            don = true;
                            itemAtr = 0;
                            break;
                        }

                        if (GetAttribClass(item_attribs[i - 1, j]) != null)
                        {
                            temp = temp + "\t\t\t\t\"" + item_attribs[i - 1, j] +  "\"\r\n\t\t\t\t{\r\n\t\t\t\t\t\"attribute_class\"\t" + "\"" + GetAttribClass(item_attribs[i - 1, j]) + "\"\r\n\t\t\t\t\t" + "\"value\"\t" + "\"" + item_attribs_value[i - 1, j] + "\"\r\n\t\t\t\t}\r\n";
                        }
                        if (GetAttribClass(item_attribs[i - 1, j + 1]) == null)
                        {
                            temp += "\t\t\t}";
                            itemAtr--;
                            break;
                        }
                    }
                    
                    don = true;
                }
                if (line.Contains("\"used_by_classes\""))
                {
                    usedBy = true;
                    goto end;
                }
                if (line.Contains("{") && usedBy == true)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (i >= number_of_items - 1) break;
                        if (used_by_classes[i - 1, j] == 1)
                        {
                            temp += "\r\n\t\t\t\t\"" + GetClassName(j) + "\"\t\"1\"";
                        }
                    }
                    temp += "\r\n\t\t\t}";
                    goto end;
                }
                if (line.Contains("\"1\"") || line.Contains("}"))
                {
                    if (usedBy == true)
                    {
                        if(!isNumeric(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")))
                        {
                            temp = "";
                            continue;
                        }
                    }
                }
                if (!line.Contains("\"1\"") && !line.Contains("}") && usedBy)
                {
                    usedBy = false;
                }
                if (line.Contains("}") && level == 2)
                {
                    temp = "";
                    int count = 0;
                    for (int k = 0; k < 14; k++)
                    {
                        if (!saved[k] && ReturnSettingVal(i - 1, k) != null && ReturnSettingVal(i - 1, k) != "")
                        {
                            temp += "\r\n\t\t\t\"" + ReturnSettingStr(k) + "\"\t\"" + ReturnSettingVal(i - 1, k) + "\"";
                            saved[k] = true;
                            count++;
                        }
                    }

                    if (count > 0) temp += "\r\n\t\t}";
                    else temp += "\t\t}";
                    goto end;
                }
                if (line.Contains("}") && level == 2 && !don && DoesItemHaveAttribs(i - 1))
                {
                    temp = "\t\t\t\"attributes\"\r\n\t\t\t{\r\n";
                    for (int j = 0; j < number_of_attribs; j++)
                    {
                        if (i >= number_of_items - 1) break;
                        if (GetAttribClass(item_attribs[i - 1, j]) != null)
                        {
                            temp += "\t\t\t\t\"" + item_attribs[i - 1, j] + "\"\r\n\t\t\t\t{\r\n\t\t\t\t\t\"attribute_class\"\t" + "\"" + GetAttribClass(item_attribs[i - 1, j]) + "\"\r\n\t\t\t\t\t" + "\"value\"\t" + "\"" + item_attribs_value[i - 1, j] + "\"\r\n\t\t\t\t}\r\n";
                        }
                        if (GetAttribClass(item_attribs[i - 1, j + 1]) == null)
                        {
                            temp += "\t\t\t}";
                            itemAtr--;
                            break;
                        }
                    }
                    don = true;
                    
                }
                if (isNumeric(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")) && lastline.Contains("\t\t\t}"))
                {
                    temp = "\t\t}\r\n" + line;
                }
                end:
                newFile.Append(temp + "\r\n");
                lastline = temp;
            }
            try
            {
                StreamWriter lol = new StreamWriter(fileName);
                lol.Write(newFile.ToString());
                lol.Close();
            }
            catch
            {
                MessageBox.Show("Something went wrong while saving!", "Who send all these babies to fight!?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressReading.Value = 100;
            //if (Osinfo.MajorVersion.ToString() + "." + Osinfo.MinorVersion.ToString() == "6.1") Windows7.DesktopIntegration.Windows7Taskbar.SetProgressState(this.Handle, Windows7Taskbar.ThumbnailProgressState.NoProgress);
        }

        private void btnSaveAs_Click(object sender, EventArgs e) //Save As doesn't work at the moment!!
        {
            filediagSave.InitialDirectory = "C:\\Program Files\\Steam\\steamapps";
            filediagSave.Filter = "All files|*.*";
            filediagSave.RestoreDirectory = false;
            DialogResult result = filediagSave.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileName = filediagSave.FileName;
                btnSave_Click(null, null);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = linkLabel1.Text;

            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch
                (
                 System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S:
                    btnSave_Click(null, null);
                    return true;
                case Keys.Control|Keys.O:
                    button1_Click(null, null);
                    return true;
            }
            return false;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if(comboName.SelectedIndex == -1) return;
            #region Assign clipboard
            c_attach_to_hands = txt_attach_to_hands.Text;
            c_baseitem = txt_baseitem.Text;
            c_image_inventory = txt_image_inventory.Text;
            c_image_inventory_size_h = txt_image_inventory_size_h.Text;
            c_image_inventory_size_w = txt_image_inventory_size_w.Text;
            c_item_class = txt_item_class.Text;
            c_item_name = txt_item_name.Text;
            c_item_quality = txt_item_quality.Text;
            c_item_slot = txt_item_slot.Text;
            c_item_type_name = txt_item_type_name.Text;
            c_max_ilevel = txt_max_ilevel.Text;
            c_min_ilevel = txt_min_ilevel.Text;
            c_model_player = txt_model_player.Text;
            for (int i = 0; i < 9; i++)
                c_used_by_classes[i] = used_by_classes[comboName.SelectedIndex, i];
            for (int j = 0; j < number_of_attribs; j++)
            {
                if (item_attribs[comboName.SelectedIndex, j] != null)
                {
                    c_item_attribs[j] = item_attribs[comboName.SelectedIndex, j];
                    c_item_attribs_value[j] = item_attribs_value[comboName.SelectedIndex, j];
                }
            }
            #endregion

        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            #region Assign textboxes
            txt_attach_to_hands.Text = c_attach_to_hands;
            txt_baseitem.Text = c_baseitem;
            txt_image_inventory.Text = c_image_inventory;
            txt_image_inventory_size_h.Text = c_image_inventory_size_h;
            txt_image_inventory_size_w.Text = c_image_inventory_size_w;
            txt_item_class.Text = c_item_class;
            txt_item_name.Text = c_item_name;
            txt_item_quality.Text = c_item_quality;
            txt_item_slot.Text = c_item_slot;
            txt_item_type_name.Text = c_item_type_name;
            txt_max_ilevel.Text = c_max_ilevel;
            txt_min_ilevel.Text = c_min_ilevel;
            txt_model_player.Text = c_model_player;
            for (int i = 0; i < 9; i++)
                used_by_classes[comboName.SelectedIndex, i] = c_used_by_classes[i];
            for (int j = 0; j < number_of_attribs; j++)
            {
                if (c_item_attribs[j] != null)
                {
                    item_attribs[comboName.SelectedIndex, j] =  c_item_attribs[j];
                    item_attribs_value[comboName.SelectedIndex, j] = c_item_attribs_value[j];
                }
            }
            #endregion

            list_used_by_classes.Items.Clear();
            list_available_classes.Items.Clear();

            for (int i = 0; i < 9; i++)
            {
                if (used_by_classes[comboName.SelectedIndex, i] == 1)
                    list_used_by_classes.Items.Add(GetClassName(i));
                else
                    list_available_classes.Items.Add(GetClassName(i));
            }

            int n = 0;
            int c = 0;
            grid_attribs.Rows.Clear();
            for (int j = 0; j < number_of_attribs; j++)
            {
                if (item_attribs[comboName.SelectedIndex, j] != null)
                {
                    n = grid_attribs.Rows.Add();
                    grid_attribs.Rows[n].Cells[0].Value = item_attribs[comboName.SelectedIndex, j];
                    grid_attribs.Rows[n].Cells[1].Value = GetAttribClass(item_attribs[comboName.SelectedIndex, j]);
                    grid_attribs.Rows[n].Cells[2].Value = item_attribs_value[comboName.SelectedIndex, j];
                    if (!item_attribs[comboName.SelectedIndex, j].Contains("custom employee number")) c++;
                }
            }
        }

    }
}
