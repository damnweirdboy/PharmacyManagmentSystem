using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubTitle;

        // Stats panel
        private Panel pnlStats;

        // Stat card labels
        private Panel cardMed;
        private Label lblMedTitle;
        private Label lblMedicineCount;

        private Panel cardSup;
        private Label lblSupTitle;
        private Label lblSupplierCount;

        private Panel cardCust;
        private Label lblCustTitle;
        private Label lblCustomerCount;

        private Panel cardSales;
        private Label lblSalesTitle;
        private Label lblSalesCount;

        // Alert labels
        private Label lblLowStockCount;
        private Label lblExpiredCount;

        // Report title
        private Label lblReportTitle;

        // Buttons
        private Panel  pnlButtons;
        private Button btnLowStock;
        private Button btnExpired;
        private Button btnAllMedicines;
        private Button btnAllSales;
        private Button btnRefresh;
        private Button btnBack;

        // Grid
        private DataGridView dgvReport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader       = new Panel();
            this.lblTitle        = new Label();
            this.lblSubTitle     = new Label();
            this.pnlStats        = new Panel();
            this.cardMed         = new Panel();
            this.lblMedTitle     = new Label();
            this.lblMedicineCount = new Label();
            this.cardSup         = new Panel();
            this.lblSupTitle     = new Label();
            this.lblSupplierCount = new Label();
            this.cardCust        = new Panel();
            this.lblCustTitle    = new Label();
            this.lblCustomerCount = new Label();
            this.cardSales       = new Panel();
            this.lblSalesTitle   = new Label();
            this.lblSalesCount   = new Label();
            this.lblLowStockCount = new Label();
            this.lblExpiredCount  = new Label();
            this.lblReportTitle  = new Label();
            this.pnlButtons      = new Panel();
            this.btnLowStock     = new Button();
            this.btnExpired      = new Button();
            this.btnAllMedicines = new Button();
            this.btnAllSales     = new Button();
            this.btnRefresh      = new Button();
            this.btnBack         = new Button();
            this.dgvReport       = new DataGridView();

            this.SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            this.Text            = "Pharmacy Management - Reports";
            this.Size            = new Size(1050, 720);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = Color.FromArgb(236, 240, 245);
            this.Font            = new Font("Segoe UI", 9.5f);
            this.Load           += new System.EventHandler(this.ReportsForm_Load);

            // ── Header ─────────────────────────────────────────────
            this.pnlHeader.Dock      = DockStyle.Top;
            this.pnlHeader.Height    = 70;
            this.pnlHeader.BackColor = Color.FromArgb(44, 62, 80);

            this.lblTitle.Text      = "📊 Reports & Statistics";
            this.lblTitle.Font      = new Font("Segoe UI", 15f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Size      = new Size(500, 36);
            this.lblTitle.Location  = new Point(20, 8);

            this.lblSubTitle.Text      = "ADO.NET | Real-time statistics and inventory reports";
            this.lblSubTitle.Font      = new Font("Segoe UI", 9f);
            this.lblSubTitle.ForeColor = Color.FromArgb(180, 190, 210);
            this.lblSubTitle.Size      = new Size(600, 20);
            this.lblSubTitle.Location  = new Point(22, 46);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubTitle);

            // ── Stat Cards Row ─────────────────────────────────────
            this.pnlStats.Location  = new Point(10, 85);
            this.pnlStats.Size      = new Size(1020, 100);
            this.pnlStats.BackColor = Color.Transparent;

            MakeCard(cardMed,   lblMedTitle,   lblMedicineCount,  "💊 Medicines",  Color.FromArgb(41, 128, 185), 0);
            MakeCard(cardSup,   lblSupTitle,   lblSupplierCount,  "🏭 Suppliers",  Color.FromArgb(39, 174, 96),  260);
            MakeCard(cardCust,  lblCustTitle,  lblCustomerCount,  "👤 Customers",  Color.FromArgb(142, 68, 173), 520);
            MakeCard(cardSales, lblSalesTitle, lblSalesCount,     "🧾 Sales",      Color.FromArgb(211, 84, 0),   780);

            this.pnlStats.Controls.Add(cardMed);
            this.pnlStats.Controls.Add(cardSup);
            this.pnlStats.Controls.Add(cardCust);
            this.pnlStats.Controls.Add(cardSales);

            // ── Alert Badges ───────────────────────────────────────
            this.lblLowStockCount.Text      = "  Low Stock Items: 0";
            this.lblLowStockCount.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblLowStockCount.ForeColor = Color.White;
            this.lblLowStockCount.BackColor = Color.FromArgb(230, 126, 34);
            this.lblLowStockCount.Size      = new Size(240, 28);
            this.lblLowStockCount.Location  = new Point(10, 195);
            this.lblLowStockCount.TextAlign = ContentAlignment.MiddleLeft;

            this.lblExpiredCount.Text      = "  Expired Items: 0";
            this.lblExpiredCount.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblExpiredCount.ForeColor = Color.White;
            this.lblExpiredCount.BackColor = Color.FromArgb(192, 57, 43);
            this.lblExpiredCount.Size      = new Size(240, 28);
            this.lblExpiredCount.Location  = new Point(265, 195);
            this.lblExpiredCount.TextAlign = ContentAlignment.MiddleLeft;

            // ── Button Row ─────────────────────────────────────────
            this.pnlButtons.Location  = new Point(10, 235);
            this.pnlButtons.Size      = new Size(1020, 46);
            this.pnlButtons.BackColor = Color.Transparent;

            MakeBtn(btnLowStock,     "📦 Low Stock",      Color.FromArgb(230, 126, 34),  0,   0, 160, 40);
            MakeBtn(btnExpired,      "⚠️ Expired",       Color.FromArgb(192, 57,  43),  170, 0, 160, 40);
            MakeBtn(btnAllMedicines, "💊 All Medicines",  Color.FromArgb(41,  128, 185), 340, 0, 160, 40);
            MakeBtn(btnAllSales,     "🧾 All Sales",      Color.FromArgb(44,  62,  80),  510, 0, 160, 40);
            MakeBtn(btnRefresh,      "🔄 Refresh Counts", Color.FromArgb(39,  174, 96),  680, 0, 160, 40);
            MakeBtn(btnBack,         "Back",              Color.FromArgb(127, 140, 141), 850, 0, 160, 40);

            this.btnLowStock.Click     += new System.EventHandler(this.btnLowStock_Click);
            this.btnExpired.Click      += new System.EventHandler(this.btnExpired_Click);
            this.btnAllMedicines.Click += new System.EventHandler(this.btnAllMedicines_Click);
            this.btnAllSales.Click     += new System.EventHandler(this.btnAllSales_Click);
            this.btnRefresh.Click      += new System.EventHandler(this.btnRefresh_Click);
            this.btnBack.Click         += new System.EventHandler(this.btnBack_Click);

            this.pnlButtons.Controls.Add(btnLowStock);
            this.pnlButtons.Controls.Add(btnExpired);
            this.pnlButtons.Controls.Add(btnAllMedicines);
            this.pnlButtons.Controls.Add(btnAllSales);
            this.pnlButtons.Controls.Add(btnRefresh);
            this.pnlButtons.Controls.Add(btnBack);

            // ── Report Title ───────────────────────────────────────
            this.lblReportTitle.Text      = "Select a report from the buttons above.";
            this.lblReportTitle.Font      = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.lblReportTitle.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblReportTitle.Size      = new Size(1020, 28);
            this.lblReportTitle.Location  = new Point(10, 290);

            // ── DataGridView ──────────────────────────────────────
            this.dgvReport.Location            = new Point(10, 325);
            this.dgvReport.Size                = new Size(1020, 350);
            this.dgvReport.BackgroundColor     = Color.White;
            this.dgvReport.BorderStyle         = BorderStyle.None;
            this.dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.MultiSelect         = false;
            this.dgvReport.ReadOnly            = true;
            this.dgvReport.AllowUserToAddRows  = false;
            this.dgvReport.RowHeadersVisible   = false;
            this.dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 245);
            this.dgvReport.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.dgvReport.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(44, 62, 80);
            this.dgvReport.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
            this.dgvReport.EnableHeadersVisualStyles = false;

            // ── Add all to Form ───────────────────────────────────
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.lblLowStockCount);
            this.Controls.Add(this.lblExpiredCount);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.lblReportTitle);
            this.Controls.Add(this.dgvReport);

            this.ResumeLayout(false);
        }

        // ── Helpers ───────────────────────────────────────────────
        private void MakeCard(Panel card, Label title, Label count,
                               string titleText, Color color, int x)
        {
            card.Size      = new Size(240, 90);
            card.Location  = new Point(x, 0);
            card.BackColor = color;

            title.Text      = titleText;
            title.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(210, 230, 255);
            title.Size      = new Size(220, 22);
            title.Location  = new Point(10, 8);

            count.Text      = "0";
            count.Font      = new Font("Segoe UI", 24f, FontStyle.Bold);
            count.ForeColor = Color.White;
            count.Size      = new Size(220, 50);
            count.Location  = new Point(10, 32);
            count.TextAlign = ContentAlignment.MiddleLeft;

            card.Controls.Add(title);
            card.Controls.Add(count);
        }

        private void MakeBtn(Button btn, string text, Color color,
                              int x, int y, int w, int h)
        {
            btn.Text      = text;
            btn.Size      = new Size(w, h);
            btn.Location  = new Point(x, y);
            btn.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor    = Cursors.Hand;
        }
    }
}
