using gradingSys_admin;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace GradingSys_SIA
{
    public partial class sideBarPanel : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public sideBarPanel()
        {
            InitializeComponent();
            panel2.MouseDown += new MouseEventHandler(panel2_MouseDown);
            panel2.MouseMove += new MouseEventHandler(panel2_MouseMove);
            panel2.MouseUp += new MouseEventHandler(panel2_MouseUp);
            this.TopMost = true;
        }

        private void panel2_MouseDown(object? sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel2_MouseMove(object? sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void panel2_MouseUp(object? sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label13.Padding = new Padding(15, 0, 0, 0);
            label1.Padding = new Padding(50, 0, 0, 0);
            label2.Padding = new Padding(50, 0, 0, 0);
            label3.Padding = new Padding(50, 0, 0, 0);
            label4.Padding = new Padding(15, 0, 0, 0);

            label1.MouseEnter += labelFunction.Label_MouseEnter;
            label1.MouseLeave += labelFunction.Label_MouseLeave;
            label2.MouseEnter += labelFunction.Label_MouseEnter;
            label2.MouseLeave += labelFunction.Label_MouseLeave;
            label3.MouseEnter += labelFunction.Label_MouseEnter;
            label3.MouseLeave += labelFunction.Label_MouseLeave;
            label4.MouseEnter += labelFunction.Label_MouseEnter;
            label4.MouseLeave += labelFunction.Label_MouseLeave;
            label13.MouseEnter += labelFunction.Label_MouseEnter;
            label13.MouseLeave += labelFunction.Label_MouseLeave;
            //loadform(new landingPage());
        }

        public void loadform(object form)
        {
            this.panel6.Controls.Clear();
            Form? fh = form as Form;
            if (fh != null)
            {
                fh.TopLevel = false;
                fh.FormBorderStyle = FormBorderStyle.None;
                fh.Dock = DockStyle.Fill;
                this.panel6.Controls.Add(fh);
                this.panel6.Tag = fh;
                fh.Show();
            }
        }

        public void exitConfirm()
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            loadform(new aptitudeView());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //loadform(new attendancePage();
        }

        private void label4_Click(object sender, EventArgs e)
        {
           exitConfirm();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            loadform(new gradeView());
        }

        private void label13_Click(object sender, EventArgs e)
        {
            loadform(new landingPage());
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            exitConfirm();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}