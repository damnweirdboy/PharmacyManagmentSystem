using System;
using System.Linq;
using System.Windows.Forms;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Forms
{
    /// <summary>
    /// Suppliers Form - uses Entity Framework for all CRUD operations.
    /// </summary>
    public partial class SuppliersForm : Form
    {
        private PharmacyDbContext _context;

        public SuppliersForm()
        {
            InitializeComponent();
            _context = new PharmacyDbContext();
        }

        // -------------------------------------------------------
        // Form Load
        // -------------------------------------------------------
        private void SuppliersForm_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        // -------------------------------------------------------
        // Load all suppliers
        // -------------------------------------------------------
        private void LoadSuppliers()
        {
            try
            {
                dgvSuppliers.DataSource = _context.Suppliers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers:\n" + ex.Message,
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
                Supplier sup = new Supplier
                {
                    SupplierName = txtName.Text.Trim(),
                    Phone        = txtPhone.Text.Trim(),
                    Email        = txtEmail.Text.Trim(),
                    Address      = txtAddress.Text.Trim()
                };

                _context.Suppliers.Add(sup);
                _context.SaveChanges();

                MessageBox.Show("Supplier added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSuppliers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding supplier:\n" + ex.Message,
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
                MessageBox.Show("Please select a supplier first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                int id  = int.Parse(txtId.Text);
                var sup = _context.Suppliers.Find(id);

                if (sup == null)
                {
                    MessageBox.Show("Supplier not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sup.SupplierName = txtName.Text.Trim();
                sup.Phone        = txtPhone.Text.Trim();
                sup.Email        = txtEmail.Text.Trim();
                sup.Address      = txtAddress.Text.Trim();

                _context.SaveChanges();

                MessageBox.Show("Supplier updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSuppliers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating supplier:\n" + ex.Message,
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
                MessageBox.Show("Please select a supplier first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this supplier?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int id  = int.Parse(txtId.Text);
                var sup = _context.Suppliers.Find(id);

                if (sup == null)
                {
                    MessageBox.Show("Supplier not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _context.Suppliers.Remove(sup);
                _context.SaveChanges();

                MessageBox.Show("Supplier deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSuppliers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting supplier:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // List
        // -------------------------------------------------------
        private void btnList_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        // -------------------------------------------------------
        // Search
        // -------------------------------------------------------
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadSuppliers();
                return;
            }

            try
            {
                dgvSuppliers.DataSource = _context.Suppliers
                    .Where(s => s.SupplierName.ToLower().Contains(keyword))
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Clear
        // -------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        // -------------------------------------------------------
        // Back
        // -------------------------------------------------------
        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        // -------------------------------------------------------
        // Row click
        // -------------------------------------------------------
        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvSuppliers.Rows[e.RowIndex];
            txtId.Text      = row.Cells["SupplierId"].Value?.ToString();
            txtName.Text    = row.Cells["SupplierName"].Value?.ToString();
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
                MessageBox.Show("Supplier name is required.", "Validation",
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

        private void SuppliersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _context?.Dispose();
        }
    }
}
