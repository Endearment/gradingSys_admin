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
    public partial class examView : Form
    {
        public examView()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form? mainForm = FormHelper.GetTopMostForm(this);
            if (mainForm != null)
            {
                FormHelper.ShowDialogWithBackdrop(mainForm, new examEdit());
            }
            else
            {
                MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gradeView_Load(object sender, EventArgs e)
        {

        }
    }
}
