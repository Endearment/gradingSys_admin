namespace gradingSys_admin
{
    partial class loginForm
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginForm));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btn_login = new Guna.UI2.WinForms.Guna2Button();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            label12 = new Label();
            label6 = new Label();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            txt_username = new Guna.UI2.WinForms.Guna2TextBox();
            txt_password = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btn_close = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btn_login
            // 
            btn_login.BorderRadius = 10;
            btn_login.CustomizableEdges = customizableEdges1;
            btn_login.DisabledState.BorderColor = Color.DarkGray;
            btn_login.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_login.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_login.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_login.FillColor = SystemColors.ActiveCaption;
            btn_login.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_login.ForeColor = Color.White;
            btn_login.Location = new Point(248, 260);
            btn_login.Margin = new Padding(2);
            btn_login.Name = "btn_login";
            btn_login.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_login.Size = new Size(106, 25);
            btn_login.TabIndex = 0;
            btn_login.Text = "LOGIN";
            btn_login.Click += guna2Button1_Click;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 50;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // label12
            // 
            label12.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(1, 151);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new Size(610, 23);
            label12.TabIndex = 10;
            label12.Text = "______________________";
            label12.TextAlign = ContentAlignment.TopCenter;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(1, 145);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(610, 14);
            label6.TabIndex = 9;
            label6.Text = "Reserve Officer's Training Corps";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label5
            // 
            label5.Font = new Font("Impact", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Goldenrod;
            label5.Location = new Point(1, 115);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(610, 30);
            label5.TabIndex = 7;
            label5.Text = "QUEZON CITY UNIVERSITY";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, 7);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(610, 106);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // txt_username
            // 
            txt_username.CustomizableEdges = customizableEdges5;
            txt_username.DefaultText = "";
            txt_username.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_username.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_username.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_username.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_username.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_username.Font = new Font("Segoe UI", 9F);
            txt_username.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_username.Location = new Point(291, 184);
            txt_username.Name = "txt_username";
            txt_username.PlaceholderText = "";
            txt_username.SelectedText = "";
            txt_username.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txt_username.Size = new Size(117, 21);
            txt_username.TabIndex = 11;
            // 
            // txt_password
            // 
            txt_password.AllowDrop = true;
            txt_password.CustomizableEdges = customizableEdges3;
            txt_password.DefaultText = "";
            txt_password.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_password.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_password.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_password.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_password.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_password.Font = new Font("Segoe UI", 9F);
            txt_password.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_password.Location = new Point(291, 211);
            txt_password.Name = "txt_password";
            txt_password.PasswordChar = '*';
            txt_password.PlaceholderText = "";
            txt_password.SelectedText = "";
            txt_password.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txt_password.Size = new Size(117, 21);
            txt_password.TabIndex = 12;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            guna2HtmlLabel2.Location = new Point(214, 211);
            guna2HtmlLabel2.Margin = new Padding(2);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(77, 21);
            guna2HtmlLabel2.TabIndex = 14;
            guna2HtmlLabel2.Text = "Password:";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.AutoSize = false;
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            guna2HtmlLabel1.Location = new Point(214, 184);
            guna2HtmlLabel1.Margin = new Padding(2);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(77, 21);
            guna2HtmlLabel1.TabIndex = 15;
            guna2HtmlLabel1.Text = "Username:";
            guna2HtmlLabel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btn_close
            // 
            btn_close.AutoSize = false;
            btn_close.BackColor = Color.Transparent;
            btn_close.Font = new Font("Segoe Print", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_close.Location = new Point(573, 1);
            btn_close.Margin = new Padding(2);
            btn_close.Name = "btn_close";
            btn_close.Size = new Size(30, 16);
            btn_close.TabIndex = 16;
            btn_close.Text = "X";
            btn_close.TextAlignment = ContentAlignment.MiddleCenter;
            btn_close.Click += btn_close_Click;
            // 
            // loginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(611, 305);
            Controls.Add(btn_close);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(txt_password);
            Controls.Add(txt_username);
            Controls.Add(label6);
            Controls.Add(label12);
            Controls.Add(label5);
            Controls.Add(pictureBox1);
            Controls.Add(btn_login);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "loginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btn_login;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Label label12;
        private Label label6;
        private Label label5;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2TextBox txt_password;
        private Guna.UI2.WinForms.Guna2TextBox txt_username;
        private Guna.UI2.WinForms.Guna2HtmlLabel btn_close;
    }
}
