namespace TF2Items.Dialogs
{
    partial class AddSetWindow
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameReference = new System.Windows.Forms.TextBox();
            this.btnEditNameReference = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create a new set";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 52);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(193, 23);
            this.txtName.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtName, "The name used to identify the set in items_game.txt (for example \"My Set\")");
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(131, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 126);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "In-game name reference";
            // 
            // txtNameReference
            // 
            this.txtNameReference.Location = new System.Drawing.Point(13, 97);
            this.txtNameReference.Name = "txtNameReference";
            this.txtNameReference.Size = new System.Drawing.Size(145, 23);
            this.txtNameReference.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtNameReference, "The name of a reference in tf_english.txt which contains the name of this set to " +
                    "display it in-game (for example \"#My_Set\")");
            // 
            // btnEditNameReference
            // 
            this.btnEditNameReference.Location = new System.Drawing.Point(163, 97);
            this.btnEditNameReference.Name = "btnEditNameReference";
            this.btnEditNameReference.Size = new System.Drawing.Size(42, 23);
            this.btnEditNameReference.TabIndex = 6;
            this.btnEditNameReference.Text = "Edit";
            this.btnEditNameReference.UseVisualStyleBackColor = true;
            this.btnEditNameReference.Click += new System.EventHandler(this.btnEditNameReference_Click);
            // 
            // AddSetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 157);
            this.Controls.Add(this.btnEditNameReference);
            this.Controls.Add(this.txtNameReference);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSetWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TF2 Items Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddSetWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNameReference;
        private System.Windows.Forms.Button btnEditNameReference;
    }
}