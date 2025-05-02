namespace GradingSys_SIA
{
    partial class sideBarPanel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sideBarPanel));
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            label5 = new Label();
            label6 = new Label();
            label12 = new Label();
            label13 = new Label();
            panel1 = new Panel();
            panel6 = new Panel();
            panel2 = new Panel();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.ForeColor = Color.White;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(0, 389);
            label1.Name = "label1";
            label1.Size = new Size(262, 58);
            label1.TabIndex = 0;
            label1.Text = "            Aptitude";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.ForeColor = Color.White;
            label2.Image = (Image)resources.GetObject("label2.Image");
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(0, 468);
            label2.Name = "label2";
            label2.Size = new Size(262, 48);
            label2.TabIndex = 1;
            label2.Text = "            Exam / Grade";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            label2.Click += label2_Click;
            // 
            // label4
            // 
            label4.ForeColor = Color.White;
            label4.Image = (Image)resources.GetObject("label4.Image");
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(3, 536);
            label4.Name = "label4";
            label4.Size = new Size(259, 48);
            label4.TabIndex = 3;
            label4.Text = "          Exit";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            label4.Click += label4_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 56);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 93);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label5
            // 
            label5.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Gold;
            label5.Location = new Point(0, 152);
            label5.Name = "label5";
            label5.Size = new Size(262, 30);
            label5.TabIndex = 2;
            label5.Text = "QUEZON CITY UNIVERSITY";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 182);
            label6.Name = "label6";
            label6.Size = new Size(262, 24);
            label6.TabIndex = 5;
            label6.Text = "Reserve Officer's Training Corps";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label12
            // 
            label12.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.White;
            label12.Location = new Point(3, 219);
            label12.Name = "label12";
            label12.Size = new Size(259, 38);
            label12.TabIndex = 6;
            label12.Text = "______________________";
            label12.TextAlign = ContentAlignment.TopCenter;
            // 
            // label13
            // 
            label13.ForeColor = Color.White;
            label13.Image = (Image)resources.GetObject("label13.Image");
            label13.ImageAlign = ContentAlignment.MiddleLeft;
            label13.Location = new Point(0, 322);
            label13.Name = "label13";
            label13.Size = new Size(262, 50);
            label13.TabIndex = 7;
            label13.Text = "            Dashboard";
            label13.TextAlign = ContentAlignment.MiddleLeft;
            label13.Click += label13_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(75, 83, 32);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 43);
            panel1.Name = "panel1";
            panel1.Size = new Size(262, 698);
            panel1.TabIndex = 0;
            // 
            // panel6
            // 
            panel6.BackColor = Color.White;
            panel6.Location = new Point(260, 43);
            panel6.Name = "panel6";
            panel6.Size = new Size(935, 698);
            panel6.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Olive;
            panel2.Controls.Add(label7);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1195, 43);
            panel2.TabIndex = 6;
            // 
            // label7
            // 
            label7.BackColor = Color.Olive;
            label7.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ControlText;
            label7.ImageAlign = ContentAlignment.TopCenter;
            label7.Location = new Point(1142, 0);
            label7.Name = "label7";
            label7.Size = new Size(53, 40);
            label7.TabIndex = 0;
            label7.Text = "✖";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            label7.Click += label7_Click_1;
            // 
            // sideBarPanel
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(239, 239, 239);
            ClientSize = new Size(1195, 741);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(panel6);
            FormBorderStyle = FormBorderStyle.None;
            Name = "sideBarPanel";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label4;
        private PictureBox pictureBox1;
        private Label label5;
        private Label label6;
        private Label label12;
        private Label label13;
        private Panel panel1;
        private Panel panel6;
        private Panel panel2;
        private Label label7;
    }
}
