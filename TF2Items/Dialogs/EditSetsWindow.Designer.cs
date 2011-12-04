namespace TF2Items.Dialogs
{
    partial class EditSetsWindow
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
            this.listSets = new System.Windows.Forms.ListBox();
            this.btnAddSet = new System.Windows.Forms.Button();
            this.btnRemoveSet = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Edit item sets";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label2.Location = new System.Drawing.Point(13, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Use the list to select a set to edit.";
            // 
            // listSets
            // 
            this.listSets.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listSets.FormattingEnabled = true;
            this.listSets.ItemHeight = 15;
            this.listSets.Location = new System.Drawing.Point(13, 57);
            this.listSets.Name = "listSets";
            this.listSets.Size = new System.Drawing.Size(259, 169);
            this.listSets.TabIndex = 2;
            // 
            // btnAddSet
            // 
            this.btnAddSet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddSet.Location = new System.Drawing.Point(13, 233);
            this.btnAddSet.Name = "btnAddSet";
            this.btnAddSet.Size = new System.Drawing.Size(47, 23);
            this.btnAddSet.TabIndex = 3;
            this.btnAddSet.Text = "Add";
            this.btnAddSet.UseVisualStyleBackColor = true;
            this.btnAddSet.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRemoveSet
            // 
            this.btnRemoveSet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRemoveSet.Location = new System.Drawing.Point(66, 233);
            this.btnRemoveSet.Name = "btnRemoveSet";
            this.btnRemoveSet.Size = new System.Drawing.Size(62, 23);
            this.btnRemoveSet.TabIndex = 4;
            this.btnRemoveSet.Text = "Remove";
            this.btnRemoveSet.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.Location = new System.Drawing.Point(218, 232);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(53, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // EditSetsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRemoveSet);
            this.Controls.Add(this.btnAddSet);
            this.Controls.Add(this.listSets);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditSetsWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TF2 Items Editor";
            this.Load += new System.EventHandler(this.EditSetsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listSets;
        private System.Windows.Forms.Button btnAddSet;
        private System.Windows.Forms.Button btnRemoveSet;
        private System.Windows.Forms.Button btnClose;
    }
}