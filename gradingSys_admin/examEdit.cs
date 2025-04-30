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

            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT cadet_id, CONCAT(last_name, ', ', first_name, ' ', middle_name) " +
                                   "FROM cadet_info WHERE cadet_id = @cadetId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cadetId", CadetId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string cadetId = reader["cadet_id"].ToString(); 
                                string cadetName = reader["CONCAT(last_name, ', ', first_name, ' ', middle_name)"].ToString(); 

                                lbl_studNum.Text = cadetId;  
                                lbl_studName.Text = cadetName; 
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

            using (MySqlConnection conn = Dbconnection.GetConnection("grading_db"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Score FROM examination WHERE Student_ID = @cadetId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cadetId", CadetId);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            txt_examScore.Text = result.ToString();
                        }
                        else
                        {
                            txt_examScore.Text = "";
                        }
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

            float maxScore = 50;

            using (MySqlConnection conn = Dbconnection.GetConnection("grading_db"))
            {
                try
                {
                    conn.Open();
                    string maxScoreQuery = "SELECT Max_Score FROM examination WHERE Student_ID = @cadetId ORDER BY Max_Score DESC LIMIT 1";

                    using (MySqlCommand maxCmd = new MySqlCommand(maxScoreQuery, conn))
                    {
                        maxCmd.Parameters.AddWithValue("@cadetId", CadetId);
                        object maxResult = maxCmd.ExecuteScalar();

                        if (maxResult != null && maxResult != DBNull.Value)
                        {
                            maxScore = Convert.ToSingle(maxResult); 
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve Max Score: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (score > maxScore)
            {
                MessageBox.Show($"Score cannot exceed the Max Score of {maxScore}.", "Invalid Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullName = "";

            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    conn.Open();
                    string nameQuery = "SELECT CONCAT(last_name, ', ', first_name, ' ', middle_name) FROM cadet_info WHERE cadet_id = @cadetId";
                    MySqlCommand nameCmd = new MySqlCommand(nameQuery, conn); 
                    nameCmd.Parameters.AddWithValue("@cadetId", CadetId);
                    object result = nameCmd.ExecuteScalar();
                    fullName = result != null ? result.ToString() : "Unknown";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching cadet name: " + ex.Message);
                    return;
                }
            }

            DialogResult dialogResult = MessageBox.Show(
                $"Are you sure you want to save the exam score of {score} for:\n\nCadet ID: {CadetId}\nName: {fullName}?",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dialogResult != DialogResult.Yes)
                return;

            using (MySqlConnection conn = Dbconnection.GetConnection("grading_db"))
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM examination WHERE Student_ID = @cadetId";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@cadetId", CadetId);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        string updateQuery = "UPDATE examination SET Score = @score WHERE Student_ID = @cadetId";
                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@score", score);
                        updateCmd.Parameters.AddWithValue("@cadetId", CadetId);
                        updateCmd.ExecuteNonQuery();
                        MessageBox.Show("Exam score updated successfully.");
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO examination (Student_id, Score) VALUES (@cadetId, @score)";
                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@cadetId", CadetId);
                        insertCmd.Parameters.AddWithValue("@score", score);
                        insertCmd.ExecuteNonQuery();
                        MessageBox.Show("Exam score added successfully.");
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving score: " + ex.Message);
                }
            }
        }
     }
 }

