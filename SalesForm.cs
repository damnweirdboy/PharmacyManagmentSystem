using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PharmacyManagementSystem.Data;

namespace PharmacyManagementSystem.Forms
{
    /// <summary>
    /// Sales Form - uses ADO.NET exclusively.
    /// Handles Insert, Update, Delete, List of sales.
    /// Calculates total price, reduces medicine quantity on sale.
    /// </summary>
    public partial class SalesForm : Form
    {
        private readonly string _connStr = DatabaseConnection.GetConnectionString();
        private int _selectedSaleId = 0;

        public SalesForm()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------
        // Form Load
        // -------------------------------------------------------
        private void SalesForm_Load(object sender, EventArgs e)
        {
            LoadMedicinesCombo();
            LoadCustomersCombo();
            LoadSales();
        }

        // -------------------------------------------------------
        // Load medicines into ComboBox
        // -------------------------------------------------------
        private void LoadMedicinesCombo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string sql = "SELECT MedicineId, MedicineName FROM Medicines ORDER BY MedicineName";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbMedicine.DataSource    = dt;
                    cmbMedicine.DisplayMember = "MedicineName";
                    cmbMedicine.ValueMember   = "MedicineId";
                    cmbMedicine.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading medicines:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Load customers into ComboBox
        // -------------------------------------------------------
        private void LoadCustomersCombo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string sql = "SELECT CustomerId, CustomerName FROM Customers ORDER BY CustomerName";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbCustomer.DataSource    = dt;
                    cmbCustomer.DisplayMember = "CustomerName";
                    cmbCustomer.ValueMember   = "CustomerId";
                    cmbCustomer.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Load all sales into DataGridView (with JOIN)
        // -------------------------------------------------------
        private void LoadSales()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            s.SaleId,
                            m.MedicineName,
                            c.CustomerName,
                            s.QuantitySold,
                            s.TotalPrice,
                            s.SaleDate
                        FROM Sales s
                        INNER JOIN Medicines m ON s.MedicineId = m.MedicineId
                        INNER JOIN Customers c ON s.CustomerId = c.CustomerId
                        ORDER BY s.SaleDate DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvSales.DataSource = dt;

