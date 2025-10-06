using AvtoServis.Data;
using AvtoServis.Data.Configuration;
using AvtoServis.Data.Repositories;
using AvtoServis.Forms.Controls;
using AvtoServis.ViewModels.Screens;
using System.Reflection;
using Timer = System.Windows.Forms.Timer;

namespace AvtoServis.Forms.Screens
{
    public partial class MainForm : Form
    {
        private bool menuExpand = false; // Spr menyusi holati (standard menu)
        private bool button1MenuExpanded = false; // button1 ga tegishli menyular ochilganmi
        private int targetHeight = 52; // SprContainer uchun maqsadli balandlik
        private const int STANDARD_MENU_HEIGHT = 430; // Standard menyuning balandligi
        private const int EXTENDED_MENU_HEIGHT = 540; // button1 menyusi bilan balandlik
        private const int COLLAPSED_HEIGHT = 52; // Yig'ilgan holat balandligi

        // Alohida Income paneli uchun (pnIncome deb faraz qilamiz)
        private bool incomeExpand = false; // Income panel holati
        private int incomeTargetHeight = 48; // pnIncome uchun maqsadli balandlik
        private const int INCOME_COLLAPSED_HEIGHT = 42; // Yig'ilgan holat balandligi
        private const int INCOME_EXTENDED_HEIGHT = 132; // Kengaygan holat balandligi (designer'da o'lchang)
        private Timer incomeTransition; // Yangi timer

        private readonly ServicesViewModel _servicesViewModel;
        private readonly ManufacturersViewModel _manufactureViewModel;
        private readonly CarModelViewModel _carModelViewModel;
        private readonly PartsViewModel _partsViewModel;
        private readonly PartQualitiesViewModel _partsQualitiesViewModel;
        private readonly SuppliersViewModel _suppliersViewModel;
        private readonly StockViewModel _stockViewModel;
        private readonly StatusesViewModel _statusesViewModel;
        private readonly CarBrandViewModel _carBrandViewModel;
        private readonly PartsIncomeViewModel _partsIncomeViewModel;
        private readonly FullPartsViewModel _fullPartsViewModel;
        private readonly PartExpensesViewModel _partExpensesViewModel;
        private readonly ServiceOrdersViewModel _serviceOrdersViewModel;
        private readonly CustomerDebtsViewModel _customerDebtsViewModel;
        private readonly string connectionString = DatabaseConfig.ConnectionString;

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.AutoScaleMode = AutoScaleMode.None; // DPI moslashuvini o'chirish

            // Panellar uchun miltirashni oldini olish
            if (ContentPanel != null) SetDoubleBuffered(ContentPanel);
            if (SprContainer != null) SetDoubleBuffered(SprContainer);
            if (sidebarContainer != null) SetDoubleBuffered(sidebarContainer);
            SetDoubleBuffered(this);
            SetDoubleBuffered(btnParts);
            menuTransition.Interval = 15;
            string connectionString = DatabaseConfig.ConnectionString;
            // Alohida Income paneli animatsiyasi uchun timer yaratish
            incomeTransition = new Timer();
            incomeTransition.Interval = 15;
            incomeTransition.Tick += incomeTransition_Tick;

            // pnIncome panelini double-buffered qilish (agar mavjud bo'lsa)
            if (pnIncome != null) SetDoubleBuffered(pnIncome);

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
                _carModelViewModel = new CarModelViewModel(new CarModelsRepository(connectionString), new CarBrandRepository(connectionString));
                _partsViewModel = new PartsViewModel(
                    new PartsRepository(connectionString),
                    new CarBrandRepository(connectionString),
                    new ManufacturersRepository(connectionString),
                    new PartQualitiesRepository(connectionString)
                );
                _suppliersViewModel = new SuppliersViewModel(new SuppliersRepository(connectionString));
                _stockViewModel = new StockViewModel(new StockRepository(connectionString));
                _statusesViewModel = new StatusesViewModel(new StatusRepository(connectionString));
                _partsQualitiesViewModel = new PartQualitiesViewModel(new PartQualitiesRepository(connectionString));
                _carBrandViewModel = new CarBrandViewModel(new CarBrandRepository(connectionString));
                _partsIncomeViewModel = new PartsIncomeViewModel(
                    new PartsIncomeRepository(connectionString),
                    new PartsRepository(connectionString),
                    new SuppliersRepository(connectionString),
                    new StatusRepository(connectionString),
                    new Finance_StatusRepository(connectionString),
                    new StockRepository(connectionString),
                    new BatchRepository(connectionString));
                _fullPartsViewModel = new FullPartsViewModel(new FullPartsRepository(connectionString));
                _partExpensesViewModel = new PartExpensesViewModel(
                    new PartsExpensesRepository(connectionString),
                    new PartsRepository(connectionString),
                    new CustomerRepository(connectionString),
                    new StatusRepository(connectionString),
                    new PartsIncomeRepository(connectionString),
                    connectionString);
                _serviceOrdersViewModel = new ServiceOrdersViewModel(
                    new ServiceOrdersRepository(connectionString),
                    new CustomerRepository(connectionString),
                    new ServicesRepository(connectionString),
                    new CarModelsRepository(connectionString),
                    new StatusRepository(connectionString),
                    new UsersRepository(),
                    connectionString);
                _customerDebtsViewModel = new CustomerDebtsViewModel( new CustomerRepository(connectionString));

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
                return;
            }

