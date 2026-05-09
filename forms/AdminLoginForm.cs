using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using PharmacyManagementSystem.Data;

namespace PharmacyManagementSystem.Forms
{
    /// <summary>
    /// Admin Login Form.
    /// Uses ADO.NET to validate username and password against the Admins table.
    /// </summary>
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------
        // Login Button Click
        // -------------------------------------------------------
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Basic input validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Use ADO.NET to query the Admins table
                using (SqlConnection conn = new SqlConnection(DatabaseConnection.GetConnectionString()))
                {
                    conn.Open();

                    // Parameterized query to prevent SQL Injection
                    string query = "SELECT COUNT(*) FROM Admins WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Login successful - open Dashboard
                            DashboardForm dashboard = new DashboardForm(username);
                            dashboard.Show();
                            this.Hide();
                        }
                        else
                        {
                            // Login failed
                            MessageBox.Show("Invalid username or password. Please try again.",
                                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.Clear();
                            txtPassword.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Exit Button Click
        // -------------------------------------------------------
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // -------------------------------------------------------
        // Allow pressing Enter in the password field to login
        // -------------------------------------------------------
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
