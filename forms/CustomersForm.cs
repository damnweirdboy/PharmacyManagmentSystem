using System;
using System.Linq;
using System.Windows.Forms;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Forms
{
    /// <summary>
    /// Customers Form - uses Entity Framework for all CRUD operations.
    /// </summary>
    public partial class CustomersForm : Form
    {
        private PharmacyDbContext _context;

        public CustomersForm()
        {
            InitializeComponent();
            _context = new PharmacyDbContext();
        }

        // -------------------------------------------------------
        // Form Load
        // -------------------------------------------------------
        private void CustomersForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        // -------------------------------------------------------
        // Load all customers
        // -------------------------------------------------------
        private void LoadCustomers()
        {
            try
            {
                dgvCustomers.DataSource = _context.Customers.ToList();

                // Hide navigation property column
                if (dgvCustomers.Columns["Sales"] != null)
                    dgvCustomers.Columns["Sales"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Insert
        // -------------------------------------------------------
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                Customer cust = new Customer
                {
                    CustomerName = txtName.Text.Trim(),
                    Phone        = txtPhone.Text.Trim(),
                    Email        = txtEmail.Text.Trim(),
                    Address      = txtAddress.Text.Trim()
                };

                _context.Customers.Add(cust);
                _context.SaveChanges();

                MessageBox.Show("Customer added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Update
        // -------------------------------------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please select a customer first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                int id   = int.Parse(txtId.Text);
                var cust = _context.Customers.Find(id);

                if (cust == null)
                {
                    MessageBox.Show("Customer not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cust.CustomerName = txtName.Text.Trim();
                cust.Phone        = txtPhone.Text.Trim();
                cust.Email        = txtEmail.Text.Trim();
                cust.Address      = txtAddress.Text.Trim();

                _context.SaveChanges();

                MessageBox.Show("Customer updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Delete
        // -------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please select a customer first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this customer?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int id   = int.Parse(txtId.Text);
                var cust = _context.Customers.Find(id);

                if (cust == null)
                {
                    MessageBox.Show("Customer not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _context.Customers.Remove(cust);
                _context.SaveChanges();

                MessageBox.Show("Customer deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // List
        // -------------------------------------------------------
        private void btnList_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        // -------------------------------------------------------
        // Search
        // -------------------------------------------------------
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadCustomers();
                return;
            }

            try
            {
                var results = _context.Customers
                    .Where(c => c.CustomerName.ToLower().Contains(keyword))
                    .ToList();

                dgvCustomers.DataSource = results;

                if (dgvCustomers.Columns["Sales"] != null)
                    dgvCustomers.Columns["Sales"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Clear / Back
        // -------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e) => ClearFields();
        private void btnBack_Click(object sender, EventArgs e)  => this.Close();

        // -------------------------------------------------------
        // Row Click
        // -------------------------------------------------------
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
            txtId.Text      = row.Cells["CustomerId"].Value?.ToString();
            txtName.Text    = row.Cells["CustomerName"].Value?.ToString();
            txtPhone.Text   = row.Cells["Phone"].Value?.ToString();
            txtEmail.Text   = row.Cells["Email"].Value?.ToString();
            txtAddress.Text = row.Cells["Address"].Value?.ToString();
        }

        // -------------------------------------------------------
        // Validation
        // -------------------------------------------------------
        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Customer name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            return true;
        }

        private void ClearFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtSearch.Clear();
        }

        private void CustomersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _context?.Dispose();
        }
    }
}
