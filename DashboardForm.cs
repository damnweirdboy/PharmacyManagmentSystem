using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using PharmacyManagementSystem.Data;

namespace PharmacyManagementSystem.Forms
{
    /// <summary>
    /// Dashboard / Main Navigation Form.
    /// Shows quick-stat counters and navigation buttons to all sub-forms.
    /// </summary>
    public partial class DashboardForm : Form
    {
        private readonly string _currentUser;

        public DashboardForm(string username)
        {
            InitializeComponent();
            _currentUser = username;
            lblWelcome.Text = $"Welcome, {username}!";
        }

        // -------------------------------------------------------
        // Form Load - refresh stat counters
        // -------------------------------------------------------
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        // -------------------------------------------------------
        // Load statistics using ADO.NET
        // -------------------------------------------------------
        private void LoadStatistics()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConnection.GetConnectionString()))
                {
                    conn.Open();

                    lblMedicineCount.Text  = GetCount(conn, "Medicines").ToString();
                    lblSupplierCount.Text  = GetCount(conn, "Suppliers").ToString();
                    lblCustomerCount.Text  = GetCount(conn, "Customers").ToString();
                    lblSalesCount.Text     = GetCount(conn, "Sales").ToString();
                }
            }
            catch
            {
                lblMedicineCount.Text = lblSupplierCount.Text =
                lblCustomerCount.Text = lblSalesCount.Text = "N/A";
            }
        }

        private int GetCount(SqlConnection conn, string tableName)
        {
            using (SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {tableName}", conn))
                return (int)cmd.ExecuteScalar();
        }

        // -------------------------------------------------------
        // Navigation Buttons
        // -------------------------------------------------------
        private void btnMedicines_Click(object sender, EventArgs e)
        {
            new MedicinesForm().ShowDialog();
            LoadStatistics();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            new SuppliersForm().ShowDialog();
            LoadStatistics();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            new CustomersForm().ShowDialog();
            LoadStatistics();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            new SalesForm().ShowDialog();
            LoadStatistics();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            new ReportsForm().ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        // -------------------------------------------------------
        // Logout
        // -------------------------------------------------------
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?",
                "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AdminLoginForm login = new AdminLoginForm();
                login.Show();
                this.Close();
            }
        }
    }
}