            PropertyInfo dbProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            dbProp?.SetValue(control, true);

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
                return;
            }

            ContentPanel.SuspendLayout();
            ContentPanel.Controls.Clear();
            userControl.AutoSize = false;
            userControl.MaximumSize = new Size(0, 0);
            userControl.MinimumSize = new Size(0, 0);
            userControl.Dock = DockStyle.Fill;
            userControl.Size = ContentPanel.ClientSize;
            SetDoubleBuffered(userControl);
            ContentPanel.Controls.Add(userControl);
            ContentPanel.BringToFront();
            ContentPanel.PerformLayout();
            ContentPanel.ResumeLayout(false);
        }

        private void menuTransition_Tick(object sender, EventArgs e)
        {
            this.SuspendLayout();
            int step = 15; // Animatsiya qadami statik
            if (SprContainer.Height < targetHeight)
            {
                SprContainer.Height += step;
                if (SprContainer.Height >= targetHeight)
                {
                    SprContainer.Height = targetHeight;
                    menuTransition.Stop();
                }
            }
            else if (SprContainer.Height > targetHeight)
            {
                SprContainer.Height -= step;
                if (SprContainer.Height <= targetHeight)
                {
                    SprContainer.Height = targetHeight;
                    menuTransition.Stop();
                    if (targetHeight == COLLAPSED_HEIGHT)
                    {
                        menuExpand = false;
                        button1MenuExpanded = false;
                    }
                }
            }
            else
            {
                menuTransition.Stop();
            }
            this.ResumeLayout(false);
        }

        private void incomeTransition_Tick(object sender, EventArgs e)
        {
            this.SuspendLayout();
            int step = 15; // Animatsiya qadami
            if (pnIncome.Height < incomeTargetHeight)
            {
                pnIncome.Height += step;
                if (pnIncome.Height >= incomeTargetHeight)
                {
                    pnIncome.Height = incomeTargetHeight;
                    incomeTransition.Stop();
                }
            }
            else if (pnIncome.Height > incomeTargetHeight)
            {
                pnIncome.Height -= step;
                if (pnIncome.Height <= incomeTargetHeight)
                {
                    pnIncome.Height = incomeTargetHeight;
                    incomeTransition.Stop();
                    if (incomeTargetHeight == INCOME_COLLAPSED_HEIGHT)
                    {
                        incomeExpand = false;
                    }
                }
            }
            else
            {
                incomeTransition.Stop();
            }
            this.ResumeLayout(false);
        }

        private void btnSpr_Click(object sender, EventArgs e)
        {
            if (!menuTransition.Enabled)
            {
                if (menuExpand || button1MenuExpanded)
                {
                    targetHeight = COLLAPSED_HEIGHT;
                    menuExpand = false;
                    button1MenuExpanded = false;
                }
                else
                {
                    targetHeight = STANDARD_MENU_HEIGHT;
                    menuExpand = true;
                    button1MenuExpanded = false;
                }
                menuTransition.Start();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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


        private void btnMain_Click(object sender, EventArgs e)
        {
            try
            {
                var testControl = new TestControl();
                if (testControl == null)
                {
                    return;
                }
                OpenUserControl(testControl);
            }
            catch (Exception)
            {
            }
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            if (_servicesViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var serviceControl = new ServiceControl(_servicesViewModel, imageList1);
                if (serviceControl == null)
                {
                    return;
                }
                OpenUserControl(serviceControl);
            }
            catch (Exception)
            {
            }
        }

        private void btnSmadeBy_Click(object sender, EventArgs e)
        {
            if (_manufactureViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var manufacturerControl = new ManufacturerControl(_manufactureViewModel, imageList1);
                if (manufacturerControl == null)
                {
                    return;
                }
                OpenUserControl(manufacturerControl);
            }
            catch (Exception)
            {
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!menuTransition.Enabled)
            {
                if (SprContainer.Height <= COLLAPSED_HEIGHT)
                {
                    targetHeight = EXTENDED_MENU_HEIGHT;
                    menuExpand = true;
                    button1MenuExpanded = true;
                }
                else if (SprContainer.Height == STANDARD_MENU_HEIGHT)
                {
                    targetHeight = EXTENDED_MENU_HEIGHT;
                    button1MenuExpanded = true;
                }
                else if (SprContainer.Height == EXTENDED_MENU_HEIGHT)
                {
                    targetHeight = STANDARD_MENU_HEIGHT;
                    menuExpand = true;
                    button1MenuExpanded = false;
                }
                menuTransition.Start();
            }
        }


        private void button6_Click_1(object sender, EventArgs e)
        {
            if (_carModelViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var carModelControl = new CarModelControl(_carModelViewModel, imageList1);
                if (carModelControl == null)
                {
                    return;
                }
                OpenUserControl(carModelControl);
            }
            catch (Exception)
            {
            }
        }

        private void btnCarBrand_Click(object sender, EventArgs e)
        {
            if (_carBrandViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var carBarndControl = new CarBrandControl(_carBrandViewModel, imageList1);
                if (carBarndControl == null)
                {
                    return;
                }
                OpenUserControl(carBarndControl);
            }
            catch (Exception)
            {
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
                var indexIncomeControl = new PartsIncomeControl(_partsIncomeViewModel, imageList1);
                if (indexIncomeControl == null)
                {
                    return;
                }
                OpenUserControl(indexIncomeControl);

                if (!incomeTransition.Enabled)
                {
                    if (incomeExpand)
                    {
                        incomeTargetHeight = INCOME_COLLAPSED_HEIGHT;
                        incomeExpand = false;
                    }
                    else
                    {
                        incomeTargetHeight = INCOME_EXTENDED_HEIGHT;
                        incomeExpand = true;
                    }
                    incomeTransition.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии indexIncomeControl: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void btnSubIncome_Click(object sender, EventArgs e)
        {

            try
            {
                var indexIncomeControl = new PartsIncomeControl(_partsIncomeViewModel, imageList1);
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

        private void btnBatch_Click(object sender, EventArgs e)
        {
            try
            {
                var indexIncomeControl = new IndexIncome(new PartsIncomeRepository(connectionString));
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

        private void btnPartQuality_Click(object sender, EventArgs e)
        {
            if (_partsQualitiesViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var partsQualityControl = new PartQualityControl(_partsQualitiesViewModel, imageList1);
                if (partsQualityControl == null)
                {
                    return;
                }
                OpenUserControl(partsQualityControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии PartsQualitiesControls: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void btnParts_Click(object sender, EventArgs e)
        {
            if (_fullPartsViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var fullPartsControl = new FullPartsControl(_fullPartsViewModel, imageList1);
                if (fullPartsControl == null)
                {
                    return;
                }
                OpenUserControl(fullPartsControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии PartsQualitiesControls: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }

        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (_partExpensesViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var partExpensesControl = new PartsExpensesControl(_partExpensesViewModel);
                if (partExpensesControl == null)
                {
                    return;
                }
                OpenUserControl(partExpensesControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии PartsQualitiesControls: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }

        }

        private void btnServis_Click(object sender, EventArgs e)
        {
            if (_serviceOrdersViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var serviceOrdersControl = new ServiceOrdersControl(_serviceOrdersViewModel);
                if (serviceOrdersControl == null)
                {
                    return;
                }
                OpenUserControl(serviceOrdersControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии PartsQualitiesControls: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void btnDepts_Click(object sender, EventArgs e)
        {
            if (_customerDebtsViewModel == null || imageList1 == null)
            {
                return;
            }
            try
            {
                var customerDeptsControl = new CustomerDebtsControl(_customerDebtsViewModel);
                if (customerDeptsControl == null)
                {
                    return;
                }
                OpenUserControl(customerDeptsControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии PartsQualitiesControls: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}