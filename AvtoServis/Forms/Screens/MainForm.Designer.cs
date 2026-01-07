namespace AvtoServis.Forms.Screens
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            sidebarContainer = new FlowLayoutPanel();
            panel1 = new Panel();
            logo = new PictureBox();
            pnMain = new Panel();
            btnMain = new Button();
            imageList1 = new ImageList(components);
            pnSell = new Panel();
            btnSell = new Button();
            pnServis = new Panel();
            btnServis = new Button();
            deptsPanel = new FlowLayoutPanel();
            panel23 = new Panel();
            btnDepts = new Button();
            panel24 = new Panel();
            debtors = new Button();
            panel25 = new Panel();
            btnMydepts = new Button();
            pnIncome = new FlowLayoutPanel();
            panel16 = new Panel();
            btnIncome = new Button();
            panel17 = new Panel();
            btIndexIncome = new Button();
            panel18 = new Panel();
            button9 = new Button();
            panel19 = new Panel();
            button10 = new Button();
            panel22 = new Panel();
            btnParts = new Button();
            pnReport = new Panel();
            btnReport = new Button();
            SprContainer = new FlowLayoutPanel();
            panel7 = new Panel();
            btnSpr = new Button();
            panel9 = new Panel();
            btnSservis = new Button();
            panel10 = new Panel();
            btnSmadeBy = new Button();
            panel11 = new Panel();
            btnSParts = new Button();
            panel20 = new Panel();
            btnPartQuality = new Button();
            panel8 = new Panel();
            btnSstatus = new Button();
            panel12 = new Panel();
            btnSsuplier = new Button();
            panel15 = new Panel();
            button6 = new Button();
            panel21 = new Panel();
            btnCarBrand = new Button();
            panel13 = new Panel();
            btnSstock = new Button();
            flowLayoutPanel5 = new FlowLayoutPanel();
            panel3 = new Panel();
            button1 = new Button();
            panel4 = new Panel();
            button2 = new Button();
            panel5 = new Panel();
            button3 = new Button();
            panel6 = new Panel();
            button4 = new Button();
            panel14 = new Panel();
            button5 = new Button();
            menuTransition = new System.Windows.Forms.Timer(components);
            sidebarTransition = new System.Windows.Forms.Timer(components);
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel29 = new Panel();
            button11 = new Button();
            panel28 = new Panel();
            button8 = new Button();
            panel27 = new Panel();
            button7 = new Button();
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            panel2 = new Panel();
            ContentPanel = new CustomPanel();
            sidebarContainer.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logo).BeginInit();
            pnMain.SuspendLayout();
            pnSell.SuspendLayout();
            pnServis.SuspendLayout();
            deptsPanel.SuspendLayout();
            panel23.SuspendLayout();
            panel24.SuspendLayout();
            panel25.SuspendLayout();
            pnIncome.SuspendLayout();
            panel16.SuspendLayout();
            panel17.SuspendLayout();
            panel18.SuspendLayout();
            panel19.SuspendLayout();
            panel22.SuspendLayout();
            pnReport.SuspendLayout();
            SprContainer.SuspendLayout();
            panel7.SuspendLayout();
            panel9.SuspendLayout();
            panel10.SuspendLayout();
            panel11.SuspendLayout();
            panel20.SuspendLayout();
            panel8.SuspendLayout();
            panel12.SuspendLayout();
            panel15.SuspendLayout();
            panel21.SuspendLayout();
            panel13.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel14.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel29.SuspendLayout();
            panel28.SuspendLayout();
            panel27.SuspendLayout();
            SuspendLayout();
            // 
            // sidebarContainer
            // 
            sidebarContainer.BackColor = Color.FromArgb(248, 248, 248);
            sidebarContainer.Controls.Add(panel1);
            sidebarContainer.Controls.Add(pnMain);
            sidebarContainer.Controls.Add(pnSell);
            sidebarContainer.Controls.Add(pnServis);
            sidebarContainer.Controls.Add(deptsPanel);
            sidebarContainer.Controls.Add(pnIncome);
            sidebarContainer.Controls.Add(panel22);
            sidebarContainer.Controls.Add(pnReport);
            sidebarContainer.Controls.Add(SprContainer);
            sidebarContainer.Dock = DockStyle.Left;
            sidebarContainer.Location = new Point(0, 0);
            sidebarContainer.Name = "sidebarContainer";
            sidebarContainer.Size = new Size(235, 780);
            sidebarContainer.TabIndex = 1;
            sidebarContainer.Paint += sidebarContainer_Paint;
            // 
            // panel1
            // 
            panel1.Controls.Add(logo);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(233, 60);
            panel1.TabIndex = 0;
            // 
            // logo
            // 
            logo.Dock = DockStyle.Fill;
            logo.ErrorImage = (Image)resources.GetObject("logo.ErrorImage");
            logo.Image = (Image)resources.GetObject("logo.Image");
            logo.Location = new Point(0, 0);
            logo.Name = "logo";
            logo.Size = new Size(233, 60);
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.TabIndex = 0;
            logo.TabStop = false;
            logo.Click += logo_Click;
            // 
            // pnMain
            // 
            pnMain.BackColor = Color.Transparent;
            pnMain.Controls.Add(btnMain);
            pnMain.Location = new Point(3, 69);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(229, 43);
            pnMain.TabIndex = 1;
            // 
            // btnMain
            // 
            btnMain.BackColor = Color.FromArgb(248, 248, 248);
            btnMain.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnMain.ImageAlign = ContentAlignment.MiddleLeft;
            btnMain.ImageIndex = 0;
            btnMain.ImageList = imageList1;
            btnMain.Location = new Point(-15, -12);
            btnMain.Name = "btnMain";
            btnMain.Padding = new Padding(25, 0, 0, 0);
            btnMain.Size = new Size(265, 67);
            btnMain.TabIndex = 0;
            btnMain.Text = "            Главная";
            btnMain.TextAlign = ContentAlignment.MiddleLeft;
            btnMain.UseVisualStyleBackColor = false;
            btnMain.Click += btnMain_Click_1;
            btnMain.MouseEnter += ApplyHoverEffect;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "icons8-home-48.png");
            imageList1.Images.SetKeyName(1, "website_social_media_marketplace_facebook_menu_selling_icon_197234.png");
            imageList1.Images.SetKeyName(2, "services_vehicle_icon_217107.png");
            imageList1.Images.SetKeyName(3, "4213448-analytics-bars-chart-document-graph-report-statistics_115362.png");
            imageList1.Images.SetKeyName(4, "icons8-bar-chart-50.png");
            imageList1.Images.SetKeyName(5, "2849824-basket-buy-market-multimedia-shop-shopping-store_107977.png");
            imageList1.Images.SetKeyName(6, "button_minus_icon_246519.png");
            imageList1.Images.SetKeyName(7, "icons8-list-48.png");
            imageList1.Images.SetKeyName(8, "icons8-delete-64.png");
            imageList1.Images.SetKeyName(9, "icons8-edit-50.png");
            imageList1.Images.SetKeyName(10, "point_icon_151143.png");
            imageList1.Images.SetKeyName(11, "icons8-flat-tire-32.png");
            imageList1.Images.SetKeyName(12, "icons8-get-cash-64.png");
            imageList1.Images.SetKeyName(13, "icons8-person-64.png");
            // 
            // pnSell
            // 
            pnSell.BackColor = Color.Transparent;
            pnSell.Controls.Add(btnSell);
            pnSell.Location = new Point(3, 118);
            pnSell.Name = "pnSell";
            pnSell.Size = new Size(229, 43);
            pnSell.TabIndex = 2;
            // 
            // btnSell
            // 
            btnSell.BackColor = Color.FromArgb(248, 248, 248);
            btnSell.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSell.ImageAlign = ContentAlignment.MiddleLeft;
            btnSell.ImageIndex = 1;
            btnSell.ImageList = imageList1;
            btnSell.Location = new Point(-15, -12);
            btnSell.Name = "btnSell";
            btnSell.Padding = new Padding(25, 0, 0, 0);
            btnSell.Size = new Size(265, 67);
            btnSell.TabIndex = 0;
            btnSell.Text = "            Продажа";
            btnSell.TextAlign = ContentAlignment.MiddleLeft;
            btnSell.UseVisualStyleBackColor = false;
            btnSell.Click += btnSell_Click;
            btnSell.MouseEnter += ApplyHoverEffect;
            // 
            // pnServis
            // 
            pnServis.BackColor = Color.Transparent;
            pnServis.Controls.Add(btnServis);
            pnServis.Location = new Point(3, 167);
            pnServis.Name = "pnServis";
            pnServis.Size = new Size(229, 43);
            pnServis.TabIndex = 2;
            // 
            // btnServis
            // 
            btnServis.BackColor = Color.FromArgb(248, 248, 248);
            btnServis.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnServis.ImageAlign = ContentAlignment.MiddleLeft;
            btnServis.ImageIndex = 2;
            btnServis.ImageList = imageList1;
            btnServis.Location = new Point(-15, -12);
            btnServis.Name = "btnServis";
            btnServis.Padding = new Padding(25, 0, 0, 0);
            btnServis.Size = new Size(265, 67);
            btnServis.TabIndex = 0;
            btnServis.Text = "            Услуги";
            btnServis.TextAlign = ContentAlignment.MiddleLeft;
            btnServis.UseVisualStyleBackColor = false;
            btnServis.Click += btnServis_Click;
            btnServis.MouseEnter += ApplyHoverEffect;
            // 
            // deptsPanel
            // 
            deptsPanel.BackColor = Color.FromArgb(248, 248, 248);
            deptsPanel.Controls.Add(panel23);
            deptsPanel.Controls.Add(panel24);
            deptsPanel.Controls.Add(panel25);
            deptsPanel.Location = new Point(3, 216);
            deptsPanel.Name = "deptsPanel";
            deptsPanel.Size = new Size(233, 47);
            deptsPanel.TabIndex = 7;
            // 
            // panel23
            // 
            panel23.BackColor = Color.Transparent;
            panel23.Controls.Add(btnDepts);
            panel23.Location = new Point(3, 3);
            panel23.Name = "panel23";
            panel23.Size = new Size(247, 43);
            panel23.TabIndex = 4;
            // 
            // btnDepts
            // 
            btnDepts.BackColor = Color.FromArgb(248, 248, 248);
            btnDepts.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnDepts.ImageAlign = ContentAlignment.MiddleLeft;
            btnDepts.ImageIndex = 12;
            btnDepts.ImageList = imageList1;
            btnDepts.Location = new Point(-15, -19);
            btnDepts.Name = "btnDepts";
            btnDepts.Padding = new Padding(25, 0, 0, 0);
            btnDepts.Size = new Size(265, 75);
            btnDepts.TabIndex = 0;
            btnDepts.Text = "            Долги";
            btnDepts.TextAlign = ContentAlignment.MiddleLeft;
            btnDepts.UseVisualStyleBackColor = false;
            btnDepts.Click += btnDepts_Click;
            // 
            // panel24
            // 
            panel24.BackColor = Color.Transparent;
            panel24.Controls.Add(debtors);
            panel24.ForeColor = Color.DodgerBlue;
            panel24.Location = new Point(3, 56);
            panel24.Margin = new Padding(3, 7, 3, 0);
            panel24.Name = "panel24";
            panel24.Size = new Size(241, 29);
            panel24.TabIndex = 7;
            // 
            // debtors
            // 
            debtors.BackColor = Color.FromArgb(248, 248, 248);
            debtors.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            debtors.ImageAlign = ContentAlignment.MiddleLeft;
            debtors.ImageIndex = 6;
            debtors.ImageList = imageList1;
            debtors.Location = new Point(-24, -11);
            debtors.Name = "debtors";
            debtors.Padding = new Padding(59, 0, 0, 0);
            debtors.Size = new Size(274, 49);
            debtors.TabIndex = 0;
            debtors.Text = "            Должники";
            debtors.TextAlign = ContentAlignment.MiddleLeft;
            debtors.UseVisualStyleBackColor = false;
            // 
            // panel25
            // 
            panel25.BackColor = Color.Transparent;
            panel25.Controls.Add(btnMydepts);
            panel25.ForeColor = Color.DodgerBlue;
            panel25.Location = new Point(3, 92);
            panel25.Margin = new Padding(3, 7, 3, 0);
            panel25.Name = "panel25";
            panel25.Size = new Size(241, 32);
            panel25.TabIndex = 8;
            // 
            // btnMydepts
            // 
            btnMydepts.BackColor = Color.FromArgb(248, 248, 248);
            btnMydepts.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnMydepts.ImageAlign = ContentAlignment.MiddleLeft;
            btnMydepts.ImageIndex = 6;
            btnMydepts.ImageList = imageList1;
            btnMydepts.Location = new Point(-24, -5);
            btnMydepts.Name = "btnMydepts";
            btnMydepts.Padding = new Padding(59, 0, 0, 0);
            btnMydepts.Size = new Size(274, 43);
            btnMydepts.TabIndex = 0;
            btnMydepts.Text = "            Мои долги";
            btnMydepts.TextAlign = ContentAlignment.MiddleLeft;
            btnMydepts.UseVisualStyleBackColor = false;
            // 
            // pnIncome
            // 
            pnIncome.BackColor = Color.FromArgb(248, 248, 248);
            pnIncome.Controls.Add(panel16);
            pnIncome.Controls.Add(panel17);
            pnIncome.Controls.Add(panel18);
            pnIncome.Controls.Add(panel19);
            pnIncome.Location = new Point(3, 269);
            pnIncome.Name = "pnIncome";
            pnIncome.Size = new Size(233, 40);
            pnIncome.TabIndex = 6;
            // 
            // panel16
            // 
            panel16.BackColor = Color.Transparent;
            panel16.Controls.Add(btnIncome);
            panel16.Location = new Point(3, 3);
            panel16.Name = "panel16";
            panel16.Size = new Size(247, 43);
            panel16.TabIndex = 4;
            // 
            // btnIncome
            // 
            btnIncome.BackColor = Color.FromArgb(248, 248, 248);
            btnIncome.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnIncome.ImageAlign = ContentAlignment.MiddleLeft;
            btnIncome.ImageIndex = 5;
            btnIncome.ImageList = imageList1;
            btnIncome.Location = new Point(-15, -19);
            btnIncome.Name = "btnIncome";
            btnIncome.Padding = new Padding(25, 0, 0, 0);
            btnIncome.Size = new Size(265, 75);
            btnIncome.TabIndex = 0;
            btnIncome.Text = "            Пополнение";
            btnIncome.TextAlign = ContentAlignment.MiddleLeft;
            btnIncome.UseVisualStyleBackColor = false;
            btnIncome.Click += btnIncome_Click;
            btnIncome.MouseEnter += ApplyHoverEffect;
            // 
            // panel17
            // 
            panel17.BackColor = Color.Transparent;
            panel17.Controls.Add(btIndexIncome);
            panel17.ForeColor = Color.DodgerBlue;
            panel17.Location = new Point(3, 56);
            panel17.Margin = new Padding(3, 7, 3, 0);
            panel17.Name = "panel17";
            panel17.Size = new Size(241, 29);
            panel17.TabIndex = 7;
            // 
            // btIndexIncome
            // 
            btIndexIncome.BackColor = Color.FromArgb(248, 248, 248);
            btIndexIncome.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btIndexIncome.ImageAlign = ContentAlignment.MiddleLeft;
            btIndexIncome.ImageIndex = 6;
            btIndexIncome.ImageList = imageList1;
            btIndexIncome.Location = new Point(-24, -11);
            btIndexIncome.Name = "btIndexIncome";
            btIndexIncome.Padding = new Padding(59, 0, 0, 0);
            btIndexIncome.Size = new Size(274, 49);
            btIndexIncome.TabIndex = 0;
            btIndexIncome.Text = "            Закупка";
            btIndexIncome.TextAlign = ContentAlignment.MiddleLeft;
            btIndexIncome.UseVisualStyleBackColor = false;
            btIndexIncome.Click += btnSubIncome_Click;
            btIndexIncome.MouseEnter += ApplyHoverEffect;
            // 
            // panel18
            // 
            panel18.BackColor = Color.Transparent;
            panel18.Controls.Add(button9);
            panel18.ForeColor = Color.DodgerBlue;
            panel18.Location = new Point(3, 92);
            panel18.Margin = new Padding(3, 7, 3, 0);
            panel18.Name = "panel18";
            panel18.Size = new Size(241, 32);
            panel18.TabIndex = 8;
            // 
            // button9
            // 
            button9.BackColor = Color.FromArgb(248, 248, 248);
            button9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button9.ImageAlign = ContentAlignment.MiddleLeft;
            button9.ImageIndex = 6;
            button9.ImageList = imageList1;
            button9.Location = new Point(-24, -5);
            button9.Name = "button9";
            button9.Padding = new Padding(59, 0, 0, 0);
            button9.Size = new Size(274, 43);
            button9.TabIndex = 0;
            button9.Text = "            Партии";
            button9.TextAlign = ContentAlignment.MiddleLeft;
            button9.UseVisualStyleBackColor = false;
            button9.Click += btnBatch_Click;
            button9.MouseEnter += ApplyHoverEffect;
            // 
            // panel19
            // 
            panel19.BackColor = Color.Transparent;
            panel19.Controls.Add(button10);
            panel19.ForeColor = Color.DodgerBlue;
            panel19.Location = new Point(3, 131);
            panel19.Margin = new Padding(3, 7, 3, 0);
            panel19.Name = "panel19";
            panel19.Size = new Size(241, 31);
            panel19.TabIndex = 7;
            // 
            // button10
            // 
            button10.BackColor = Color.FromArgb(248, 248, 248);
            button10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button10.ImageAlign = ContentAlignment.MiddleLeft;
            button10.ImageIndex = 6;
            button10.ImageList = imageList1;
            button10.Location = new Point(-24, -5);
            button10.Name = "button10";
            button10.Padding = new Padding(59, 0, 0, 0);
            button10.Size = new Size(274, 43);
            button10.TabIndex = 0;
            button10.Text = "            Что то";
            button10.TextAlign = ContentAlignment.MiddleLeft;
            button10.UseVisualStyleBackColor = false;
            button10.MouseEnter += ApplyHoverEffect;
            // 
            // panel22
            // 
            panel22.BackColor = Color.Transparent;
            panel22.Controls.Add(btnParts);
            panel22.Location = new Point(3, 315);
            panel22.Name = "panel22";
            panel22.Size = new Size(229, 43);
            panel22.TabIndex = 7;
            // 
            // btnParts
            // 
            btnParts.BackColor = Color.FromArgb(248, 248, 248);
            btnParts.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnParts.ImageAlign = ContentAlignment.MiddleLeft;
            btnParts.ImageIndex = 11;
            btnParts.ImageList = imageList1;
            btnParts.Location = new Point(-15, -12);
            btnParts.Name = "btnParts";
            btnParts.Padding = new Padding(25, 0, 0, 0);
            btnParts.Size = new Size(265, 67);
            btnParts.TabIndex = 0;
            btnParts.Text = "            Запчасты";
            btnParts.TextAlign = ContentAlignment.MiddleLeft;
            btnParts.UseVisualStyleBackColor = false;
            btnParts.Click += btnParts_Click;
            // 
            // pnReport
            // 
            pnReport.BackColor = Color.Transparent;
            pnReport.Controls.Add(btnReport);
            pnReport.Location = new Point(3, 364);
            pnReport.Name = "pnReport";
            pnReport.Size = new Size(229, 43);
            pnReport.TabIndex = 3;
            // 
            // btnReport
            // 
            btnReport.BackColor = Color.FromArgb(248, 248, 248);
            btnReport.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnReport.ImageAlign = ContentAlignment.MiddleLeft;
            btnReport.ImageIndex = 3;
            btnReport.ImageList = imageList1;
            btnReport.Location = new Point(-15, -12);
            btnReport.Name = "btnReport";
            btnReport.Padding = new Padding(25, 0, 0, 0);
            btnReport.Size = new Size(265, 67);
            btnReport.TabIndex = 0;
            btnReport.Text = "            Отчёти";
            btnReport.TextAlign = ContentAlignment.MiddleLeft;
            btnReport.UseVisualStyleBackColor = false;
            btnReport.MouseEnter += ApplyHoverEffect;
            // 
            // SprContainer
            // 
            SprContainer.BackColor = Color.FromArgb(248, 248, 248);
            SprContainer.Controls.Add(panel7);
            SprContainer.Controls.Add(panel9);
            SprContainer.Controls.Add(panel10);
            SprContainer.Controls.Add(panel11);
            SprContainer.Controls.Add(panel20);
            SprContainer.Controls.Add(panel8);
            SprContainer.Controls.Add(panel12);
            SprContainer.Controls.Add(panel15);
            SprContainer.Controls.Add(panel21);
            SprContainer.Controls.Add(panel13);
            SprContainer.Controls.Add(flowLayoutPanel5);
            SprContainer.Location = new Point(3, 413);
            SprContainer.Name = "SprContainer";
            SprContainer.Size = new Size(233, 46);
            SprContainer.TabIndex = 5;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Transparent;
            panel7.Controls.Add(btnSpr);
            panel7.Location = new Point(3, 3);
            panel7.Name = "panel7";
            panel7.Size = new Size(247, 43);
            panel7.TabIndex = 4;
            // 
            // btnSpr
            // 
            btnSpr.BackColor = Color.FromArgb(248, 248, 248);
            btnSpr.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSpr.ImageAlign = ContentAlignment.MiddleLeft;
            btnSpr.ImageIndex = 4;
            btnSpr.ImageList = imageList1;
            btnSpr.Location = new Point(-15, -19);
            btnSpr.Name = "btnSpr";
            btnSpr.Padding = new Padding(25, 0, 0, 0);
            btnSpr.Size = new Size(265, 75);
            btnSpr.TabIndex = 0;
            btnSpr.Text = "            Справочник";
            btnSpr.TextAlign = ContentAlignment.MiddleLeft;
            btnSpr.UseVisualStyleBackColor = false;
            btnSpr.Click += btnSpr_Click;
            btnSpr.MouseEnter += ApplyHoverEffect;
            // 
            // panel9
            // 
            panel9.BackColor = Color.Transparent;
            panel9.Controls.Add(btnSservis);
            panel9.ForeColor = Color.DodgerBlue;
            panel9.Location = new Point(3, 56);
            panel9.Margin = new Padding(3, 7, 3, 0);
            panel9.Name = "panel9";
            panel9.Size = new Size(241, 29);
            panel9.TabIndex = 7;
            // 
            // btnSservis
            // 
            btnSservis.BackColor = Color.FromArgb(248, 248, 248);
            btnSservis.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSservis.ImageAlign = ContentAlignment.MiddleLeft;
            btnSservis.ImageIndex = 6;
            btnSservis.ImageList = imageList1;
            btnSservis.Location = new Point(-24, -11);
            btnSservis.Name = "btnSservis";
            btnSservis.Padding = new Padding(59, 0, 0, 0);
            btnSservis.Size = new Size(274, 49);
            btnSservis.TabIndex = 0;
            btnSservis.Text = "            Услуги";
            btnSservis.TextAlign = ContentAlignment.MiddleLeft;
            btnSservis.UseVisualStyleBackColor = false;
            btnSservis.Click += btnService_Click;
            btnSservis.MouseEnter += ApplyHoverEffect;
            // 
            // panel10
            // 
            panel10.BackColor = Color.Transparent;
            panel10.Controls.Add(btnSmadeBy);
            panel10.ForeColor = Color.DodgerBlue;
            panel10.Location = new Point(3, 92);
            panel10.Margin = new Padding(3, 7, 3, 0);
            panel10.Name = "panel10";
            panel10.Size = new Size(241, 32);
            panel10.TabIndex = 8;
            // 
            // btnSmadeBy
            // 
            btnSmadeBy.BackColor = Color.FromArgb(248, 248, 248);
            btnSmadeBy.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSmadeBy.ImageAlign = ContentAlignment.MiddleLeft;
            btnSmadeBy.ImageIndex = 6;
            btnSmadeBy.ImageList = imageList1;
            btnSmadeBy.Location = new Point(-24, -5);
            btnSmadeBy.Name = "btnSmadeBy";
            btnSmadeBy.Padding = new Padding(59, 0, 0, 0);
            btnSmadeBy.Size = new Size(274, 43);
            btnSmadeBy.TabIndex = 0;
            btnSmadeBy.Text = "            Производитель";
            btnSmadeBy.TextAlign = ContentAlignment.MiddleLeft;
            btnSmadeBy.UseVisualStyleBackColor = false;
            btnSmadeBy.Click += btnSmadeBy_Click;
            btnSmadeBy.MouseEnter += ApplyHoverEffect;
            // 
            // panel11
            // 
            panel11.BackColor = Color.Transparent;
            panel11.Controls.Add(btnSParts);
            panel11.ForeColor = Color.DodgerBlue;
            panel11.Location = new Point(3, 131);
            panel11.Margin = new Padding(3, 7, 3, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(241, 31);
            panel11.TabIndex = 7;
            // 
            // btnSParts
            // 
            btnSParts.BackColor = Color.FromArgb(248, 248, 248);
            btnSParts.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSParts.ImageAlign = ContentAlignment.MiddleLeft;
            btnSParts.ImageIndex = 6;
            btnSParts.ImageList = imageList1;
            btnSParts.Location = new Point(-24, -5);
            btnSParts.Name = "btnSParts";
            btnSParts.Padding = new Padding(59, 0, 0, 0);
            btnSParts.Size = new Size(274, 43);
            btnSParts.TabIndex = 0;
            btnSParts.Text = "            Запчасти";
            btnSParts.TextAlign = ContentAlignment.MiddleLeft;
            btnSParts.UseVisualStyleBackColor = false;
            btnSParts.Click += btnSpartQuality_Click;
            btnSParts.MouseEnter += ApplyHoverEffect;
            // 
            // panel20
            // 
            panel20.BackColor = Color.Transparent;
            panel20.Controls.Add(btnPartQuality);
            panel20.ForeColor = Color.DodgerBlue;
            panel20.Location = new Point(3, 169);
            panel20.Margin = new Padding(3, 7, 3, 0);
            panel20.Name = "panel20";
            panel20.Size = new Size(241, 31);
            panel20.TabIndex = 9;
            // 
            // btnPartQuality
            // 
            btnPartQuality.BackColor = Color.FromArgb(248, 248, 248);
            btnPartQuality.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnPartQuality.ImageAlign = ContentAlignment.MiddleLeft;
            btnPartQuality.ImageIndex = 6;
            btnPartQuality.ImageList = imageList1;
            btnPartQuality.Location = new Point(-24, -5);
            btnPartQuality.Name = "btnPartQuality";
            btnPartQuality.Padding = new Padding(59, 0, 0, 0);
            btnPartQuality.Size = new Size(274, 43);
            btnPartQuality.TabIndex = 0;
            btnPartQuality.Text = "            Качества Запчасти";
            btnPartQuality.TextAlign = ContentAlignment.MiddleLeft;
            btnPartQuality.UseVisualStyleBackColor = false;
            btnPartQuality.Click += btnPartQuality_Click;
            btnPartQuality.MouseEnter += ApplyHoverEffect;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Transparent;
            panel8.Controls.Add(btnSstatus);
            panel8.ForeColor = Color.DodgerBlue;
            panel8.Location = new Point(3, 207);
            panel8.Margin = new Padding(3, 7, 3, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(241, 29);
            panel8.TabIndex = 6;
            // 
            // btnSstatus
            // 
            btnSstatus.BackColor = Color.FromArgb(248, 248, 248);
            btnSstatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSstatus.ImageAlign = ContentAlignment.MiddleLeft;
            btnSstatus.ImageIndex = 6;
            btnSstatus.ImageList = imageList1;
            btnSstatus.Location = new Point(-24, -5);
            btnSstatus.Name = "btnSstatus";
            btnSstatus.Padding = new Padding(59, 0, 0, 0);
            btnSstatus.Size = new Size(274, 43);
            btnSstatus.TabIndex = 0;
            btnSstatus.Text = "            Статуси";
            btnSstatus.TextAlign = ContentAlignment.MiddleLeft;
            btnSstatus.UseVisualStyleBackColor = false;
            btnSstatus.Click += btnSstatus_Click;
            btnSstatus.MouseEnter += ApplyHoverEffect;
            // 
            // panel12
            // 
            panel12.BackColor = Color.Transparent;
            panel12.Controls.Add(btnSsuplier);
            panel12.ForeColor = Color.DodgerBlue;
            panel12.Location = new Point(3, 243);
            panel12.Margin = new Padding(3, 7, 3, 0);
            panel12.Name = "panel12";
            panel12.Size = new Size(241, 31);
            panel12.TabIndex = 7;
            // 
            // btnSsuplier
            // 
            btnSsuplier.BackColor = Color.FromArgb(248, 248, 248);
            btnSsuplier.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSsuplier.ImageAlign = ContentAlignment.MiddleLeft;
            btnSsuplier.ImageIndex = 6;
            btnSsuplier.ImageList = imageList1;
            btnSsuplier.Location = new Point(-24, -5);
            btnSsuplier.Name = "btnSsuplier";
            btnSsuplier.Padding = new Padding(59, 0, 0, 0);
            btnSsuplier.Size = new Size(274, 43);
            btnSsuplier.TabIndex = 0;
            btnSsuplier.Text = "            Поставщики";
            btnSsuplier.TextAlign = ContentAlignment.MiddleLeft;
            btnSsuplier.UseVisualStyleBackColor = false;
            btnSsuplier.Click += btnSsuplier_Click;
            btnSsuplier.MouseEnter += ApplyHoverEffect;
            // 
            // panel15
            // 
            panel15.BackColor = Color.Transparent;
            panel15.Controls.Add(button6);
            panel15.ForeColor = Color.DodgerBlue;
            panel15.Location = new Point(3, 281);
            panel15.Margin = new Padding(3, 7, 3, 0);
            panel15.Name = "panel15";
            panel15.Size = new Size(241, 32);
            panel15.TabIndex = 9;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(248, 248, 248);
            button6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button6.ImageAlign = ContentAlignment.MiddleLeft;
            button6.ImageIndex = 6;
            button6.ImageList = imageList1;
            button6.Location = new Point(-24, -5);
            button6.Name = "button6";
            button6.Padding = new Padding(59, 0, 0, 0);
            button6.Size = new Size(274, 43);
            button6.TabIndex = 0;
            button6.Text = "            Модель машини";
            button6.TextAlign = ContentAlignment.MiddleLeft;
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click_1;
            button6.MouseEnter += ApplyHoverEffect;
            // 
            // panel21
            // 
            panel21.BackColor = Color.Transparent;
            panel21.Controls.Add(btnCarBrand);
            panel21.ForeColor = Color.DodgerBlue;
            panel21.Location = new Point(3, 320);
            panel21.Margin = new Padding(3, 7, 3, 0);
            panel21.Name = "panel21";
            panel21.Size = new Size(241, 32);
            panel21.TabIndex = 10;
            // 
            // btnCarBrand
            // 
            btnCarBrand.BackColor = Color.FromArgb(248, 248, 248);
            btnCarBrand.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnCarBrand.ImageAlign = ContentAlignment.MiddleLeft;
            btnCarBrand.ImageIndex = 6;
            btnCarBrand.ImageList = imageList1;
            btnCarBrand.Location = new Point(-24, -5);
            btnCarBrand.Name = "btnCarBrand";
            btnCarBrand.Padding = new Padding(59, 0, 0, 0);
            btnCarBrand.Size = new Size(274, 43);
            btnCarBrand.TabIndex = 0;
            btnCarBrand.Text = "            Марка машини";
            btnCarBrand.TextAlign = ContentAlignment.MiddleLeft;
            btnCarBrand.UseVisualStyleBackColor = false;
            btnCarBrand.Click += btnCarBrand_Click;
            btnCarBrand.MouseEnter += ApplyHoverEffect;
            // 
            // panel13
            // 
            panel13.BackColor = Color.Transparent;
            panel13.Controls.Add(btnSstock);
            panel13.ForeColor = Color.DodgerBlue;
            panel13.Location = new Point(3, 359);
            panel13.Margin = new Padding(3, 7, 3, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(241, 31);
            panel13.TabIndex = 8;
            // 
            // btnSstock
            // 
            btnSstock.BackColor = Color.FromArgb(248, 248, 248);
            btnSstock.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSstock.ImageAlign = ContentAlignment.MiddleLeft;
            btnSstock.ImageIndex = 6;
            btnSstock.ImageList = imageList1;
            btnSstock.Location = new Point(-24, -5);
            btnSstock.Name = "btnSstock";
            btnSstock.Padding = new Padding(59, 0, 0, 0);
            btnSstock.Size = new Size(274, 43);
            btnSstock.TabIndex = 0;
            btnSstock.Text = "            Склад";
            btnSstock.TextAlign = ContentAlignment.MiddleLeft;
            btnSstock.UseVisualStyleBackColor = false;
            btnSstock.Click += btnSstock_Click;
            btnSstock.MouseEnter += ApplyHoverEffect;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.BackColor = Color.FromArgb(248, 248, 248);
            flowLayoutPanel5.Controls.Add(panel3);
            flowLayoutPanel5.Controls.Add(panel4);
            flowLayoutPanel5.Controls.Add(panel5);
            flowLayoutPanel5.Controls.Add(panel6);
            flowLayoutPanel5.Controls.Add(panel14);
            flowLayoutPanel5.Location = new Point(3, 393);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(250, 167);
            flowLayoutPanel5.TabIndex = 9;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.Controls.Add(button1);
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(247, 35);
            panel3.TabIndex = 4;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(248, 248, 248);
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button1.ForeColor = Color.DodgerBlue;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.ImageIndex = 6;
            button1.ImageList = imageList1;
            button1.Location = new Point(-15, -9);
            button1.Name = "button1";
            button1.Padding = new Padding(50, 0, 0, 0);
            button1.Size = new Size(265, 57);
            button1.TabIndex = 0;
            button1.Text = "           Nimadir";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += ApplyHoverEffect;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Transparent;
            panel4.Controls.Add(button2);
            panel4.ForeColor = Color.DodgerBlue;
            panel4.Location = new Point(3, 41);
            panel4.Margin = new Padding(3, 0, 3, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(241, 29);
            panel4.TabIndex = 7;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(248, 248, 248);
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.ImageIndex = 10;
            button2.ImageList = imageList1;
            button2.Location = new Point(-24, -7);
            button2.Name = "button2";
            button2.Padding = new Padding(80, 0, 0, 0);
            button2.Size = new Size(274, 49);
            button2.TabIndex = 0;
            button2.Text = "            Услуги";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Transparent;
            panel5.Controls.Add(button3);
            panel5.ForeColor = Color.DodgerBlue;
            panel5.Location = new Point(3, 70);
            panel5.Margin = new Padding(3, 0, 3, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(241, 29);
            panel5.TabIndex = 8;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(248, 248, 248);
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.ImageIndex = 10;
            button3.ImageList = imageList1;
            button3.Location = new Point(-24, -7);
            button3.Name = "button3";
            button3.Padding = new Padding(80, 0, 0, 0);
            button3.Size = new Size(274, 49);
            button3.TabIndex = 0;
            button3.Text = "            Услуги";
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            panel6.BackColor = Color.Transparent;
            panel6.Controls.Add(button4);
            panel6.ForeColor = Color.DodgerBlue;
            panel6.Location = new Point(3, 99);
            panel6.Margin = new Padding(3, 0, 3, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(241, 29);
            panel6.TabIndex = 9;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(248, 248, 248);
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.ImageIndex = 10;
            button4.ImageList = imageList1;
            button4.Location = new Point(-24, -7);
            button4.Name = "button4";
            button4.Padding = new Padding(80, 0, 0, 0);
            button4.Size = new Size(274, 49);
            button4.TabIndex = 0;
            button4.Text = "            Услуги";
            button4.TextAlign = ContentAlignment.MiddleLeft;
            button4.UseVisualStyleBackColor = false;
            // 
            // panel14
            // 
            panel14.BackColor = Color.Transparent;
            panel14.Controls.Add(button5);
            panel14.ForeColor = Color.DodgerBlue;
            panel14.Location = new Point(3, 128);
            panel14.Margin = new Padding(3, 0, 3, 0);
            panel14.Name = "panel14";
            panel14.Size = new Size(241, 29);
            panel14.TabIndex = 10;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(248, 248, 248);
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button5.ImageAlign = ContentAlignment.MiddleLeft;
            button5.ImageIndex = 10;
            button5.ImageList = imageList1;
            button5.Location = new Point(-24, -7);
            button5.Name = "button5";
            button5.Padding = new Padding(80, 0, 0, 0);
            button5.Size = new Size(274, 49);
            button5.TabIndex = 0;
            button5.Text = "            Услуги";
            button5.TextAlign = ContentAlignment.MiddleLeft;
            button5.UseVisualStyleBackColor = false;
            // 
            // menuTransition
            // 
            menuTransition.Interval = 100000000;
            menuTransition.Tick += menuTransition_Tick;
            // 
            // sidebarTransition
            // 
            sidebarTransition.Interval = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.FromArgb(248, 248, 248);
            flowLayoutPanel1.Controls.Add(panel29);
            flowLayoutPanel1.Controls.Add(panel28);
            flowLayoutPanel1.Controls.Add(panel27);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(235, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1181, 51);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // panel29
            // 
            panel29.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel29.BackColor = Color.Transparent;
            panel29.Controls.Add(button11);
            panel29.Location = new Point(1119, 3);
            panel29.Name = "panel29";
            panel29.Size = new Size(59, 46);
            panel29.TabIndex = 4;
            // 
            // button11
            // 
            button11.BackColor = Color.FromArgb(248, 248, 248);
            button11.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button11.ImageAlign = ContentAlignment.MiddleLeft;
            button11.ImageIndex = 13;
            button11.ImageList = imageList1;
            button11.Location = new Point(-15, -12);
            button11.Name = "button11";
            button11.Padding = new Padding(25, 0, 0, 0);
            button11.Size = new Size(84, 67);
            button11.TabIndex = 0;
            button11.TextAlign = ContentAlignment.MiddleLeft;
            button11.UseVisualStyleBackColor = false;
            button11.Click += btnprofile_Click;
            button11.MouseEnter += ApplyHoverEffect;
            // 
            // panel28
            // 
            panel28.BackColor = Color.Transparent;
            panel28.Controls.Add(button8);
            panel28.Location = new Point(1054, 3);
            panel28.Name = "panel28";
            panel28.Size = new Size(59, 46);
            panel28.TabIndex = 4;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(248, 248, 248);
            button8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button8.ImageAlign = ContentAlignment.MiddleLeft;
            button8.ImageIndex = 2;
            button8.ImageList = imageList1;
            button8.Location = new Point(-15, -12);
            button8.Name = "button8";
            button8.Padding = new Padding(25, 0, 0, 0);
            button8.Size = new Size(82, 67);
            button8.TabIndex = 0;
            button8.TextAlign = ContentAlignment.MiddleLeft;
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            button8.MouseEnter += ApplyHoverEffect;
            // 
            // panel27
            // 
            panel27.BackColor = Color.Transparent;
            panel27.Controls.Add(button7);
            panel27.Location = new Point(989, 3);
            panel27.Name = "panel27";
            panel27.Size = new Size(59, 46);
            panel27.TabIndex = 3;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(248, 248, 248);
            button7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button7.ImageAlign = ContentAlignment.MiddleLeft;
            button7.ImageIndex = 1;
            button7.ImageList = imageList1;
            button7.Location = new Point(-15, -12);
            button7.Name = "button7";
            button7.Padding = new Padding(25, 0, 0, 0);
            button7.Size = new Size(86, 67);
            button7.TabIndex = 0;
            button7.TextAlign = ContentAlignment.MiddleLeft;
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            button7.MouseEnter += ApplyHoverEffect;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.FromArgb(224, 224, 224);
            flowLayoutPanel2.Dock = DockStyle.Bottom;
            flowLayoutPanel2.Location = new Point(235, 736);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(1181, 44);
            flowLayoutPanel2.TabIndex = 3;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.BackColor = Color.FromArgb(224, 224, 224);
            flowLayoutPanel3.Dock = DockStyle.Right;
            flowLayoutPanel3.Location = new Point(1384, 51);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(32, 685);
            flowLayoutPanel3.TabIndex = 4;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.BackColor = Color.FromArgb(224, 224, 224);
            flowLayoutPanel4.Dock = DockStyle.Left;
            flowLayoutPanel4.Location = new Point(235, 51);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(42, 685);
            flowLayoutPanel4.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(224, 224, 224);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(277, 51);
            panel2.Name = "panel2";
            panel2.Size = new Size(1107, 51);
            panel2.TabIndex = 6;
            // 
            // ContentPanel
            // 
            ContentPanel.CornerRadius = 15;
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.Location = new Point(277, 102);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new Size(1107, 634);
            ContentPanel.TabIndex = 7;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 248, 248);
            ClientSize = new Size(1416, 780);
            Controls.Add(ContentPanel);
            Controls.Add(panel2);
            Controls.Add(flowLayoutPanel4);
            Controls.Add(flowLayoutPanel3);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(sidebarContainer);
            MinimumSize = new Size(1100, 295);
            Name = "MainForm";
            Text = "SmartServis";
            Load += MainForm_Load;
            sidebarContainer.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logo).EndInit();
            pnMain.ResumeLayout(false);
            pnSell.ResumeLayout(false);
            pnServis.ResumeLayout(false);
            deptsPanel.ResumeLayout(false);
            panel23.ResumeLayout(false);
            panel24.ResumeLayout(false);
            panel25.ResumeLayout(false);
            pnIncome.ResumeLayout(false);
            panel16.ResumeLayout(false);
            panel17.ResumeLayout(false);
            panel18.ResumeLayout(false);
            panel19.ResumeLayout(false);
            panel22.ResumeLayout(false);
            pnReport.ResumeLayout(false);
            SprContainer.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel11.ResumeLayout(false);
            panel20.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel12.ResumeLayout(false);
            panel15.ResumeLayout(false);
            panel21.ResumeLayout(false);
            panel13.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel14.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            panel29.ResumeLayout(false);
            panel28.ResumeLayout(false);
            panel27.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel sidebarContainer;
        private Panel panel1;
        private Panel pnMain;
        private ImageList imageList1;
        private Panel pnServis;
        private Button btnServis;
        private Panel pnReport;
        private Button btnReport;
        private Panel panel7;
        private Button btnSpr;
        private FlowLayoutPanel SprContainer;
        private Panel panel8;
        private Button btnSstatus;
        private Panel panel9;
        private Button btnSservis;
        private Panel panel10;
        private Button btnSmadeBy;
        private Panel panel11;
        private Button btnSParts;
        private Panel panel12;
        private Button btnSsuplier;
        private Panel panel13;
        private Button btnSstock;
        private System.Windows.Forms.Timer menuTransition;
        private System.Windows.Forms.Timer sidebarTransition;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel4;
        private Panel panel2;
        private FlowLayoutPanel flowLayoutPanel5;
        private Panel panel3;
        private Button button1;
        private Panel panel4;
        private Button button2;
        private Panel panel5;
        private Button button3;
        private Panel panel6;
        private Button button4;
        private Panel panel14;
        private Button button5;
        private Panel panel15;
        private Button button6;
        private FlowLayoutPanel pnIncome;
        private Panel panel16;
        private Button btnIncome;
        private Panel panel17;
        private Button btIndexIncome;
        private Panel panel18;
        private Button button9;
        private Panel panel19;
        private Button button10;
        private Button btnMain;
        private Panel panel20;
        private Button btnPartQuality;
        private Panel panel21;
        private Button btnCarBrand;
        private Panel panel22;
        private Button btnParts;
        private CustomPanel ContentPanel;
        private FlowLayoutPanel deptsPanel;
        private Panel panel23;
        private Button btnDepts;
        private Panel panel24;
        private Button debtors;
        private Panel panel25;
        private Button btnMydepts;
        private Panel pnSell;
        private Button btnSell;
        private Panel panel27;
        private Button button7;
        private Panel panel28;
        private Button button8;
        private Panel panel29;
        private Button button11;
        private PictureBox logo;
    }
}