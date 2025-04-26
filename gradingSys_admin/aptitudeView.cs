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
    public partial class aptitudeView : Form
    {
        public aptitudeView()
        {
            InitializeComponent();
        }

        private void aptitudeData_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form? mainForm = FormHelper.GetTopMostForm(this);
            if (mainForm != null)
            {
                FormHelper.ShowDialogWithBackdrop(mainForm, new aptitudeEdit());
            }
            else
            {
                MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
