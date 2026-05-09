using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    partial class AdminLoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private Panel pnlMain;
        private Panel pnlCard;
        private Label lblTitle;
        private Label lblSubTitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private Label lblFooter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain     = new Panel();
            this.pnlCard     = new Panel();
            this.lblTitle    = new Label();
            this.lblSubTitle = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin    = new Button();
            this.btnExit     = new Button();
            this.lblFooter   = new Label();

            this.SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            this.Text             = "Pharmacy Management System - Login";
            this.Size             = new Size(520, 480);
            this.StartPosition    = FormStartPosition.CenterScreen;
            this.FormBorderStyle  = FormBorderStyle.FixedSingle;
            this.MaximizeBox      = false;
            this.BackColor        = Color.FromArgb(236, 240, 245);
            this.Font             = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            // ── pnlMain (full-form background panel) ─────────────
            this.pnlMain.Dock      = DockStyle.Fill;
            this.pnlMain.BackColor = Color.FromArgb(20, 52, 100);

            // ── pnlCard (white login card) ────────────────────────
            this.pnlCard.Size      = new Size(380, 340);
            this.pnlCard.Location  = new Point(70, 70);
            this.pnlCard.BackColor = Color.White;
            this.pnlCard.BorderStyle = BorderStyle.None;

            // Custom rounded border effect via Paint
            this.pnlCard.Paint += (s, e) =>
            {
                var rect = new System.Drawing.Rectangle(0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
                using (var pen = new System.Drawing.Pen(Color.FromArgb(210, 215, 225), 1))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            // ── lblTitle ─────────────────────────────────────────
            this.lblTitle.Text      = "💊 Pharmacy Management";
            this.lblTitle.Font      = new Font("Segoe UI", 15f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(20, 52, 100);
            this.lblTitle.Size      = new Size(360, 36);
            this.lblTitle.Location  = new Point(10, 22);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // ── lblSubTitle ──────────────────────────────────────
            this.lblSubTitle.Text      = "Administrator Login";
            this.lblSubTitle.Font      = new Font("Segoe UI", 10f, FontStyle.Regular);
            this.lblSubTitle.ForeColor = Color.FromArgb(120, 130, 145);
            this.lblSubTitle.Size      = new Size(360, 22);
            this.lblSubTitle.Location  = new Point(10, 60);
            this.lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;

            // ── lblUsername ──────────────────────────────────────
            this.lblUsername.Text      = "Username";
            this.lblUsername.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.FromArgb(60, 70, 85);
            this.lblUsername.Size      = new Size(300, 20);
            this.lblUsername.Location  = new Point(40, 105);

            // ── txtUsername ──────────────────────────────────────
            this.txtUsername.Size      = new Size(300, 30);
            this.txtUsername.Location  = new Point(40, 128);
            this.txtUsername.Font      = new Font("Segoe UI", 10f);
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;
            this.txtUsername.BackColor = Color.FromArgb(245, 247, 250);

            // ── lblPassword ──────────────────────────────────────
            this.lblPassword.Text      = "Password";
            this.lblPassword.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.lblPassword.ForeColor = Color.FromArgb(60, 70, 85);
            this.lblPassword.Size      = new Size(300, 20);
            this.lblPassword.Location  = new Point(40, 170);

            // ── txtPassword ──────────────────────────────────────
            this.txtPassword.Size         = new Size(300, 30);
            this.txtPassword.Location     = new Point(40, 193);
            this.txtPassword.Font         = new Font("Segoe UI", 10f);
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.BorderStyle  = BorderStyle.FixedSingle;
            this.txtPassword.BackColor    = Color.FromArgb(245, 247, 250);
            this.txtPassword.KeyDown     += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);

            // ── btnLogin ─────────────────────────────────────────
            this.btnLogin.Text      = "Login";
            this.btnLogin.Size      = new Size(140, 38);
            this.btnLogin.Location  = new Point(40, 250);
            this.btnLogin.Font      = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnLogin.BackColor = Color.FromArgb(39, 174, 96);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor    = Cursors.Hand;
            this.btnLogin.Click    += new System.EventHandler(this.btnLogin_Click);

            // ── btnExit ──────────────────────────────────────────
            this.btnExit.Text      = "Exit";
            this.btnExit.Size      = new Size(140, 38);
            this.btnExit.Location  = new Point(200, 250);
            this.btnExit.Font      = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnExit.BackColor = Color.FromArgb(149, 165, 166);
            this.btnExit.ForeColor = Color.White;
            this.btnExit.FlatStyle = FlatStyle.Flat;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.Cursor    = Cursors.Hand;
            this.btnExit.Click    += new System.EventHandler(this.btnExit_Click);

            // ── lblFooter ─────────────────────────────────────────
            this.lblFooter.Text      = "Pharmacy Management System © 2024";
            this.lblFooter.Font      = new Font("Segoe UI", 8f);
            this.lblFooter.ForeColor = Color.FromArgb(180, 185, 195);
            this.lblFooter.Size      = new Size(360, 20);
            this.lblFooter.Location  = new Point(10, 308);
            this.lblFooter.TextAlign = ContentAlignment.MiddleCenter;

            // ── Assemble pnlCard ─────────────────────────────────
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblSubTitle);
            this.pnlCard.Controls.Add(this.lblUsername);
            this.pnlCard.Controls.Add(this.txtUsername);
            this.pnlCard.Controls.Add(this.lblPassword);
            this.pnlCard.Controls.Add(this.txtPassword);
            this.pnlCard.Controls.Add(this.btnLogin);
            this.pnlCard.Controls.Add(this.btnExit);
            this.pnlCard.Controls.Add(this.lblFooter);

            // ── Assemble pnlMain ─────────────────────────────────
            this.pnlMain.Controls.Add(this.pnlCard);

            // ── Add to Form ───────────────────────────────────────
            this.Controls.Add(this.pnlMain);

            this.ResumeLayout(false);
        }
    }
}
