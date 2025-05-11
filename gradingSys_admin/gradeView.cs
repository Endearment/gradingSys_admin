using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace gradingSys_admin
{

    public partial class gradeView : Form
    {
        public string CadetId { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";
        public gradeView()
        {
            InitializeComponent();
            this.Load += gradeView_Load;
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gradeView_Load(object sender, EventArgs e)
        {
            lbl_studName.Text = $"{FirstName} {MiddleName} {LastName}";
            lbl_studNum.Text = CadetId;

            LoadPerformanceData();
        }

        private void LoadPerformanceData()
        {
            string connectionString = "server=database-sia-cis.c7gskq208sgz.ap-southeast-2.rds.amazonaws.com;user=admin;password=05152025CIASIA-admin;database=cis_db;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    ap.Aptitude_Points,
                    gm.Final_Grade,
                    gm.midterm_attendance_grade,
                    gm.finals_attendance_grade,
                    ex.Score
                FROM aptitude ap
                LEFT JOIN grade_management gm ON ap.Student_ID = gm.Student_ID
                LEFT JOIN examination ex ON ap.Student_ID = ex.Student_ID
                WHERE ap.Student_ID = @StudentId
                LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", CadetId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int aptitude = reader["Aptitude_Points"] != DBNull.Value ? Convert.ToInt32(reader["Aptitude_Points"]) : 0;
                                circularProgressBar1.Value = aptitude;
                                circularProgressBar1.Text = aptitude.ToString();

                                int finalGrade = reader["Final_Grade"] != DBNull.Value ? Convert.ToInt32(reader["Final_Grade"]) : 0;
                                circularProgressBar4.Value = finalGrade;
                                circularProgressBar4.Text = finalGrade.ToString();

                                int score = reader["Score"] != DBNull.Value ? Convert.ToInt32(reader["Score"]) : 0;
                                circularProgressBar3.Value = Math.Min(score * 2, 100);
                                circularProgressBar3.Text = (score * 2).ToString();

                                int month = DateTime.Now.Month;
                                string attendanceColumn = (month >= 1 && month <= 3) ? "midterm_attendance_grade"
                                                          : (month >= 4 && month <= 5) ? "finals_attendance_grade"
                                                          : "";

                                if (!string.IsNullOrEmpty(attendanceColumn))
                                {
                                    int attendanceGrade = reader[attendanceColumn] != DBNull.Value ? Convert.ToInt32(reader[attendanceColumn]) : 0;
                                    int normalizedAttendance = (int)((attendanceGrade / 30.0) * 100);
                                    circularProgressBar2.Value = Math.Min(normalizedAttendance, 100);
                                    circularProgressBar2.Text = $"{attendanceGrade}%";
                                }
                                else
                                {
                                    circularProgressBar2.Value = 0;
                                    circularProgressBar2.Text = "0%";
                                }
                            }
                            else
                            {
                                circularProgressBar1.Value = 0;
                                circularProgressBar1.Text = "0";

                                circularProgressBar4.Value = 0;
                                circularProgressBar4.Text = "0";

                                circularProgressBar3.Value = 0;
                                circularProgressBar3.Text = "0";

                                circularProgressBar2.Value = 0;
                                circularProgressBar2.Text = "0%";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading performance data: " + ex.Message);
                }
            }
        }
    }
}