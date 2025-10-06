using System;
using System.Windows.Forms;
using AvtoServis.ViewModels.Screens;
using AvtoServis.Model.DTOs;
using AvtoServis.Forms.Controls;

namespace AvtoServis.Forms.Modals.PartExpenses
{
    public partial class SearchProductDialog : Form
    {
        private readonly FullPartsControl _fullPartsControl;
        private readonly SaleViewModel _saleViewModel;
        private readonly ImageList _imageList;
        private Panel _mainPanel;

        public SearchProductDialog(FullPartsViewModel fullPartsViewModel, SaleViewModel saleViewModel, ImageList actionImageList)
        {
            if (fullPartsViewModel == null) throw new ArgumentNullException(nameof(fullPartsViewModel));
            if (saleViewModel == null) throw new ArgumentNullException(nameof(saleViewModel));
            if (actionImageList == null) throw new ArgumentNullException(nameof(actionImageList));

            _saleViewModel = saleViewModel;
            _imageList = actionImageList;
            _fullPartsControl = new FullPartsControl(fullPartsViewModel, actionImageList, isDialogMode: true)
            {
                Dock = DockStyle.Fill
            };

            InitializeComponent();
            ConfigureDialog();
            OpenUserControl(_fullPartsControl);
        }

        private void InitializeComponent()
        {
            _mainPanel = new Panel();
            SuspendLayout();
            // 
            // _mainPanel
            // 
            _mainPanel.Dock = DockStyle.Fill;
            _mainPanel.Location = new Point(0, 0);
            _mainPanel.Name = "_mainPanel";
            _mainPanel.Size = new Size(1000, 600);
            _mainPanel.TabIndex = 0;
            // 
            // SearchProductDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(_mainPanel);
            Name = "SearchProductDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Поиск товара";
            ResumeLayout(false);
        }

        private void ConfigureDialog()
        {
            var btnExport = _fullPartsControl.Controls.Find("btnExport", true).FirstOrDefault() as Button;
            if (btnExport != null)
            {
                btnExport.Visible = false;
            }

            var dataGridView = _fullPartsControl.Controls.Find("dataGridView", true).FirstOrDefault() as DataGridView;
            if (dataGridView != null)
            {
                dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            }
            else
            {
                _saleViewModel.NotificationMessage = "Ошибка: DataGridView компоненти топилмади";
            }

            // Faqat zarur ustunlarni ko‘rsatish
            try
            {
                _fullPartsControl.SetVisibleColumns(new[]
                {
                    "PartID", "PartName", "CatalogNumber", "RemainingQuantity",
                    "IsAvailable", "StockName", "CarBrandName", "ManufacturerName", "QualityName"
                });
                _fullPartsControl.LoadData();
            }
            catch (Exception ex)
            {
                _saleViewModel.NotificationMessage = $"Ошибка при загрузке данных в FullPartsControl: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"ConfigureDialog Error: {ex.Message}");
            }
        }

        private void OpenUserControl(UserControl control)
        {
            try
            {
                _mainPanel.Controls.Clear();
                _mainPanel.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                System.Diagnostics.Debug.WriteLine("OpenUserControl: FullPartsControl successfully added to mainPanel.");
            }
            catch (Exception ex)
            {
                _saleViewModel.NotificationMessage = $"Ошибка при открытии FullPartsControl: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"OpenUserControl Error: {ex.Message}");
            }
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                var row = ((DataGridView)sender).Rows[e.RowIndex];
                if (row.Tag == null || !(row.Tag is int id))
                {
                    _saleViewModel.NotificationMessage = "Ошибка: Неверный идентификатор товара";
                    return;
                }

                var part = _saleViewModel.GetPartById(id);
                if (part != null)
                {
                    _saleViewModel.AddProductFromSearch(part);
                    Close();
                }
                else
                {
                    _saleViewModel.NotificationMessage = "Товар не найден";
                }
            }
            catch (InvalidCastException)
            {
                _saleViewModel.NotificationMessage = "Ошибка: Неверный формат идентификатора товара";
            }
            catch (Exception ex)
            {
                _saleViewModel.NotificationMessage = $"Ошибка при добавлении товара: {ex.Message}";
            }
        }
    }
}