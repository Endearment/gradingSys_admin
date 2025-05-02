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
    public partial class gradeEdit : Form
    {
        public gradeEdit()
        {
            InitializeComponent();
        }

        private void gradeEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    string query = @"SELECT ci.cadet_id, ci.last_name, ci.first_name, ci.middle_name, ex.Score 
                                     FROM cis_db.cadet_info ci 
                                     LEFT JOIN cis_db.examination ex ON ci.cadet_id = ex.Student_ID";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    guna2DataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void btn_viewGrade_Click(object sender, EventArgs e)
        {
            Form? mainForm = FormHelper.GetTopMostForm(this);
            if (mainForm != null)
            {
                gradeView editForm = new gradeView();
                FormHelper.ShowDialogWithBackdrop(mainForm, editForm);
            }
            else
            {
                MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_edtScore_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                object? cellValue = guna2DataGridView1.CurrentRow.Cells["cadet_id"].Value;
                object? scoreValue = guna2DataGridView1.CurrentRow.Cells["Score"].Value;

                if (cellValue != null)
                {
                    if (scoreValue == DBNull.Value || string.IsNullOrEmpty(scoreValue?.ToString()))
                    {
                        MessageBox.Show("This cadet has no score yet. You cannot edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string cadetId = cellValue.ToString()!;
                    Form? mainForm = FormHelper.GetTopMostForm(this);
                    if (mainForm != null)
                    {
                        examEdit examForm = new examEdit();
                        examForm.CadetId = cadetId;

                        examForm.FormClosed += (s, args) => LoadData();

                        FormHelper.ShowDialogWithBackdrop(mainForm, examForm);
                    }
                    else
                    {
                        MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a cadet to edit the score.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No cadet row is selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_addScore_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                object? cellValue = guna2DataGridView1.CurrentRow.Cells["cadet_id"].Value;
                if (cellValue != null)
                {
                    string cadetId = cellValue.ToString()!;
                    string currentTerm = "";

                    using (MySqlConnection termConn = Dbconnection.GetConnection("cis_db"))
                    {
                        try
                        {
                            termConn.Open();
                            string termQuery = "SELECT term FROM examination";
                            using (MySqlCommand termCmd = new MySqlCommand(termQuery, termConn))
                            {
                                object? termResult = termCmd.ExecuteScalar();
                                if (termResult != null && termResult != DBNull.Value)
                                {
                                    currentTerm = termResult.ToString()!;
                                }
                                else
                                {
                                    currentTerm = "Midterm";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to fetch term: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
                    {
                        try
                        {
                            conn.Open();

                            string checkQuery = "SELECT COUNT(*) FROM examination WHERE Student_ID = @cadetId AND term = @term";
                            using (MySqlCommand cmd = new MySqlCommand(checkQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@cadetId", cadetId);
                                cmd.Parameters.AddWithValue("@term", currentTerm);
                                long count = (long)cmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    MessageBox.Show(
                                        "A score has already been recorded for this student in the current term.\nPlease use the 'Edit Score' option instead.",
                                        "Duplicate Entry Not Allowed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    );
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error checking existing score: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    Form? mainForm = FormHelper.GetTopMostForm(this);
                    if (mainForm != null)
                    {
                        examEdit examForm = new examEdit();
                        examForm.CadetId = cadetId;
                        examForm.FormClosed += (s, args) => LoadData();

                        FormHelper.ShowDialogWithBackdrop(mainForm, examForm);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a cadet to add a score.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No cadet row is selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text;

            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                string query = @"
                    SELECT ci.cadet_id, ci.last_name, ci.first_name, ci.middle_name, ex.Score 
                    FROM cis_db.cadet_info ci
                    LEFT JOIN cis_db.examination ex ON ci.cadet_id = ex.Student_ID
                    WHERE ci.cadet_id LIKE @search 
                       OR ci.last_name LIKE @search 
                       OR ci.first_name LIKE @search 
                       OR ci.middle_name LIKE @search 
                       OR ex.Score LIKE @search";

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
