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
            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim(); 

            string role = GetUserRole(username, password);

            if (role == "unknown")
            {
                MessageBox.Show("Invalid username or password. Please try again.");
                return;
            }

            this.Hide();
            sideBarPanel sideBar = new sideBarPanel(username, role);
            sideBar.Show();

        }

       private string GetUserRole(string username, string password)
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                conn.Open();

                string query = "SELECT role FROM users WHERE username = @username AND password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); 

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string dbRole = reader["role"].ToString();
                            return dbRole == "CO-G1" ? "cadet" : dbRole.ToLower(); 
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
