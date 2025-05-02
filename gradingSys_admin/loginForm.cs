using GradingSys_SIA;
using MySql.Data.MySqlClient;

namespace gradingSys_admin
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string enteredId = txt_username.Text.Trim();
            string role = GetUserRole(enteredId);

            if (role == "unknown")
            {
                MessageBox.Show("Invalid ID. Please try again.");
                return;
            }

            this.Hide();
            sideBarPanel sideBar = new sideBarPanel(enteredId, role);
            sideBar.Show();

        }

        private string GetUserRole(string userId)
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                conn.Open();

                string cadetQuery = "SELECT cadet_id FROM cadet_info WHERE cadet_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(cadetQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return "cadet";
                        }
                    }
                }

                string instructorQuery = "SELECT instructor_id FROM instructor_info WHERE instructor_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(instructorQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return "instructor";
                        }
                    }
                }
            }

            return "unknown";
        }


        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
