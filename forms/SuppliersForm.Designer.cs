using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    partial class SuppliersForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel        pnlHeader;
        private Label        lblTitle;
        private Label        lblSubTitle;
        private Panel        pnlInput;
        private Label        lblId;
        private TextBox      txtId;
        private Label        lblName;
        private TextBox      txtName;
        private Label        lblPhone;
        private TextBox      txtPhone;
        private Label        lblEmail;
        private TextBox      txtEmail;
        private Label        lblAddress;
        private TextBox      txtAddress;
        private Label        lblSearch;
        private TextBox      txtSearch;
        private Button       btnSearch;
        private Button       btnInsert;
        private Button       btnUpdate;
        private Button       btnDelete;
        private Button       btnList;
        private Button       btnClear;
        private Button       btnBack;
        private DataGridView dgvSuppliers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader    = new Panel();
            this.lblTitle     = new Label();
            this.lblSubTitle  = new Label();
            this.pnlInput     = new Panel();
            this.lblId        = new Label();
            this.txtId        = new TextBox();
            this.lblName      = new Label();
            this.txtName      = new TextBox();
            this.lblPhone     = new Label();
            this.txtPhone     = new TextBox();
            this.lblEmail     = new Label();
            this.txtEmail     = new TextBox();
            this.lblAddress   = new Label();
            this.txtAddress   = new TextBox();
            this.lblSearch    = new Label();
            this.txtSearch    = new TextBox();
            this.btnSearch    = new Button();
            this.btnInsert    = new Button();
            this.btnUpdate    = new Button();
            this.btnDelete    = new Button();
            this.btnList      = new Button();
            this.btnClear     = new Button();
            this.btnBack      = new Button();
            this.dgvSuppliers = new DataGridView();

            this.SuspendLayout();

            // Form
            this.Text            = "Pharmacy Management - Suppliers";
            this.Size            = new Size(1000, 640);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = Color.FromArgb(236, 240, 245);
            this.Font            = new Font("Segoe UI", 9.5f);
            this.Load           += new System.EventHandler(this.SuppliersForm_Load);
            this.FormClosing    += new FormClosingEventHandler(this.SuppliersForm_FormClosing);

            // Header
            this.pnlHeader.Dock      = DockStyle.Top;
            this.pnlHeader.Height    = 70;
            this.pnlHeader.BackColor = Color.FromArgb(39, 174, 96);

            this.lblTitle.Text      = "🏭 Supplier Management";
            this.lblTitle.Font      = new Font("Segoe UI", 15f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Size      = new Size(500, 36);
            this.lblTitle.Location  = new Point(20, 8);

            this.lblSubTitle.Text      = "Entity Framework | Insert • Update • Delete • List • Search";
            this.lblSubTitle.Font      = new Font("Segoe UI", 9f);
            this.lblSubTitle.ForeColor = Color.FromArgb(200, 240, 210);
            this.lblSubTitle.Size      = new Size(600, 20);
            this.lblSubTitle.Location  = new Point(22, 46);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubTitle);

            // Input panel
            this.pnlInput.Location  = new Point(10, 85);
            this.pnlInput.Size      = new Size(320, 510);
            this.pnlInput.BackColor = Color.White;

            AddLbl(pnlInput, lblId,      "ID (auto):",       10, 10);
            AddTb (pnlInput, txtId,      10, 32,  true);
            AddLbl(pnlInput, lblName,    "Supplier Name *:", 10, 68);
            AddTb (pnlInput, txtName,    10, 90,  false);
            AddLbl(pnlInput, lblPhone,   "Phone:",           10, 126);
            AddTb (pnlInput, txtPhone,   10, 148, false);
            AddLbl(pnlInput, lblEmail,   "Email:",           10, 184);
            AddTb (pnlInput, txtEmail,   10, 206, false);
            AddLbl(pnlInput, lblAddress, "Address:",         10, 242);
            AddTb (pnlInput, txtAddress, 10, 264, false);
            AddLbl(pnlInput, lblSearch,  "Search by Name:",  10, 308);

            this.txtSearch.Location    = new Point(10, 330);
            this.txtSearch.Size        = new Size(200, 28);
            this.txtSearch.Font        = new Font("Segoe UI", 9.5f);
            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            pnlInput.Controls.Add(this.txtSearch);

            Btn(pnlInput, btnSearch, "Search", Color.FromArgb(41, 128, 185), 218, 330, 80, 28);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            Btn(pnlInput, btnInsert, "Add",     Color.FromArgb(39,  174, 96),  10,  380, 130, 36);
            Btn(pnlInput, btnUpdate, "Update",  Color.FromArgb(211, 84,  0),   170, 380, 130, 36);
            Btn(pnlInput, btnDelete, "Delete",  Color.FromArgb(192, 57,  43),  10,  428, 130, 36);
            Btn(pnlInput, btnList,   "List All",Color.FromArgb(41,  128, 185), 170, 428, 130, 36);
            Btn(pnlInput, btnClear,  "Clear",   Color.FromArgb(127, 140, 141), 10,  476, 130, 36);
            Btn(pnlInput, btnBack,   "Back",    Color.FromArgb(127, 140, 141), 170, 476, 130, 36);

            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnList.Click   += new System.EventHandler(this.btnList_Click);
            this.btnClear.Click  += new System.EventHandler(this.btnClear_Click);
            this.btnBack.Click   += new System.EventHandler(this.btnBack_Click);

            // DataGridView
            this.dgvSuppliers.Location            = new Point(345, 85);
            this.dgvSuppliers.Size                = new Size(640, 515);
            this.dgvSuppliers.BackgroundColor     = Color.White;
            this.dgvSuppliers.BorderStyle         = BorderStyle.None;
            this.dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuppliers.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuppliers.MultiSelect         = false;
            this.dgvSuppliers.ReadOnly            = true;
            this.dgvSuppliers.AllowUserToAddRows  = false;
            this.dgvSuppliers.RowHeadersVisible   = false;
            this.dgvSuppliers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 245);
            this.dgvSuppliers.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.dgvSuppliers.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(39, 174, 96);
            this.dgvSuppliers.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
            this.dgvSuppliers.EnableHeadersVisualStyles = false;
            this.dgvSuppliers.CellClick += new DataGridViewCellEventHandler(this.dgvSuppliers_CellClick);

            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.dgvSuppliers);

            this.ResumeLayout(false);
        }

        private void AddLbl(Panel p, Label l, string t, int x, int y)
        {
            l.Text = t; l.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            l.ForeColor = Color.FromArgb(60, 70, 85); l.Size = new Size(290, 18); l.Location = new Point(x, y);
            p.Controls.Add(l);
        }

        private void AddTb(Panel p, TextBox tb, int x, int y, bool ro)
        {
            tb.Location = new Point(x, y); tb.Size = new Size(290, 28);
            tb.Font = new Font("Segoe UI", 9.5f); tb.BorderStyle = BorderStyle.FixedSingle;
            tb.ReadOnly = ro; tb.BackColor = ro ? Color.FromArgb(235, 237, 239) : Color.FromArgb(245, 247, 250);
            p.Controls.Add(tb);
        }

        private void Btn(Panel p, Button b, string t, Color c, int x, int y, int w, int h)
        {
            b.Text = t; b.Size = new Size(w, h); b.Location = new Point(x, y);
            b.Font = new Font("Segoe UI", 9f, FontStyle.Bold); b.BackColor = c;
            b.ForeColor = Color.White; b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0; b.Cursor = Cursors.Hand;
            p.Controls.Add(b);
        }
    }
}
