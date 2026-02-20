using System.Windows;
using HospitalBillingSystem.Services;
using HospitalBillingSystem.Views;

namespace HospitalBillingSystem
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Initialize database
            DatabaseInitializer.Initialize();

            // Show login window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}

