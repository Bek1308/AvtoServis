using AvtoServis.Forms.Screens;

namespace AvtoServis
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2); // Yuqori DPI uchun
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SignInForm signInForm = new SignInForm();
            //if (signInForm.ShowDialog() == DialogResult.OK)
            //{
                Application.Run(new MainForm()); // MainForm asosiy oyna sifatida ochiladi
            //}
        }
    }
}