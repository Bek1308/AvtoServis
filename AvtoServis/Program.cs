using System;
using System.Windows.Forms;
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
            ApplicationConfiguration.Initialize();
            SignInForm signInForm = new SignInForm();
            if (signInForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm()); // MainForm asosiy oyna sifatida ochiladi
            }
        }
    }
}