using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PharmacyManagementSystem.Data;

namespace PharmacyManagementSystem.Forms
{
    /// <summary>
    /// Reports Form - uses ADO.NET to show summary statistics and filtered reports.
    /// Shows: total counts, low-stock medicines, expired medicines.
    /// </summary>
    public partial class ReportsForm : Form
    {
        private readonly string _connStr = DatabaseConnection.GetConnectionString();

        public ReportsForm()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------
        // Form Load - auto refresh counts on open
        // -------------------------------------------------------
        private void ReportsForm_Load(object sender, EventArgs e)
        {
            RefreshCounts();
        }

        // -------------------------------------------------------
        // Refresh Counts Button
        // -------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshCounts();
        }

        // -------------------------------------------------------
        // Load summary counts from database using ADO.NET
        // -------------------------------------------------------
        private void RefreshCounts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    lblMedicineCount.Text  = "  " + GetScalar(conn, "SELECT COUNT(*) FROM Medicines");
                    lblSupplierCount.Text  = "  " + GetScalar(conn, "SELECT COUNT(*) FROM Suppliers");
                    lblCustomerCount.Text  = "  " + GetScalar(conn, "SELECT COUNT(*) FROM Customers");
                    lblSalesCount.Text     = "  " + GetScalar(conn, "SELECT COUNT(*) FROM Sales");

                    // Low stock count
                    int lowStock = (int)GetScalar(conn, "SELECT COUNT(*) FROM Medicines WHERE Quantity <= 10");
                    lblLowStockCount.Text = "  Low Stock Items: " + lowStock;

                    // Expired count
                    int expired = (int)GetScalar(conn,
                        $"SELECT COUNT(*) FROM Medicines WHERE ExpiryDate < '{DateTime.Today:yyyy-MM-dd}'");
                    lblExpiredCount.Text = "  Expired Items: " + expired;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report data:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private object GetScalar(SqlConnection conn, string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
                return cmd.ExecuteScalar();
        }

        // -------------------------------------------------------
        // Low Stock Medicines Button (Quantity <= 10)
        // -------------------------------------------------------
        private void btnLowStock_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string sql = @"
                        SELECT
                            MedicineId   AS [ID],
                            MedicineName AS [Medicine Name],
                            Category     AS [Category],
                            Price        AS [Price],
                            Quantity     AS [Quantity],
                            ExpiryDate   AS [Expiry Date]
                        FROM Medicines
                        WHERE Quantity <= 10
                        ORDER BY Quantity ASC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReport.DataSource = dt;
                    lblReportTitle.Text = $"📦 Low Stock Medicines (Qty ≤ 10)  —  {dt.Rows.Count} record(s) found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Expired Medicines Button (ExpiryDate < today)
        // -------------------------------------------------------
        private void btnExpired_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT
                            MedicineId   AS [ID],
                            MedicineName AS [Medicine Name],
                            Category     AS [Category],
                            Price        AS [Price],
                            Quantity     AS [Quantity],
                            ExpiryDate   AS [Expiry Date]
                        FROM Medicines
                        WHERE ExpiryDate < @Today
                        ORDER BY ExpiryDate ASC", conn);

                    cmd.Parameters.AddWithValue("@Today", DateTime.Today);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReport.DataSource = dt;
                    lblReportTitle.Text = $"⚠️ Expired Medicines  —  {dt.Rows.Count} record(s) found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // All Medicines Report
        // -------------------------------------------------------
        private void btnAllMedicines_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string sql = @"
                        SELECT
                            MedicineId   AS [ID],
                            MedicineName AS [Medicine Name],
                            Category     AS [Category],
                            Price        AS [Price],
                            Quantity     AS [Quantity],
                            ExpiryDate   AS [Expiry Date]
                        FROM Medicines
                        ORDER BY MedicineName ASC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReport.DataSource = dt;
                    lblReportTitle.Text = $"💊 All Medicines  —  {dt.Rows.Count} record(s) found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // All Sales Report (with medicine and customer names)
        // -------------------------------------------------------
        private void btnAllSales_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string sql = @"
                        SELECT
                            s.SaleId         AS [Sale ID],
                            m.MedicineName   AS [Medicine],
                            c.CustomerName   AS [Customer],
                            s.QuantitySold   AS [Qty Sold],
                            s.TotalPrice     AS [Total Price],
                            s.SaleDate       AS [Sale Date]
                        FROM Sales s
                        INNER JOIN Medicines m ON s.MedicineId = m.MedicineId
                        INNER JOIN Customers c ON s.CustomerId = c.CustomerId
                        ORDER BY s.SaleDate DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReport.DataSource = dt;
                    lblReportTitle.Text = $"🧾 All Sales  —  {dt.Rows.Count} record(s) found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------
        // Back Button
        // -------------------------------------------------------
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
