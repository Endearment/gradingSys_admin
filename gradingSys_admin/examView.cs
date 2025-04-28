using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
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


        private void OpenExamEdit(string cadetId)
        {
            Form? mainForm = FormHelper.GetTopMostForm(this);
            if (mainForm != null)
            {
                examEdit editForm = new examEdit();
                editForm.CadetId = cadetId;
                FormHelper.ShowDialogWithBackdrop(mainForm, editForm);
            }
            else
            {
                MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    guna2DataGridView2.DataSource = dt;
                    if (!guna2DataGridView2.Columns.Contains("editButton"))
                    {
                        DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                        editButton.Name = "editButton";
                        editButton.HeaderText = "Action";
                        editButton.Text = "Edit";
                        editButton.UseColumnTextForButtonValue = true;
                        editButton.DefaultCellStyle.BackColor = Color.Lavender;
                        editButton.DefaultCellStyle.ForeColor = Color.Black;
                        editButton.FlatStyle = FlatStyle.Flat;
                        guna2DataGridView2.Columns.Add(editButton);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void gradeView_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2DataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView2.Columns[e.ColumnIndex].Name == "editButton" && e.RowIndex >= 0)
            {
                object? cellValue = guna2DataGridView2.Rows[e.RowIndex].Cells["cadet_id"].Value;
                if (cellValue != null)
                {
                    string cadetId = cellValue.ToString()!;
                    OpenExamEdit(cadetId);
                }
                else
                {
                    MessageBox.Show("Cadet ID is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
