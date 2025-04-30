using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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
            LoadData();
        }

        private void OpenAptitudeEdit(string cadetId)
        {
            Form? mainForm = FormHelper.GetTopMostForm(this);
            if (mainForm != null)
            {
                aptitudeEdit editForm = new aptitudeEdit();
                editForm.CadetId = cadetId;
                FormHelper.ShowDialogWithBackdrop(mainForm, editForm);
            }
            else
            {
                MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            using (MySqlConnection conn = Dbconnection.GetConnection())
            {
                try
                {
                    string query = "SELECT cadet_id, last_name, first_name, middle_name FROM cadet_info";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    guna2DataGridView1.DataSource = dt;
                    if (!guna2DataGridView1.Columns.Contains("editButton"))
                    {
                        DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                        editButton.Name = "editButton";
                        editButton.HeaderText = "Action";
                        editButton.Text = "Edit Aptitude";
                        editButton.UseColumnTextForButtonValue = true;
                        editButton.DefaultCellStyle.BackColor = Color.Lavender;
                        editButton.DefaultCellStyle.ForeColor = Color.Black;
                        editButton.FlatStyle = FlatStyle.Flat;
                        guna2DataGridView1.Columns.Add(editButton);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "editButton" && e.RowIndex >= 0)
            {
                object? cellValue = guna2DataGridView1.Rows[e.RowIndex].Cells["cadet_id"].Value;
                if (cellValue != null)
                {
                    string cadetId = cellValue.ToString()!;
                    OpenAptitudeEdit(cadetId);
                }
                else
                {
                    MessageBox.Show("Cadet ID is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
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

        private void guna2Button2_Click(object sender, EventArgs e)
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
    }
}
