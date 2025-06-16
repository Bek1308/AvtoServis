namespace AvtoServis.Forms.Screens
{
    partial class SignInForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblError = new Label();
            txtLogin = new TextBox();
            lblLogin = new Label();
            txtPsw = new Label();
            txtPasword = new TextBox();
            btnSignIn = new Button();
            footer = new Label();
            lblsp = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblError
            // 
            lblError.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblError.Location = new Point(130, 115);
            lblError.Name = "lblError";
            lblError.Size = new Size(321, 31);
            lblError.TabIndex = 0;
            lblError.Text = "Вход в система-SmartServis";
            // 
            // txtLogin
            // 
            txtLogin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtLogin.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtLogin.Location = new Point(130, 282);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(299, 38);
            txtLogin.TabIndex = 1;
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblLogin.ForeColor = SystemColors.ControlDarkDark;
            lblLogin.Location = new Point(130, 242);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(72, 28);
            lblLogin.TabIndex = 2;
            lblLogin.Text = "Логин";
            // 
            // txtPsw
            // 
            txtPsw.AutoSize = true;
            txtPsw.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtPsw.ForeColor = SystemColors.ControlDarkDark;
            txtPsw.Location = new Point(130, 345);
            txtPsw.Name = "txtPsw";
            txtPsw.Size = new Size(85, 28);
            txtPsw.TabIndex = 4;
            txtPsw.Text = "Пароль";
            // 
            // txtPasword
            // 
            txtPasword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPasword.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtPasword.Location = new Point(130, 385);
            txtPasword.Name = "txtPasword";
            txtPasword.Size = new Size(299, 38);
            txtPasword.TabIndex = 3;
     
            // 
            // btnSignIn
            // 
            btnSignIn.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnSignIn.BackColor = SystemColors.Highlight;
            btnSignIn.FlatStyle = FlatStyle.Flat;
            btnSignIn.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSignIn.ForeColor = SystemColors.ControlLightLight;
            btnSignIn.Location = new Point(130, 459);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(299, 42);
            btnSignIn.TabIndex = 5;
            btnSignIn.Text = "Вход";
            btnSignIn.UseVisualStyleBackColor = false;
            // 
            // footer
            // 
            footer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            footer.AutoSize = true;
            footer.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            footer.ForeColor = SystemColors.ControlDarkDark;
            footer.Location = new Point(130, 737);
            footer.Name = "footer";
            footer.Size = new Size(144, 19);
            footer.TabIndex = 6;
            footer.Text = "SmartServis 2025 ";
            // 
            // lblsp
            // 
            lblsp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblsp.AutoSize = true;
            lblsp.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblsp.ForeColor = SystemColors.ControlDarkDark;
            lblsp.Location = new Point(289, 737);
            lblsp.Name = "lblsp";
            lblsp.Size = new Size(131, 19);
            lblsp.TabIndex = 7;
            lblsp.Text = "support@mail.tj";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(130, 176);
            label1.Name = "label1";
            label1.Size = new Size(321, 31);
            label1.TabIndex = 8;
            label1.Text = "Вход в система-SmartServis";
            // 
            // SignInForm
            // 
            AccessibleDescription = "";
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(596, 775);
            Controls.Add(label1);
            Controls.Add(lblsp);
            Controls.Add(footer);
            Controls.Add(btnSignIn);
            Controls.Add(txtPsw);
            Controls.Add(txtPasword);
            Controls.Add(lblLogin);
            Controls.Add(txtLogin);
            Controls.Add(lblError);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            MaximizeBox = false;
            MaximumSize = new Size(618, 826);
            Name = "SignInForm";
            StartPosition = FormStartPosition.CenterScreen;
            Load += SignInForm_Load;
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion

        private Label lblError;
        private TextBox txtLogin;
        private Label lblLogin;
        private Label txtPsw;
        private TextBox txtPasword;
        private Button btnSignIn;
        private Label footer;
        private Label lblsp;
        private Label label1;
    }
}