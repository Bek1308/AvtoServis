using AvtoServis.Data.Configuration;
using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Forms.Controls;
using AvtoServis.Services.Core;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvtoServis.Forms.Screens
{
    public partial class MainForm : Form
    {
        private bool menuExpand = false; // Spr menyusi holati (standard menu)
        private bool button1MenuExpanded = false; // button1 ga tegishli menyular ochilganmi
        private int targetHeight = 52; // SprContainer uchun maqsadli balandlik
        private const int TRIGGER_DISTANCE = 70; // Ishlatilmaydi, saqlanadi
        private const int TOP_PANEL_HEIGHT = 50;
        private const int STANDARD_MENU_HEIGHT = 360; // Standard menyuning balandligi
        private const int EXTENDED_MENU_HEIGHT = 470; // button1 menyusi bilan balandlik
        private const int COLLAPSED_HEIGHT = 52; // Yig'ilgan holat balandligi
        private readonly ServicesViewModel _servicesViewModel;
        private readonly ManufacturersViewModel _manufactureViewModel;
        private readonly CarModelsViewModel _carModelViewModel;
        private readonly PartsViewModel _partsViewModel;
        private readonly SuppliersViewModel _suppliersViewModel;
        private readonly StockViewModel _stockViewModel;
        private readonly StatusesViewModel _statusesViewModel;


        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            // Panellar uchun miltirashni oldini olish, agar null bo'lmasa
            if (ContentPanel != null) SetDoubleBuffered(ContentPanel);
            if (SprContainer != null) SetDoubleBuffered(SprContainer);
            if (sidebarContainer != null) SetDoubleBuffered(sidebarContainer);

            menuTransition.Interval = 5;
            string connectionString = DatabaseConfig.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Ошибка: строка подключения к базе данных не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("MainForm Init Error: Connection string is null or empty.");
                return;
            }

            try
            {
                _servicesViewModel = new ServicesViewModel(new ServicesRepository(connectionString));
                _manufactureViewModel = new ManufacturersViewModel(new ManufacturersRepository(connectionString));
                _carModelViewModel = new CarModelsViewModel(new CarModelsRepository(connectionString), new CarBrandRepository(connectionString));
                _partsViewModel = new PartsViewModel(
                    new PartsRepository(connectionString),
                    new PartQualitiesRepository(connectionString),
                    new CarBrandRepository(connectionString),
                    new ManufacturersRepository(connectionString) // Cast keraksiz, chunki ManufacturersRepository IManufacturersRepository ni implement qiladi
                );
                _suppliersViewModel = new SuppliersViewModel(new SuppliersRepository(connectionString));
                _stockViewModel = new StockViewModel(new StockRepository(connectionString));
                _statusesViewModel = new StatusesViewModel(new StatusRepository(connectionString));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации ViewModel: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"MainForm Init Error: {ex.Message}");
                return;
            }
        }

        private void SetDoubleBuffered(Control control)
        {
            if (control == null)
            {
                return; // Skip if control is null
            }

            // Reflection orqali DoubleBuffered ni o‘rnatish
            PropertyInfo dbProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            dbProp?.SetValue(control, true);

            // ControlStyles optimallashtirish
            MethodInfo setStyleMethod = typeof(Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            if (setStyleMethod != null)
            {
                setStyleMethod.Invoke(control, new object[] {
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.OptimizedDoubleBuffer, true
                });
            }
        }

        private void OpenUserControl(UserControl userControl)
        {
            if (ContentPanel == null || userControl == null)
            {
                return; // ContentPanel yoki userControl null bo'lsa, metodni o'tkazib yubor
            }

            ContentPanel.SuspendLayout();
            ContentPanel.Controls.Clear(); // Avvalgi kontentni tozalash
            userControl.AutoSize = false; // AutoSize ni o‘chirish
            userControl.MaximumSize = new Size(0, 0); // Cheklovlarni olib tashlash
            userControl.MinimumSize = new Size(0, 0);
            userControl.Dock = DockStyle.Fill; // Panelni to‘ldirish
            userControl.Size = ContentPanel.ClientSize; // O‘lchamni majburan moslashtirish
            SetDoubleBuffered(userControl); // Miltirashni oldini olish
            ContentPanel.Controls.Add(userControl);
            ContentPanel.BringToFront(); // ContentPanel ni oldinga chiqarish
            ContentPanel.PerformLayout(); // Layout ni majburiy yangilash
            ContentPanel.ResumeLayout(false);
        }

        private void menuTransition_Tick(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (SprContainer.Height < targetHeight)
            {
                // Expand: Increase height until target is reached
                SprContainer.Height += 15;
                if (SprContainer.Height >= targetHeight)
                {
                    SprContainer.Height = targetHeight;
                    menuTransition.Stop();
                }
            }
            else if (SprContainer.Height > targetHeight)
            {
                // Collapse: Decrease height until target is reached
                SprContainer.Height -= 15;
                if (SprContainer.Height <= targetHeight)
                {
                    SprContainer.Height = targetHeight;
                    menuTransition.Stop();
                    if (targetHeight == COLLAPSED_HEIGHT)
                    {
                        menuExpand = false;
                        button1MenuExpanded = false; // Reset when fully collapsed
                    }
                }
            }
            else
            {
                // Already at target height
                menuTransition.Stop();
            }
            this.ResumeLayout(false);
        }

        private void btnSpr_Click(object sender, EventArgs e)
        {
            if (!menuTransition.Enabled)
            {
                if (menuExpand || button1MenuExpanded)
                {
                    // Collapse to 52 pixels
                    targetHeight = COLLAPSED_HEIGHT;
                    menuExpand = false;
                    button1MenuExpanded = false;
                }
                else
                {
                    // Expand to standard menu (360 pixels)
                    targetHeight = STANDARD_MENU_HEIGHT;
                    menuExpand = true;
                    button1MenuExpanded = false;
                }
                menuTransition.Start();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Hech qanday dizayn sozlamalari o‘zgartirilmaydi
        }

        public void ApplyHoverEffect(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                Color originalBackColor = btn.BackColor == Color.Empty ? Color.FromArgb(248, 248, 248) : btn.BackColor;
                Color originalBorderColor = btn.FlatAppearance.BorderColor;

                btn.MouseEnter -= ApplyHoverEffect;
                btn.MouseEnter += (s, args) =>
                {
                    btn.BackColor = Color.FromArgb(200, 230, 255);
                    btn.FlatAppearance.BorderColor = Color.FromArgb(100, 200, 230, 255);
                };

                btn.MouseLeave += (s, args) =>
                {
                    btn.BackColor = originalBackColor;
                    btn.FlatAppearance.BorderColor = originalBorderColor;
                };
            }
        }

        private void panel2_Paint(object sender, EventArgs e)
        {
            // Hozircha bo‘sh
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            try
            {
                var testControl = new TestControl();
                if (testControl == null)
                {
                    return; // TestControl yaratilmagan bo'lsa, o'tkazib yubor
                }
                OpenUserControl(testControl);
            }
            catch (Exception)
            {
                // Xatolikni yutib yubor, dastur to'xtamasligi uchun
            }
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            if (_servicesViewModel == null || imageList1 == null)
            {
                return; // Null bo'lsa, o'tkazib yubor
            }
            try
            {
                var serviceControl = new ServiceControl(_servicesViewModel, imageList1);
                if (serviceControl == null)
                {
                    return; // ServiceControl yaratilmagan bo'lsa, o'tkazib yubor
                }
                OpenUserControl(serviceControl);
            }
            catch (Exception)
            {
                // Xatolikni yutib yubor, dastur to'xtamasligi uchun
            }
        }

        private void btnSmadeBy_Click(object sender, EventArgs e)
        {
            if (_manufactureViewModel == null || imageList1 == null)
            {
                return; // Null bo'lsa, o'tkazib yubor
            }
            try
            {
                var manufacturerControl = new ManufacturerControl(_manufactureViewModel, imageList1);
                if (manufacturerControl == null)
                {
                    return; // ManufacturerControl yaratilmagan bo'lsa, o'tkazib yubor
                }
                OpenUserControl(manufacturerControl);
            }
            catch (Exception)
            {
                // Xatolikni yutib yubor, dastur to'xtamasligi uchun
            }
        }

        private void SprContainer_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!menuTransition.Enabled)
            {
                if (SprContainer.Height <= COLLAPSED_HEIGHT)
                {
                    // Collapsed state: Expand to full height including button1 items
                    targetHeight = EXTENDED_MENU_HEIGHT;
                    menuExpand = true;
                    button1MenuExpanded = true;
                }
                else if (SprContainer.Height == STANDARD_MENU_HEIGHT)
                {
                    // Standard menu open: Expand further for button1 items
                    targetHeight = EXTENDED_MENU_HEIGHT;
                    button1MenuExpanded = true;
                }
                else if (SprContainer.Height == EXTENDED_MENU_HEIGHT)
                {
                    // Fully expanded: Collapse to standard menu height
                    targetHeight = STANDARD_MENU_HEIGHT;
                    menuExpand = true;
                    button1MenuExpanded = false;
                }
                menuTransition.Start();
            }
        }

        private void ContentPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (_carModelViewModel == null || imageList1 == null)
            {
                return; // Null bo'lsa, o'tkazib yubor
            }
            try
            {
                var carModelControl = new CarModelControl(_carModelViewModel, imageList1);
                if (carModelControl == null)
                {
                    return; // ServiceControl yaratilmagan bo'lsa, o'tkazib yubor
                }
                OpenUserControl(carModelControl);
            }
            catch (Exception)
            {
                // Xatolikni yutib yubor, dastur to'xtamasligi uchun
            }
        }

        private void btnSpartQuality_Click(object sender, EventArgs e)
        {
            if (_partsViewModel == null || imageList1 == null)
            {
                MessageBox.Show("Ошибка: PartsViewModel или ImageList не инициализированы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("btnSpartQuality_Click Error: PartsViewModel or ImageList is null.");
                return;
            }
            try
            {
                var partsControl = new PartsControl(_partsViewModel, imageList1);
                if (partsControl == null)
                {
                    MessageBox.Show("Ошибка: PartsControl не создан.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine("btnSpartQuality_Click Error: PartsControl is null.");
                    return;
                }
                OpenUserControl(partsControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии PartsControl: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"btnSpartQuality_Click Error: {ex.Message}");
            }
        }

        private void btnSsuplier_Click(object sender, EventArgs e)
        {
            if (_suppliersViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var supplierControl = new SuppliersControl(_suppliersViewModel, imageList1);
                if (supplierControl == null)
                {
                    return;
                }
                OpenUserControl(supplierControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии SuppliersControl: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

            }

        }

        private void btnSstock_Click(object sender, EventArgs e)
        {
            if (_stockViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var stockControl = new StockControl(_stockViewModel, imageList1);
                if (stockControl == null)
                {
                    return;
                }
                OpenUserControl(stockControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии StockControl: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

            }

        }

        private void btnSstatus_Click(object sender, EventArgs e)
        {
            if (_statusesViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var statusControl = new StatusesControl(_statusesViewModel, imageList1);
                if (statusControl == null)
                {
                    return;
                }
                OpenUserControl(statusControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии StatusControl: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

            }

        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            try
            {
                var indexIncomeControl = new IndexIncome();
                if (indexIncomeControl == null)
                {
                    return;
                }
                OpenUserControl(indexIncomeControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии indexIncomeControl: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

            }
        }

        private void sidebarContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}