using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TF2Items.Properties;


namespace TF2Items.Dialogs
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
        private void ChangeSteamappsFolder()
        {
            while (true)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(folderBrowserDialog1.SelectedPath + @"\team fortress 2 content.gcf"))
                    {
                        var res = MessageBox.Show(
                            "team fortress 2 content.gcf could not be found in that folder!\r\nPlease select your steamapps folder!",
                            "TF2 Items Editor",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error);
                        if (res == DialogResult.OK) continue;
                        return;
                    }
                    Settings.Default.SteamFolder = folderBrowserDialog1.SelectedPath;
                    Settings.Default.Save();
                }
                break;
            }
            string[] dirs = Directory.GetDirectories(Settings.Default.SteamFolder);
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
                MessageBox.Show("No user folders found in steamapps!",
                                "TF2 Items Editor",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
        }

        private void btnEditSteamappsFolder_Click(object sender, EventArgs e)
        {
            ChangeSteamappsFolder();
            txtSteamappsFolder.Text = Settings.Default.SteamFolder;
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            Settings.Default.Upgrade();
            Settings.Default.Reload();
            txtSteamappsFolder.Text = Settings.Default.SteamFolder;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
