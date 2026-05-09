using System;
using System.Linq;
using System.Windows.Forms;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Forms
{
    /// <summary>
    /// Medicines Form - uses Entity Framework for all CRUD operations.
    /// </summary>
    public partial class MedicinesForm : Form
    {
        private PharmacyDbContext _context;

        public MedicinesForm()
        {
            InitializeComponent();
            _context = new PharmacyDbContext();
        }

        // -------------------------------------------------------
        // Form Load
        // -------------------------------------------------------
        private void MedicinesForm_Load(object sender, EventArgs e)
        {
            LoadMedicines();
        }

        // -------------------------------------------------------
        // Load all medicines into the DataGridView
        // -------------------------------------------------------
        private void LoadMedicines()
        {
            try
            {
                var medicines = _context.Medicines.ToList();
                dgvMedicines.DataSource = medicines;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading medicines:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Format DataGridView columns
        // -------------------------------------------------------
        private void FormatGrid()
        {
            if (dgvMedicines.Columns.Count == 0) return;

            // Hide navigation columns
            if (dgvMedicines.Columns["Sales"] != null)
                dgvMedicines.Columns["Sales"].Visible = false;

            dgvMedicines.Columns["MedicineId"].HeaderText   = "ID";
            dgvMedicines.Columns["MedicineName"].HeaderText = "Medicine Name";
            dgvMedicines.Columns["Category"].HeaderText     = "Category";
            dgvMedicines.Columns["Price"].HeaderText        = "Price";
            dgvMedicines.Columns["Quantity"].HeaderText     = "Quantity";
            dgvMedicines.Columns["ExpiryDate"].HeaderText   = "Expiry Date";
        }

        // -------------------------------------------------------
        // Insert Button
        // -------------------------------------------------------
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                Medicine med = new Medicine
                {
                    MedicineName = txtName.Text.Trim(),
                    Category     = txtCategory.Text.Trim(),
                    Price        = decimal.Parse(txtPrice.Text.Trim()),
                    Quantity     = int.Parse(txtQuantity.Text.Trim()),
                    ExpiryDate   = dtpExpiry.Value.Date
                };

                _context.Medicines.Add(med);
                _context.SaveChanges();

                MessageBox.Show("Medicine added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMedicines();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding medicine:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Update Button
        // -------------------------------------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please select a medicine from the list first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                int id  = int.Parse(txtId.Text);
                var med = _context.Medicines.Find(id);

                if (med == null)
                {
                    MessageBox.Show("Medicine not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                med.MedicineName = txtName.Text.Trim();
                med.Category     = txtCategory.Text.Trim();
                med.Price        = decimal.Parse(txtPrice.Text.Trim());
                med.Quantity     = int.Parse(txtQuantity.Text.Trim());
                med.ExpiryDate   = dtpExpiry.Value.Date;

                _context.SaveChanges();

                MessageBox.Show("Medicine updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMedicines();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating medicine:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Delete Button
        // -------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please select a medicine from the list first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this medicine?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int id  = int.Parse(txtId.Text);
                var med = _context.Medicines.Find(id);

                if (med == null)
                {
                    MessageBox.Show("Medicine not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _context.Medicines.Remove(med);
                _context.SaveChanges();

                MessageBox.Show("Medicine deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMedicines();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting medicine:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // List Button
        // -------------------------------------------------------
        private void btnList_Click(object sender, EventArgs e)
        {
            LoadMedicines();
        }

        // -------------------------------------------------------
        // Search Button
        // -------------------------------------------------------
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadMedicines();
                return;
            }

            try
            {
                var results = _context.Medicines
                    .Where(m => m.MedicineName.ToLower().Contains(keyword))
                    .ToList();

                dgvMedicines.DataSource = results;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching medicines:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Clear Button
        // -------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        // -------------------------------------------------------
        // Back Button
        // -------------------------------------------------------
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // -------------------------------------------------------
        // DataGridView row click - populate input fields
        // -------------------------------------------------------
        private void dgvMedicines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvMedicines.Rows[e.RowIndex];

            txtId.Text       = row.Cells["MedicineId"].Value?.ToString();
            txtName.Text     = row.Cells["MedicineName"].Value?.ToString();
            txtCategory.Text = row.Cells["Category"].Value?.ToString();
            txtPrice.Text    = row.Cells["Price"].Value?.ToString();
            txtQuantity.Text = row.Cells["Quantity"].Value?.ToString();

            if (row.Cells["ExpiryDate"].Value != null &&
                DateTime.TryParse(row.Cells["ExpiryDate"].Value.ToString(), out DateTime dt))
                dtpExpiry.Value = dt;
        }

        // -------------------------------------------------------
        // Validation
        // -------------------------------------------------------
        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Medicine name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price <= 0)
            {
                MessageBox.Show("Price must be a positive decimal number.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out int qty) || qty < 0)
            {
                MessageBox.Show("Quantity must be a positive integer.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            return true;
        }

        // -------------------------------------------------------
        // Clear fields
        // -------------------------------------------------------
        private void ClearFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtCategory.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtSearch.Clear();
            dtpExpiry.Value = DateTime.Today;
        }

        // -------------------------------------------------------
        // Form closing - dispose EF context
        // -------------------------------------------------------
        private void MedicinesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _context?.Dispose();
        }
    }
}
