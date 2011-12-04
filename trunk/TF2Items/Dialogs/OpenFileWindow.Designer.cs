namespace TF2Items.Dialogs
{
    partial class OpenFileWindow
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
            this.labelOpen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioGcf = new System.Windows.Forms.RadioButton();
            this.radioSteamapps = new System.Windows.Forms.RadioButton();
            this.radioManual = new System.Windows.Forms.RadioButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // labelOpen
            // 
            this.labelOpen.AutoSize = true;
            this.labelOpen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.labelOpen.ForeColor = System.Drawing.Color.Blue;
            this.labelOpen.Location = new System.Drawing.Point(12, 9);
            this.labelOpen.Name = "labelOpen";
            this.labelOpen.Size = new System.Drawing.Size(95, 21);
            this.labelOpen.TabIndex = 0;
            this.labelOpen.Text = "Open <file>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "How would you like to open this file?";
            // 
            // radioGcf
            // 
            this.radioGcf.AutoSize = true;
            this.radioGcf.Location = new System.Drawing.Point(26, 52);
            this.radioGcf.Name = "radioGcf";
            this.radioGcf.Size = new System.Drawing.Size(349, 19);
            this.radioGcf.TabIndex = 2;
            this.radioGcf.TabStop = true;
            this.radioGcf.Text = "Extract from .gcf to steamapps/USERNAME/team fortress 2/...";
            this.radioGcf.UseVisualStyleBackColor = true;
            this.radioGcf.CheckedChanged += new System.EventHandler(this.radioGcf_CheckedChanged);
            // 
            // radioSteamapps
            // 
            this.radioSteamapps.AutoSize = true;
            this.radioSteamapps.Location = new System.Drawing.Point(26, 77);
            this.radioSteamapps.Name = "radioSteamapps";
            this.radioSteamapps.Size = new System.Drawing.Size(284, 19);
            this.radioSteamapps.TabIndex = 3;
            this.radioSteamapps.TabStop = true;
            this.radioSteamapps.Text = "Find in steamapps/USERNAME/team fortress 2/...\r\n";
            this.radioSteamapps.UseVisualStyleBackColor = true;
            this.radioSteamapps.CheckedChanged += new System.EventHandler(this.radioSteamapps_CheckedChanged);
            // 
            // radioManual
            // 
            this.radioManual.AutoSize = true;
            this.radioManual.Location = new System.Drawing.Point(26, 102);
            this.radioManual.Name = "radioManual";
            this.radioManual.Size = new System.Drawing.Size(137, 19);
            this.radioManual.TabIndex = 4;
            this.radioManual.TabStop = true;
            this.radioManual.Text = "Manually find the file";
            this.radioManual.UseVisualStyleBackColor = true;
            this.radioManual.CheckedChanged += new System.EventHandler(this.radioManual_CheckedChanged);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(306, 122);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(225, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select your steamapps folder";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // OpenFileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 151);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.radioManual);
            this.Controls.Add(this.radioSteamapps);
            this.Controls.Add(this.radioGcf);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelOpen);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenFileWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TF2 Items Editor";
            this.Load += new System.EventHandler(this.OpenFileDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioGcf;
        private System.Windows.Forms.RadioButton radioSteamapps;
        private System.Windows.Forms.RadioButton radioManual;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}