using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class UserProfileControl : UserControl
    {
        private readonly UserProfileViewModel _viewModel;
        private UserProfileDto _userProfile;
        private bool _isEditing;
        private readonly System.Windows.Forms.Timer _animationTimer;
        private Image _originalAvatar;
        private int _animationStepCount;
        private readonly int _animationSteps = 10;

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public UserProfileControl(UserProfileViewModel viewModel, int userId)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _userProfile = new UserProfileDto { UserID = userId };
            _isEditing = false;
            _animationTimer = new System.Windows.Forms.Timer { Interval = 20 };
            _animationStepCount = 0;
            InitializeComponent();
            EnhanceVisualStyles();
            SetToolTips();
            LoadUserProfile();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            AdjustLayoutForSize(Size);
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Панель профиля пользователя");
            toolTip.SetToolTip(headerPanel, "Заголовок профиля");
            toolTip.SetToolTip(titleLabel, "Название профиля");
            toolTip.SetToolTip(avatarPanel, "Аватар пользователя");
            toolTip.SetToolTip(avatarPictureBox, "Изображение профиля");
            toolTip.SetToolTip(btnChangeAvatar, "Изменить изображение профиля");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(profilePanel, "Информация о пользователе");
            toolTip.SetToolTip(lblUsername, "Имя пользователя");
            toolTip.SetToolTip(txtUsername, "Имя пользователя в системе");
            toolTip.SetToolTip(lblFullName, "Полное имя");
            toolTip.SetToolTip(txtFullName, "ФИО пользователя");
            toolTip.SetToolTip(lblRole, "Роль");
            toolTip.SetToolTip(txtRole, "Роль пользователя в системе");
            toolTip.SetToolTip(lblEmail, "Электронная почта");
            toolTip.SetToolTip(txtEmail, "Адрес электронной почты");
            toolTip.SetToolTip(lblPhone, "Телефон");
            toolTip.SetToolTip(txtPhone, "Номер телефона");
            toolTip.SetToolTip(lblCreatedDate, "Дата создания");
            toolTip.SetToolTip(txtCreatedDate, "Дата создания учетной записи");
            toolTip.SetToolTip(lblIsActive, "Статус активности");
            toolTip.SetToolTip(chkIsActive, "Указывает, активен ли пользователь");
            toolTip.SetToolTip(btnEdit, "Редактировать профиль");
            toolTip.SetToolTip(btnChangePassword, "Изменить пароль");
            toolTip.SetToolTip(btnExit, "Выйти из профиля");
        }

        private void EnhanceVisualStyles()
        {
            txtUsername.BackColor = Color.White;
            txtFullName.BackColor = Color.White;
            txtRole.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            txtPhone.BackColor = Color.White;
            txtCreatedDate.BackColor = Color.White;
            chkIsActive.FlatAppearance.BorderSize = 0;
            chkIsActive.FlatAppearance.CheckedBackColor = Color.FromArgb(34, 197, 94);
            chkIsActive.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 231, 235);
            headerPanel.Paint += (s, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(59, 130, 246),
                    Color.FromArgb(99, 179, 255),
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };
        }

        private void AdjustLayoutForSize(Size newSize)
        {
            int baseWidth = 960;
            float scaleFactor = Math.Min((float)newSize.Width / baseWidth, 1f);
            int fontSize = Math.Max(6, (int)(8 * scaleFactor));
            int titleFontSize = Math.Max(8, (int)(10 * scaleFactor));
            int margin = Math.Max(8, (int)(16 * scaleFactor));

            tableLayoutPanel.Padding = new Padding(margin);
            tableLayoutPanel.RowStyles[0].Height = 60 * scaleFactor;
            tableLayoutPanel.RowStyles[1].Height = 2 * scaleFactor;

            titleLabel.Font = new Font("Segoe UI", titleFontSize, FontStyle.Bold);
            lblUsername.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            lblFullName.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            lblRole.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            lblEmail.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            lblPhone.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            lblCreatedDate.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            lblIsActive.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            txtUsername.Font = new Font("Segoe UI", fontSize);
            txtFullName.Font = new Font("Segoe UI", fontSize);
            txtRole.Font = new Font("Segoe UI", fontSize);
            txtEmail.Font = new Font("Segoe UI", fontSize);
            txtPhone.Font = new Font("Segoe UI", fontSize);
            txtCreatedDate.Font = new Font("Segoe UI", fontSize);
            chkIsActive.Font = new Font("Segoe UI", fontSize);
            btnChangeAvatar.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            btnEdit.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            btnChangePassword.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            btnExit.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);

            // Center avatarPictureBox and set avatarPanel's Region to match
            int avatarWidth = (int)(393 * scaleFactor); // Scale PictureBox size
            int avatarHeight = (int)(262 * scaleFactor);
            avatarPictureBox.Size = new Size(avatarWidth, avatarHeight);
            int avatarX = (avatarPanel.Width - avatarWidth) / 2;
            int avatarY = (avatarPanel.Height - avatarHeight) / 2;
            avatarPictureBox.Location = new Point(avatarX, avatarY);

            if (scaleFactor > 0.8f)
            {
                int radius = (int)(8 * scaleFactor);
                tableLayoutPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, newSize.Width, newSize.Height, radius, radius));
                headerPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, headerPanel.Width, headerPanel.Height, (int)(6 * scaleFactor), (int)(6 * scaleFactor)));
                // Set avatarPanel's Region to match avatarPictureBox
                avatarPanel.Region = Region.FromHrgn(CreateRoundRectRgn(avatarX, avatarY, avatarX + avatarWidth, avatarY + avatarHeight, avatarWidth / 2, avatarHeight / 2));
                profilePanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, profilePanel.Width, profilePanel.Height, radius, radius));
                // Set avatarPictureBox's Region to match its size
                avatarPictureBox.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, avatarWidth, avatarHeight, avatarWidth / 2, avatarHeight / 2));
            }
            else
            {
                // Clear regions if scaleFactor <= 0.8
                tableLayoutPanel.Region = null;
                headerPanel.Region = null;
                avatarPanel.Region = null;
                profilePanel.Region = null;
                avatarPictureBox.Region = null;
            }
        }

        private void UserProfileControl_Resize(object sender, EventArgs e)
        {
            AdjustLayoutForSize(Size);
        }

        private void LoadUserProfile()
        {
            try
            {
                _userProfile = _viewModel.GetUserDetails(_userProfile.UserID);
                if (_userProfile == null)
                {
                    MessageBox.Show("Пользователь не найден или неактивен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ParentForm?.Close();
                    return;
                }

                txtUsername.Text = _userProfile.Username;
                txtFullName.Text = _userProfile.FullName;
                txtRole.Text = _userProfile.Role;
                txtEmail.Text = _userProfile.Email;
                txtPhone.Text = _userProfile.Phone;
                txtCreatedDate.Text = _userProfile.CreatedDate.ToString("dd.MM.yyyy HH:mm");
                chkIsActive.Checked = _userProfile.IsActive;
                chkIsActive.Text = _userProfile.IsActive ? "Активен" : "Неактивен";
                chkIsActive.ForeColor = _userProfile.IsActive ? Color.FromArgb(34, 197, 94) : Color.FromArgb(239, 68, 68);

                _originalAvatar = _viewModel.GetUserAvatar(_userProfile.UserID);
                avatarPictureBox.Image = _originalAvatar ?? CreatePlaceholderAvatar();
                ToggleEditMode(false);
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "загрузка профиля");
            }
        }

        private Image CreatePlaceholderAvatar()
        {
            var bitmap = new Bitmap(100, 100);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.FromArgb(229, 231, 235));
                using (var brush = new SolidBrush(Color.FromArgb(17, 24, 39)))
                {
                    g.DrawString("?", new Font("Segoe UI", 40, FontStyle.Bold), brush, new PointF(35, 30));
                }
            }
            return bitmap;
        }

        private void ToggleEditMode(bool enable)
        {
            _isEditing = enable;
            txtUsername.Enabled = enable;
            txtUsername.ReadOnly = !enable;
            txtFullName.Enabled = enable;
            txtFullName.ReadOnly = !enable;
            txtRole.Enabled = false; // Keep disabled
            txtRole.ReadOnly = true; // Keep read-only
            txtEmail.Enabled = enable;
            txtEmail.ReadOnly = !enable;
            txtPhone.Enabled = enable;
            txtPhone.ReadOnly = !enable;
            txtCreatedDate.Enabled = false; // Keep disabled
            txtCreatedDate.ReadOnly = true; // Keep read-only
            chkIsActive.Enabled = enable;
            btnChangeAvatar.Enabled = enable;
            btnEdit.Text = enable ? "Сохранить" : "Редактировать";
            btnChangePassword.Enabled = !enable;
            btnExit.Enabled = !enable;
            StartFadeAnimation(enable);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isEditing)
                {
                    ToggleEditMode(true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Имя пользователя не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!string.IsNullOrEmpty(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Неверный формат электронной почты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _userProfile.Username = txtUsername.Text;
                _userProfile.FullName = txtFullName.Text;
                // txtRole is not editable, so no update needed
                _userProfile.Email = txtEmail.Text;
                _userProfile.Phone = txtPhone.Text;
                _userProfile.IsActive = chkIsActive.Checked;

                _viewModel.UpdateUserProfile(_userProfile);
                if (avatarPictureBox.Image != _originalAvatar)
                {
                    _viewModel.UpdateUserAvatar(_userProfile.UserID, avatarPictureBox.Image);
                }

                MessageBox.Show("Профиль успешно обновлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ToggleEditMode(false);
                LoadUserProfile();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "сохранение профиля");
            }
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new Form
                {
                    Text = "Изменить пароль",
                    Size = new Size(300, 200),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                })
                {
                    var lblCurrent = new Label { Text = "Текущий пароль:", Location = new Point(20, 20), AutoSize = true };
                    var txtCurrent = new TextBox { Location = new Point(20, 40), Size = new Size(240, 20), PasswordChar = '*' };
                    var lblNew = new Label { Text = "Новый пароль:", Location = new Point(20, 65), AutoSize = true };
                    var txtNew = new TextBox { Location = new Point(20, 85), Size = new Size(240, 20), PasswordChar = '*' };
                    var lblConfirm = new Label { Text = "Подтвердить пароль:", Location = new Point(20, 110), AutoSize = true };
                    var txtConfirm = new TextBox { Location = new Point(20, 130), Size = new Size(240, 20), PasswordChar = '*' };
                    var btnSave = new Button
                    {
                        Text = "Сохранить",
                        Location = new Point(80, 160),
                        Size = new Size(75, 23),
                        BackColor = Color.FromArgb(59, 130, 246),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    var btnCancel = new Button
                    {
                        Text = "Отмена",
                        Location = new Point(160, 160),
                        Size = new Size(75, 23),
                        BackColor = Color.FromArgb(239, 68, 68),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    btnSave.Click += (s, ev) =>
                    {
                        if (txtNew.Text != txtConfirm.Text)
                        {
                            MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(txtNew.Text) || txtNew.Text.Length < 6)
                        {
                            MessageBox.Show("Новый пароль должен быть не менее 6 символов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        try
                        {
                            bool isValid = _viewModel.ValidateCurrentPassword(_userProfile.UserID, txtCurrent.Text);
                            if (!isValid)
                            {
                                MessageBox.Show("Неверный текущий пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            _viewModel.UpdateUserPassword(_userProfile.UserID, txtNew.Text);
                            MessageBox.Show("Пароль успешно изменен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dialog.Close();
                        }
                        catch (Exception ex)
                        {
                            LogAndShowError(ex, "изменение пароля");
                        }
                    };
                    btnCancel.Click += (s, ev) => dialog.Close();

                    dialog.Controls.AddRange(new Control[] { lblCurrent, txtCurrent, lblNew, txtNew, lblConfirm, txtConfirm, btnSave, btnCancel });
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "изменение пароля");
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (_isEditing)
            {
                var result = MessageBox.Show("Вы хотите выйти без сохранения изменений?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
            }
            ParentForm?.Close();
        }

        private void BtnChangeAvatar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    dialog.Title = "Выберите изображение аватара";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var image = Image.FromFile(dialog.FileName);
                        avatarPictureBox.Image = image;
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "изменение аватара");
            }
        }

        private void ChkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            chkIsActive.Text = chkIsActive.Checked ? "Активен" : "Неактивен";
            chkIsActive.ForeColor = chkIsActive.Checked ? Color.FromArgb(34, 197, 94) : Color.FromArgb(239, 68, 68);
        }

        private void StartFadeAnimation(bool isEditing)
        {
            _animationTimer.Stop();
            //_animationTimer.Tick -= AnimationTimer_Tick;
            _animationStepCount = 0;

            Color startColor = isEditing ? Color.White : Color.FromArgb(243, 244, 246);
            Color endColor = isEditing ? Color.FromArgb(243, 244, 246) : Color.White;

            _animationTimer.Tick += AnimationTimer_Tick;

            void AnimationTimer_Tick(object s, EventArgs e)
            {
                _animationStepCount++;
                float t = (float)_animationStepCount / _animationSteps;
                int r = (int)(startColor.R + (endColor.R - startColor.R) * t);
                int g = (int)(startColor.G + (endColor.G - startColor.G) * t);
                int b = (int)(startColor.B + (endColor.B - startColor.B) * t);

                r = Math.Clamp(r, 0, 255);
                g = Math.Clamp(g, 0, 255);
                b = Math.Clamp(b, 0, 255);

                Color newColor = Color.FromArgb(r, g, b);
                profilePanel.BackColor = newColor;
                txtUsername.BackColor = newColor;
                txtFullName.BackColor = newColor;
                txtRole.BackColor = newColor;
                txtEmail.BackColor = newColor;
                txtPhone.BackColor = newColor;
                txtCreatedDate.BackColor = newColor;

                System.Diagnostics.Debug.WriteLine($"Step {_animationStepCount}: t={t}, Color=({r},{g},{b})");

                if (_animationStepCount >= _animationSteps)
                {
                    _animationTimer.Stop();
                    _animationTimer.Tick -= AnimationTimer_Tick;
                    Color finalColor = isEditing ? Color.FromArgb(243, 244, 246) : Color.White;
                    profilePanel.BackColor = finalColor;
                    txtUsername.BackColor = finalColor;
                    txtFullName.BackColor = finalColor;
                    txtRole.BackColor = finalColor;
                    txtEmail.BackColor = finalColor;
                    txtPhone.BackColor = finalColor;
                    txtCreatedDate.BackColor = finalColor;
                    System.Diagnostics.Debug.WriteLine($"Animation complete: FinalColor=({finalColor.R},{finalColor.G},{finalColor.B})");
                }
            }

            _animationTimer.Start();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void LogAndShowError(Exception ex, string operation)
        {
            System.Diagnostics.Debug.WriteLine($"{operation} Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            MessageBox.Show($"Произошла ошибка при {operation.ToLower()}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}