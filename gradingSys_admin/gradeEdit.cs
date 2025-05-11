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
using System;
using System.IO;

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
            guna2DataGridView1.RowPrePaint += guna2DataGridView1_RowPrePaint;

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
            if (guna2DataGridView1.CurrentRow != null)
            {
                object? cadetId = guna2DataGridView1.CurrentRow.Cells["cadet_id"].Value;
                object? firstName = guna2DataGridView1.CurrentRow.Cells["first_name"].Value;
                object? middleName = guna2DataGridView1.CurrentRow.Cells["middle_name"].Value;
                object? lastName = guna2DataGridView1.CurrentRow.Cells["last_name"].Value;

                if (cadetId != null && firstName != null && middleName != null && lastName != null)
                {
                    Form? mainForm = FormHelper.GetTopMostForm(this);
                    if (mainForm != null)
                    {
                        gradeView editForm = new gradeView
                        {
                            CadetId = cadetId.ToString()!,
                            FirstName = firstName.ToString()!,
                            MiddleName = middleName.ToString()!,
                            LastName = lastName.ToString()!
                        };

                        FormHelper.ShowDialogWithBackdrop(mainForm, editForm);
                    }
                    else
                    {
                        MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a cadet with complete information.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No cadet row is selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private void guna2DataGridView1_RowPrePaint(object? sender, DataGridViewRowPrePaintEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null) return;

            DataGridViewRow row = grid.Rows[e.RowIndex];
            object? scoreValue = row.Cells["Score"].Value;

            if (scoreValue != null && scoreValue != DBNull.Value)
            {
                if (int.TryParse(scoreValue.ToString(), out int score))
                {
                    if (score <= 25)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = grid.DefaultCellStyle.BackColor;
                        row.DefaultCellStyle.ForeColor = grid.DefaultCellStyle.ForeColor;
                    }
                }
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

        private void btn_formScore_Click(object sender, EventArgs e)
        {
            string connectionString = "server=database-sia-cis.c7gskq208sgz.ap-southeast-2.rds.amazonaws.com;user=admin;password=05152025CIASIA-admin;database=cis_db;";


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Title = "Select a CSV File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    string[] lines = File.ReadAllLines(filePath);

                    if (lines.Length <= 1)
                    {
                        MessageBox.Show("CSV file is empty or missing data.");
                        return;
                    }

                    string[] headers = lines[0].Split(',');
                    int idIndex = Array.IndexOf(headers, "ID_Student/Cadet");
                    int scoreIndex = Array.IndexOf(headers, "Score");

                    if (idIndex == -1 || scoreIndex == -1)
                    {
                        MessageBox.Show("CSV file missing required columns.");
                        return;
                    }

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                int updatedCount = 0;
                                int skippedCount = 0;

                                int currentMonth = DateTime.Now.Month;
                                bool isMidterm = currentMonth >= 1 && currentMonth <= 3;
                                bool isFinals = currentMonth >= 4 && currentMonth <= 5;

                                for (int i = 1; i < lines.Length; i++)
                                {
                                    string[] data = lines[i].Split(',');

                                    if (data.Length <= Math.Max(idIndex, scoreIndex))
                                        continue;

                                    string studentId = data[idIndex].Trim();
                                    string rawScore = data[scoreIndex].Trim();

                                    string[] scoreParts = rawScore.Split('/');
                                    if (scoreParts.Length == 0 || !float.TryParse(scoreParts[0].Trim(), out float score))
                                        continue;

                                    string checkQuery = "SELECT midterm_exam_score, finals_exam_score FROM examination WHERE Student_Id = @StudentId LIMIT 1";
                                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn, transaction))
                                    {
                                        checkCmd.Parameters.Clear();
                                        checkCmd.Parameters.AddWithValue("@StudentId", studentId);

                                        using (var reader = checkCmd.ExecuteReader())
                                        {
                                            if (!reader.Read())
                                            {
                                                skippedCount++;
                                                continue;
                                            }

                                            float midtermScore = reader["midterm_exam_score"] != DBNull.Value ? Convert.ToSingle(reader["midterm_exam_score"]) : 0;
                                            float finalsScore = reader["finals_exam_score"] != DBNull.Value ? Convert.ToSingle(reader["finals_exam_score"]) : 0;

                                            bool skip = (isMidterm && midtermScore > 0) || (isFinals && finalsScore > 0);
                                            if (skip)
                                            {
                                                skippedCount++;
                                                continue;
                                            }
                                        }
                                    }

                                    string updateQuery = "UPDATE examination SET ";
                                    if (isMidterm)
                                        updateQuery += "midterm_exam_score = @Score ";
                                    else if (isFinals)
                                        updateQuery += "finals_exam_score = @Score ";
                                    else
                                        continue;

                                    updateQuery += "WHERE Student_Id = @StudentId";

                                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn, transaction))
                                    {
                                        updateCmd.Parameters.Clear();
                                        updateCmd.Parameters.AddWithValue("@Score", score);
                                        updateCmd.Parameters.AddWithValue("@StudentId", studentId);
                                        updateCmd.ExecuteNonQuery();
                                        updatedCount++;
                                    }
                                }

                                transaction.Commit();
                                MessageBox.Show($"✅ Updated {updatedCount} student scores.\n⚠️ Skipped {skippedCount} already existing scores.");
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Database Error: " + ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File Error: " + ex.Message);
                }
            }
        }
    }
}