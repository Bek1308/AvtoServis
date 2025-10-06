namespace AvtoServis.Forms
{
    partial class BatchDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblBatchInfo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutDetails;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel headerPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblBatchInfo = new System.Windows.Forms.Label();
            this.flowLayoutDetails = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = Color.FromArgb(74, 144, 226);
            this.headerPanel.Controls.Add(this.lblBatchInfo);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1000, 70);
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Paint += (s, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, this.headerPanel.Width, this.headerPanel.Height),
                    Color.FromArgb(74, 144, 226),
                    Color.FromArgb(52, 120, 200),
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, 0, 0, this.headerPanel.Width, this.headerPanel.Height);
                }
            };

            // 
            // lblBatchInfo
            // 
            this.lblBatchInfo.AutoSize = true;
            this.lblBatchInfo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBatchInfo.ForeColor = System.Drawing.Color.White;
            this.lblBatchInfo.Location = new System.Drawing.Point(20, 20);
            this.lblBatchInfo.Name = "lblBatchInfo";
            this.lblBatchInfo.Size = new System.Drawing.Size(280, 38);
            this.lblBatchInfo.TabIndex = 0;
            this.lblBatchInfo.Text = "Загрузка сведений...";

            // 
            // flowLayoutDetails
            // 
            this.flowLayoutDetails.AutoScroll = true;
            this.flowLayoutDetails.BackColor = Color.FromArgb(234, 241, 251);
            this.flowLayoutDetails.Location = new System.Drawing.Point(20, 90);
            this.flowLayoutDetails.Name = "flowLayoutDetails";
            this.flowLayoutDetails.Size = new System.Drawing.Size(960, 550);
            this.flowLayoutDetails.TabIndex = 1;

            // 
            // btnClose
            // 
            this.btnClose.BackColor = Color.FromArgb(74, 144, 226);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(840, 660);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnClose.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 140, 40, 8, 8));

            // 
            // BatchDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(234, 241, 251);
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.flowLayoutDetails);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сведения о Партия";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }
}