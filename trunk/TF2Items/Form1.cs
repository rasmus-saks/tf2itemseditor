using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TF2Items.Properties;



//using Windows7.DesktopIntegration;
//using Windows7.DesktopIntegration.WindowsForms;

namespace TF2Items
{
    public partial class Form1 : Form
    {
        private const int NumberOfItems = 375;
        private const int NumberOfAttribs = 200;
        private static readonly Regex IsNumber = new Regex(@"^\d+$");
        private readonly string[] _attachToHands = new string[NumberOfItems];
        private readonly string[] _attribAname = new string[NumberOfAttribs];
        private readonly string[] _attribClass = new string[NumberOfAttribs];
        private readonly double[] _attribMinvalue = new double[NumberOfAttribs];
        private readonly string[] _attribName = new string[NumberOfAttribs];
        private readonly string[] _baseitem = new string[NumberOfItems];
        private readonly string[] _craftClass = new string[NumberOfItems];
        private readonly string[] _imageInventory = new string[NumberOfItems];
        private readonly string[] _imageInventorySizeH = new string[NumberOfItems];
        private readonly string[] _imageInventorySizeW = new string[NumberOfItems];
        private readonly string[,] _itemAttribs = new string[NumberOfItems,NumberOfAttribs];
        private readonly double[,] _itemAttribsValue = new double[NumberOfItems,NumberOfAttribs];
        private readonly string[] _itemClass = new string[NumberOfItems];
        private readonly string[] _itemName = new string[NumberOfItems];
        private readonly string[] _itemQuality = new string[NumberOfItems];
        private readonly string[] _itemSlot = new string[NumberOfItems];
        private readonly string[] _itemTypeName = new string[NumberOfItems];
        private readonly string[] _maxIlevel = new string[NumberOfItems];
        private readonly string[] _minIlevel = new string[NumberOfItems];
        private readonly string[] _modelPlayer = new string[NumberOfItems];
        private readonly string[] _name = new string[NumberOfItems];
        private readonly bool[] _saved = new bool[14];
        private readonly int[,] _usedByClasses = new int[NumberOfItems,9];
        private string _fileName;

        private bool _firstSetup;
        private string _lastTip;
        private double _percent;
        private int _saveNum = -1;
        private string _saveStr = "";
        private int lastSel;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            filediagOpen.Title = Resources.Form1_button1_Click_Select_items_game_txt_or_team_fortress_2_content_gcf;

