namespace TF2Items.Dialogs
{
    partial class AttributesWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributesWindow));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.listAttribs = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(201, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Drag-and-drop an attribute to add it.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Search";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(12, 54);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(295, 23);
			this.textBox1.TabIndex = 2;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// listAttribs
			// 
			this.listAttribs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listAttribs.FormattingEnabled = true;
			this.listAttribs.IntegralHeight = false;
			this.listAttribs.ItemHeight = 15;
			this.listAttribs.Location = new System.Drawing.Point(12, 98);
			this.listAttribs.Name = "listAttribs";
			this.listAttribs.Size = new System.Drawing.Size(295, 323);
			this.listAttribs.TabIndex = 3;
			this.listAttribs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listAttribs_MouseDown);
			this.listAttribs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listAttribs_MouseMove);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 21);
			this.label3.TabIndex = 4;
			this.label3.Text = "Attributes";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Transparent;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ImageIndex = 0;
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new System.Drawing.Point(289, 55);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(15, 16);
			this.button1.TabIndex = 5;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "cross.png");
			// 
			// AttributesWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(319, 433);
			this.ControlBox = false;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listAttribs);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AttributesWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "TF2 Items Editor";
			this.Load += new System.EventHandler(this.AttributesWindow_Load);
			this.Shown += new System.EventHandler(this.AttributesWindow_Shown);
			this.Move += new System.EventHandler(this.AttributesWindow_Move);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listAttribs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ImageList imageList1;
    }
}