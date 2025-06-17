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
            pnMain = new Panel();
            btnMain = new Button();
            imageList1 = new ImageList(components);
            pnIncome = new Panel();
            btnIncome = new Button();
            pnServis = new Panel();
            btnServis = new Button();
            pnSell = new Panel();
            btnSell = new Button();
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
            btnSpartQuality = new Button();
            panel8 = new Panel();
            btnSparts = new Button();
            panel12 = new Panel();
            btnSsuplier = new Button();
            panel15 = new Panel();
            button6 = new Button();
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
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            panel2 = new Panel();
            ContentPanel = new CustomPanel();
            sidebarContainer.SuspendLayout();
            pnMain.SuspendLayout();
            pnIncome.SuspendLayout();
            pnServis.SuspendLayout();
            pnSell.SuspendLayout();
            pnReport.SuspendLayout();
            SprContainer.SuspendLayout();
            panel7.SuspendLayout();
            panel9.SuspendLayout();
            panel10.SuspendLayout();
            panel11.SuspendLayout();
            panel8.SuspendLayout();
            panel12.SuspendLayout();
            panel15.SuspendLayout();
            panel13.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel14.SuspendLayout();
            SuspendLayout();
            // 
            // sidebarContainer
            // 
            sidebarContainer.BackColor = Color.FromArgb(248, 248, 248);
            sidebarContainer.Controls.Add(panel1);
            sidebarContainer.Controls.Add(pnMain);
            sidebarContainer.Controls.Add(pnIncome);
            sidebarContainer.Controls.Add(pnServis);
            sidebarContainer.Controls.Add(pnSell);
            sidebarContainer.Controls.Add(pnReport);
            sidebarContainer.Controls.Add(SprContainer);
            sidebarContainer.Dock = DockStyle.Left;
            sidebarContainer.Location = new Point(0, 0);
            sidebarContainer.Margin = new Padding(3, 2, 3, 2);
            sidebarContainer.Name = "sidebarContainer";
            sidebarContainer.Size = new Size(206, 585);
            sidebarContainer.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Location = new Point(3, 2);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(219, 57);
            panel1.TabIndex = 0;
            // 
            // pnMain
            // 
            pnMain.BackColor = Color.Transparent;
            pnMain.Controls.Add(btnMain);
            pnMain.Location = new Point(3, 63);
            pnMain.Margin = new Padding(3, 2, 3, 2);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(200, 32);
            pnMain.TabIndex = 1;
            pnMain.Paint += panel2_Paint;
            // 
            // btnMain
            // 
            btnMain.BackColor = Color.FromArgb(248, 248, 248);
            btnMain.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnMain.ImageAlign = ContentAlignment.MiddleLeft;
            btnMain.ImageIndex = 0;
            btnMain.ImageList = imageList1;
            btnMain.Location = new Point(-13, -9);
            btnMain.Margin = new Padding(3, 2, 3, 2);
            btnMain.Name = "btnMain";
            btnMain.Padding = new Padding(22, 0, 0, 0);
            btnMain.Size = new Size(232, 50);
            btnMain.TabIndex = 0;
            btnMain.Text = "            Главная";
            btnMain.TextAlign = ContentAlignment.MiddleLeft;
            btnMain.UseVisualStyleBackColor = false;
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
            // 
            // pnIncome
            // 
            pnIncome.BackColor = Color.Transparent;
            pnIncome.Controls.Add(btnIncome);
            pnIncome.Location = new Point(3, 99);
            pnIncome.Margin = new Padding(3, 2, 3, 2);
            pnIncome.Name = "pnIncome";
            pnIncome.Size = new Size(200, 32);
            pnIncome.TabIndex = 2;
            // 
            // btnIncome
            // 
            btnIncome.BackColor = Color.FromArgb(248, 248, 248);
            btnIncome.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnIncome.ImageAlign = ContentAlignment.MiddleLeft;
            btnIncome.ImageIndex = 5;
            btnIncome.ImageList = imageList1;
            btnIncome.Location = new Point(-13, -9);
            btnIncome.Margin = new Padding(3, 2, 3, 2);
            btnIncome.Name = "btnIncome";
            btnIncome.Padding = new Padding(22, 0, 0, 0);
            btnIncome.Size = new Size(232, 50);
            btnIncome.TabIndex = 0;
            btnIncome.Text = "            Пополнение";
            btnIncome.TextAlign = ContentAlignment.MiddleLeft;
            btnIncome.UseVisualStyleBackColor = false;
            btnIncome.MouseEnter += ApplyHoverEffect;
            // 
            // pnServis
            // 
            pnServis.BackColor = Color.Transparent;
            pnServis.Controls.Add(btnServis);
            pnServis.Location = new Point(3, 135);
            pnServis.Margin = new Padding(3, 2, 3, 2);
            pnServis.Name = "pnServis";
            pnServis.Size = new Size(200, 32);
            pnServis.TabIndex = 2;
            // 
            // btnServis
            // 
            btnServis.BackColor = Color.FromArgb(248, 248, 248);
            btnServis.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnServis.ImageAlign = ContentAlignment.MiddleLeft;
            btnServis.ImageIndex = 2;
            btnServis.ImageList = imageList1;
            btnServis.Location = new Point(-13, -9);
            btnServis.Margin = new Padding(3, 2, 3, 2);
            btnServis.Name = "btnServis";
            btnServis.Padding = new Padding(22, 0, 0, 0);
            btnServis.Size = new Size(232, 50);
            btnServis.TabIndex = 0;
            btnServis.Text = "            Услуги";
            btnServis.TextAlign = ContentAlignment.MiddleLeft;
            btnServis.UseVisualStyleBackColor = false;
            btnServis.MouseEnter += ApplyHoverEffect;
            // 
            // pnSell
            // 
            pnSell.BackColor = Color.Transparent;
            pnSell.Controls.Add(btnSell);
            pnSell.Location = new Point(3, 171);
            pnSell.Margin = new Padding(3, 2, 3, 2);
            pnSell.Name = "pnSell";
            pnSell.Size = new Size(200, 32);
            pnSell.TabIndex = 2;
            // 
            // btnSell
            // 
            btnSell.BackColor = Color.FromArgb(248, 248, 248);
            btnSell.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSell.ImageAlign = ContentAlignment.MiddleLeft;
            btnSell.ImageIndex = 1;
            btnSell.ImageList = imageList1;
            btnSell.Location = new Point(-13, -9);
            btnSell.Margin = new Padding(3, 2, 3, 2);
            btnSell.Name = "btnSell";
            btnSell.Padding = new Padding(22, 0, 0, 0);
            btnSell.Size = new Size(232, 50);
            btnSell.TabIndex = 0;
            btnSell.Text = "            Продажа";
            btnSell.TextAlign = ContentAlignment.MiddleLeft;
            btnSell.UseVisualStyleBackColor = false;
            btnSell.MouseEnter += ApplyHoverEffect;
            // 
            // pnReport
            // 
            pnReport.BackColor = Color.Transparent;
            pnReport.Controls.Add(btnReport);
            pnReport.Location = new Point(3, 207);
            pnReport.Margin = new Padding(3, 2, 3, 2);
            pnReport.Name = "pnReport";
            pnReport.Size = new Size(200, 32);
            pnReport.TabIndex = 3;
            // 
            // btnReport
            // 
            btnReport.BackColor = Color.FromArgb(248, 248, 248);
            btnReport.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnReport.ImageAlign = ContentAlignment.MiddleLeft;
            btnReport.ImageIndex = 3;
            btnReport.ImageList = imageList1;
            btnReport.Location = new Point(-13, -9);
            btnReport.Margin = new Padding(3, 2, 3, 2);
            btnReport.Name = "btnReport";
            btnReport.Padding = new Padding(22, 0, 0, 0);
            btnReport.Size = new Size(232, 50);
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
            SprContainer.Controls.Add(panel8);
            SprContainer.Controls.Add(panel12);
            SprContainer.Controls.Add(panel15);
            SprContainer.Controls.Add(panel13);
            SprContainer.Controls.Add(flowLayoutPanel5);
            SprContainer.Location = new Point(3, 243);
            SprContainer.Margin = new Padding(3, 2, 3, 2);
            SprContainer.Name = "SprContainer";
            SprContainer.Size = new Size(204, 39);
            SprContainer.TabIndex = 5;
            SprContainer.Paint += SprContainer_Paint;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Transparent;
            panel7.Controls.Add(btnSpr);
            panel7.Location = new Point(3, 2);
            panel7.Margin = new Padding(3, 2, 3, 2);
            panel7.Name = "panel7";
            panel7.Size = new Size(216, 32);
            panel7.TabIndex = 4;
            // 
            // btnSpr
            // 
            btnSpr.BackColor = Color.FromArgb(248, 248, 248);
            btnSpr.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSpr.ImageAlign = ContentAlignment.MiddleLeft;
            btnSpr.ImageIndex = 4;
            btnSpr.ImageList = imageList1;
            btnSpr.Location = new Point(-13, -14);
            btnSpr.Margin = new Padding(3, 2, 3, 2);
            btnSpr.Name = "btnSpr";
            btnSpr.Padding = new Padding(22, 0, 0, 0);
            btnSpr.Size = new Size(232, 56);
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
            panel9.Location = new Point(3, 41);
            panel9.Margin = new Padding(3, 5, 3, 0);
            panel9.Name = "panel9";
            panel9.Size = new Size(211, 22);
            panel9.TabIndex = 7;
            // 
            // btnSservis
            // 
            btnSservis.BackColor = Color.FromArgb(248, 248, 248);
            btnSservis.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSservis.ImageAlign = ContentAlignment.MiddleLeft;
            btnSservis.ImageIndex = 6;
            btnSservis.ImageList = imageList1;
            btnSservis.Location = new Point(-21, -8);
            btnSservis.Margin = new Padding(3, 2, 3, 2);
            btnSservis.Name = "btnSservis";
            btnSservis.Padding = new Padding(52, 0, 0, 0);
            btnSservis.Size = new Size(240, 37);
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
            panel10.Location = new Point(3, 68);
            panel10.Margin = new Padding(3, 5, 3, 0);
            panel10.Name = "panel10";
            panel10.Size = new Size(211, 24);
            panel10.TabIndex = 8;
            // 
            // btnSmadeBy
            // 
            btnSmadeBy.BackColor = Color.FromArgb(248, 248, 248);
            btnSmadeBy.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSmadeBy.ImageAlign = ContentAlignment.MiddleLeft;
            btnSmadeBy.ImageIndex = 6;
            btnSmadeBy.ImageList = imageList1;
            btnSmadeBy.Location = new Point(-21, -4);
            btnSmadeBy.Margin = new Padding(3, 2, 3, 2);
            btnSmadeBy.Name = "btnSmadeBy";
            btnSmadeBy.Padding = new Padding(52, 0, 0, 0);
            btnSmadeBy.Size = new Size(240, 32);
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
            panel11.Controls.Add(btnSpartQuality);
            panel11.ForeColor = Color.DodgerBlue;
            panel11.Location = new Point(3, 97);
            panel11.Margin = new Padding(3, 5, 3, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(211, 23);
            panel11.TabIndex = 7;
            // 
            // btnSpartQuality
            // 
            btnSpartQuality.BackColor = Color.FromArgb(248, 248, 248);
            btnSpartQuality.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSpartQuality.ImageAlign = ContentAlignment.MiddleLeft;
            btnSpartQuality.ImageIndex = 6;
            btnSpartQuality.ImageList = imageList1;
            btnSpartQuality.Location = new Point(-21, -4);
            btnSpartQuality.Margin = new Padding(3, 2, 3, 2);
            btnSpartQuality.Name = "btnSpartQuality";
            btnSpartQuality.Padding = new Padding(52, 0, 0, 0);
            btnSpartQuality.Size = new Size(240, 32);
            btnSpartQuality.TabIndex = 0;
            btnSpartQuality.Text = "            Качество запчаст";
            btnSpartQuality.TextAlign = ContentAlignment.MiddleLeft;
            btnSpartQuality.UseVisualStyleBackColor = false;
            btnSpartQuality.Click += btnSpartQuality_Click;
            btnSpartQuality.MouseEnter += ApplyHoverEffect;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Transparent;
            panel8.Controls.Add(btnSparts);
            panel8.ForeColor = Color.DodgerBlue;
            panel8.Location = new Point(3, 125);
            panel8.Margin = new Padding(3, 5, 3, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(211, 22);
            panel8.TabIndex = 6;
            // 
            // btnSparts
            // 
            btnSparts.BackColor = Color.FromArgb(248, 248, 248);
            btnSparts.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSparts.ImageAlign = ContentAlignment.MiddleLeft;
            btnSparts.ImageIndex = 6;
            btnSparts.ImageList = imageList1;
            btnSparts.Location = new Point(-21, -4);
            btnSparts.Margin = new Padding(3, 2, 3, 2);
            btnSparts.Name = "btnSparts";
            btnSparts.Padding = new Padding(52, 0, 0, 0);
            btnSparts.Size = new Size(240, 32);
            btnSparts.TabIndex = 0;
            btnSparts.Text = "            Запчасти";
            btnSparts.TextAlign = ContentAlignment.MiddleLeft;
            btnSparts.UseVisualStyleBackColor = false;
            btnSparts.MouseEnter += ApplyHoverEffect;
            // 
            // panel12
            // 
            panel12.BackColor = Color.Transparent;
            panel12.Controls.Add(btnSsuplier);
            panel12.ForeColor = Color.DodgerBlue;
            panel12.Location = new Point(3, 152);
            panel12.Margin = new Padding(3, 5, 3, 0);
            panel12.Name = "panel12";
            panel12.Size = new Size(211, 23);
            panel12.TabIndex = 7;
            // 
            // btnSsuplier
            // 
            btnSsuplier.BackColor = Color.FromArgb(248, 248, 248);
            btnSsuplier.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSsuplier.ImageAlign = ContentAlignment.MiddleLeft;
            btnSsuplier.ImageIndex = 6;
            btnSsuplier.ImageList = imageList1;
            btnSsuplier.Location = new Point(-21, -4);
            btnSsuplier.Margin = new Padding(3, 2, 3, 2);
            btnSsuplier.Name = "btnSsuplier";
            btnSsuplier.Padding = new Padding(52, 0, 0, 0);
            btnSsuplier.Size = new Size(240, 32);
            btnSsuplier.TabIndex = 0;
            btnSsuplier.Text = "            Поставщики";
            btnSsuplier.TextAlign = ContentAlignment.MiddleLeft;
            btnSsuplier.UseVisualStyleBackColor = false;
            btnSsuplier.MouseEnter += ApplyHoverEffect;
            // 
            // panel15
            // 
            panel15.BackColor = Color.Transparent;
            panel15.Controls.Add(button6);
            panel15.ForeColor = Color.DodgerBlue;
            panel15.Location = new Point(3, 180);
            panel15.Margin = new Padding(3, 5, 3, 0);
            panel15.Name = "panel15";
            panel15.Size = new Size(211, 24);
            panel15.TabIndex = 9;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(248, 248, 248);
            button6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button6.ImageAlign = ContentAlignment.MiddleLeft;
            button6.ImageIndex = 6;
            button6.ImageList = imageList1;
            button6.Location = new Point(-21, -4);
            button6.Margin = new Padding(3, 2, 3, 2);
            button6.Name = "button6";
            button6.Padding = new Padding(52, 0, 0, 0);
            button6.Size = new Size(240, 32);
            button6.TabIndex = 0;
            button6.Text = "            Модель машини";
            button6.TextAlign = ContentAlignment.MiddleLeft;
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click_1;
            button6.MouseEnter += ApplyHoverEffect;
            // 
            // panel13
            // 
            panel13.BackColor = Color.Transparent;
            panel13.Controls.Add(btnSstock);
            panel13.ForeColor = Color.DodgerBlue;
            panel13.Location = new Point(3, 209);
            panel13.Margin = new Padding(3, 5, 3, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(211, 23);
            panel13.TabIndex = 8;
            // 
            // btnSstock
            // 
            btnSstock.BackColor = Color.FromArgb(248, 248, 248);
            btnSstock.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSstock.ImageAlign = ContentAlignment.MiddleLeft;
            btnSstock.ImageIndex = 6;
            btnSstock.ImageList = imageList1;
            btnSstock.Location = new Point(-21, -4);
            btnSstock.Margin = new Padding(3, 2, 3, 2);
            btnSstock.Name = "btnSstock";
            btnSstock.Padding = new Padding(52, 0, 0, 0);
            btnSstock.Size = new Size(240, 32);
            btnSstock.TabIndex = 0;
            btnSstock.Text = "            Склад";
            btnSstock.TextAlign = ContentAlignment.MiddleLeft;
            btnSstock.UseVisualStyleBackColor = false;
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
            flowLayoutPanel5.Location = new Point(3, 234);
            flowLayoutPanel5.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(219, 125);
            flowLayoutPanel5.TabIndex = 9;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.Controls.Add(button1);
            panel3.Location = new Point(3, 2);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(216, 26);
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
            button1.Location = new Point(-13, -7);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Padding = new Padding(44, 0, 0, 0);
            button1.Size = new Size(232, 43);
            button1.TabIndex = 0;
            button1.Text = "           Статуси";
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
            panel4.Location = new Point(3, 30);
            panel4.Margin = new Padding(3, 0, 3, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(211, 22);
            panel4.TabIndex = 7;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(248, 248, 248);
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.ImageIndex = 10;
            button2.ImageList = imageList1;
            button2.Location = new Point(-21, -5);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Padding = new Padding(70, 0, 0, 0);
            button2.Size = new Size(240, 37);
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
            panel5.Location = new Point(3, 52);
            panel5.Margin = new Padding(3, 0, 3, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(211, 22);
            panel5.TabIndex = 8;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(248, 248, 248);
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.ImageIndex = 10;
            button3.ImageList = imageList1;
            button3.Location = new Point(-21, -5);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Padding = new Padding(70, 0, 0, 0);
            button3.Size = new Size(240, 37);
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
            panel6.Location = new Point(3, 74);
            panel6.Margin = new Padding(3, 0, 3, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(211, 22);
            panel6.TabIndex = 9;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(248, 248, 248);
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.ImageIndex = 10;
            button4.ImageList = imageList1;
            button4.Location = new Point(-21, -5);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Padding = new Padding(70, 0, 0, 0);
            button4.Size = new Size(240, 37);
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
            panel14.Location = new Point(3, 96);
            panel14.Margin = new Padding(3, 0, 3, 0);
            panel14.Name = "panel14";
            panel14.Size = new Size(211, 22);
            panel14.TabIndex = 10;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(248, 248, 248);
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button5.ImageAlign = ContentAlignment.MiddleLeft;
            button5.ImageIndex = 10;
            button5.ImageList = imageList1;
            button5.Location = new Point(-21, -5);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Padding = new Padding(70, 0, 0, 0);
            button5.Size = new Size(240, 37);
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
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(206, 0);
            flowLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1035, 34);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.FromArgb(224, 224, 224);
            flowLayoutPanel2.Dock = DockStyle.Bottom;
            flowLayoutPanel2.Location = new Point(206, 552);
            flowLayoutPanel2.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(1035, 33);
            flowLayoutPanel2.TabIndex = 3;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.BackColor = Color.FromArgb(224, 224, 224);
            flowLayoutPanel3.Dock = DockStyle.Right;
            flowLayoutPanel3.Location = new Point(1213, 34);
            flowLayoutPanel3.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(28, 518);
            flowLayoutPanel3.TabIndex = 4;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.BackColor = Color.FromArgb(224, 224, 224);
            flowLayoutPanel4.Dock = DockStyle.Left;
            flowLayoutPanel4.Location = new Point(206, 34);
            flowLayoutPanel4.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(37, 518);
            flowLayoutPanel4.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(224, 224, 224);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(243, 34);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(970, 38);
            panel2.TabIndex = 6;
            // 
            // ContentPanel
            // 
            ContentPanel.CornerRadius = 15;
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.Location = new Point(243, 72);
            ContentPanel.Margin = new Padding(3, 2, 3, 2);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new Size(970, 480);
            ContentPanel.TabIndex = 7;
            ContentPanel.Paint += ContentPanel_Paint;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 248, 248);
            ClientSize = new Size(1241, 585);
            Controls.Add(ContentPanel);
            Controls.Add(panel2);
            Controls.Add(flowLayoutPanel4);
            Controls.Add(flowLayoutPanel3);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(sidebarContainer);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(440, 235);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            sidebarContainer.ResumeLayout(false);
            pnMain.ResumeLayout(false);
            pnIncome.ResumeLayout(false);
            pnServis.ResumeLayout(false);
            pnSell.ResumeLayout(false);
            pnReport.ResumeLayout(false);
            SprContainer.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel11.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel12.ResumeLayout(false);
            panel15.ResumeLayout(false);
            panel13.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel14.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel sidebarContainer;
        private Panel panel1;
        private Button btnMain;
        private Panel pnMain;
        private ImageList imageList1;
        private Panel pnSell;
        private Button btnSell;
        private Panel pnServis;
        private Button btnServis;
        private Panel pnIncome;
        private Button btnIncome;
        private Panel pnReport;
        private Button btnReport;
        private Panel panel7;
        private Button btnSpr;
        private FlowLayoutPanel SprContainer;
        private Panel panel8;
        private Button btnSparts;
        private Panel panel9;
        private Button btnSservis;
        private Panel panel10;
        private Button btnSmadeBy;
        private Panel panel11;
        private Button btnSpartQuality;
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
        private CustomPanel ContentPanel;
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
    }
}