            filediagOpen.Filter =
                Resources.
                    Form1_button1_Click_items_game_txt_items_game_txt_Team_Fortress_2_content_team_fortress_2_content_gcf;
            filediagOpen.RestoreDirectory = false;
            DialogResult result = filediagOpen.ShowDialog();
            if (result != DialogResult.OK) return;
            if (filediagOpen.FileName.Contains(".gcf"))
            {
                var extract = new Process
                                  {
                                      StartInfo =
                                          {
                                              FileName = "HLExtract.exe",
                                              Arguments = "-c -p \"" + filediagOpen.FileName +
                                                          "\" -e \"root\\tf\\scripts\\items\\items_game.txt\""
                                          }
                                  };
                if (!File.Exists("HLExtract.exe"))
                {
                    MessageBox.Show(Resources.Form1_button1_Click_HLExtract_exe_is_missing_from_the_program_folder_,
                                    Resources.Form1_button1_Click_Who_send_all_these_babies_to_fight__,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists("HLLib.dll"))
                {
                    MessageBox.Show(Resources.Form1_button1_Click_HLLib_dll_is_missing_from_the_program_folder_,
                                    Resources.Form1_button1_Click_Who_send_all_these_babies_to_fight__,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
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
                    MessageBox.Show(Resources.Form1_button1_Click_Something_went_wrong_when_extracting___,
                                    Resources.Form1_button1_Click_Who_send_all_these_babies_to_fight__,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show(
                    Resources.Form1_button1_Click_Extracted_successfully_to_ +
                    filediagOpen.FileName.Replace("team fortress 2 content.gcf", "items_game.txt") +
                    Resources.Form1_button1_Click_,
                    Resources.Form1_button1_Click_Listen_up_,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                _fileName = filediagOpen.FileName.Replace("team fortress 2 content.gcf", "items_game.txt");
                extract.Kill();
                extract.Close();
            }
            else _fileName = filediagOpen.FileName;
            _firstSetup = true;
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
            comboName.Text = Resources.Form1_button1_Click_Select_an_item;
            for (int jj = 0; jj < NumberOfItems; jj++) for (int kk = 0; kk < 9; kk++) _usedByClasses[jj, kk] = 0;
            ReadFile();
        }

        public void ReadFile()
        {
            using (var sReader = new StreamReader(_fileName))
            {
                string line;
                string current = "";
                int i = 0;
                var file = new StreamReader(_fileName);
                _percent = file.BaseStream.Length/(double) 100;
                bool usedby = false;
                bool inAttribs = false;
                int itemAtr = -1;
                string lastline = "";
                int level = 0;
                int aN = 0;
                bool inSets = false;
                while ((line = file.ReadLine()) != null)
                {
                    progressReading.Value = (int) file.BaseStream.Position/(int) _percent > 100
                                                ? 100
                                                : (int) file.BaseStream.Position/(int) _percent;
                    // if (Osinfo.MajorVersion.ToString() + "." + Osinfo.MinorVersion.ToString() == "6.1") progressReading.SetTaskbarProgress(); //Only show progress bar on the taskbar if using Windows 7
                    if (inSets) continue;
                    if (!inAttribs)
                    {
                        if (line.Contains("\t\t\"name\"")) //Parsing new item
                        {
                            string tmp = line.Replace("\"name\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            if (tmp.Substring(0, 1) == " ") tmp = tmp.Substring(1); //Removes leading space in Polycount class bundles
                            current = tmp;
                            comboName.Items.Insert(i, tmp + "\r\n");
                            _name[i] = tmp;
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
                            _itemAttribsValue[i - 1, aN] =
                                Converter.ToDouble(Regex.Replace(line.Replace("\"", "").Replace("	", "").Replace("value", ""), "(?<comment>//.*)", ""));
                            aN++;
                        }
                        if (line.Contains("\"force_gc_to_generate\"") && itemAtr > 0) continue;
                        if (line.Contains("\"use_custom_logic\"") && itemAtr > 0) continue;
                        if (line.Contains("\"") && itemAtr > 0 && !line.Contains("\"value\"")) _itemAttribs[i - 1, aN] = line.Replace("\"", "").Replace("	", "");

                        #region Assign arrays

                        if (line.Contains("\"item_class\""))
                        {
                            string tmp = line.Replace("\"item_class\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _itemClass[i - 1] = tmp;
                        }
                        if (line.Contains("\"craft_class\""))
                        {
                            string tmp = line.Replace("\"craft_class\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _craftClass[i - 1] = tmp;
                        }
                        if (line.Contains("\"item_type_name\""))
                        {
                            string tmp = line.Replace("\"item_type_name\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _itemTypeName[i - 1] = tmp;
                        }
                        if (line.Contains("\"item_name\""))
                        {
                            string tmp = line.Replace("\"item_name\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _itemName[i - 1] = tmp;
                        }
                        if (line.Contains("\"item_slot\""))
                        {
                            string tmp = line.Replace("\"item_slot\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _itemSlot[i - 1] = tmp;
                        }
                        if (line.Contains("\"item_quality\""))
                        {
                            string tmp = line.Replace("\"item_quality\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _itemQuality[i - 1] = tmp;
                        }
                        if (line.Contains("\"baseitem\""))
                        {
                            string tmp = line.Replace("\"baseitem\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _baseitem[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"min_ilevel\""))
                        {
                            string tmp = line.Replace("\"min_ilevel\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _minIlevel[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"max_ilevel\""))
                        {
                            string tmp = line.Replace("\"max_ilevel\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _maxIlevel[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"image_inventory\""))
                        {
                            string tmp = line.Replace("\"image_inventory\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _imageInventory[i - 1] = tmp;
                        }
                        if (line.Contains("\"image_inventory_size_w\""))
                        {
                            string tmp = line.Replace("\"image_inventory_size_w\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _imageInventorySizeW[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"image_inventory_size_h\""))
                        {
                            string tmp = line.Replace("\"image_inventory_size_h\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _imageInventorySizeH[i - 1] = tmp.Replace(" ", "");
                        }
                        if (line.Contains("\"model_player\""))
                        {
                            string tmp = line.Replace("\"model_player\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _modelPlayer[i - 1] = tmp;
                        }
                        if (line.Contains("\"attach_to_hands\""))
                        {
                            string tmp = line.Replace("\"attach_to_hands\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            _attachToHands[i - 1] = tmp.Replace(" ", "");
                        }

                        #endregion

                        if (line.Contains("\"used_by_classes\"")) usedby = true;
                        if (line.Contains("}") && usedby) usedby = false;
                        if (line.Contains("\"1\"") && usedby)
                        {
                            string tmp = line.Replace("\"1\"", "");
                            int res;
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

                            _usedByClasses[i - 1, res] = 1;
                        }
                    }
                    else
                    {
                        if (line.Contains("\"item_sets\""))
                        {
                            inSets = true;
                            continue;
                        }
                        if (line.Contains("\"name\""))
                        {
                            string tmp = line.Replace("\"name\"", "");
                            tmp = tmp.Replace("\"", "");
                            tmp = tmp.Replace("	", "");
                            current = tmp;
                            _attribName[i] = tmp;
                            list_all_attribs.Items.Add(tmp);
                            i++;
                        }
                        else if (line.Contains("\"attribute_class\""))
                        {
                            _attribClass[i - 1] =
                                line.Replace("\"attribute_class\"", "").Replace("\"", "").Replace("\t", "");
                        }
                        else if (line.Contains("\"attribute_name\""))
                        {
                            _attribAname[i - 1] =
                                line.Replace("\"attribute_name\"", "").Replace("\"", "").Replace("\t", "");
                        }
                        else if (line.Contains("\"attribute_name\""))
                        {
                            _attribMinvalue[i - 1] =
                                Converter.ToDouble(
                                    line.Replace("\"min_value\"", "").Replace("\"", "").Replace("\t", ""));
                        }
                    }
                    if (current.Contains("Paint Can 14") && line.Contains("ui/item_paint_can_pickup.wav"))
                    {
                        inAttribs = true;
                        i = 0;
                    }
                    lastline = line;
                }
                file.Close();
                sReader.Close();
                _attribAname[113] = "mod mini-crit airborne";
                //Somehow the mini-crit airborne doesn't have attribute_name, here's a workaround
                comboName.SelectedIndex = comboName.SelectedIndex;
                progressReading.Value = 100;
                //if(Osinfo.MajorVersion.ToString() + "." + Osinfo.MinorVersion.ToString() == "6.1") Windows7.DesktopIntegration.Windows7Taskbar.SetProgressState(this.Handle, Windows7Taskbar.ThumbnailProgressState.NoProgress);
            }
            _firstSetup = false;
            comboName.SelectedIndex = 0;
        }

        /// <summary>Returns a TF2 class name based on the classid</summary>
        /// <param name="classid">The class id, starting from 0</param>
        /// <returns>The class name, lowercase or "Invalid classid!" on error</returns>
        public static string GetClassName(int classid)
        {
            switch (classid)
            {
                case 0:
                    return "scout";
                case 1:
                    return "soldier";
                case 2:
                    return "pyro";
                case 3:
                    return "demoman";
                case 4:
                    return "heavy";
                case 5:
                    return "engineer";
                case 6:
                    return "medic";
                case 7:
                    return "sniper";
                case 8:
                    return "spy";
                default:
                    return "Invalid classid!";
            }
        }

        /// <summary>Returns a TF2 class id based on the name</summary>
        /// <param name="classname">The class name, lowercase</param>
        /// <returns>The class id, starting from 0 or -1 on error</returns>
        public static int GetClassId(string classname)
        {
            switch (classname)
            {
                case "scout":
                    return 0;
                case "soldier":
                    return 1;
                case "pyro":
                    return 2;
                case "demoman":
                    return 3;
                case "heavy":
                    return 4;
                case "engineer":
                    return 5;
                case "medic":
                    return 6;
                case "sniper":
                    return 7;
                case "spy":
                    return 8;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Returns a bool indicating if the item has attributes
        /// </summary>
        /// <param name="itemid">The id of the item</param>
        public bool DoesItemHaveAttribs(int itemid)
        {
            if (itemid < 0 || itemid >= NumberOfItems) return false;
            for (int i = 0; i < NumberOfAttribs; i++) if (_itemAttribs[itemid, i] != null) return true;
            return false;
        }

        public static bool IsNumeric(string theValue)
        {
            Match m = IsNumber.Match(theValue);
            return m.Success;
        }

        private string GetAttribClass(string attribname)
        {
            for (int k = 0; k < NumberOfAttribs; k++) if (_attribName[k] == attribname) return _attribClass[k];
            return "";
        }

/*
        private string GetAttribAname(string attribname)
        {
            for (int k = 0; k < number_of_attribs; k++) if (attrib_name[k] == attribname) return attrib_aname[k];
            return "";
        }
*/

        public string ReturnSettingVal(int item, int sId)
        {
            if (item < 0 || item >= NumberOfItems || sId < 0 || sId > 13) return null;
            switch (sId)
            {
                case 0:
                    return _itemClass[item];
                case 1:
                    return _craftClass[item];
                case 2:
                    return _itemTypeName[item];
                case 3:
                    return _itemName[item];
                case 4:
                    return _itemSlot[item];
                case 5:
                    return _itemQuality[item];
                case 6:
                    return _baseitem[item];
                case 7:
                    return _minIlevel[item];
                case 8:
                    return _maxIlevel[item];
                case 9:
                    return _imageInventory[item];
                case 10:
                    return _imageInventorySizeW[item];
                case 11:
                    return _imageInventorySizeH[item];
                case 12:
                    return _modelPlayer[item];
                case 13:
                    return _attachToHands[item];
            }
            return "";
        }

        public string ReturnSettingStr(int sId)
        {
            if (sId < 0 || sId > 13) return null;
            switch (sId)
            {
                case 0:
                    return "item_class";
                case 1:
                    return "craft_class";
                case 2:
                    return "item_type_name";
                case 3:
                    return "item_name";
                case 4:
                    return "item_slot";
                case 5:
                    return "item_quality";
                case 6:
                    return "baseitem";
                case 7:
                    return "min_ilevel";
                case 8:
                    return "max_ilevel";
                case 9:
                    return "image_inventory";
                case 10:
                    return "image_inventory_size_w";
                case 11:
                    return "image_inventory_size_h";
                case 12:
                    return "model_player";
                case 13:
                    return "attach_to_hands";
            }

            return "";
        }

        public int GetAttribId(string attribName)
        {
            for (int i = 0; i < NumberOfAttribs; i++) if (_attribName[i] == attribName) return i;
            return -1;
        }

        private void ComboNameSelectedIndexChanged(object sender, EventArgs e) //When user selects an item
        {
            if (comboName.SelectedIndex == -1) return;
            _firstSetup = true;

            txt_item_class.Text = _itemClass[comboName.SelectedIndex];
            txt_craft_class.Text = _craftClass[comboName.SelectedIndex];
            txt_item_type_name.Text = _itemTypeName[comboName.SelectedIndex];
            txt_item_name.Text = _itemName[comboName.SelectedIndex];
            txt_item_slot.Text = _itemSlot[comboName.SelectedIndex];
            txt_item_quality.Text = _itemQuality[comboName.SelectedIndex];
            txt_baseitem.Text = _baseitem[comboName.SelectedIndex];
            txt_min_ilevel.Text = _minIlevel[comboName.SelectedIndex];
            txt_max_ilevel.Text = _maxIlevel[comboName.SelectedIndex];
            txt_image_inventory.Text = _imageInventory[comboName.SelectedIndex];
            txt_image_inventory_size_w.Text = _imageInventorySizeW[comboName.SelectedIndex];
            txt_image_inventory_size_h.Text = _imageInventorySizeH[comboName.SelectedIndex];
            txt_model_player.Text = _modelPlayer[comboName.SelectedIndex];
            txt_attach_to_hands.Text = _attachToHands[comboName.SelectedIndex];

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
                if (_usedByClasses[comboName.SelectedIndex, i] == 1) list_used_by_classes.Items.Add(GetClassName(i));
                else list_available_classes.Items.Add(GetClassName(i));
            }
            int c = 0;
            grid_attribs.Rows.Clear();
            for (int j = 0; j < NumberOfAttribs; j++)
            {
                if (_itemAttribs[comboName.SelectedIndex, j] != null && _itemAttribs[comboName.SelectedIndex, j] != "")
                {
                    int n = grid_attribs.Rows.Add();
                    grid_attribs.Rows[n].Cells[0].Value = _itemAttribs[comboName.SelectedIndex, j];
                    grid_attribs.Rows[n].Cells[1].Value = GetAttribClass(_itemAttribs[comboName.SelectedIndex, j]);
                    grid_attribs.Rows[n].Cells[2].Value = _itemAttribsValue[comboName.SelectedIndex, j];
                    if (!_itemAttribs[comboName.SelectedIndex, j].Contains("custom employee number")) c++;
                }
            }
            //list_all_attribs.Enabled = c != 0;
            _firstSetup = false;
        }

        private void Button1Click1(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (list_available_classes.SelectedItem == null) return;
            list_used_by_classes.Items.Add(list_available_classes.SelectedItem);
            list_available_classes.Items.Remove(list_available_classes.SelectedItem);
            for (int i = 0; i < 9; i++)
            {
                if (list_used_by_classes.Items.Contains(GetClassName(i))) _usedByClasses[comboName.SelectedIndex, i] = 1;
                else _usedByClasses[comboName.SelectedIndex, i] = 0;
            }
        }

        private void MoveRightBtnClick(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (list_used_by_classes.SelectedItem == null) return;
            list_available_classes.Items.Add(list_used_by_classes.SelectedItem);
            list_used_by_classes.Items.Remove(list_used_by_classes.SelectedItem);
            for (int i = 0; i < 9; i++)
            {
                if (list_used_by_classes.Items.Contains(GetClassName(i))) _usedByClasses[comboName.SelectedIndex, i] = 1;
                else _usedByClasses[comboName.SelectedIndex, i] = 0;
            }
        }

        private void ListAllAttribsDoubleClick(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            int n = grid_attribs.Rows.Add();
            grid_attribs.Rows[n].Cells[0].Value = list_all_attribs.SelectedItem.ToString();
            grid_attribs.Rows[n].Cells[1].Value = GetAttribClass(list_all_attribs.SelectedItem.ToString());
            _itemAttribs[comboName.SelectedIndex, n] = list_all_attribs.SelectedItem.ToString();
        }

        private void GridAttribsCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (e == null) return;
            int i = 0;
            if (e.ColumnIndex == 0 || _firstSetup) return;
            foreach (DataGridViewRow roo in grid_attribs.Rows)
            {
                if (roo.Cells[2].Value == null) continue;
                _itemAttribsValue[comboName.SelectedIndex, i] = Converter.ToDouble(roo.Cells[2].Value.ToString());
                _itemAttribs[comboName.SelectedIndex, i] = roo.Cells[0].Value.ToString();
                i++;
            }
        }

        public void SaveFile()
        {
            var newFile = new StringBuilder();
            if (!File.Exists(_fileName))
            {
                FileStream rofl = File.Create(_fileName);
                rofl.Close();
            }
            string[] file = File.ReadAllLines(_fileName);

            int i = 0;
            string temp;
            string current = null;
            int itemAtr = 0;
            string lastline = "";
            bool don = false;
            bool usedBy = false;
            int level = 0;
            bool inTools = false;
            bool inSets = false;
            bool inAttribs = false;
            for (int ii = 0; ii < 14; ii++) _saved[ii] = false;
            foreach (string line in file)
            {
                progressReading.Value = newFile.Length/(int) _percent > 100 ? (100) : (newFile.Length/(int) _percent);
                //if (Osinfo.MajorVersion.ToString() + "." + Osinfo.MinorVersion.ToString() == "6.1") progressReading.SetTaskbarProgress(); //Only show progress bar on the taskbar if using Windows 7
                temp = line;
                if (line.Contains("{")) level++;
                if (line.Contains("}")) level--;
                if (line.Contains("}") && current == "Paint Can 14" && level == 1)
                {
                    inAttribs = true;
                    goto end;
                }
                if (inAttribs) goto end;
                if (line.Contains("\"item_sets\""))
                {
                    inSets = true;
                    goto end;
                }

                if (inSets) goto end;
                if (line.Contains("\"tool\"") && !line.Contains("_class"))
                {
                    inTools = true;
                    goto end;
                }
                if (level == 4 && inTools) goto end;
                if (level == 3 && inTools)
                {
                    inTools = false;
                    goto end;
                }
                if (line.Contains("\"name\"") && current != "Paint Can 14" && level == 3) //Parsing new item
                {
                    i++;
                    current = line.Replace("name", "").Replace("\"", "").Replace("\t", "");
                    don = false;
                    for (int ii = 0; ii < 14; ii++) _saved[ii] = false;
                    temp = i < NumberOfItems ? line.Replace(current, _name[i - 1]) : line;
                    goto end;
                }
                
                #region Write from arrays

                if (line.Contains("\"item_class\""))
                {
                    temp = line.Replace("\"item_class\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _itemClass[i - 1]);
                    _saved[0] = true;
                    goto end;
                }
                if (line.Contains("\"craft_class\""))
                {
                    temp = line.Replace("\"craft_class\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _craftClass[i - 1]);
                    _saved[1] = true;
                    goto end;
                }
                if (line.Contains("\"item_type_name\""))
                {
                    temp = line.Replace("\"item_type_name\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _itemTypeName[i - 1]);
                    _saved[2] = true;
                    goto end;
                }
                if (line.Contains("\"item_name\""))
                {
                    temp = line.Replace("\"item_name\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _itemName[i - 1]);
                    _saved[3] = true;
                    goto end;
                }
                if (line.Contains("\"item_slot\""))
                {
                    temp = line.Replace("\"item_slot\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _itemSlot[i - 1]);
                    _saved[4] = true;
                    goto end;
                }
                if (line.Contains("\"item_quality\""))
                {
                    temp = line.Replace("\"item_quality\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _itemQuality[i - 1]);
                    _saved[5] = true;
                    goto end;
                }
                if (line.Contains("\"baseitem\""))
                {
                    temp = line.Replace("\"baseitem\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "").Replace(" ", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _baseitem[i - 1]);
                    _saved[6] = true;
                    goto end;
                }
                if (line.Contains("\"min_ilevel\""))
                {
                    temp = line.Replace("\"min_ilevel\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _minIlevel[i - 1]);
                    _saved[7] = true;
                    goto end;
                }
                if (line.Contains("\"max_ilevel\""))
                {
                    temp = line.Replace("\"max_ilevel\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _maxIlevel[i - 1]);
                    _saved[8] = true;
                    goto end;
                }
                if (line.Contains("\"image_inventory\""))
                {
                    temp = line.Replace("\"image_inventory\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _imageInventory[i - 1]);
                    _saved[9] = true;
                    goto end;
                }
                if (line.Contains("\"image_inventory_size_w\""))
                {
                    temp = line.Replace("\"image_inventory_size_w\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _imageInventorySizeW[i - 1]);
                    _saved[10] = true;
                    goto end;
                }
                if (line.Contains("\"image_inventory_size_h\""))
                {
                    temp = line.Replace("\"image_inventory_size_h\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _imageInventorySizeH[i - 1]);
                    _saved[11] = true;
                    goto end;
                }
                if (line.Contains("\"model_player\""))
                {
                    temp = line.Replace("\"model_player\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _modelPlayer[i - 1]);
                    _saved[12] = true;
                    goto end;
                }
                if (line.Contains("\"attach_to_hands\""))
                {
                    temp = line.Replace("\"attach_to_hands\"", "");
                    temp = temp.Replace("\"", "");
                    temp = temp.Replace("	", "");
                    if (temp.Length == 0)
                    {
                        temp = line;
                        goto end;
                    }
                    temp = line.Replace(temp, _attachToHands[i - 1]);
                    _saved[13] = true;
                    goto end;
                }

                #endregion
                if (current == null) goto end;
                if (current.Contains("Upgradeable")) goto end;
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
                if (line.Contains("\"") && don && itemAtr > 0) continue;
                if (line.Contains("\"") && itemAtr > 0 && !don)
                {
                    temp = "";
                    for (int j = 0; j < NumberOfAttribs; j++)
                    {
                        if (i >= NumberOfItems - 1) break;
                        if (_itemAttribs[i - 1, j] == "custom employee number")
                        {
                            //Badges have weird attributes, here's a dirty workaround...
                            temp =
                                "\t\t\t\t\"custom employee number\"\r\n\t\t\t\t{\r\n\t\t\t\t\t\"attribute_class\"\t\"set_employee_number\"\r\n\t\t\t\t\t\"force_gc_to_generate\"\t\"1\"\r\n\t\t\t\t\t\"use_custom_logic\"\t\"employee_number\"\r\n\t\t\t\t}\r\n";
                            continue;
                        }
                        if (_itemAttribs[i - 1, j] == "set supply crate series")
                        {
                            //So do supply crates
                            temp =
                                "\t\t\t\t\"set supply crate series\"\r\n\t\t\t\t{\r\n\t\t\t\t\t\"attribute_class\"\t\"supply_crate_series\"\r\n\t\t\t\t\t\"value\"\t\"" + _itemAttribsValue[i-1, j] + "\"\r\n\t\t\t\t\t\"force_gc_to_generate\"\t\"1\"\r\n\t\t\t\t}\r\n";
                            continue;
                        }
                        if (_itemAttribs[i - 1, j] == "set item tint RGB" && current == "Paint Can")
                        {
                            //And paint cans
                            temp =
                                "\t\t\t\t\"set item tint RGB\"\r\n\t\t\t\t{\r\n\t\t\t\t\t\"attribute_class\"\t\"set_item_tint_rgb\"\r\n\t\t\t\t\t\"force_gc_to_generate\"\t\"1\"\r\n\t\t\t\t}\r\n";
                            continue;
                        }
                        if (GetAttribClass(_itemAttribs[i - 1, j]) != null &&
                            GetAttribClass(_itemAttribs[i - 1, j]) != "")
                        {
                            temp = temp + "\t\t\t\t\"" + _itemAttribs[i - 1, j] +
                                   "\"\r\n\t\t\t\t{\r\n\t\t\t\t\t\"attribute_class\"\t" + "\"" +
                                   GetAttribClass(_itemAttribs[i - 1, j]) + "\"\r\n\t\t\t\t\t" + "\"value\"\t" + "\"" +
                                   _itemAttribsValue[i - 1, j] + "\"\r\n\t\t\t\t}\r\n";
                        }
                        if (j + 1 == NumberOfAttribs)
                        {
                            temp += "\t\t\t}";
                            itemAtr--;
                            break;
                        }
                        if (GetAttribClass(_itemAttribs[i - 1, j + 1]) != null) continue;
                        temp += "\t\t\t}";
                        itemAtr--;
                        break;
                    }

                    don = true;
                }
                if (line.Contains("\"used_by_classes\""))
                {
                    usedBy = true;
                    goto end;
                }
                if (line.Contains("{") && usedBy)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (i >= NumberOfItems - 1) break;
                        if (_usedByClasses[i - 1, j] == 1) temp += "\r\n\t\t\t\t\"" + GetClassName(j) + "\"\t\"1\"";
                    }
                    temp += "\r\n\t\t\t}";
                    goto end;
                }
                if (line.Contains("\"1\"") || line.Contains("}")) if (usedBy) if (!IsNumeric(line.Replace("\"", "").Replace("\t", "").Replace(" ", ""))) continue;
                if (!line.Contains("\"1\"") && !line.Contains("}") && usedBy) usedBy = false;
                if (line.Contains("}") && level == 2)
                {
                    temp = "";
                    int count = 0;
                    for (int k = 0; k < 14; k++)
                    {
                        if (_saved[k] || ReturnSettingVal(i - 1, k) == null || ReturnSettingVal(i - 1, k) == "") continue;
                        temp += "\r\n\t\t\t\"" + ReturnSettingStr(k) + "\"\t\"" + ReturnSettingVal(i - 1, k) + "\"";
                        _saved[k] = true;
                        count++;
                    }

                    temp += count > 0 ? "\r\n\t\t}" : "\t\t}";
                    goto end;
                }
                if (line.Contains("}") && level == 2 && !don && DoesItemHaveAttribs(i - 1))
                {
                    temp = "\t\t\t\"attributes\"\r\n\t\t\t{\r\n";
                    for (int j = 0; j < NumberOfAttribs; j++)
                    {
                        if (i >= NumberOfItems - 1) break;
                        if (GetAttribClass(_itemAttribs[i - 1, j]) != null)
                        {
                            temp += "\t\t\t\t\"" + _itemAttribs[i - 1, j] +
                                    "\"\r\n\t\t\t\t{\r\n\t\t\t\t\t\"attribute_class\"\t" + "\"" +
                                    GetAttribClass(_itemAttribs[i - 1, j]) + "\"\r\n\t\t\t\t\t" + "\"value\"\t" + "\"" +
                                    _itemAttribsValue[i - 1, j] + "\"\r\n\t\t\t\t}\r\n";
                        }
                        if (GetAttribClass(_itemAttribs[i - 1, j + 1]) != null) continue;
                        temp += "\t\t\t}";
                        itemAtr--;
                        break;
                    }
                    don = true;
                }
                if (IsNumeric(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")) && lastline.Contains("\t\t\t}")) temp = "\t\t}\r\n" + line;
                end:
                newFile.Append(temp + "\r\n");
                lastline = temp;
            }
            try
            {
                var lol = new StreamWriter(_fileName);
                lol.Write(newFile.ToString());
                lol.Close();
            }
            catch
            {
                MessageBox.Show(Resources.Form1_SaveFile_Something_went_wrong_while_saving_,
                                Resources.Form1_SaveFile_Who_send_all_these_babies_to_fight__,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            progressReading.Value = 100;
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            if (_fileName == null) return;
            SaveFile();
            //if (Osinfo.MajorVersion.ToString() + "." + Osinfo.MinorVersion.ToString() == "6.1") Windows7.DesktopIntegration.Windows7Taskbar.SetProgressState(this.Handle, Windows7Taskbar.ThumbnailProgressState.NoProgress);
        }

        private void BtnSaveAsClick(object sender, EventArgs e) //Save As doesn't work at the moment!!
        {
            if (_fileName == null) return;
            filediagSave.InitialDirectory = "C:\\Program Files\\Steam\\steamapps";
            filediagSave.Filter = Resources.Form1_BtnSaveAsClick_All_files____;
            filediagSave.RestoreDirectory = false;
            DialogResult result = filediagSave.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (_fileName == filediagSave.FileName)
                {
                    SaveFile();
                    return;
                }
                if (File.Exists(@filediagSave.FileName) && filediagSave.FileName != _fileName) File.Delete(@filediagSave.FileName);
                File.Copy(_fileName, @filediagSave.FileName);
                _fileName = filediagSave.FileName;
                SaveFile();
            }
        }

        private void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = linkLabel1.Text;

            try
            {
                Process.Start(target);
            }
            catch
                (
                Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259) MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S:
                    BtnSaveClick(null, null);
                    return true;
                case Keys.Control | Keys.O:
                    Button1Click(null, null);
                    return true;
            }
            return false;
        }

        private void BtnCopyClick(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            string[] file = File.ReadAllLines(_fileName);
            int level = 0;
            string lastline = "";
            _saveStr = "";
            bool saving = false;
            foreach (string line in file)
            {
                string temp = line;
                if (line.Contains("{"))
                {
                    level++;
                    if (!saving) goto end;
                }
                if (line.Contains("}"))
                {
                    level--;
                    if (!saving) goto end;
                }

                if (IsNumeric(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")) &&
                    (lastline.Contains("\t\t}") || lastline.Contains("\t\t{")))
                {
                    if (Convert.ToInt32(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")) ==
                        comboName.SelectedIndex)
                    {
                        saving = true;
                        _saveNum = comboName.SelectedIndex;
                        _saveStr += line + "\r\n";
                        goto end;
                    }
                }
                if (saving && level == 2 && line.Contains("\t\t}"))
                {
                    _saveStr += "\t\t}";
                    break;
                }
                if (saving) _saveStr += line + "\r\n";
                end:
                lastline = temp;
            }
            return;
        }

        private void BtnPasteClick(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (_saveNum == -1) return;
            var newFile = new StringBuilder();
            if (!File.Exists(_fileName))
            {
                FileStream rofl = File.Create(_fileName);
                rofl.Close();
            }
            string[] file = File.ReadAllLines(_fileName);
            int level = 0;
            string lastline = "";
            bool saving = false;
            bool savd = false;
            foreach (string line in file)
            {
                string temp = line;
                if (savd) goto end;
                if (line.Contains("{"))
                {
                    level++;
                    if (!saving) goto end;
                }
                if (line.Contains("}"))
                {
                    level--;
                    if (!saving) goto end;
                }

                if (IsNumeric(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")) &&
                    (lastline.Contains("\t\t}") || lastline.Contains("\t\t{")))
                {
                    if (Convert.ToInt32(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")) ==
                        comboName.SelectedIndex)
                    {
                        temp = _saveStr.Replace("\"" + _saveNum + "\"", "\"" + comboName.SelectedIndex + "\"");
                        saving = true;
                        goto end;
                    }
                }
                if (saving && level == 2 && line.Contains("\t\t}"))
                {
                    saving = false;
                    savd = true;
                    continue;
                }
                if (saving) continue;
                end:
                newFile.Append(temp + "\r\n");
                lastline = temp;
            }
            try
            {
                var lol = new StreamWriter(_fileName);
                lol.Write(newFile.ToString());
                lol.Close();
            }
            catch
            {
                MessageBox.Show(Resources.Form1_SaveFile_Something_went_wrong_while_saving_,
                                Resources.Form1_SaveFile_Who_send_all_these_babies_to_fight__,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            ReadFile();
        }

        private void ListAllAttribsMouseMove1(object sender, MouseEventArgs e)
        {
            var listBox = (ListBox) sender;
            try
            {
                int index = listBox.IndexFromPoint(e.Location);
                if (index > -1 && index < listBox.Items.Count)
                {
                    string tipp = listBox.Items[index].ToString();
                    int idd = GetAttribId(tipp);
                    string tip = ToolTips.MArrItemToolTips[idd];
                    if (tip != _lastTip)
                    {
                        ListToolTip.SetToolTip(listBox, tip);
                        _lastTip = tip;
                    }
                }
            }
            catch
            {
                ListToolTip.Hide(listBox);
            }
        }


        private void GridAttribsUserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            if (e == null || _firstSetup) return;
            for (int i = 0; i < NumberOfAttribs; i++)
            {
                _itemAttribs[comboName.SelectedIndex, i] = null;
                _itemAttribsValue[comboName.SelectedIndex, i] = 0;
            }
            foreach (DataGridViewRow roo in grid_attribs.Rows)
            {
                if (roo.Cells[2].Value == null) roo.Cells[2].Value = 0;
                _itemAttribsValue[comboName.SelectedIndex, roo.Index] =
                    Converter.ToDouble(roo.Cells[2].Value.ToString());
                _itemAttribs[comboName.SelectedIndex, roo.Index] = roo.Cells[0].Value.ToString();
            }
        }

        private void TextBox1TextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            list_all_attribs.Items.Clear();
            for (int i = 0; i < NumberOfAttribs; i++)
            {
                if (_attribName[i] == null || _attribName[i] == "") continue;
                if (_attribName[i].ToLower().Contains(searchBox.Text.ToLower())) list_all_attribs.Items.Add(_attribName[i]);
            }
        }

        private void ComboNameTextUpdate(object sender, EventArgs e)
        {
            if (_firstSetup) return;
            if (comboName.Text == "") return;
            if (comboName.DroppedDown) return;
            if (comboName.SelectedIndex != -1) lastSel = comboName.SelectedIndex;
            _name[lastSel] = comboName.Text.Replace("\r\n", "");
            comboName.Items[lastSel] = _name[lastSel];
            comboName.Select(_name[lastSel].Length, _name[lastSel].Length);
        }

        private void Button1Click2(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            filediagSave.Title = Resources.Form1_Button1Click2_Select_a_file_to_save_the_item_to;

            filediagSave.Filter = Resources.Form1_Button1Click2_Any_file____;
            filediagSave.RestoreDirectory = false;
            DialogResult result = filediagSave.ShowDialog();
            if (result != DialogResult.OK) return;
            string tmpFile = filediagSave.FileName;
            if (!File.Exists(@tmpFile))
            {
                FileStream rofl = File.Create(@tmpFile);
                rofl.Close();
            }
            string[] file = File.ReadAllLines(_fileName);
            int level = 0;
            string lastline = "";
            _saveStr = "";
            bool saving = false;

            foreach (string line in file)
            {
                string temp = line;
                if (line.Contains("{"))
                {
                    level++;
                    if (!saving) goto end;
                }
                if (line.Contains("}"))
                {
                    level--;
                    if (!saving) goto end;
                }

                if (IsNumeric(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")) &&
                    (lastline.Contains("\t\t}") || lastline.Contains("\t\t{")))
                {
                    if (Convert.ToInt32(line.Replace("\"", "").Replace("\t", "").Replace(" ", "")) ==
                        comboName.SelectedIndex)
                    {
                        saving = true;
                        _saveNum = comboName.SelectedIndex;
                        _saveStr += line + "\r\n";
                        goto end;
                    }
                }
                if (saving && level == 2 && line.Contains("\t\t}"))
                {
                    _saveStr += "\t\t}";
                    break;
                }
                if (saving) _saveStr += line + "\r\n";
                end:
                lastline = temp;
            }
            var rofll = new StreamWriter(@tmpFile);
            rofll.Write(_saveStr);
            rofll.Close();
            return;
        }

        #region Save textboxes

        private void TxtItemClassTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _itemClass[comboName.SelectedIndex] = txt_item_class.Text;
        }

        private void TxtItemNameTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _itemName[comboName.SelectedIndex] = txt_item_name.Text;
        }

        private void TxtItemSlotTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _itemSlot[comboName.SelectedIndex] = txt_item_slot.Text;
        }

        private void TxtItemQualityTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _itemQuality[comboName.SelectedIndex] = txt_item_quality.Text;
        }

        private void TxtBaseitemTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _baseitem[comboName.SelectedIndex] = txt_baseitem.Text;
        }

        private void TxtMinIlevelTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _minIlevel[comboName.SelectedIndex] = txt_min_ilevel.Text;
        }

        private void TxtMaxIlevelTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _maxIlevel[comboName.SelectedIndex] = txt_max_ilevel.Text;
        }

        private void TxtImageInventoryTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _imageInventory[comboName.SelectedIndex] = txt_image_inventory.Text;
        }

        private void TxtImageInventorySizeWTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _imageInventorySizeW[comboName.SelectedIndex] = txt_image_inventory_size_w.Text;
        }

        private void TxtImageInventorySizeHTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _imageInventorySizeH[comboName.SelectedIndex] = txt_image_inventory_size_h.Text;
        }

        private void TxtModelPlayerTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _modelPlayer[comboName.SelectedIndex] = txt_model_player.Text;
        }

        private void TxtAttachToHandsTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _attachToHands[comboName.SelectedIndex] = txt_attach_to_hands.Text;
        }

        private void TxtItemTypeNameTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _itemTypeName[comboName.SelectedIndex] = txt_item_type_name.Text;
        }

        private void TxtCraftClassTextChanged(object sender, EventArgs e)
        {
            if (comboName.SelectedIndex == -1) return;
            _craftClass[comboName.SelectedIndex] = txt_craft_class.Text;
        }

        #endregion
    }
}