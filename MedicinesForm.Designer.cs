using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    partial class MedicinesForm
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Panel  pnlHeader;
        private Label  lblTitle;
        private Label  lblSubTitle;

        // Input panel
        private Panel      pnlInput;
        private Label      lblId;
        private TextBox    txtId;
        private Label      lblName;
        private TextBox    txtName;
        private Label      lblCategory;
        private TextBox    txtCategory;
        private Label      lblPrice;
        private TextBox    txtPrice;
        private Label      lblQuantity;
        private TextBox    txtQuantity;
        private Label      lblExpiry;
        private DateTimePicker dtpExpiry;

        // Search
        private Label      lblSearch;
        private TextBox    txtSearch;
        private Button     btnSearch;

        // Buttons
        private Button btnInsert;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnList;
        private Button btnClear;
        private Button btnBack;

        // Grid
        private DataGridView dgvMedicines;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader   = new Panel();
            this.lblTitle    = new Label();
            this.lblSubTitle = new Label();
            this.pnlInput    = new Panel();
            this.lblId       = new Label();
            this.txtId       = new TextBox();
            this.lblName     = new Label();
            this.txtName     = new TextBox();
            this.lblCategory = new Label();
            this.txtCategory = new TextBox();
            this.lblPrice    = new Label();
            this.txtPrice    = new TextBox();
            this.lblQuantity = new Label();
            this.txtQuantity = new TextBox();
            this.lblExpiry   = new Label();
            this.dtpExpiry   = new DateTimePicker();
            this.lblSearch   = new Label();
            this.txtSearch   = new TextBox();
            this.btnSearch   = new Button();
            this.btnInsert   = new Button();
            this.btnUpdate   = new Button();
            this.btnDelete   = new Button();
            this.btnList     = new Button();
            this.btnClear    = new Button();
            this.btnBack     = new Button();
            this.dgvMedicines = new DataGridView();

            this.SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            this.Text            = "Pharmacy Management - Medicines";
            this.Size            = new Size(1000, 680);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = Color.FromArgb(236, 240, 245);
            this.Font            = new Font("Segoe UI", 9.5f);
            this.Load           += new System.EventHandler(this.MedicinesForm_Load);
            this.FormClosing    += new FormClosingEventHandler(this.MedicinesForm_FormClosing);

            // ── Header ────────────────────────────────────────────
            this.pnlHeader.Dock      = DockStyle.Top;
            this.pnlHeader.Height    = 70;
            this.pnlHeader.BackColor = Color.FromArgb(20, 52, 100);

            this.lblTitle.Text      = "💊 Medicine Management";
            this.lblTitle.Font      = new Font("Segoe UI", 15f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Size      = new Size(500, 36);
            this.lblTitle.Location  = new Point(20, 8);

            this.lblSubTitle.Text      = "Entity Framework | Insert • Update • Delete • List • Search";
            this.lblSubTitle.Font      = new Font("Segoe UI", 9f);
            this.lblSubTitle.ForeColor = Color.FromArgb(180, 200, 230);
            this.lblSubTitle.Size      = new Size(600, 20);
            this.lblSubTitle.Location  = new Point(22, 46);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubTitle);

            // ── Input Panel ───────────────────────────────────────
            this.pnlInput.Location  = new Point(10, 85);
            this.pnlInput.Size      = new Size(320, 550);
            this.pnlInput.BackColor = Color.White;

            // ID (read-only)
            AddLabel(this.pnlInput, this.lblId, "ID (auto):", 10, 10);
            AddTextBox(this.pnlInput, this.txtId, 10, 32, true);

            // Name
            AddLabel(this.pnlInput, this.lblName, "Medicine Name *:", 10, 68);
            AddTextBox(this.pnlInput, this.txtName, 10, 90, false);

            // Category
            AddLabel(this.pnlInput, this.lblCategory, "Category:", 10, 126);
            AddTextBox(this.pnlInput, this.txtCategory, 10, 148, false);

            // Price
            AddLabel(this.pnlInput, this.lblPrice, "Price *:", 10, 184);
            AddTextBox(this.pnlInput, this.txtPrice, 10, 206, false);

            // Quantity
            AddLabel(this.pnlInput, this.lblQuantity, "Quantity *:", 10, 242);
            AddTextBox(this.pnlInput, this.txtQuantity, 10, 264, false);

            // Expiry Date
            AddLabel(this.pnlInput, this.lblExpiry, "Expiry Date:", 10, 300);
            this.dtpExpiry.Location = new Point(10, 322);
            this.dtpExpiry.Size     = new Size(290, 28);
            this.dtpExpiry.Format   = DateTimePickerFormat.Short;
            this.pnlInput.Controls.Add(this.dtpExpiry);

            // Search
            AddLabel(this.pnlInput, this.lblSearch, "Search by Name:", 10, 368);
            this.txtSearch.Location = new Point(10, 390);
            this.txtSearch.Size     = new Size(200, 28);
            this.txtSearch.Font     = new Font("Segoe UI", 9.5f);
            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.pnlInput.Controls.Add(this.txtSearch);

            StyleButton(this.btnSearch, "Search", Color.FromArgb(41, 128, 185), 218, 390, 80, 28);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.pnlInput.Controls.Add(this.btnSearch);

            // Action buttons
            StyleButton(this.btnInsert, "Add",    Color.FromArgb(39, 174, 96),  10, 440, 130, 36);
            StyleButton(this.btnUpdate, "Update", Color.FromArgb(211, 84, 0),   170, 440, 130, 36);
            StyleButton(this.btnDelete, "Delete", Color.FromArgb(192, 57, 43),  10,  488, 130, 36);
            StyleButton(this.btnList,   "List All",Color.FromArgb(41, 128, 185),170, 488, 130, 36);
            StyleButton(this.btnClear,  "Clear",  Color.FromArgb(127, 140, 141),10,  536, 130, 36);
            StyleButton(this.btnBack,   "Back",   Color.FromArgb(127, 140, 141),170, 536, 130, 36);

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
            this.dgvMedicines.Location            = new Point(345, 85);
            this.dgvMedicines.Size                = new Size(640, 555);
            this.dgvMedicines.BackgroundColor     = Color.White;
            this.dgvMedicines.BorderStyle         = BorderStyle.None;
            this.dgvMedicines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedicines.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicines.MultiSelect         = false;
            this.dgvMedicines.ReadOnly            = true;
            this.dgvMedicines.AllowUserToAddRows  = false;
            this.dgvMedicines.RowHeadersVisible   = false;
            this.dgvMedicines.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 245);
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(20, 52, 100);
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
            this.dgvMedicines.EnableHeadersVisualStyles = false;
            this.dgvMedicines.CellClick += new DataGridViewCellEventHandler(this.dgvMedicines_CellClick);

            // ── Add to Form ───────────────────────────────────────
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.dgvMedicines);

            this.ResumeLayout(false);
        }

        // ── Helpers ───────────────────────────────────────────────
        private void AddLabel(Panel p, Label lbl, string text, int x, int y)
        {
            lbl.Text      = text;
            lbl.Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(60, 70, 85);
            lbl.Size      = new Size(290, 18);
            lbl.Location  = new Point(x, y);
            p.Controls.Add(lbl);
        }

        private void AddTextBox(Panel p, TextBox tb, int x, int y, bool readOnly)
        {
            tb.Location    = new Point(x, y);
            tb.Size        = new Size(290, 28);
            tb.Font        = new Font("Segoe UI", 9.5f);
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.ReadOnly    = readOnly;
            tb.BackColor   = readOnly ? System.Drawing.Color.FromArgb(235, 237, 239) : System.Drawing.Color.FromArgb(245, 247, 250);
            p.Controls.Add(tb);
        }

        private void StyleButton(Button btn, string text, Color color, int x, int y, int w, int h)
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