                    // Format columns
                    if (dgvSales.Columns.Count > 0)
                    {
                        dgvSales.Columns["SaleId"].HeaderText       = "ID";
                        dgvSales.Columns["MedicineName"].HeaderText = "Medicine";
                        dgvSales.Columns["CustomerName"].HeaderText = "Customer";
                        dgvSales.Columns["QuantitySold"].HeaderText = "Qty Sold";
                        dgvSales.Columns["TotalPrice"].HeaderText   = "Total Price";
                        dgvSales.Columns["SaleDate"].HeaderText     = "Sale Date";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Calculate Button - price × quantity
        // -------------------------------------------------------
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (cmbMedicine.SelectedValue == null)
            {
                MessageBox.Show("Please select a medicine first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantitySold.Text.Trim(), out int qty) || qty <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantitySold.Focus();
                return;
            }

            try
            {
                int medicineId = (int)cmbMedicine.SelectedValue;

                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string sql = "SELECT Price FROM Medicines WHERE MedicineId = @Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", medicineId);

                    decimal price       = (decimal)cmd.ExecuteScalar();
                    decimal totalPrice  = price * qty;
                    txtTotalPrice.Text  = totalPrice.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating total:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Insert Sale
        // -------------------------------------------------------
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            int medicineId = (int)cmbMedicine.SelectedValue;
            int customerId = (int)cmbCustomer.SelectedValue;
            int qty        = int.Parse(txtQuantitySold.Text.Trim());
            decimal total  = decimal.Parse(txtTotalPrice.Text.Trim());

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();

                    // 1) Check available stock
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT Quantity FROM Medicines WHERE MedicineId = @MedId", conn);
                    checkCmd.Parameters.AddWithValue("@MedId", medicineId);
                    int availableQty = (int)checkCmd.ExecuteScalar();

                    if (qty > availableQty)
                    {
                        MessageBox.Show(
                            $"Not enough medicine quantity.\nAvailable: {availableQty}, Requested: {qty}",
                            "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 2) Insert sale record
                    SqlCommand insertCmd = new SqlCommand(@"
                        INSERT INTO Sales (MedicineId, CustomerId, QuantitySold, TotalPrice, SaleDate)
                        VALUES (@MedId, @CustId, @Qty, @Total, @Date)", conn);

                    insertCmd.Parameters.AddWithValue("@MedId",  medicineId);
                    insertCmd.Parameters.AddWithValue("@CustId", customerId);
                    insertCmd.Parameters.AddWithValue("@Qty",    qty);
                    insertCmd.Parameters.AddWithValue("@Total",  total);
                    insertCmd.Parameters.AddWithValue("@Date",   dtpSaleDate.Value);
                    insertCmd.ExecuteNonQuery();

                    // 3) Reduce medicine quantity
                    SqlCommand updateQtyCmd = new SqlCommand(@"
                        UPDATE Medicines SET Quantity = Quantity - @Qty
                        WHERE MedicineId = @MedId", conn);
                    updateQtyCmd.Parameters.AddWithValue("@Qty",   qty);
                    updateQtyCmd.Parameters.AddWithValue("@MedId", medicineId);
                    updateQtyCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Sale recorded successfully! Medicine stock reduced.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSales();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting sale:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Update Sale
        // -------------------------------------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedSaleId == 0)
            {
                MessageBox.Show("Please select a sale from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            int medicineId = (int)cmbMedicine.SelectedValue;
            int customerId = (int)cmbCustomer.SelectedValue;
            int qty        = int.Parse(txtQuantitySold.Text.Trim());
            decimal total  = decimal.Parse(txtTotalPrice.Text.Trim());

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"
                        UPDATE Sales
                        SET MedicineId   = @MedId,
                            CustomerId   = @CustId,
                            QuantitySold = @Qty,
                            TotalPrice   = @Total,
                            SaleDate     = @Date
                        WHERE SaleId = @SaleId", conn);

                    cmd.Parameters.AddWithValue("@MedId",  medicineId);
                    cmd.Parameters.AddWithValue("@CustId", customerId);
                    cmd.Parameters.AddWithValue("@Qty",    qty);
                    cmd.Parameters.AddWithValue("@Total",  total);
                    cmd.Parameters.AddWithValue("@Date",   dtpSaleDate.Value);
                    cmd.Parameters.AddWithValue("@SaleId", _selectedSaleId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Sale updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSales();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating sale:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Delete Sale
        // -------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedSaleId == 0)
            {
                MessageBox.Show("Please select a sale from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this sale?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Sales WHERE SaleId = @SaleId", conn);
                    cmd.Parameters.AddWithValue("@SaleId", _selectedSaleId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Sale deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSales();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting sale:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // List Button
        // -------------------------------------------------------
        private void btnList_Click(object sender, EventArgs e)
        {
            LoadSales();
        }

        // -------------------------------------------------------
        // Clear Button
        // -------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        // -------------------------------------------------------
        // Back Button
        // -------------------------------------------------------
        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        // -------------------------------------------------------
        // DataGridView row click
        // -------------------------------------------------------
        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvSales.Rows[e.RowIndex];

            // Store selected sale ID
            _selectedSaleId = Convert.ToInt32(row.Cells["SaleId"].Value);

            // Populate quantity and total
            txtQuantitySold.Text = row.Cells["QuantitySold"].Value?.ToString();
            txtTotalPrice.Text   = row.Cells["TotalPrice"].Value?.ToString();

            if (row.Cells["SaleDate"].Value != null &&
                DateTime.TryParse(row.Cells["SaleDate"].Value.ToString(), out DateTime dt))
                dtpSaleDate.Value = dt;

            // Try to match ComboBox values by name
            string medicineName = row.Cells["MedicineName"].Value?.ToString();
            string customerName = row.Cells["CustomerName"].Value?.ToString();

            foreach (DataRowView item in cmbMedicine.Items)
                if (item["MedicineName"].ToString() == medicineName) { cmbMedicine.SelectedItem = item; break; }

            foreach (DataRowView item in cmbCustomer.Items)
                if (item["CustomerName"].ToString() == customerName) { cmbCustomer.SelectedItem = item; break; }
        }

        // -------------------------------------------------------
        // Validation
        // -------------------------------------------------------
        private bool ValidateInputs()
        {
            if (cmbMedicine.SelectedValue == null)
            {
                MessageBox.Show("Please select a medicine.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtQuantitySold.Text.Trim(), out int qty) || qty <= 0)
            {
                MessageBox.Show("Quantity sold must be a positive integer.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantitySold.Focus();
                return false;
            }

            if (!decimal.TryParse(txtTotalPrice.Text.Trim(), out decimal total) || total <= 0)
            {
                MessageBox.Show("Please calculate the total price first using the Calculate button.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearFields()
        {
            _selectedSaleId      = 0;
            cmbMedicine.SelectedIndex  = -1;
            cmbCustomer.SelectedIndex  = -1;
            txtQuantitySold.Clear();
            txtTotalPrice.Clear();
            dtpSaleDate.Value    = DateTime.Today;
        }
    }
}
