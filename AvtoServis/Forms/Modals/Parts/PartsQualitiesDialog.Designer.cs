namespace AvtoServis.Forms.Controls
{
    partial class PartsQualitiesDialog
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
            tableLayoutPanel = new TableLayoutPanel();
            lblTitle = new Label();
            dataGridView = new DataGridView();
            btnAdd = new Button();
            btnClose = new Button();
            lblCount = new Label();
            lblError = new Label();
            timerError = new System.Windows.Forms.Timer(components);
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            tableLayoutPanel.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel.Controls.Add(dataGridView, 0, 1);
            tableLayoutPanel.Controls.Add(btnAdd, 1, 1);
            tableLayoutPanel.Controls.Add(btnClose, 1, 3);
            tableLayoutPanel.Controls.Add(lblCount, 0, 2);
            tableLayoutPanel.Controls.Add(lblError, 0, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(14, 12, 14, 12);
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.Size = new Size(525, 338);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.Location = new Point(17, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(274, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Управление качествами";
            // 
            // dataGridView
            // 
            dataGridView.BackgroundColor = Color.White;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(17, 52);
            dataGridView.Margin = new Padding(3, 2, 3, 2);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(386, 204);
            dataGridView.TabIndex = 2;
            dataGridView.CellClick += DataGridView_CellClick;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(25, 118, 210);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(409, 52);
            btnAdd.Margin = new Padding(3, 2, 3, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(88, 34);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(409, 283);
            btnClose.Margin = new Padding(3, 2, 3, 2);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 29);
            btnClose.TabIndex = 4;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI", 10F);
            lblCount.ForeColor = Color.FromArgb(33, 37, 41);
            lblCount.Location = new Point(17, 259);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(80, 19);
            lblCount.TabIndex = 5;
            lblCount.Text = "Качества: 0";
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 10F);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(17, 281);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 19);
            lblError.TabIndex = 6;
            lblError.Visible = false;
            // 
            // timerError
            // 
            timerError.Interval = 3000;
            timerError.Tick += TimerError_Tick;
            // 
            // PartsQualitiesDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(525, 338);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartsQualitiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление качествами";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblTitle;
        private DataGridView dataGridView;
        private Button btnAdd;
        private Button btnClose;
        private Label lblCount;
        private Label lblError;
        private System.Windows.Forms.Timer timerError;
    }
}