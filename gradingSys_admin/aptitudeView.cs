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

        private void LoadData()
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    string query = @"SELECT ci.cadet_id, ci.last_name, ci.first_name, ci.middle_name
                                     FROM cis_db.cadet_info ci 
                                     LEFT JOIN cis_db.examination ex ON ci.cadet_id = ex.Student_ID";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    guna2DataGridView1.DataSource = dt;

                    if (!guna2DataGridView1.Columns.Contains("editButton"))
                    {
                        DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                        editButton.Name = "editButton";
                        editButton.HeaderText = "Action";
                        editButton.Text = "✍Aptitude";
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text;

            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                string query = @"
                    SELECT ci.cadet_id, ci.last_name, ci.first_name, ci.middle_name
                    FROM cis_db.cadet_info ci
                    LEFT JOIN cis_db.examination ex ON ci.cadet_id = ex.Student_ID
                    WHERE ci.cadet_id LIKE @search 
                       OR ci.last_name LIKE @search 
                       OR ci.first_name LIKE @search 
                       OR ci.middle_name LIKE @search";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    guna2DataGridView1.DataSource = dt;
                }
            }
        }
    }
}
