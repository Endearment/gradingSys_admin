using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Guna.UI2.WinForms;

namespace gradingSys_admin
{
    public partial class aptitudeEdit : Form
    {
        public aptitudeEdit()
        {
            InitializeComponent();
            this.TopMost = true;
            guna2BorderlessForm1.DragForm = false;

        }

        private void aptitudeEdit_Load(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Your data will not be save, are you sure you want to exit?", "Close Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }

        }
    }
}
