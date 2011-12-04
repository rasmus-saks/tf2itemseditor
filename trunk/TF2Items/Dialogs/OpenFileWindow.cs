using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TF2Items.Properties;


namespace TF2Items.Dialogs
{
    public partial class OpenFileWindow : Form
    {
        private string retVal = "";
        private string file = "";
        private string filePath = "";
        public OpenFileWindow()
        {
            InitializeComponent();
        }

        private void OpenFileDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
            radioGcf.Checked = false;
            radioManual.Checked = false;
            radioSteamapps.Checked = false;
            switch(Settings.Default.OpenFileSetting)
            {
                case 0:
                    radioGcf.Checked = true;
                    btnNext.Enabled = true;
                    break;
                case 1:
                    radioSteamapps.Checked = true;
                    btnNext.Enabled = true;
                    break;
                case 2:
                    radioManual.Checked = true;
                    btnNext.Enabled = true;
                    break;
            }
            

        }

        public string ShowWindow(string filename, string path)
        {
            labelOpen.Text = "Open " + filename;
            file = filename;
            filePath = path;
            Settings.Default.Upgrade();
            ShowDialog();
            Settings.Default.Save();
            return retVal;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (radioGcf.Checked) //Extract from gcf
            {
                Cursor = Cursors.WaitCursor;
                Settings.Default.OpenFileSetting = 0;
                HLLib.hlInitialize();
                HLLib.HLPackageType ePackageType = HLLib.HLPackageType.HL_PACKAGE_GCF;
                uint uiMode = (uint) HLLib.HLFileMode.HL_MODE_READ | (uint) HLLib.HLFileMode.HL_MODE_VOLATILE;
                uint uiPackage;
                HLLib.hlCreatePackage(ePackageType, out uiPackage);
                HLLib.hlBindPackage(uiPackage);
                if(String.IsNullOrEmpty(Settings.Default.SteamFolder))
                {
                    showDia:
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (!File.Exists(folderBrowserDialog1.SelectedPath + @"\team fortress 2 content.gcf"))
                        {
                            Cursor = Cursors.Arrow;
                            var res = MessageBox.Show(
                                "team fortress 2 content.gcf could not be found in that folder!\r\nPlease select your SteamApps folder!",
                                "TF2 Items Editor",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Error);
                            if (res == DialogResult.OK) goto showDia;
                            return;
                        }
                        Settings.Default.SteamFolder = folderBrowserDialog1.SelectedPath;
                        Settings.Default.Save();

                    }
                    else return;

                }
                Cursor = Cursors.WaitCursor;
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
                            if (ret == "") return;
                            Settings.Default.SteamUser = ret;
                            Settings.Default.Save();
                        }
                    }
                    else if(match.Count == 1)
                    {
                        Settings.Default.SteamUser = match[0];
                        Settings.Default.Save();
                    }
                    else
                    {
                        Cursor = Cursors.Arrow;
                        MessageBox.Show("No user folders found in steamapps!",
                                        "TF2 Items Editor",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                }
                Cursor = Cursors.WaitCursor;
                string p = Settings.Default.SteamFolder + @"\team fortress 2 content.gcf";
                HLLib.hlPackageOpenFile(p, uiMode);
                IntPtr pRoot = HLLib.hlPackageGetRoot();
                IntPtr pItem = HLLib.hlFolderGetItemByPath(pRoot, filePath + file, HLLib.HLFindType.HL_FIND_ALL);
                p = Settings.Default.SteamFolder + @"\" + Settings.Default.SteamUser +
                           @"\team fortress 2" + filePath;
                Directory.CreateDirectory(p);
                if (File.Exists(p + file))
                {
                    File.Delete(p + file);
                }
                if(!HLLib.hlItemExtract(pItem, p))
                {
                    Cursor = Cursors.Arrow;
                    MessageBox.Show("Error when extracting:\r\n" +
                                    HLLib.hlGetString(HLLib.HLOption.HL_ERROR_SHORT_FORMATED));
                    retVal = "";
                    Close();
                }
                Cursor = Cursors.WaitCursor;
                retVal = File.Exists(p + file) ? p + file : "";
                Close();
            }
            else if (radioSteamapps.Checked) //Find in steamapps
            {
                Cursor = Cursors.WaitCursor;
                Settings.Default.OpenFileSetting = 1;
                if (Settings.Default.SteamFolder == null || Settings.Default.SteamFolder.ToString() == "")
                {
                showDia:
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (!File.Exists(folderBrowserDialog1.SelectedPath + @"\team fortress 2 content.gcf"))
                        {
                            Cursor = Cursors.Arrow;
                            var res = MessageBox.Show(
                                "team fortress 2 content.gcf could not be found in that folder!\r\nPlease select your steamapps folder!",
                                "TF2 Items Editor",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Error);
                            if (res == DialogResult.OK) goto showDia;
                            return;
                        }
                        Settings.Default.SteamFolder = folderBrowserDialog1.SelectedPath;
                        Settings.Default.Save();

                    }
                    else return;

                }
                Cursor = Cursors.WaitCursor;
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
                            if (ret == "") return;
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
                        Cursor = Cursors.Arrow;
                        MessageBox.Show("No user folders found in steamapps!",
                                        "TF2 Items Editor",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                }
                string p = Settings.Default.SteamFolder + @"\" + Settings.Default.SteamUser +
                           @"\team fortress 2" + filePath + file;
                retVal = File.Exists(p) ? p : "";
                if(!File.Exists(p))
                {
                    Cursor = Cursors.Arrow;
                    MessageBox.Show("Couldn't find "+ file +"!\r\nPlease make sure it is present in " + p,
                                "TF2 Items Editor",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    return;
                }
                Close();
            }
            else if (radioManual.Checked) //Find manually
            {
                Settings.Default.OpenFileSetting = 2;
                openFileDialog1.Filter = file.Split('.')[1] + " files|*." + file.Split('.')[1];
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    retVal = openFileDialog1.FileName;
                    Close();
                }
                else
                {
                    retVal = "";
                    return;
                }
            }
        }

        private void radioGcf_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
        }

        private void radioSteamapps_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
        }

        private void radioManual_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            retVal = "";
            Close();
        }
    }
}
