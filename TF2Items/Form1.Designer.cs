namespace TF2Items
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnOpen = new System.Windows.Forms.Button();
            this.filediagOpen = new System.Windows.Forms.OpenFileDialog();
            this.comboName = new System.Windows.Forms.ComboBox();
            this.txt_item_class = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_craft_class = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_item_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_item_slot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_item_quality = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_baseitem = new System.Windows.Forms.TextBox();
            this.txt_min_ilevel = new System.Windows.Forms.TextBox();
            this.txt_max_ilevel = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_image_inventory = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_image_inventory_size_w = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_image_inventory_size_h = new System.Windows.Forms.TextBox();
            this.txt_model_player = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_attach_to_hands = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.list_used_by_classes = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.list_available_classes = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.move_left_btn = new System.Windows.Forms.Button();
            this.move_right_btn = new System.Windows.Forms.Button();
            this.grid_attribs = new System.Windows.Forms.DataGridView();
            this.data_hdr_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grid_hdr_class = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grid_hdr_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.list_all_attribs = new System.Windows.Forms.ListBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.filediagSave = new System.Windows.Forms.SaveFileDialog();
            this.progressReading = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_item_type_name = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.ListToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grid_attribs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(49, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.button1_Click);
            // 
            // filediagOpen
            // 
            this.filediagOpen.FileName = "items_game.txt";
            // 
            // comboName
            // 
            this.comboName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboName.DropDownHeight = 503;
            this.comboName.FormattingEnabled = true;
            this.comboName.IntegralHeight = false;
            this.comboName.ItemHeight = 13;
            this.comboName.Location = new System.Drawing.Point(12, 41);
            this.comboName.Name = "comboName";
            this.comboName.Size = new System.Drawing.Size(209, 21);
            this.comboName.TabIndex = 4;
            this.comboName.SelectedIndexChanged += new System.EventHandler(this.comboName_SelectedIndexChanged);
            this.comboName.TextUpdate += new System.EventHandler(this.comboName_TextUpdate);
            // 
            // txt_item_class
            // 
            this.txt_item_class.Location = new System.Drawing.Point(6, 34);
            this.txt_item_class.Name = "txt_item_class";
            this.txt_item_class.Size = new System.Drawing.Size(208, 20);
            this.txt_item_class.TabIndex = 5;
            this.txt_item_class.TextChanged += new System.EventHandler(this.txt_item_class_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Item class";
            // 
            // txt_craft_class
            // 
            this.txt_craft_class.Location = new System.Drawing.Point(6, 73);
            this.txt_craft_class.Name = "txt_craft_class";
            this.txt_craft_class.Size = new System.Drawing.Size(208, 20);
            this.txt_craft_class.TabIndex = 6;
            this.txt_craft_class.TextChanged += new System.EventHandler(this.txt_craft_class_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Craft class";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Item name";
            // 
            // txt_item_name
            // 
            this.txt_item_name.Location = new System.Drawing.Point(6, 151);
            this.txt_item_name.Name = "txt_item_name";
            this.txt_item_name.Size = new System.Drawing.Size(208, 20);
            this.txt_item_name.TabIndex = 8;
            this.txt_item_name.TextChanged += new System.EventHandler(this.txt_item_name_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Item slot";
            // 
            // txt_item_slot
            // 
            this.txt_item_slot.Location = new System.Drawing.Point(6, 190);
            this.txt_item_slot.Name = "txt_item_slot";
            this.txt_item_slot.Size = new System.Drawing.Size(208, 20);
            this.txt_item_slot.TabIndex = 9;
            this.txt_item_slot.TextChanged += new System.EventHandler(this.txt_item_slot_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Item quality";
            // 
            // txt_item_quality
            // 
            this.txt_item_quality.Location = new System.Drawing.Point(6, 229);
            this.txt_item_quality.Name = "txt_item_quality";
            this.txt_item_quality.Size = new System.Drawing.Size(208, 20);
            this.txt_item_quality.TabIndex = 10;
            this.txt_item_quality.TextChanged += new System.EventHandler(this.txt_item_quality_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(223, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Baseitem";
            // 
            // txt_baseitem
            // 
            this.txt_baseitem.Location = new System.Drawing.Point(223, 229);
            this.txt_baseitem.Name = "txt_baseitem";
            this.txt_baseitem.Size = new System.Drawing.Size(61, 20);
            this.txt_baseitem.TabIndex = 16;
            this.txt_baseitem.TextChanged += new System.EventHandler(this.txt_baseitem_TextChanged);
            // 
            // txt_min_ilevel
            // 
            this.txt_min_ilevel.Location = new System.Drawing.Point(290, 229);
            this.txt_min_ilevel.Name = "txt_min_ilevel";
            this.txt_min_ilevel.Size = new System.Drawing.Size(65, 20);
            this.txt_min_ilevel.TabIndex = 17;
            this.txt_min_ilevel.TextChanged += new System.EventHandler(this.txt_min_ilevel_TextChanged);
            // 
            // txt_max_ilevel
            // 
            this.txt_max_ilevel.Location = new System.Drawing.Point(361, 229);
            this.txt_max_ilevel.Name = "txt_max_ilevel";
            this.txt_max_ilevel.Size = new System.Drawing.Size(71, 20);
            this.txt_max_ilevel.TabIndex = 18;
            this.txt_max_ilevel.TextChanged += new System.EventHandler(this.txt_max_ilevel_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(290, 213);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Min level";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(361, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Max level";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(223, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Inventory image";
            // 
            // txt_image_inventory
            // 
            this.txt_image_inventory.Location = new System.Drawing.Point(223, 30);
            this.txt_image_inventory.Name = "txt_image_inventory";
            this.txt_image_inventory.Size = new System.Drawing.Size(209, 20);
            this.txt_image_inventory.TabIndex = 11;
            this.txt_image_inventory.TextChanged += new System.EventHandler(this.txt_image_inventory_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(223, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Inventory image width";
            // 
            // txt_image_inventory_size_w
            // 
            this.txt_image_inventory_size_w.Location = new System.Drawing.Point(223, 73);
            this.txt_image_inventory_size_w.Name = "txt_image_inventory_size_w";
            this.txt_image_inventory_size_w.Size = new System.Drawing.Size(209, 20);
            this.txt_image_inventory_size_w.TabIndex = 12;
            this.txt_image_inventory_size_w.TextChanged += new System.EventHandler(this.txt_image_inventory_size_w_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(223, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Inventory image height";
            // 
            // txt_image_inventory_size_h
            // 
            this.txt_image_inventory_size_h.Location = new System.Drawing.Point(223, 112);
            this.txt_image_inventory_size_h.Name = "txt_image_inventory_size_h";
            this.txt_image_inventory_size_h.Size = new System.Drawing.Size(209, 20);
            this.txt_image_inventory_size_h.TabIndex = 13;
            this.txt_image_inventory_size_h.TextChanged += new System.EventHandler(this.txt_image_inventory_size_h_TextChanged);
            // 
            // txt_model_player
            // 
            this.txt_model_player.Location = new System.Drawing.Point(223, 151);
            this.txt_model_player.Name = "txt_model_player";
            this.txt_model_player.Size = new System.Drawing.Size(209, 20);
            this.txt_model_player.TabIndex = 14;
            this.txt_model_player.TextChanged += new System.EventHandler(this.txt_model_player_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(223, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Player model";
            // 
            // txt_attach_to_hands
            // 
            this.txt_attach_to_hands.Location = new System.Drawing.Point(223, 190);
            this.txt_attach_to_hands.Name = "txt_attach_to_hands";
            this.txt_attach_to_hands.Size = new System.Drawing.Size(209, 20);
            this.txt_attach_to_hands.TabIndex = 15;
            this.txt_attach_to_hands.TextChanged += new System.EventHandler(this.txt_attach_to_hands_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(223, 174);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Attach to hands";
            // 
            // list_used_by_classes
            // 
            this.list_used_by_classes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.list_used_by_classes.FormattingEnabled = true;
            this.list_used_by_classes.Location = new System.Drawing.Point(12, 31);
            this.list_used_by_classes.Name = "list_used_by_classes";
            this.list_used_by_classes.Size = new System.Drawing.Size(85, 121);
            this.list_used_by_classes.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(84, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Used by classes";
            // 
            // list_available_classes
            // 
            this.list_available_classes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.list_available_classes.FormattingEnabled = true;
            this.list_available_classes.Location = new System.Drawing.Point(158, 31);
            this.list_available_classes.Name = "list_available_classes";
            this.list_available_classes.Size = new System.Drawing.Size(85, 121);
            this.list_available_classes.TabIndex = 22;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(155, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "Available classes";
            // 
            // move_left_btn
            // 
            this.move_left_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.move_left_btn.Location = new System.Drawing.Point(103, 100);
            this.move_left_btn.Name = "move_left_btn";
            this.move_left_btn.Size = new System.Drawing.Size(49, 23);
            this.move_left_btn.TabIndex = 20;
            this.move_left_btn.Text = "<-";
            this.move_left_btn.UseVisualStyleBackColor = true;
            this.move_left_btn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // move_right_btn
            // 
            this.move_right_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.move_right_btn.Location = new System.Drawing.Point(103, 129);
            this.move_right_btn.Name = "move_right_btn";
            this.move_right_btn.Size = new System.Drawing.Size(49, 23);
            this.move_right_btn.TabIndex = 21;
            this.move_right_btn.Text = "->";
            this.move_right_btn.UseVisualStyleBackColor = true;
            this.move_right_btn.Click += new System.EventHandler(this.move_right_btn_Click);
            // 
            // grid_attribs
            // 
            this.grid_attribs.AllowDrop = true;
            this.grid_attribs.AllowUserToAddRows = false;
            this.grid_attribs.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grid_attribs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_attribs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.data_hdr_name,
            this.grid_hdr_class,
            this.grid_hdr_value});
            this.grid_attribs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.grid_attribs.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.grid_attribs.Location = new System.Drawing.Point(5, 19);
            this.grid_attribs.Name = "grid_attribs";
            this.grid_attribs.RowHeadersWidth = 25;
            this.grid_attribs.ShowCellErrors = false;
            this.grid_attribs.ShowCellToolTips = false;
            this.grid_attribs.ShowEditingIcon = false;
            this.grid_attribs.ShowRowErrors = false;
            this.grid_attribs.Size = new System.Drawing.Size(488, 215);
            this.grid_attribs.TabIndex = 23;
            this.grid_attribs.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_attribs_CellValueChanged);
            this.grid_attribs.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.grid_attribs_UserDeletedRow);
            // 
            // data_hdr_name
            // 
            this.data_hdr_name.HeaderText = "Name";
            this.data_hdr_name.Name = "data_hdr_name";
            this.data_hdr_name.ReadOnly = true;
            this.data_hdr_name.Width = 200;
            // 
            // grid_hdr_class
            // 
            this.grid_hdr_class.HeaderText = "Attribute Class";
            this.grid_hdr_class.Name = "grid_hdr_class";
            this.grid_hdr_class.ReadOnly = true;
            this.grid_hdr_class.Width = 200;
            // 
            // grid_hdr_value
            // 
            this.grid_hdr_value.HeaderText = "Value";
            this.grid_hdr_value.Name = "grid_hdr_value";
            this.grid_hdr_value.Width = 60;
            // 
            // list_all_attribs
            // 
            this.list_all_attribs.FormattingEnabled = true;
            this.list_all_attribs.Location = new System.Drawing.Point(499, 74);
            this.list_all_attribs.Name = "list_all_attribs";
            this.list_all_attribs.Size = new System.Drawing.Size(188, 160);
            this.list_all_attribs.Sorted = true;
            this.list_all_attribs.TabIndex = 24;
            this.list_all_attribs.DoubleClick += new System.EventHandler(this.list_all_attribs_DoubleClick);
            this.list_all_attribs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.list_all_attribs_MouseMove_1);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(499, 58);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(163, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "All attributes - double-click to add";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(523, 105);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(179, 26);
            this.label19.TabIndex = 11;
            this.label19.Text = "Created by bogeyman_EST, 2010\r\nGCF extracter thanks to Nem\'s tools:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(67, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(115, 12);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(28, 23);
            this.btnSaveAs.TabIndex = 3;
            this.btnSaveAs.Text = "As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Visible = false;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // progressReading
            // 
            this.progressReading.Location = new System.Drawing.Point(12, 2);
            this.progressReading.MarqueeAnimationSpeed = 1;
            this.progressReading.Name = "progressReading";
            this.progressReading.Size = new System.Drawing.Size(696, 10);
            this.progressReading.Step = 1;
            this.progressReading.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_item_type_name);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_max_ilevel);
            this.groupBox1.Controls.Add(this.txt_min_ilevel);
            this.groupBox1.Controls.Add(this.txt_attach_to_hands);
            this.groupBox1.Controls.Add(this.txt_baseitem);
            this.groupBox1.Controls.Add(this.txt_model_player);
            this.groupBox1.Controls.Add(this.txt_image_inventory_size_h);
            this.groupBox1.Controls.Add(this.txt_image_inventory_size_w);
            this.groupBox1.Controls.Add(this.txt_image_inventory);
            this.groupBox1.Controls.Add(this.txt_item_quality);
            this.groupBox1.Controls.Add(this.txt_item_slot);
            this.groupBox1.Controls.Add(this.txt_item_name);
            this.groupBox1.Controls.Add(this.txt_craft_class);
            this.groupBox1.Controls.Add(this.txt_item_class);
            this.groupBox1.Location = new System.Drawing.Point(15, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 260);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Item type";
            // 
            // txt_item_type_name
            // 
            this.txt_item_type_name.Location = new System.Drawing.Point(6, 113);
            this.txt_item_type_name.Name = "txt_item_type_name";
            this.txt_item_type_name.Size = new System.Drawing.Size(208, 20);
            this.txt_item_type_name.TabIndex = 19;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.move_right_btn);
            this.groupBox2.Controls.Add(this.move_left_btn);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.list_available_classes);
            this.groupBox2.Controls.Add(this.list_used_by_classes);
            this.groupBox2.Location = new System.Drawing.Point(459, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 158);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Classes";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.searchBox);
            this.groupBox3.Controls.Add(this.list_all_attribs);
            this.groupBox3.Controls.Add(this.grid_attribs);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Location = new System.Drawing.Point(15, 334);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(693, 242);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Attributes";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(499, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "Search";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(499, 35);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(188, 20);
            this.searchBox.TabIndex = 25;
            this.searchBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(377, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(331, 41);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(459, 132);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(248, 13);
            this.linkLabel1.TabIndex = 355;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://nemesis.thewavelength.net/index.php?p=35";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(227, 41);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(43, 21);
            this.btnCopy.TabIndex = 356;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(276, 41);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(43, 21);
            this.btnPaste.TabIndex = 357;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 581);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.comboName);
            this.Controls.Add(this.progressReading);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "TF2 Items Editor v0.8.5 R3";
            ((System.ComponentModel.ISupportInitialize)(this.grid_attribs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.OpenFileDialog filediagOpen;
        private System.Windows.Forms.ComboBox comboName;
        private System.Windows.Forms.TextBox txt_item_class;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_craft_class;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_item_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_item_slot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_item_quality;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_baseitem;
        private System.Windows.Forms.TextBox txt_min_ilevel;
        private System.Windows.Forms.TextBox txt_max_ilevel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_image_inventory;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_image_inventory_size_w;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_image_inventory_size_h;
        private System.Windows.Forms.TextBox txt_model_player;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_attach_to_hands;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox list_used_by_classes;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox list_available_classes;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button move_left_btn;
        private System.Windows.Forms.Button move_right_btn;
        private System.Windows.Forms.DataGridView grid_attribs;
        private System.Windows.Forms.ListBox list_all_attribs;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.SaveFileDialog filediagSave;
        private System.Windows.Forms.ProgressBar progressReading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_hdr_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn grid_hdr_class;
        private System.Windows.Forms.DataGridViewTextBoxColumn grid_hdr_value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_item_type_name;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.ToolTip ListToolTip;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label17;
    }
}

