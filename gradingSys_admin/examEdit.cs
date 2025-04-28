using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gradingSys_admin
{
    public partial class examEdit : Form
    {
        public string CadetId { get; set; } = string.Empty;
        public examEdit()
        {
            InitializeComponent();
            this.TopMost = true;
            guna2BorderlessForm1.DragForm = false;
        }

        private void gradeEdit_Load(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to close this?", "Close Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }
    }
}
