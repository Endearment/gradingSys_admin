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

            using (MySqlConnection conn = Dbconnection.GetConnection())
            {
                try
                {
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

            int haircutDemerit = chk_hair.Checked ? -1 : 0;
            int uniformDemerit = chk_uniform.Checked ? -1 : 0;
            int makeupDemerit = chk_makeup.Checked ? -1 : 0;
            int earringsDemerit = chk_earrings.Checked ? -1 : 0;
            int facialHairDemerit = chk_facial.Checked ? -1 : 0;
            int tardinessDemerit = chk_tardiness.Checked ? -1 : 0;


            int totalDemerits = haircutDemerit + uniformDemerit + makeupDemerit +
                               earringsDemerit + facialHairDemerit + tardinessDemerit;

            int aptitudePoints = 100 - Math.Abs(totalDemerits);

            try
            {
                using (MySqlConnection conn = Dbconnection.GetConnection())
                {
                    conn.ChangeDatabase("grading_db");

                    string query = @"INSERT INTO aptitude 
                (ID, Student_ID, Haircut_Demerits, Uniform_Demerits, Makeup_Demerits, 
                 Earrings_Demerits, Facial_Hair_Demerits, Tardiness_Demerits, 
                 Attitude_Demerits, Total_Demerits)
                VALUES 
                (UUID(), @studentId, @haircut, @uniform, @makeup, @earrings, 
                 @facialHair, @tardiness, 0, @totalDemerits)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentId", CadetId);
                        cmd.Parameters.AddWithValue("@haircut", haircutDemerit);
                        cmd.Parameters.AddWithValue("@uniform", uniformDemerit);
                        cmd.Parameters.AddWithValue("@makeup", makeupDemerit);
                        cmd.Parameters.AddWithValue("@earrings", earringsDemerit);
                        cmd.Parameters.AddWithValue("@facialHair", facialHairDemerit);
                        cmd.Parameters.AddWithValue("@tardiness", tardinessDemerit);
                        cmd.Parameters.AddWithValue("@totalDemerits", totalDemerits);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data saved successfully!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
   
    }
}
