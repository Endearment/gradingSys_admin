namespace gradingSys_admin
{
    partial class examEdit
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            lbl_studName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_studNum = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btn_exit = new Guna.UI2.WinForms.Guna2Button();
            txt_examScore = new Guna.UI2.WinForms.Guna2TextBox();
            btn_save = new Guna.UI2.WinForms.Guna2Button();
            lbl_score = new Label();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 40;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // lbl_studName
            // 
            lbl_studName.BackColor = Color.Transparent;
            lbl_studName.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_studName.Location = new Point(69, 42);
            lbl_studName.Name = "lbl_studName";
            lbl_studName.Size = new Size(203, 30);
            lbl_studName.TabIndex = 0;
            lbl_studName.Text = "-------------------------";
            // 
            // lbl_studNum
            // 
            lbl_studNum.BackColor = Color.Transparent;
            lbl_studNum.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_studNum.Location = new Point(69, 75);
            lbl_studNum.Name = "lbl_studNum";
            lbl_studNum.Size = new Size(155, 30);
            lbl_studNum.TabIndex = 1;
            lbl_studNum.Text = "-------------------";
            // 
            // btn_exit
            // 
            btn_exit.CustomizableEdges = customizableEdges5;
            btn_exit.DisabledState.BorderColor = Color.DarkGray;
            btn_exit.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_exit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_exit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_exit.FillColor = Color.Red;
            btn_exit.Font = new Font("Segoe UI", 9F);
            btn_exit.ForeColor = Color.White;
            btn_exit.Location = new Point(358, 285);
            btn_exit.Name = "btn_exit";
            btn_exit.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btn_exit.Size = new Size(138, 41);
            btn_exit.TabIndex = 15;
            btn_exit.Text = "Exit";
            btn_exit.Click += btn_exit_Click;
            // 
            // txt_examScore
            // 
            txt_examScore.CustomizableEdges = customizableEdges1;
            txt_examScore.DefaultText = "";
            txt_examScore.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_examScore.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_examScore.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_examScore.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_examScore.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_examScore.Font = new Font("Segoe UI", 9F);
            txt_examScore.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_examScore.Location = new Point(173, 10);
            txt_examScore.Margin = new Padding(4, 5, 4, 5);
            txt_examScore.Name = "txt_examScore";
            txt_examScore.PlaceholderText = "Exam Score";
            txt_examScore.SelectedText = "";
            txt_examScore.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txt_examScore.Size = new Size(130, 49);
            txt_examScore.TabIndex = 16;
            txt_examScore.KeyPress += txt_examScore_KeyPress;
            // 
            // btn_save
            // 
            btn_save.CustomizableEdges = customizableEdges3;
            btn_save.DisabledState.BorderColor = Color.DarkGray;
            btn_save.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_save.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_save.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_save.Font = new Font("Segoe UI", 9F);
            btn_save.ForeColor = Color.White;
            btn_save.Location = new Point(109, 285);
            btn_save.Name = "btn_save";
            btn_save.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_save.Size = new Size(138, 41);
            btn_save.TabIndex = 19;
            btn_save.Text = "Save";
            btn_save.Click += btn_save_Click;
            // 
            // lbl_score
            // 
            lbl_score.Location = new Point(0, 10);
            lbl_score.Name = "lbl_score";
            lbl_score.Size = new Size(166, 49);
            lbl_score.TabIndex = 20;
            lbl_score.Text = "Enter Exam Score:";
            lbl_score.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(txt_examScore);
            panel1.Controls.Add(lbl_score);
            panel1.Location = new Point(158, 159);
            panel1.Name = "panel1";
            panel1.Size = new Size(310, 70);
            panel1.TabIndex = 21;
            // 
            // examEdit
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(574, 356);
            Controls.Add(panel1);
            Controls.Add(btn_save);
            Controls.Add(btn_exit);
            Controls.Add(lbl_studNum);
            Controls.Add(lbl_studName);
            FormBorderStyle = FormBorderStyle.None;
            Name = "examEdit";
            Text = "gradeEdit";
            Load += gradeEdit_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_studNum;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_studName;
        private Guna.UI2.WinForms.Guna2Button btn_exit;
        //private Guna.UI2.WinForms.Guna2Button guna2Button3;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2TextBox txt_examScore;
        private Guna.UI2.WinForms.Guna2Button btn_save;
        private Panel panel1;
        private Label lbl_score;
    }
}