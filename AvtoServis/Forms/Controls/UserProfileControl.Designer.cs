using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    partial class UserProfileControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            toolTip = new ToolTip(components);
            tableLayoutPanel = new TableLayoutPanel();
            headerPanel = new Panel();
            titleLabel = new Label();
            separator = new Panel();
            avatarPanel = new Panel();
            avatarImagePanel = new Panel();
            avatarPictureBox = new PictureBox();
            profilePanel = new Panel();
            profileTableLayout = new TableLayoutPanel();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblRole = new Label();
            txtRole = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblCreatedDate = new Label();
            txtCreatedDate = new TextBox();
            lblIsActive = new Label();
            chkIsActive = new CheckBox();
            buttonPanel = new FlowLayoutPanel();
            btnEdit = new Button();
            btnChangePassword = new Button();
            btnChangeAvatar = new Button();
            btnExit = new Button();
            tableLayoutPanel.SuspendLayout();
            headerPanel.SuspendLayout();
            avatarPanel.SuspendLayout();
            avatarImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)avatarPictureBox).BeginInit();
            profilePanel.SuspendLayout();
            profileTableLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.4439659F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.5560341F));
            tableLayoutPanel.Controls.Add(headerPanel, 0, 0);
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(avatarPanel, 0, 2);
            tableLayoutPanel.Controls.Add(profilePanel, 1, 2);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(960, 540);
            tableLayoutPanel.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(59, 130, 246);
            tableLayoutPanel.SetColumnSpan(headerPanel, 2);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(19, 19);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(922, 54);
            headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(20, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(207, 23);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Профиль пользователя";
            // 
            // separator
            // 
            separator.BackColor = Color.FromArgb(229, 231, 235);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Dock = DockStyle.Fill;
            separator.Location = new Point(19, 79);
            separator.Name = "separator";
            separator.Size = new Size(922, 1);
            separator.TabIndex = 1;
            // 
            // avatarPanel
            // 
            avatarPanel.BackColor = Color.White;
            avatarPanel.Controls.Add(avatarImagePanel);
            avatarPanel.Dock = DockStyle.Fill;
            avatarPanel.Location = new Point(19, 81);
            avatarPanel.Name = "avatarPanel";
            avatarPanel.Size = new Size(425, 440);
            avatarPanel.TabIndex = 2;
            // 
            // avatarImagePanel
            // 
            avatarImagePanel.BackColor = Color.Transparent;
            avatarImagePanel.Controls.Add(avatarPictureBox);
            avatarImagePanel.Dock = DockStyle.Fill;
            avatarImagePanel.Location = new Point(0, 0);
            avatarImagePanel.Name = "avatarImagePanel";
            avatarImagePanel.Size = new Size(425, 440);
            avatarImagePanel.TabIndex = 0;
            // 
            // avatarPictureBox
            // 
            avatarPictureBox.BackColor = Color.FromArgb(229, 231, 235);
            avatarPictureBox.Location = new Point(16, 69);
            avatarPictureBox.Name = "avatarPictureBox";
            avatarPictureBox.Size = new Size(393, 262);
            avatarPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            avatarPictureBox.TabIndex = 0;
            avatarPictureBox.TabStop = false;
            // 
            // profilePanel
            // 
            profilePanel.BackColor = Color.White;
            profilePanel.Controls.Add(profileTableLayout);
            profilePanel.Controls.Add(buttonPanel);
            profilePanel.Dock = DockStyle.Fill;
            profilePanel.Location = new Point(450, 81);
            profilePanel.Name = "profilePanel";
            profilePanel.Size = new Size(491, 440);
            profilePanel.TabIndex = 3;
            // 
            // profileTableLayout
            // 
            profileTableLayout.ColumnCount = 2;
            profileTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 176F));
            profileTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            profileTableLayout.Controls.Add(lblUsername, 0, 0);
            profileTableLayout.Controls.Add(txtUsername, 1, 0);
            profileTableLayout.Controls.Add(lblFullName, 0, 1);
            profileTableLayout.Controls.Add(txtFullName, 1, 1);
            profileTableLayout.Controls.Add(lblRole, 0, 2);
            profileTableLayout.Controls.Add(txtRole, 1, 2);
            profileTableLayout.Controls.Add(lblEmail, 0, 3);
            profileTableLayout.Controls.Add(txtEmail, 1, 3);
            profileTableLayout.Controls.Add(lblPhone, 0, 4);
            profileTableLayout.Controls.Add(txtPhone, 1, 4);
            profileTableLayout.Controls.Add(lblCreatedDate, 0, 5);
            profileTableLayout.Controls.Add(txtCreatedDate, 1, 5);
            profileTableLayout.Controls.Add(lblIsActive, 0, 6);
            profileTableLayout.Controls.Add(chkIsActive, 1, 6);
            profileTableLayout.Dock = DockStyle.Top;
            profileTableLayout.Location = new Point(0, 0);
            profileTableLayout.Name = "profileTableLayout";
            profileTableLayout.RowCount = 7;
            profileTableLayout.RowStyles.Add(new RowStyle());
            profileTableLayout.RowStyles.Add(new RowStyle());
            profileTableLayout.RowStyles.Add(new RowStyle());
            profileTableLayout.RowStyles.Add(new RowStyle());
            profileTableLayout.RowStyles.Add(new RowStyle());
            profileTableLayout.RowStyles.Add(new RowStyle());
            profileTableLayout.RowStyles.Add(new RowStyle());
            profileTableLayout.Size = new Size(491, 320);
            profileTableLayout.TabIndex = 15;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Dock = DockStyle.Fill;
            lblUsername.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(17, 24, 39);
            lblUsername.Location = new Point(3, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Padding = new Padding(5);
            lblUsername.Size = new Size(170, 31);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Имя пользователя";
            lblUsername.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Dock = DockStyle.Fill;
            txtUsername.Font = new Font("Segoe UI", 8F);
            txtUsername.ForeColor = Color.FromArgb(17, 24, 39);
            txtUsername.Location = new Point(179, 3);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(309, 25);
            txtUsername.TabIndex = 1;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Dock = DockStyle.Fill;
            lblFullName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(17, 24, 39);
            lblFullName.Location = new Point(3, 31);
            lblFullName.Name = "lblFullName";
            lblFullName.Padding = new Padding(5);
            lblFullName.Size = new Size(170, 31);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "ФИО";
            lblFullName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtFullName
            // 
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.Dock = DockStyle.Fill;
            txtFullName.Font = new Font("Segoe UI", 8F);
            txtFullName.ForeColor = Color.FromArgb(17, 24, 39);
            txtFullName.Location = new Point(179, 34);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(309, 25);
            txtFullName.TabIndex = 3;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Dock = DockStyle.Fill;
            lblRole.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblRole.ForeColor = Color.FromArgb(17, 24, 39);
            lblRole.Location = new Point(3, 62);
            lblRole.Name = "lblRole";
            lblRole.Padding = new Padding(5);
            lblRole.Size = new Size(170, 31);
            lblRole.TabIndex = 4;
            lblRole.Text = "Роль";
            lblRole.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtRole
            // 
            txtRole.BorderStyle = BorderStyle.FixedSingle;
            txtRole.Dock = DockStyle.Fill;
            txtRole.Enabled = false;
            txtRole.Font = new Font("Segoe UI", 8F);
            txtRole.ForeColor = Color.FromArgb(17, 24, 39);
            txtRole.Location = new Point(179, 65);
            txtRole.Name = "txtRole";
            txtRole.ReadOnly = true;
            txtRole.Size = new Size(309, 25);
            txtRole.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Dock = DockStyle.Fill;
            lblEmail.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(17, 24, 39);
            lblEmail.Location = new Point(3, 93);
            lblEmail.Name = "lblEmail";
            lblEmail.Padding = new Padding(5);
            lblEmail.Size = new Size(170, 31);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Эл. почта";
            lblEmail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Dock = DockStyle.Fill;
            txtEmail.Font = new Font("Segoe UI", 8F);
            txtEmail.ForeColor = Color.FromArgb(17, 24, 39);
            txtEmail.Location = new Point(179, 96);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(309, 25);
            txtEmail.TabIndex = 7;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Dock = DockStyle.Fill;
            lblPhone.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(17, 24, 39);
            lblPhone.Location = new Point(3, 124);
            lblPhone.Name = "lblPhone";
            lblPhone.Padding = new Padding(5);
            lblPhone.Size = new Size(170, 31);
            lblPhone.TabIndex = 8;
            lblPhone.Text = "Телефон";
            lblPhone.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Dock = DockStyle.Fill;
            txtPhone.Font = new Font("Segoe UI", 8F);
            txtPhone.ForeColor = Color.FromArgb(17, 24, 39);
            txtPhone.Location = new Point(179, 127);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(309, 25);
            txtPhone.TabIndex = 9;
            // 
            // lblCreatedDate
            // 
            lblCreatedDate.AutoSize = true;
            lblCreatedDate.Dock = DockStyle.Fill;
            lblCreatedDate.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblCreatedDate.ForeColor = Color.FromArgb(17, 24, 39);
            lblCreatedDate.Location = new Point(3, 155);
            lblCreatedDate.Name = "lblCreatedDate";
            lblCreatedDate.Padding = new Padding(5);
            lblCreatedDate.Size = new Size(170, 31);
            lblCreatedDate.TabIndex = 10;
            lblCreatedDate.Text = "Дата создания";
            lblCreatedDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCreatedDate
            // 
            txtCreatedDate.BorderStyle = BorderStyle.FixedSingle;
            txtCreatedDate.Dock = DockStyle.Fill;
            txtCreatedDate.Enabled = false;
            txtCreatedDate.Font = new Font("Segoe UI", 8F);
            txtCreatedDate.ForeColor = Color.FromArgb(17, 24, 39);
            txtCreatedDate.Location = new Point(179, 158);
            txtCreatedDate.Name = "txtCreatedDate";
            txtCreatedDate.ReadOnly = true;
            txtCreatedDate.Size = new Size(309, 25);
            txtCreatedDate.TabIndex = 11;
            // 
            // lblIsActive
            // 
            lblIsActive.AutoSize = true;
            lblIsActive.Dock = DockStyle.Fill;
            lblIsActive.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblIsActive.ForeColor = Color.FromArgb(17, 24, 39);
            lblIsActive.Location = new Point(3, 186);
            lblIsActive.Name = "lblIsActive";
            lblIsActive.Padding = new Padding(5);
            lblIsActive.Size = new Size(170, 134);
            lblIsActive.TabIndex = 12;
            lblIsActive.Text = "Активен";
            lblIsActive.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Dock = DockStyle.Fill;
            chkIsActive.Enabled = false;
            chkIsActive.FlatStyle = FlatStyle.Flat;
            chkIsActive.Font = new Font("Segoe UI", 8F);
            chkIsActive.ForeColor = Color.FromArgb(17, 24, 39);
            chkIsActive.Location = new Point(179, 189);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(309, 128);
            chkIsActive.TabIndex = 13;
            chkIsActive.Text = "Активен";
            chkIsActive.CheckedChanged += ChkIsActive_CheckedChanged;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnChangeAvatar);
            buttonPanel.Controls.Add(btnChangePassword);
            buttonPanel.Controls.Add(btnExit);
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Location = new Point(0, 397);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(491, 43);
            buttonPanel.TabIndex = 14;
            // 
            // btnEdit
            // 
            btnEdit.AutoSize = true;
            btnEdit.BackColor = Color.FromArgb(59, 130, 246);
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(99, 179, 255);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(3, 3);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(124, 29);
            btnEdit.TabIndex = 0;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnChangePassword
            // 
            btnChangePassword.AutoSize = true;
            btnChangePassword.BackColor = Color.FromArgb(245, 158, 11);
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.FlatAppearance.MouseOverBackColor = Color.FromArgb(251, 191, 36);
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(230, 3);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(84, 29);
            btnChangePassword.TabIndex = 1;
            btnChangePassword.Text = "Пароль";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += BtnChangePassword_Click;
            // 
            // btnChangeAvatar
            // 
            btnChangeAvatar.AutoSize = true;
            btnChangeAvatar.BackColor = Color.FromArgb(59, 130, 246);
            btnChangeAvatar.FlatAppearance.BorderSize = 0;
            btnChangeAvatar.FlatAppearance.MouseOverBackColor = Color.FromArgb(99, 179, 255);
            btnChangeAvatar.FlatStyle = FlatStyle.Flat;
            btnChangeAvatar.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnChangeAvatar.ForeColor = Color.White;
            btnChangeAvatar.Location = new Point(133, 3);
            btnChangeAvatar.Name = "btnChangeAvatar";
            btnChangeAvatar.Size = new Size(91, 29);
            btnChangeAvatar.TabIndex = 2;
            btnChangeAvatar.Text = "Картинка";
            btnChangeAvatar.UseVisualStyleBackColor = false;
            btnChangeAvatar.Click += BtnChangeAvatar_Click;
            // 
            // btnExit
            // 
            btnExit.AutoSize = true;
            btnExit.BackColor = Color.FromArgb(239, 68, 68);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 113, 113);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(320, 3);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(90, 29);
            btnExit.TabIndex = 3;
            btnExit.Text = "Выйти";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += BtnExit_Click;
            // 
            // UserProfileControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(tableLayoutPanel);
            MinimumSize = new Size(600, 400);
            Name = "UserProfileControl";
            Size = new Size(960, 540);
            Resize += UserProfileControl_Resize;
            tableLayoutPanel.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            avatarPanel.ResumeLayout(false);
            avatarImagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)avatarPictureBox).EndInit();
            profilePanel.ResumeLayout(false);
            profileTableLayout.ResumeLayout(false);
            profileTableLayout.PerformLayout();
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Panel headerPanel;
        private Label titleLabel;
        private Panel separator;
        private Panel avatarPanel;
        private Panel avatarImagePanel;
        private PictureBox avatarPictureBox;
        private Panel profilePanel;
        private TableLayoutPanel profileTableLayout;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblRole;
        private TextBox txtRole;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblCreatedDate;
        private TextBox txtCreatedDate;
        private Label lblIsActive;
        private CheckBox chkIsActive;
        private FlowLayoutPanel buttonPanel;
        private Button btnEdit;
        private Button btnChangePassword;
        private Button btnChangeAvatar;
        private Button btnExit;
        private ToolTip toolTip;
    }
}