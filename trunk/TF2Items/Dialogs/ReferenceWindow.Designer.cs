namespace TF2Items.Dialogs
{
    partial class ReferenceWindow
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
            this.txtReference = new System.Windows.Forms.TextBox();
            this.gridReferences = new System.Windows.Forms.DataGridView();
            this.colReference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridReferences)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose a reference";
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(12, 58);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(343, 23);
            this.txtReference.TabIndex = 1;
            this.txtReference.TextChanged += new System.EventHandler(this.txtReference_TextChanged);
            // 
            // gridReferences
            // 
            this.gridReferences.AllowUserToAddRows = false;
            this.gridReferences.AllowUserToDeleteRows = false;
            this.gridReferences.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridReferences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridReferences.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReference,
            this.colValue});
            this.gridReferences.Location = new System.Drawing.Point(12, 87);
            this.gridReferences.Name = "gridReferences";
            this.gridReferences.ReadOnly = true;
            this.gridReferences.RowHeadersVisible = false;
            this.gridReferences.Size = new System.Drawing.Size(405, 379);
            this.gridReferences.TabIndex = 2;
            this.gridReferences.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridReferences_CellDoubleClick);
            // 
            // colReference
            // 
            this.colReference.HeaderText = "Reference";
            this.colReference.Name = "colReference";
            this.colReference.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.button1.Image = global::TF2Items.Properties.Resources.add;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(361, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "New";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(268, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search for an existing reference or add a new one.";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(345, 473);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(264, 473);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Select";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ReferenceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 505);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridReferences);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReferenceWindow";
            this.ShowIcon = false;
            this.Text = "TF2 Items Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReferenceWindow_FormClosing);
            this.Load += new System.EventHandler(this.ReferenceWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridReferences)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.DataGridView gridReferences;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReference;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}