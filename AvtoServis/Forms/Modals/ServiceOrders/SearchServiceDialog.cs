using System;
using System.Windows.Forms;
using AvtoServis.ViewModels.Screens;
using AvtoServis.Model.DTOs;
using AvtoServis.Forms.Controls;

namespace AvtoServis.Forms.Modals.ServiceExpenses
{
    public partial class SearchServiceDialog : Form
    {
        private readonly FullServiceControl _fullServiceControl;
        private readonly ServiceOrderViewModel _serviceViewModel;
        private readonly ImageList _imageList;

        public SearchServiceDialog(ServiceOrdersViewModel serviceOrdersViewModel, ServiceOrderViewModel serviceViewModel, ImageList actionImageList)
        {
            if (serviceOrdersViewModel == null) throw new ArgumentNullException(nameof(serviceOrdersViewModel));
            if (serviceViewModel == null) throw new ArgumentNullException(nameof(serviceViewModel));
            if (actionImageList == null) throw new ArgumentNullException(nameof(actionImageList));

            _serviceViewModel = serviceViewModel;
            _imageList = actionImageList;
            _fullServiceControl = new FullServiceControl(serviceOrdersViewModel, actionImageList, isDialogMode: true)
            {
                Dock = DockStyle.Fill
            };

            InitializeComponent();
            ConfigureDialog();
            OpenUserControl(_fullServiceControl);
        }

        private void ConfigureDialog()
        {
            var btnExport = _fullServiceControl.Controls.Find("btnExport", true).FirstOrDefault() as Button;
            if (btnExport != null)
            {
                btnExport.Visible = false;
            }

            var dataGridView = _fullServiceControl.Controls.Find("dataGridView", true).FirstOrDefault() as DataGridView;
            if (dataGridView != null)
            {
                dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            }
            else
            {
                _serviceViewModel.NotificationMessage = "Ошибка: Компонент DataGridView не найден";
            }

            try
            {
                _fullServiceControl.SetVisibleColumns(new[]
                {
                    "ServiceID", "Name", "Price", "SoldCount", "TotalRevenue"
                });
                _fullServiceControl.LoadData();
            }
            catch (Exception ex)
            {
                _serviceViewModel.NotificationMessage = $"Ошибка при загрузке данных в FullServiceControl: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"ConfigureDialog Error: {ex.Message}");
            }
        }

        private void OpenUserControl(UserControl control)
        {
            try
            {
                if (_mainPanel == null)
                {
                    _serviceViewModel.NotificationMessage = "Ошибка: Панель управления не инициализирована";
                    System.Diagnostics.Debug.WriteLine("OpenUserControl Error: _mainPanel is null");
                    return;
                }
                _mainPanel.Controls.Clear();
                _mainPanel.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                System.Diagnostics.Debug.WriteLine("OpenUserControl: FullServiceControl успешно добавлен в mainPanel.");
            }
            catch (Exception ex)
            {
                _serviceViewModel.NotificationMessage = $"Ошибка при открытии FullServiceControl: {ex.Message}";
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
                    _serviceViewModel.NotificationMessage = "Ошибка: Неверный идентификатор услуги";
                    return;
                }

                var service = _serviceViewModel.GetServiceById(id);
                if (service != null)
                {
                    _serviceViewModel.AddServiceFromSearch(service);
                    Close();
                }
                else
                {
                    _serviceViewModel.NotificationMessage = "Услуга не найдена";
                }
            }
            catch (InvalidCastException)
            {
                _serviceViewModel.NotificationMessage = "Ошибка: Неверный формат идентификатора услуги";
            }
            catch (Exception ex)
            {
                _serviceViewModel.NotificationMessage = $"Ошибка при добавлении услуги: {ex.Message}";
            }
        }
    }
}