using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Panel  pnlHeader;
        private Label  lblTitle;
        private Label  lblWelcome;

        // Stat cards
        private Panel  pnlStats;
        private Panel  cardMedicines;
        private Panel  cardSuppliers;
        private Panel  cardCustomers;
        private Panel  cardSales;
        private Label  lblMedicineStat;
        private Label  lblMedicineCount;
        private Label  lblSupplierStat;
        private Label  lblSupplierCount;
        private Label  lblCustomerStat;
        private Label  lblCustomerCount;
        private Label  lblSalesStat;
        private Label  lblSalesCount;

        // Nav buttons
        private Panel  pnlNav;
        private Button btnMedicines;
        private Button btnSuppliers;
        private Button btnCustomers;
        private Button btnSales;
        private Button btnReports;
        private Button btnRefresh;
        private Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader       = new Panel();
            this.lblTitle        = new Label();
            this.lblWelcome      = new Label();
            this.pnlStats        = new Panel();
            this.cardMedicines   = new Panel();
            this.cardSuppliers   = new Panel();
            this.cardCustomers   = new Panel();
            this.cardSales       = new Panel();
            this.lblMedicineStat  = new Label();
            this.lblMedicineCount = new Label();
            this.lblSupplierStat  = new Label();
            this.lblSupplierCount = new Label();
            this.lblCustomerStat  = new Label();
            this.lblCustomerCount = new Label();
            this.lblSalesStat    = new Label();
            this.lblSalesCount   = new Label();
            this.pnlNav          = new Panel();
            this.btnMedicines    = new Button();
            this.btnSuppliers    = new Button();
            this.btnCustomers    = new Button();
            this.btnSales        = new Button();
            this.btnReports      = new Button();
            this.btnRefresh      = new Button();
            this.btnLogout       = new Button();

            this.SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            this.Text            = "Pharmacy Management System - Dashboard";
            this.Size            = new Size(880, 620);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = Color.FromArgb(236, 240, 245);
            this.Font            = new Font("Segoe UI", 9.5f);
            this.Load           += new System.EventHandler(this.DashboardForm_Load);

            // ── pnlHeader ────────────────────────────────────────
            this.pnlHeader.Dock      = DockStyle.Top;
            this.pnlHeader.Height    = 80;
            this.pnlHeader.BackColor = Color.FromArgb(20, 52, 100);

            this.lblTitle.Text      = "💊 Pharmacy Management System";
            this.lblTitle.Font      = new Font("Segoe UI", 16f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Size      = new Size(500, 40);
            this.lblTitle.Location  = new Point(20, 10);

            this.lblWelcome.Text      = "Welcome!";
            this.lblWelcome.Font      = new Font("Segoe UI", 10f);
            this.lblWelcome.ForeColor = Color.FromArgb(180, 200, 230);
            this.lblWelcome.Size      = new Size(500, 24);
            this.lblWelcome.Location  = new Point(22, 50);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblWelcome);

            // ── pnlStats (stat cards row) ─────────────────────────
            this.pnlStats.Location  = new Point(20, 100);
            this.pnlStats.Size      = new Size(840, 120);
            this.pnlStats.BackColor = Color.Transparent;

            BuildStatCard(this.cardMedicines, this.lblMedicineStat,  this.lblMedicineCount,  0,   "💊 Medicines",  Color.FromArgb(41, 128, 185));
            BuildStatCard(this.cardSuppliers, this.lblSupplierStat,  this.lblSupplierCount,  210, "🏭 Suppliers",  Color.FromArgb(39, 174, 96));
            BuildStatCard(this.cardCustomers, this.lblCustomerStat,  this.lblCustomerCount,  420, "👤 Customers",  Color.FromArgb(142, 68, 173));
            BuildStatCard(this.cardSales,     this.lblSalesStat,      this.lblSalesCount,     630, "🧾 Sales",      Color.FromArgb(211, 84, 0));

            this.pnlStats.Controls.Add(this.cardMedicines);
            this.pnlStats.Controls.Add(this.cardSuppliers);
            this.pnlStats.Controls.Add(this.cardCustomers);
            this.pnlStats.Controls.Add(this.cardSales);

            // ── pnlNav (navigation buttons) ───────────────────────
            this.pnlNav.Location  = new Point(20, 240);
            this.pnlNav.Size      = new Size(840, 310);
            this.pnlNav.BackColor = Color.Transparent;

            BuildNavButton(this.btnMedicines, "💊 Medicines",  Color.FromArgb(41,  128, 185), 0,   0,   this.btnMedicines_Click);
            BuildNavButton(this.btnSuppliers, "🏭 Suppliers",  Color.FromArgb(39,  174, 96),  210, 0,   this.btnSuppliers_Click);
            BuildNavButton(this.btnCustomers, "👤 Customers",  Color.FromArgb(142, 68,  173), 420, 0,   this.btnCustomers_Click);
            BuildNavButton(this.btnSales,     "🧾 Sales",      Color.FromArgb(211, 84,  0),   630, 0,   this.btnSales_Click);
            BuildNavButton(this.btnReports,   "📊 Reports",    Color.FromArgb(52,  73,  94),  0,   145, this.btnReports_Click);
            BuildNavButton(this.btnRefresh,   "🔄 Refresh",   Color.FromArgb(41,  128, 185), 210, 145, this.btnRefresh_Click);
            BuildNavButton(this.btnLogout,    "🔒 Logout",     Color.FromArgb(192, 57,  43),  630, 145, this.btnLogout_Click);

            this.pnlNav.Controls.Add(this.btnMedicines);
            this.pnlNav.Controls.Add(this.btnSuppliers);
            this.pnlNav.Controls.Add(this.btnCustomers);
            this.pnlNav.Controls.Add(this.btnSales);
            this.pnlNav.Controls.Add(this.btnReports);
            this.pnlNav.Controls.Add(this.btnRefresh);
            this.pnlNav.Controls.Add(this.btnLogout);

            // ── Add to form ───────────────────────────────────────
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlNav);

            this.ResumeLayout(false);
        }

        // ── Helper: build a stat card ─────────────────────────────
        private void BuildStatCard(Panel card, Label lblText, Label lblCount,
                                   int x, string title, Color color)
        {
            card.Size      = new Size(190, 100);
            card.Location  = new Point(x, 0);
            card.BackColor = color;

            lblText.Text      = title;
            lblText.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblText.ForeColor = Color.FromArgb(220, 230, 255);
            lblText.Size      = new Size(170, 24);
            lblText.Location  = new Point(10, 10);

            lblCount.Text      = "0";
            lblCount.Font      = new Font("Segoe UI", 26f, FontStyle.Bold);
            lblCount.ForeColor = Color.White;
            lblCount.Size      = new Size(170, 55);
            lblCount.Location  = new Point(10, 36);
            lblCount.TextAlign = ContentAlignment.MiddleLeft;

            card.Controls.Add(lblText);
            card.Controls.Add(lblCount);
        }

        // ── Helper: build a nav button ────────────────────────────
        private void BuildNavButton(Button btn, string text, Color color,
                                    int x, int y,
                                    System.EventHandler handler)
        {
            btn.Text      = text;
            btn.Size      = new Size(190, 120);
            btn.Location  = new Point(x, y);
            btn.Font      = new Font("Segoe UI", 10f, FontStyle.Bold);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor    = Cursors.Hand;
            btn.Click    += handler;
        }
    }
}
