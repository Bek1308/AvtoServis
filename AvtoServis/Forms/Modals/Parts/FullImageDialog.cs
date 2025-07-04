namespace AvtoServis.Forms.Controls
{
    public partial class FullImageDialog : Form
    {
        private readonly string _imagePath;

        public FullImageDialog(string imagePath)
        {
            _imagePath = imagePath ?? throw new ArgumentNullException(nameof(imagePath));
            InitializeComponent();
            LoadImage();
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(pictureBox, "Полноэкранное изображение детали");
            toolTip.SetToolTip(btnClose, "Закрыть окно");
        }

        private void LoadImage()
        {
            try
            {
                if (!File.Exists(_imagePath))
                {
                    MessageBox.Show("Изображение не найдено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                using (var img = Image.FromFile(_imagePath))
                {
                    pictureBox.Image = new Bitmap(img);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadImage Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                Close();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}