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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace gradingSys_admin
{
    public partial class examEdit : Form
    {
        public string CadetId { get; set; } = string.Empty;

        public examEdit()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Load += gradeEdit_Load;
            guna2BorderlessForm1.DragForm = false;
        }

        private void gradeEdit_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CadetId)) return;

            LoadCadetInfo();
            LoadExamScore();
        }

        private void LoadCadetInfo()
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT cadet_id, CONCAT(last_name, ', ', first_name, ' ', middle_name) AS full_name " +
                                   "FROM cadet_info WHERE cadet_id = @cadetId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cadetId", CadetId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lbl_studNum.Text = reader["cadet_id"].ToString();
                                lbl_studName.Text = reader["full_name"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No student found with the provided Cadet ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load student information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadExamScore()
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Score FROM examination WHERE Student_ID = @cadetId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cadetId", CadetId);
                        object result = cmd.ExecuteScalar();
                        txt_examScore.Text = result != null && result != DBNull.Value ? result.ToString() : "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load exam score: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to close this?", "Close Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CadetId))
            {
                MessageBox.Show("Cadet ID is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(txt_examScore.Text, out float score))
            {
                MessageBox.Show("Please enter a valid numeric exam score.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float maxScore = GetMaxScore();
            if (score > maxScore)
            {
                MessageBox.Show($"Score cannot exceed the Max Score of {maxScore}.", "Invalid Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullName = GetCadetFullName();
            if (string.IsNullOrEmpty(fullName)) return;

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to save the exam score of {score} for:\n\nCadet ID: {CadetId}\nName: {fullName}?",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            SaveExamScore(score);
        }

        private float GetMaxScore()
        {
            float maxScore = 50f;

            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Max_Score FROM examination WHERE Student_ID = @cadetId ORDER BY Max_Score DESC LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cadetId", CadetId);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            maxScore = Convert.ToSingle(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve Max Score: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return maxScore;
        }

        private string GetCadetFullName()
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT CONCAT(last_name, ', ', first_name, ' ', middle_name) AS full_name FROM cadet_info WHERE cadet_id = @cadetId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cadetId", CadetId);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching cadet name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return string.Empty;
                }
            }
        }

        private void SaveExamScore(float score)
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM examination WHERE Student_ID = @cadetId";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@cadetId", CadetId);
                        long count = (long)checkCmd.ExecuteScalar();

                        string sql = count > 0
                            ? "UPDATE examination SET " + (IsMidterm() ? "midterm_exam_score" : "finals_exam_score") + " = @score WHERE Student_ID = @cadetId"
                            : "INSERT INTO examination (Student_ID, " + (IsMidterm() ? "midterm_exam_score" : "finals_exam_score") + ") VALUES (@cadetId, @score)";

                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@score", score);
                            cmd.Parameters.AddWithValue("@cadetId", CadetId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show(count > 0 ? "Exam score updated successfully." : "Exam score added successfully.");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving score: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsMidterm()
        {
            int currentMonth = DateTime.Now.Month;

            return (currentMonth >= 1 && currentMonth <= 3) || (currentMonth >= 8 && currentMonth <= 10);
        }


        private void txt_examScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txt_examScore.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }
    }
}

