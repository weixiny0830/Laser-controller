namespace LaserController_4
{
    partial class Form2
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.BtopenCOM = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Tbbaudrate = new System.Windows.Forms.ComboBox();
            this.Tbcom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Btcalibrate = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Tbcalibrate = new System.Windows.Forms.TextBox();
            this.Lbcalibrate = new System.Windows.Forms.Label();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.BtopenCOM);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.Tbbaudrate);
            this.groupBox6.Controls.Add(this.Tbcom);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(259, 125);
            this.groupBox6.TabIndex = 41;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Laser 1";
            // 
            // BtopenCOM
            // 
            this.BtopenCOM.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtopenCOM.Location = new System.Drawing.Point(10, 85);
            this.BtopenCOM.Name = "BtopenCOM";
            this.BtopenCOM.Size = new System.Drawing.Size(230, 32);
            this.BtopenCOM.TabIndex = 8;
            this.BtopenCOM.Text = "Open COM";
            this.BtopenCOM.UseVisualStyleBackColor = true;
            this.BtopenCOM.Click += new System.EventHandler(this.BtopenCOM_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM";
            // 
            // Tbbaudrate
            // 
            this.Tbbaudrate.FormattingEnabled = true;
            this.Tbbaudrate.Location = new System.Drawing.Point(85, 52);
            this.Tbbaudrate.Name = "Tbbaudrate";
            this.Tbbaudrate.Size = new System.Drawing.Size(155, 27);
            this.Tbbaudrate.TabIndex = 5;
            // 
            // Tbcom
            // 
            this.Tbcom.FormattingEnabled = true;
            this.Tbcom.Location = new System.Drawing.Point(85, 19);
            this.Tbcom.Name = "Tbcom";
            this.Tbcom.Size = new System.Drawing.Size(155, 27);
            this.Tbcom.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Baud Rate";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.Btcalibrate);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.Tbcalibrate);
            this.groupBox4.Controls.Add(this.Lbcalibrate);
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 156);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(259, 127);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Laser Calibration";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 19);
            this.label9.TabIndex = 18;
            this.label9.Text = "Laser Power";
            // 
            // Btcalibrate
            // 
            this.Btcalibrate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btcalibrate.Location = new System.Drawing.Point(16, 66);
            this.Btcalibrate.Name = "Btcalibrate";
            this.Btcalibrate.Size = new System.Drawing.Size(225, 27);
            this.Btcalibrate.TabIndex = 29;
            this.Btcalibrate.Text = "Calibrate";
            this.Btcalibrate.UseVisualStyleBackColor = true;
            this.Btcalibrate.Click += new System.EventHandler(this.Btcalibrate_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(215, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 19);
            this.label6.TabIndex = 18;
            this.label6.Text = "mW";
            // 
            // Tbcalibrate
            // 
            this.Tbcalibrate.Location = new System.Drawing.Point(105, 26);
            this.Tbcalibrate.Name = "Tbcalibrate";
            this.Tbcalibrate.Size = new System.Drawing.Size(100, 26);
            this.Tbcalibrate.TabIndex = 18;
            // 
            // Lbcalibrate
            // 
            this.Lbcalibrate.AutoSize = true;
            this.Lbcalibrate.Location = new System.Drawing.Point(12, 97);
            this.Lbcalibrate.Name = "Lbcalibrate";
            this.Lbcalibrate.Size = new System.Drawing.Size(0, 19);
            this.Lbcalibrate.TabIndex = 30;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(73, 22);
            this.toolStripStatusLabel2.Text = "Service Time";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 290);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(288, 25);
            this.toolStrip1.TabIndex = 42;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 315);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button BtopenCOM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Tbcalibrate;
        private System.Windows.Forms.Label Lbcalibrate;
        public System.Windows.Forms.ComboBox Tbbaudrate;
        public System.Windows.Forms.ComboBox Tbcom;
        public System.Windows.Forms.Button Btcalibrate;
        private System.Windows.Forms.ToolStripLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}