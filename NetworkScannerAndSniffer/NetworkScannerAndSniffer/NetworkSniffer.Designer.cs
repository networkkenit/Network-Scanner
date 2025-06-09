namespace NetworkScannerAndSniffer
{
    partial class NetworkSniffer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.form_title = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.packets_dgv = new System.Windows.Forms.DataGridView();
            this.packet = new System.Windows.Forms.DataGridViewImageColumn();
            this.Domain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.both = new System.Windows.Forms.RadioButton();
            this.IP_V6_radioButton = new System.Windows.Forms.RadioButton();
            this.IP_V4_radioButton = new System.Windows.Forms.RadioButton();
            this.ARP = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.loading_btn = new System.Windows.Forms.Button();
            this.Enter_rb = new System.Windows.Forms.RadioButton();
            this.choose_rb = new System.Windows.Forms.RadioButton();
            this.Gatway_tb = new System.Windows.Forms.TextBox();
            this.gatways_compobox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.spoofarp_toggle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Start_button = new System.Windows.Forms.Button();
            this.adapter_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.analyse_textBox = new System.Windows.Forms.RichTextBox();
            this.analyse_treeView = new System.Windows.Forms.TreeView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Up = new System.Windows.Forms.DataGridViewImageColumn();
            this.Analyse = new System.Windows.Forms.DataGridViewImageColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.GetGatwayBGW = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packets_dgv)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.ARP.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // form_title
            // 
            this.form_title.BackColor = System.Drawing.Color.Gainsboro;
            this.form_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.form_title.FlatAppearance.BorderSize = 0;
            this.form_title.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.form_title.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.form_title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form_title.Location = new System.Drawing.Point(0, 0);
            this.form_title.Name = "form_title";
            this.form_title.Size = new System.Drawing.Size(1229, 33);
            this.form_title.TabIndex = 0;
            this.form_title.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.ARP);
            this.groupBox1.Controls.Add(this.Start_button);
            this.groupBox1.Controls.Add(this.adapter_comboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1209, 482);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet Trafic";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.packets_dgv);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(296, 30);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(894, 419);
            this.panel1.TabIndex = 18;
            // 
            // packets_dgv
            // 
            this.packets_dgv.AllowUserToAddRows = false;
            this.packets_dgv.AllowUserToDeleteRows = false;
            this.packets_dgv.BackgroundColor = System.Drawing.Color.White;
            this.packets_dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.packets_dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.packets_dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.packets_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packets_dgv.ColumnHeadersVisible = false;
            this.packets_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.packet,
            this.Domain,
            this.Data,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewImageColumn1,
            this.dataGridViewImageColumn2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.packets_dgv.DefaultCellStyle = dataGridViewCellStyle1;
            this.packets_dgv.GridColor = System.Drawing.Color.White;
            this.packets_dgv.Location = new System.Drawing.Point(22, 51);
            this.packets_dgv.MultiSelect = false;
            this.packets_dgv.Name = "packets_dgv";
            this.packets_dgv.ReadOnly = true;
            this.packets_dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.packets_dgv.RowHeadersVisible = false;
            this.packets_dgv.RowTemplate.Height = 35;
            this.packets_dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.packets_dgv.Size = new System.Drawing.Size(853, 329);
            this.packets_dgv.TabIndex = 3;
            this.packets_dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packets_dgv_CellContentClick);
            this.packets_dgv.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.packets_dgv_CellMouseLeave);
            this.packets_dgv.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.packets_dgv_CellMouseMove);
            this.packets_dgv.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.packets_dgv_RowPrePaint);
            // 
            // packet
            // 
            this.packet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.packet.HeaderText = "";
            this.packet.Name = "packet";
            this.packet.ReadOnly = true;
            this.packet.Width = 5;
            // 
            // Domain
            // 
            this.Domain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Domain.HeaderText = "Domain";
            this.Domain.Name = "Domain";
            this.Domain.ReadOnly = true;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID_Packet";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 25;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 5;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(424, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(103, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 1;
            this.label5.Text = "Domain";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.both);
            this.groupBox3.Controls.Add(this.IP_V6_radioButton);
            this.groupBox3.Controls.Add(this.IP_V4_radioButton);
            this.groupBox3.Location = new System.Drawing.Point(23, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 104);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Show :";
            // 
            // both
            // 
            this.both.AutoSize = true;
            this.both.Location = new System.Drawing.Point(144, 64);
            this.both.Name = "both";
            this.both.Size = new System.Drawing.Size(50, 19);
            this.both.TabIndex = 2;
            this.both.Text = "Both";
            this.both.UseVisualStyleBackColor = true;
            // 
            // IP_V6_radioButton
            // 
            this.IP_V6_radioButton.AutoSize = true;
            this.IP_V6_radioButton.Location = new System.Drawing.Point(75, 64);
            this.IP_V6_radioButton.Name = "IP_V6_radioButton";
            this.IP_V6_radioButton.Size = new System.Drawing.Size(51, 19);
            this.IP_V6_radioButton.TabIndex = 1;
            this.IP_V6_radioButton.Text = "IP V6";
            this.IP_V6_radioButton.UseVisualStyleBackColor = true;
            // 
            // IP_V4_radioButton
            // 
            this.IP_V4_radioButton.AutoSize = true;
            this.IP_V4_radioButton.Checked = true;
            this.IP_V4_radioButton.Location = new System.Drawing.Point(12, 64);
            this.IP_V4_radioButton.Name = "IP_V4_radioButton";
            this.IP_V4_radioButton.Size = new System.Drawing.Size(51, 19);
            this.IP_V4_radioButton.TabIndex = 0;
            this.IP_V4_radioButton.TabStop = true;
            this.IP_V4_radioButton.Text = "IP V4";
            this.IP_V4_radioButton.UseVisualStyleBackColor = true;
            // 
            // ARP
            // 
            this.ARP.Controls.Add(this.groupBox4);
            this.ARP.Controls.Add(this.spoofarp_toggle);
            this.ARP.Controls.Add(this.label2);
            this.ARP.Location = new System.Drawing.Point(23, 225);
            this.ARP.Name = "ARP";
            this.ARP.Size = new System.Drawing.Size(257, 212);
            this.ARP.TabIndex = 16;
            this.ARP.TabStop = false;
            this.ARP.Text = "ARP ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.loading_btn);
            this.groupBox4.Controls.Add(this.Enter_rb);
            this.groupBox4.Controls.Add(this.choose_rb);
            this.groupBox4.Controls.Add(this.Gatway_tb);
            this.groupBox4.Controls.Add(this.gatways_compobox);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(8, 58);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(235, 135);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Gatway";
            // 
            // loading_btn
            // 
            this.loading_btn.BackColor = System.Drawing.Color.White;
            this.loading_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.loading_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.loading_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loading_btn.Image = global::NetworkScannerAndSniffer.Properties.Resources.Rolling_1s_16px;
            this.loading_btn.Location = new System.Drawing.Point(192, 96);
            this.loading_btn.Name = "loading_btn";
            this.loading_btn.Size = new System.Drawing.Size(28, 23);
            this.loading_btn.TabIndex = 19;
            this.loading_btn.UseVisualStyleBackColor = false;
            this.loading_btn.Visible = false;
            // 
            // Enter_rb
            // 
            this.Enter_rb.AutoSize = true;
            this.Enter_rb.Location = new System.Drawing.Point(143, 22);
            this.Enter_rb.Name = "Enter_rb";
            this.Enter_rb.Size = new System.Drawing.Size(52, 19);
            this.Enter_rb.TabIndex = 18;
            this.Enter_rb.Text = "Enter";
            this.Enter_rb.UseVisualStyleBackColor = true;
            // 
            // choose_rb
            // 
            this.choose_rb.AutoSize = true;
            this.choose_rb.Checked = true;
            this.choose_rb.Location = new System.Drawing.Point(25, 22);
            this.choose_rb.Name = "choose_rb";
            this.choose_rb.Size = new System.Drawing.Size(65, 19);
            this.choose_rb.TabIndex = 17;
            this.choose_rb.TabStop = true;
            this.choose_rb.Text = "Choose";
            this.choose_rb.UseVisualStyleBackColor = true;
            this.choose_rb.CheckedChanged += new System.EventHandler(this.choose_rb_CheckedChanged);
            // 
            // Gatway_tb
            // 
            this.Gatway_tb.Enabled = false;
            this.Gatway_tb.Location = new System.Drawing.Point(16, 96);
            this.Gatway_tb.Name = "Gatway_tb";
            this.Gatway_tb.Size = new System.Drawing.Size(170, 23);
            this.Gatway_tb.TabIndex = 16;
            this.Gatway_tb.TextChanged += new System.EventHandler(this.Gatway_tb_TextChanged);
            // 
            // gatways_compobox
            // 
            this.gatways_compobox.FormattingEnabled = true;
            this.gatways_compobox.Location = new System.Drawing.Point(16, 56);
            this.gatways_compobox.Name = "gatways_compobox";
            this.gatways_compobox.Size = new System.Drawing.Size(170, 23);
            this.gatways_compobox.TabIndex = 12;
            this.gatways_compobox.SelectedIndexChanged += new System.EventHandler(this.gatways_compobox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 15);
            this.label4.TabIndex = 15;
            // 
            // spoofarp_toggle
            // 
            this.spoofarp_toggle.FlatAppearance.BorderSize = 0;
            this.spoofarp_toggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.spoofarp_toggle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.spoofarp_toggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.spoofarp_toggle.Image = global::NetworkScannerAndSniffer.Properties.Resources.icons8_schalter_aus_40;
            this.spoofarp_toggle.Location = new System.Drawing.Point(119, 22);
            this.spoofarp_toggle.Name = "spoofarp_toggle";
            this.spoofarp_toggle.Size = new System.Drawing.Size(75, 30);
            this.spoofarp_toggle.TabIndex = 8;
            this.spoofarp_toggle.Tag = false;
            this.spoofarp_toggle.UseVisualStyleBackColor = true;
            this.spoofarp_toggle.Click += new System.EventHandler(this.spoofarp_toggle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = " Arp Spoofing :";
            // 
            // Start_button
            // 
            this.Start_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.Start_button.Location = new System.Drawing.Point(191, 443);
            this.Start_button.Name = "Start_button";
            this.Start_button.Size = new System.Drawing.Size(89, 33);
            this.Start_button.TabIndex = 10;
            this.Start_button.Tag = false;
            this.Start_button.Text = "Start";
            this.Start_button.UseVisualStyleBackColor = false;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // adapter_comboBox
            // 
            this.adapter_comboBox.FormattingEnabled = true;
            this.adapter_comboBox.Location = new System.Drawing.Point(47, 67);
            this.adapter_comboBox.Name = "adapter_comboBox";
            this.adapter_comboBox.Size = new System.Drawing.Size(170, 23);
            this.adapter_comboBox.TabIndex = 6;
            this.adapter_comboBox.SelectedIndexChanged += new System.EventHandler(this.adapter_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(31, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select Adapter :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.analyse_textBox);
            this.groupBox2.Controls.Add(this.analyse_treeView);
            this.groupBox2.Location = new System.Drawing.Point(6, 540);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1209, 343);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Packet Analyse";
            // 
            // analyse_textBox
            // 
            this.analyse_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.analyse_textBox.Location = new System.Drawing.Point(560, 22);
            this.analyse_textBox.Name = "analyse_textBox";
            this.analyse_textBox.ReadOnly = true;
            this.analyse_textBox.Size = new System.Drawing.Size(630, 300);
            this.analyse_textBox.TabIndex = 1;
            this.analyse_textBox.Text = "";
            // 
            // analyse_treeView
            // 
            this.analyse_treeView.Location = new System.Drawing.Point(17, 25);
            this.analyse_treeView.Name = "analyse_treeView";
            this.analyse_treeView.Size = new System.Drawing.Size(525, 300);
            this.analyse_treeView.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ID.HeaderText = "Packet_ID";
            this.ID.Name = "ID";
            // 
            // Up
            // 
            this.Up.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Up.HeaderText = "";
            this.Up.Image = global::NetworkScannerAndSniffer.Properties.Resources.icons8_hochladen_32;
            this.Up.Name = "Up";
            // 
            // Analyse
            // 
            this.Analyse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Analyse.HeaderText = "";
            this.Analyse.Image = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
            this.Analyse.Name = "Analyse";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 873);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1229, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(68, 17);
            this.status.Text = "Info Here....";
            // 
            // GetGatwayBGW
            // 
            this.GetGatwayBGW.WorkerSupportsCancellation = true;
            this.GetGatwayBGW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetGatwayBGW_DoWork);
            // 
            // NetworkSniffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 895);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.form_title);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NetworkSniffer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NetworkSniffer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetworkSniffer_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packets_dgv)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ARP.ResumeLayout(false);
            this.ARP.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button form_title;
        private GroupBox groupBox1;
        private Button spoofarp_toggle;
        private ComboBox adapter_comboBox;
        private Label label1;
        private Button Start_button;
        private Label label2;
        private Label label4;
        private ComboBox gatways_compobox;
        private GroupBox ARP;
        private GroupBox groupBox3;
        private RadioButton both;
        private RadioButton IP_V6_radioButton;
        private RadioButton IP_V4_radioButton;
        private Label label3;
        private GroupBox groupBox2;
        private RichTextBox analyse_textBox;
        private TreeView analyse_treeView;
        private Panel panel1;
        private Label label6;
        private Label label5;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewImageColumn Up;
        private DataGridViewImageColumn Analyse;
        private DataGridView packets_dgv;
        private DataGridViewImageColumn packet;
        private DataGridViewTextBoxColumn Domain;
        private DataGridViewTextBoxColumn Data;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewImageColumn dataGridViewImageColumn1;
        private DataGridViewImageColumn dataGridViewImageColumn2;
        private GroupBox groupBox4;
        private RadioButton Enter_rb;
        private RadioButton choose_rb;
        private TextBox Gatway_tb;
        private Button loading_btn;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel status;
        private System.ComponentModel.BackgroundWorker GetGatwayBGW;
    }
}