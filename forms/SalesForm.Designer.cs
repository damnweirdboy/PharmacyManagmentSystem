using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    partial class SalesForm
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Panel  pnlHeader;
        private Label  lblTitle;
        private Label  lblSubTitle;

        // Input Panel
        private Panel          pnlInput;
        private Label          lblMedicine;
        private ComboBox       cmbMedicine;
        private Label          lblCustomer;
        private ComboBox       cmbCustomer;
        private Label          lblQtySold;
        private TextBox        txtQuantitySold;
        private Label          lblTotalPrice;
        private TextBox        txtTotalPrice;
        private Label          lblSaleDate;
        private DateTimePicker dtpSaleDate;

        // Buttons
        private Button btnCalculate;
        private Button btnInsert;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnList;
        private Button btnClear;
        private Button btnBack;

        // Grid
        private DataGridView dgvSales;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader      = new Panel();
            this.lblTitle       = new Label();
            this.lblSubTitle    = new Label();
            this.pnlInput       = new Panel();
            this.lblMedicine    = new Label();
            this.cmbMedicine    = new ComboBox();
            this.lblCustomer    = new Label();
            this.cmbCustomer    = new ComboBox();
            this.lblQtySold     = new Label();
            this.txtQuantitySold = new TextBox();
            this.lblTotalPrice  = new Label();
            this.txtTotalPrice  = new TextBox();
            this.lblSaleDate    = new Label();
            this.dtpSaleDate    = new DateTimePicker();
            this.btnCalculate   = new Button();
            this.btnInsert      = new Button();
            this.btnUpdate      = new Button();
            this.btnDelete      = new Button();
            this.btnList        = new Button();
            this.btnClear       = new Button();
            this.btnBack        = new Button();
            this.dgvSales       = new DataGridView();

            this.SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            this.Text            = "Pharmacy Management - Sales";
            this.Size            = new Size(1050, 680);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = Color.FromArgb(236, 240, 245);
            this.Font            = new Font("Segoe UI", 9.5f);
            this.Load           += new System.EventHandler(this.SalesForm_Load);

            // ── Header ─────────────────────────────────────────────
            this.pnlHeader.Dock      = DockStyle.Top;
            this.pnlHeader.Height    = 70;
            this.pnlHeader.BackColor = Color.FromArgb(211, 84, 0);

            this.lblTitle.Text      = "🧾 Sales Management";
            this.lblTitle.Font      = new Font("Segoe UI", 15f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Size      = new Size(500, 36);
            this.lblTitle.Location  = new Point(20, 8);

            this.lblSubTitle.Text      = "ADO.NET | Insert • Update • Delete • List • Stock Check";
            this.lblSubTitle.Font      = new Font("Segoe UI", 9f);
            this.lblSubTitle.ForeColor = Color.FromArgb(240, 200, 170);
            this.lblSubTitle.Size      = new Size(600, 20);
            this.lblSubTitle.Location  = new Point(22, 46);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubTitle);

            // ── Input Panel ───────────────────────────────────────
            this.pnlInput.Location  = new Point(10, 85);
            this.pnlInput.Size      = new Size(340, 555);
            this.pnlInput.BackColor = Color.White;

            // Medicine ComboBox
            AddLabel(this.pnlInput, this.lblMedicine, "Select Medicine *:", 10, 12);
            this.cmbMedicine.Location     = new Point(10, 34);
            this.cmbMedicine.Size         = new Size(310, 28);
            this.cmbMedicine.Font         = new Font("Segoe UI", 9.5f);
            this.cmbMedicine.DropDownStyle = ComboBoxStyle.DropDownList;
            this.pnlInput.Controls.Add(this.cmbMedicine);

            // Customer ComboBox
            AddLabel(this.pnlInput, this.lblCustomer, "Select Customer *:", 10, 72);
            this.cmbCustomer.Location     = new Point(10, 94);
            this.cmbCustomer.Size         = new Size(310, 28);
            this.cmbCustomer.Font         = new Font("Segoe UI", 9.5f);
            this.cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.pnlInput.Controls.Add(this.cmbCustomer);

            // Quantity Sold
            AddLabel(this.pnlInput, this.lblQtySold, "Quantity Sold *:", 10, 132);
            AddTextBox(this.pnlInput, this.txtQuantitySold, 10, 154, false);

            // Total Price (read-only, filled by Calculate)
            AddLabel(this.pnlInput, this.lblTotalPrice, "Total Price (auto):", 10, 192);
            AddTextBox(this.pnlInput, this.txtTotalPrice, 10, 214, true);

            // Sale Date
            AddLabel(this.pnlInput, this.lblSaleDate, "Sale Date:", 10, 252);
            this.dtpSaleDate.Location = new Point(10, 274);
            this.dtpSaleDate.Size     = new Size(310, 28);
            this.dtpSaleDate.Format   = DateTimePickerFormat.Short;
            this.pnlInput.Controls.Add(this.dtpSaleDate);

            // Calculate button
            StyleBtn(this.btnCalculate, "💰 Calculate Price", Color.FromArgb(22, 160, 133), 10, 318, 310, 36);
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            this.pnlInput.Controls.Add(this.btnCalculate);

            // Action buttons
            StyleBtn(this.btnInsert, "Add Sale",  Color.FromArgb(39,  174, 96),  10,  370, 145, 36);
            StyleBtn(this.btnUpdate, "Update",    Color.FromArgb(211, 84,  0),   175, 370, 145, 36);
            StyleBtn(this.btnDelete, "Delete",    Color.FromArgb(192, 57,  43),  10,  418, 145, 36);
            StyleBtn(this.btnList,   "List All",  Color.FromArgb(41,  128, 185), 175, 418, 145, 36);
            StyleBtn(this.btnClear,  "Clear",     Color.FromArgb(127, 140, 141), 10,  466, 145, 36);
            StyleBtn(this.btnBack,   "Back",      Color.FromArgb(127, 140, 141), 175, 466, 145, 36);

            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnList.Click   += new System.EventHandler(this.btnList_Click);
            this.btnClear.Click  += new System.EventHandler(this.btnClear_Click);
            this.btnBack.Click   += new System.EventHandler(this.btnBack_Click);

            this.pnlInput.Controls.Add(this.btnInsert);
            this.pnlInput.Controls.Add(this.btnUpdate);
            this.pnlInput.Controls.Add(this.btnDelete);
            this.pnlInput.Controls.Add(this.btnList);
            this.pnlInput.Controls.Add(this.btnClear);
            this.pnlInput.Controls.Add(this.btnBack);

            // ── DataGridView ──────────────────────────────────────
            this.dgvSales.Location            = new Point(365, 85);
            this.dgvSales.Size                = new Size(670, 555);
            this.dgvSales.BackgroundColor     = Color.White;
            this.dgvSales.BorderStyle         = BorderStyle.None;
            this.dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSales.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSales.MultiSelect         = false;
            this.dgvSales.ReadOnly            = true;
            this.dgvSales.AllowUserToAddRows  = false;
            this.dgvSales.RowHeadersVisible   = false;
            this.dgvSales.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 245);
            this.dgvSales.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.dgvSales.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(211, 84, 0);
            this.dgvSales.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
            this.dgvSales.EnableHeadersVisualStyles = false;
            this.dgvSales.CellClick += new DataGridViewCellEventHandler(this.dgvSales_CellClick);

            // ── Add to Form ───────────────────────────────────────
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.dgvSales);

            this.ResumeLayout(false);
        }

        // ── Helpers ───────────────────────────────────────────────
        private void AddLabel(Panel p, Label lbl, string text, int x, int y)
        {
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(60, 70, 85);
            lbl.Size = new Size(310, 18);
            lbl.Location = new Point(x, y);
            p.Controls.Add(lbl);
        }

        private void AddTextBox(Panel p, TextBox tb, int x, int y, bool readOnly)
        {
            tb.Location = new Point(x, y);
            tb.Size = new Size(310, 28);
            tb.Font = new Font("Segoe UI", 9.5f);
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.ReadOnly = readOnly;
            tb.BackColor = readOnly ? System.Drawing.Color.FromArgb(235, 237, 239)
                                    : System.Drawing.Color.FromArgb(245, 247, 250);
            p.Controls.Add(tb);
        }

        private void StyleBtn(Button btn, string text, System.Drawing.Color color,
                               int x, int y, int w, int h)
        {
            btn.Text = text;
            btn.Size = new Size(w, h);
            btn.Location = new Point(x, y);
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }
    }
}
