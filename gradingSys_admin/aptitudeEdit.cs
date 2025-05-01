using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace gradingSys_admin
{
    public partial class aptitudeEdit : Form
    {
        public string CadetId { get; set; } = string.Empty;
        public string StudentID { get; set; } = string.Empty;
        public aptitudeEdit()
        {
            InitializeComponent();
            this.TopMost = true;
            guna2BorderlessForm1.DragForm = false;
        }

        private void LoadCadetInfo()
        {
            if (string.IsNullOrEmpty(CadetId))
            {
                MessageBox.Show("No Cadet ID provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM cadet_info WHERE cadet_id = @cadetId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cadetId", CadetId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentName.Text = $"{reader["last_name"]}, {reader["first_name"]} {reader["middle_name"]}";
                                student_number.Text = reader["cadet_id"].ToString();
                            }
                            if (reader["profile_picture"] != DBNull.Value)
                            {
                                byte[] imgBytes = (byte[])reader["profile_picture"];
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    guna2CirclePictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                //guna2CirclePictureBox1.Image = Properties.Resources.default_profile;
                                // (replace 'default_profile' with your actual default image in Resources)
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading cadet data: " + ex.Message);
                }
            }
        }

        private void aptitudeEdit_Load(object sender, EventArgs e)
        {
            LoadCadetInfo();
            UpdateAptitudePointsProgress(CadetId);
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Your data will not be save, are you sure you want to exit?", "Close Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int haircutDemerit = chk_hair.Checked ? 1 : 0;
            int uniformDemerit = chk_uniform.Checked ? 1 : 0;
            int makeupDemerit = chk_makeup.Checked ? 1 : 0;
            int earringsDemerit = chk_earrings.Checked ? 1 : 0;
            int facialHairDemerit = chk_facial.Checked ? 1 : 0;
            int tardinessDemerit = chk_tardiness.Checked ? 1 : 0;

            int totalDemerits = haircutDemerit + uniformDemerit + makeupDemerit +
                                earringsDemerit + facialHairDemerit + tardinessDemerit;

            // Get merits from combo boxes
            int accomplishmentMerit = int.TryParse(mrt_accomp.SelectedItem?.ToString(), out int acc) ? acc : 0;
            int touringMerit = int.TryParse(mrt_tour.SelectedItem?.ToString(), out int tour) ? tour : 0;
            int totalMerits = accomplishmentMerit + touringMerit;

            try
            {
                using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
                {
                    conn.Open();
                    CadetId = CadetId.Trim();

                    string checkQuery = "SELECT COUNT(*) FROM aptitude WHERE Student_ID = @studentId";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@studentId", CadetId);
                        int studentExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        int currentPoints = 100;

                        if (studentExists > 0)
                        {
                            string getPointsQuery = "SELECT Aptitude_Points FROM aptitude WHERE Student_ID = @studentId ORDER BY Record_ID DESC LIMIT 1";
                            using (MySqlCommand getPointsCmd = new MySqlCommand(getPointsQuery, conn))
                            {
                                getPointsCmd.Parameters.AddWithValue("@studentId", CadetId);
                                object result = getPointsCmd.ExecuteScalar();
                                if (result != null && result != DBNull.Value)
                                {
                                    currentPoints = Convert.ToInt32(result);
                                }
                            }

                            int newPoints = currentPoints - totalDemerits + totalMerits;
                            if (newPoints > 100) newPoints = 100;
                            if (newPoints < 0) newPoints = 0;

                            string updateQuery = @"UPDATE aptitude SET
                        Haircut_Demerits = Haircut_Demerits + @haircut,
                        Uniform_Demerits = Uniform_Demerits + @uniform,
                        Makeup_Demerits = Makeup_Demerits + @makeup,
                        Earrings_Demerits = Earrings_Demerits + @earrings,
                        Facial_Hair_Demerits = Facial_Hair_Demerits + @facialHair,
                        Tardiness_Demerits = Tardiness_Demerits + @tardiness,
                        Total_Demerits = Total_Demerits + @totalDemerits,
                        Accomplishment_Merits = Accomplishment_Merits + @accMerit,
                        Touring_Merits = Touring_Merits + @tourMerit,
                        Aptitude_Points = @newPoints
                        WHERE Student_ID = @studentId";

                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@haircut", haircutDemerit);
                                updateCmd.Parameters.AddWithValue("@uniform", uniformDemerit);
                                updateCmd.Parameters.AddWithValue("@makeup", makeupDemerit);
                                updateCmd.Parameters.AddWithValue("@earrings", earringsDemerit);
                                updateCmd.Parameters.AddWithValue("@facialHair", facialHairDemerit);
                                updateCmd.Parameters.AddWithValue("@tardiness", tardinessDemerit);
                                updateCmd.Parameters.AddWithValue("@totalDemerits", totalDemerits);
                                updateCmd.Parameters.AddWithValue("@accMerit", accomplishmentMerit);
                                updateCmd.Parameters.AddWithValue("@tourMerit", touringMerit);
                                updateCmd.Parameters.AddWithValue("@newPoints", newPoints);
                                updateCmd.Parameters.AddWithValue("@studentId", CadetId);

                                int rowsAffected = updateCmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("No data was updated. Check if the student exists in the aptitude table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            int newPoints = 100 - totalDemerits + totalMerits;
                            if (newPoints > 100) newPoints = 100;
                            if (newPoints < 0) newPoints = 0;

                            string insertQuery = @"INSERT INTO aptitude (
                        Student_ID, Haircut_Demerits, Uniform_Demerits, Makeup_Demerits,
                        Earrings_Demerits, Facial_Hair_Demerits, Tardiness_Demerits,
                        Total_Demerits, Accomplishment_Merits, Touring_Merits, Aptitude_Points)
                        VALUES (
                        @studentId, @haircut, @uniform, @makeup,
                        @earrings, @facialHair, @tardiness,
                        @totalDemerits, @accMerit, @tourMerit, @newPoints)";

                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@studentId", CadetId);
                                insertCmd.Parameters.AddWithValue("@haircut", haircutDemerit);
                                insertCmd.Parameters.AddWithValue("@uniform", uniformDemerit);
                                insertCmd.Parameters.AddWithValue("@makeup", makeupDemerit);
                                insertCmd.Parameters.AddWithValue("@earrings", earringsDemerit);
                                insertCmd.Parameters.AddWithValue("@facialHair", facialHairDemerit);
                                insertCmd.Parameters.AddWithValue("@tardiness", tardinessDemerit);
                                insertCmd.Parameters.AddWithValue("@totalDemerits", totalDemerits);
                                insertCmd.Parameters.AddWithValue("@accMerit", accomplishmentMerit);
                                insertCmd.Parameters.AddWithValue("@tourMerit", touringMerit);
                                insertCmd.Parameters.AddWithValue("@newPoints", newPoints);

                                insertCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Data inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateAptitudePointsProgress(string cadetId)
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                conn.Open();
                string query = "SELECT Aptitude_Points FROM aptitude WHERE Student_ID = @studentId ORDER BY Record_ID DESC LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@studentId", cadetId);
                    object result = cmd.ExecuteScalar();

                    int points;

                    if (result != null && result != DBNull.Value)
                    {
                        points = Convert.ToInt32(result);
                    }
                    else
                    {
                        points = 100;
                    }

                    circularProgressBar1.Value = points;
                    circularProgressBar1.Text = points.ToString();
                }
            }
        }
    }
}

