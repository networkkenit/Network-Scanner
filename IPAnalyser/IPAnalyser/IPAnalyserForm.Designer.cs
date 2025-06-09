namespace IPAnalyser
{
    partial class IPAnalyserForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Broadcast_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ip_class_textBox = new System.Windows.Forms.TextBox();
            this.recommend = new System.Windows.Forms.Label();
            this.start_ip_textBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.end_ip_textBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.net_id_textBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.net_id_count_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.max_host_textBox = new System.Windows.Forms.TextBox();
            this.mask_bits_trackBar = new System.Windows.Forms.TrackBar();
            this.mask_bits_richTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mask_textBox = new System.Windows.Forms.TextBox();
            this.Ip_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Ip_textBox = new System.Windows.Forms.TextBox();
            this.choose_ip_radioButton = new System.Windows.Forms.RadioButton();
            this.Enter_ip_radioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mask_bits_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Broadcast_tb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.ip_class_textBox);
            this.groupBox1.Controls.Add(this.recommend);
            this.groupBox1.Controls.Add(this.start_ip_textBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.end_ip_textBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.net_id_textBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.net_id_count_textBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.max_host_textBox);
            this.groupBox1.Controls.Add(this.mask_bits_trackBar);
            this.groupBox1.Controls.Add(this.mask_bits_richTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.mask_textBox);
            this.groupBox1.Controls.Add(this.Ip_comboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Ip_textBox);
            this.groupBox1.Controls.Add(this.choose_ip_radioButton);
            this.groupBox1.Controls.Add(this.Enter_ip_radioButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(807, 468);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Net ID :";
            // 
            // Broadcast_tb
            // 
            this.Broadcast_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Broadcast_tb.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Broadcast_tb.Location = new System.Drawing.Point(538, 363);
            this.Broadcast_tb.Name = "Broadcast_tb";
            this.Broadcast_tb.ReadOnly = true;
            this.Broadcast_tb.Size = new System.Drawing.Size(232, 27);
            this.Broadcast_tb.TabIndex = 42;
            this.Broadcast_tb.Text = "0.0.0.0";
            this.Broadcast_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(400, 362);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 41;
            this.label4.Text = "Braodcast  :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(14, 363);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 20);
            this.label13.TabIndex = 39;
            this.label13.Text = "IP CLass :";
            // 
            // ip_class_textBox
            // 
            this.ip_class_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_class_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ip_class_textBox.Location = new System.Drawing.Point(152, 362);
            this.ip_class_textBox.Name = "ip_class_textBox";
            this.ip_class_textBox.ReadOnly = true;
            this.ip_class_textBox.Size = new System.Drawing.Size(232, 27);
            this.ip_class_textBox.TabIndex = 38;
            this.ip_class_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // recommend
            // 
            this.recommend.AutoSize = true;
            this.recommend.BackColor = System.Drawing.Color.White;
            this.recommend.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.recommend.ForeColor = System.Drawing.Color.White;
            this.recommend.Location = new System.Drawing.Point(400, 429);
            this.recommend.Name = "recommend";
            this.recommend.Size = new System.Drawing.Size(0, 20);
            this.recommend.TabIndex = 37;
            // 
            // start_ip_textBox
            // 
            this.start_ip_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.start_ip_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.start_ip_textBox.Location = new System.Drawing.Point(538, 268);
            this.start_ip_textBox.Name = "start_ip_textBox";
            this.start_ip_textBox.ReadOnly = true;
            this.start_ip_textBox.Size = new System.Drawing.Size(232, 27);
            this.start_ip_textBox.TabIndex = 36;
            this.start_ip_textBox.Text = "0.0.0.0";
            this.start_ip_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(400, 318);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "Last IP : ";
            // 
            // end_ip_textBox
            // 
            this.end_ip_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.end_ip_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.end_ip_textBox.Location = new System.Drawing.Point(538, 317);
            this.end_ip_textBox.Name = "end_ip_textBox";
            this.end_ip_textBox.ReadOnly = true;
            this.end_ip_textBox.Size = new System.Drawing.Size(232, 27);
            this.end_ip_textBox.TabIndex = 34;
            this.end_ip_textBox.Text = "0.0.0.0";
            this.end_ip_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(400, 268);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 20);
            this.label10.TabIndex = 33;
            this.label10.Text = "First IP  :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(400, 222);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 20);
            this.label11.TabIndex = 31;
            this.label11.Text = "Net ID :";
            // 
            // net_id_textBox
            // 
            this.net_id_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.net_id_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.net_id_textBox.Location = new System.Drawing.Point(538, 222);
            this.net_id_textBox.Name = "net_id_textBox";
            this.net_id_textBox.ReadOnly = true;
            this.net_id_textBox.Size = new System.Drawing.Size(232, 27);
            this.net_id_textBox.TabIndex = 30;
            this.net_id_textBox.Text = "0.0.0.0";
            this.net_id_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(14, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 20);
            this.label8.TabIndex = 29;
            this.label8.Text = "Max ID Count :";
            // 
            // net_id_count_textBox
            // 
            this.net_id_count_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.net_id_count_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.net_id_count_textBox.Location = new System.Drawing.Point(152, 314);
            this.net_id_count_textBox.Name = "net_id_count_textBox";
            this.net_id_count_textBox.ReadOnly = true;
            this.net_id_count_textBox.Size = new System.Drawing.Size(232, 27);
            this.net_id_count_textBox.TabIndex = 28;
            this.net_id_count_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(14, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 20);
            this.label7.TabIndex = 27;
            this.label7.Text = "Max Hosts Count  :";
            // 
            // max_host_textBox
            // 
            this.max_host_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.max_host_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.max_host_textBox.Location = new System.Drawing.Point(152, 264);
            this.max_host_textBox.Name = "max_host_textBox";
            this.max_host_textBox.ReadOnly = true;
            this.max_host_textBox.Size = new System.Drawing.Size(232, 27);
            this.max_host_textBox.TabIndex = 26;
            this.max_host_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mask_bits_trackBar
            // 
            this.mask_bits_trackBar.Enabled = false;
            this.mask_bits_trackBar.LargeChange = 1;
            this.mask_bits_trackBar.Location = new System.Drawing.Point(12, 174);
            this.mask_bits_trackBar.Maximum = 32;
            this.mask_bits_trackBar.Name = "mask_bits_trackBar";
            this.mask_bits_trackBar.Size = new System.Drawing.Size(636, 45);
            this.mask_bits_trackBar.TabIndex = 21;
            this.mask_bits_trackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.mask_bits_trackBar.Scroll += new System.EventHandler(this.mask_bits_trackBar_Scroll);
            // 
            // mask_bits_richTextBox
            // 
            this.mask_bits_richTextBox.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mask_bits_richTextBox.Location = new System.Drawing.Point(33, 118);
            this.mask_bits_richTextBox.Name = "mask_bits_richTextBox";
            this.mask_bits_richTextBox.ReadOnly = true;
            this.mask_bits_richTextBox.Size = new System.Drawing.Size(615, 50);
            this.mask_bits_richTextBox.TabIndex = 20;
            this.mask_bits_richTextBox.Text = "00000000000000000000000000000000";
            this.mask_bits_richTextBox.TextChanged += new System.EventHandler(this.mask_bits_richTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(14, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Subnet Mask  :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(664, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 31);
            this.label5.TabIndex = 22;
            this.label5.Text = "/ 0";
            // 
            // mask_textBox
            // 
            this.mask_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mask_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mask_textBox.Location = new System.Drawing.Point(152, 219);
            this.mask_textBox.Name = "mask_textBox";
            this.mask_textBox.ReadOnly = true;
            this.mask_textBox.Size = new System.Drawing.Size(232, 27);
            this.mask_textBox.TabIndex = 23;
            this.mask_textBox.Text = "0.0.0.0";
            this.mask_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Ip_comboBox
            // 
            this.Ip_comboBox.Enabled = false;
            this.Ip_comboBox.FormattingEnabled = true;
            this.Ip_comboBox.Location = new System.Drawing.Point(214, 63);
            this.Ip_comboBox.Name = "Ip_comboBox";
            this.Ip_comboBox.Size = new System.Drawing.Size(232, 23);
            this.Ip_comboBox.TabIndex = 5;
            this.Ip_comboBox.SelectedIndexChanged += new System.EventHandler(this.Ip_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(140, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP  :";
            // 
            // Ip_textBox
            // 
            this.Ip_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ip_textBox.Location = new System.Drawing.Point(214, 31);
            this.Ip_textBox.Name = "Ip_textBox";
            this.Ip_textBox.Size = new System.Drawing.Size(232, 23);
            this.Ip_textBox.TabIndex = 1;
            this.Ip_textBox.TextChanged += new System.EventHandler(this.Ip_textBox_TextChanged);
            // 
            // choose_ip_radioButton
            // 
            this.choose_ip_radioButton.AutoSize = true;
            this.choose_ip_radioButton.Location = new System.Drawing.Point(12, 65);
            this.choose_ip_radioButton.Name = "choose_ip_radioButton";
            this.choose_ip_radioButton.Size = new System.Drawing.Size(78, 19);
            this.choose_ip_radioButton.TabIndex = 2;
            this.choose_ip_radioButton.TabStop = true;
            this.choose_ip_radioButton.Text = "Choose IP";
            this.choose_ip_radioButton.UseVisualStyleBackColor = true;
            // 
            // Enter_ip_radioButton
            // 
            this.Enter_ip_radioButton.AutoSize = true;
            this.Enter_ip_radioButton.Checked = true;
            this.Enter_ip_radioButton.Location = new System.Drawing.Point(12, 31);
            this.Enter_ip_radioButton.Name = "Enter_ip_radioButton";
            this.Enter_ip_radioButton.Size = new System.Drawing.Size(65, 19);
            this.Enter_ip_radioButton.TabIndex = 3;
            this.Enter_ip_radioButton.TabStop = true;
            this.Enter_ip_radioButton.Text = "Enter IP";
            this.Enter_ip_radioButton.UseVisualStyleBackColor = true;
            this.Enter_ip_radioButton.CheckedChanged += new System.EventHandler(this.Enter_ip_radioButton_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(140, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP :";
            // 
            // IPAnalyserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 492);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IPAnalyserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IPAnalyserForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mask_bits_trackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Label label13;
        private TextBox ip_class_textBox;
        private Label recommend;
        private TextBox start_ip_textBox;
        private Label label9;
        private TextBox end_ip_textBox;
        private Label label10;
        private Label label11;
        private TextBox net_id_textBox;
        private Label label8;
        private TextBox net_id_count_textBox;
        private Label label7;
        private TextBox max_host_textBox;
        private TrackBar mask_bits_trackBar;
        private RichTextBox mask_bits_richTextBox;
        private Label label3;
        private Label label5;
        private TextBox mask_textBox;
        private ComboBox Ip_comboBox;
        private Label label1;
        private TextBox Ip_textBox;
        private RadioButton choose_ip_radioButton;
        private RadioButton Enter_ip_radioButton;
        private Label label2;
        private TextBox Broadcast_tb;
        private Label label4;
    }
}