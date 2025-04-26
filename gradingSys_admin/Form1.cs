using GradingSys_SIA;

namespace gradingSys_admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            sideBarPanel sideBar = new sideBarPanel();
            sideBar.Show();

        }
    }
}
