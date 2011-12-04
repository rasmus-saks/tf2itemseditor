namespace TF2Items.Dialogs
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSteamappsFolder = new System.Windows.Forms.TextBox();
            this.btnEditSteamappsFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Steamapps folder";
            // 
            // txtSteamappsFolder
            // 
            this.txtSteamappsFolder.Location = new System.Drawing.Point(13, 57);
            this.txtSteamappsFolder.Name = "txtSteamappsFolder";
            this.txtSteamappsFolder.ReadOnly = true;
            this.txtSteamappsFolder.Size = new System.Drawing.Size(212, 23);
            this.txtSteamappsFolder.TabIndex = 2;
            // 
            // btnEditSteamappsFolder
            // 
            this.btnEditSteamappsFolder.Location = new System.Drawing.Point(231, 57);
            this.btnEditSteamappsFolder.Name = "btnEditSteamappsFolder";
            this.btnEditSteamappsFolder.Size = new System.Drawing.Size(45, 23);
            this.btnEditSteamappsFolder.TabIndex = 3;
            this.btnEditSteamappsFolder.Text = "Edit";
            this.btnEditSteamappsFolder.UseVisualStyleBackColor = true;
            this.btnEditSteamappsFolder.Click += new System.EventHandler(this.btnEditSteamappsFolder_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 82);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 117);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEditSteamappsFolder);
            this.Controls.Add(this.txtSteamappsFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TF2 Items Editor";
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSteamappsFolder;
        private System.Windows.Forms.Button btnEditSteamappsFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnClose;
    }
}