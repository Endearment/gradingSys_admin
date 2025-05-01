using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace gradingSys_admin
{
    public partial class aptitudeView : Form
    {
        public aptitudeView()
        {
            InitializeComponent();

        }

        private void aptitudeData_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void OpenAptitudeEdit(string cadetId)
        {
            Form? mainForm = FormHelper.GetTopMostForm(this);
            if (mainForm != null)
            {
                aptitudeEdit editForm = new aptitudeEdit();
                editForm.CadetId = cadetId;
                FormHelper.ShowDialogWithBackdrop(mainForm, editForm);
            }
            else
            {
                MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            using (MySqlConnection conn = Dbconnection.GetConnection("cis_db"))
            {
                try
                {
                    string query = @"SELECT ci.cadet_id, ci.last_name, ci.first_name,  ci.middle_name, ex.Score FROM cis_db.cadet_info ci LEFT JOIN grading_db.examination ex ON ci.cadet_id = ex.Student_ID";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    guna2DataGridView1.DataSource = dt;

                    if (!guna2DataGridView1.Columns.Contains("editButton"))
                    {
                        DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                        editButton.Name = "editButton";
                        editButton.HeaderText = "Action";
                        editButton.Text = "✍Aptitude";
                        editButton.UseColumnTextForButtonValue = true;
                        editButton.DefaultCellStyle.BackColor = Color.Lavender;
                        editButton.DefaultCellStyle.ForeColor = Color.Black;
                        editButton.FlatStyle = FlatStyle.Flat;
                        guna2DataGridView1.Columns.Add(editButton);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "editButton" && e.RowIndex >= 0)
            {
                object? cellValue = guna2DataGridView1.Rows[e.RowIndex].Cells["cadet_id"].Value;
                if (cellValue != null)
                {
                    string cadetId = cellValue.ToString()!;
                    OpenAptitudeEdit(cadetId);
                }
                else
                {
                    MessageBox.Show("Cadet ID is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                object? cellValue = guna2DataGridView1.CurrentRow.Cells["cadet_id"].Value;
                if (cellValue != null)
                {
                    string cadetId = cellValue.ToString()!;
                    string currentTerm = "";

                    using (MySqlConnection termConn = Dbconnection.GetConnection("grading_db"))
                    {
                        try
                        {
                            termConn.Open();
                            string termQuery = "SELECT Term FROM examination ORDER BY Term DESC LIMIT 1";
                            using (MySqlCommand termCmd = new MySqlCommand(termQuery, termConn))
                            {
                                object? termResult = termCmd.ExecuteScalar();
                                if (termResult != null)
                                {
                                    currentTerm = termResult.ToString()!;
                                }
                                else
                                {
                                    MessageBox.Show("Unable to determine current term.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to fetch term: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    using (MySqlConnection conn = Dbconnection.GetConnection("grading_db"))
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                object? cellValue = guna2DataGridView1.CurrentRow.Cells["cadet_id"].Value;
                if (cellValue != null)
                {
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

        private void btn_gScore_Click(object sender, EventArgs e)
        {
            Form? mainForm = FormHelper.GetTopMostForm(this);
            if (mainForm != null)
            {
                gradeView editForm = new gradeView();
                //gradeView.CadetId = cadetId;
                FormHelper.ShowDialogWithBackdrop(mainForm, editForm);
            }
            else
            {
                MessageBox.Show("Unable to determine the top-most form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
