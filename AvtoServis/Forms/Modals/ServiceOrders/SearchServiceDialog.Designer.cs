namespace AvtoServis.Forms.Modals.ServiceExpenses
{
    partial class SearchServiceDialog
    {
        private System.ComponentModel.IContainer components = null;
        private Panel _mainPanel;

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
            this.components = new System.ComponentModel.Container();
            this._mainPanel = new Panel();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.Dock = DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(1000, 600);
            this._mainPanel.TabIndex = 0;
            // 
            // SearchServiceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this._mainPanel);
            this.Name = "SearchServiceDialog";
            this.Text = "Поиск услуги";
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }
    }
}