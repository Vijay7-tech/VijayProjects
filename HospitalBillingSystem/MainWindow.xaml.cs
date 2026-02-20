using System.Windows;
using HospitalBillingSystem.Models;
using HospitalBillingSystem.Views;

namespace HospitalBillingSystem
{
    public partial class MainWindow : Window
    {
        private User _currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            txtWelcome.Text = $"Welcome, {user.FullName} ({user.Role})";
            
            // Set menu visibility based on role
            ConfigureMenuForRole(user.Role);
            
            // Load dashboard by default
            MainFrame.Navigate(new DashboardPage(_currentUser));
        }

        private void ConfigureMenuForRole(string role)
        {
            // All roles can see dashboard
            btnDashboard.Visibility = Visibility.Visible;
            
            // Configure visibility based on role
            switch (role)
            {
                case "Admin":
                    // Admin can see everything
                    break;
                case "Doctor":
                    btnPharmacy.Visibility = Visibility.Collapsed;
                    break;
                case "Receptionist":
                    // Receptionist handles patients and billing
                    btnPharmacy.Visibility = Visibility.Collapsed;
                    btnLab.Visibility = Visibility.Collapsed;
                    break;
                case "Pharmacist":
                    // Pharmacist only sees pharmacy
                    btnPatients.Visibility = Visibility.Collapsed;
                    btnDoctors.Visibility = Visibility.Collapsed;
                    btnBilling.Visibility = Visibility.Collapsed;
                    btnLab.Visibility = Visibility.Collapsed;
                    break;
                case "LabTechnician":
                    // Lab technician only sees lab
                    btnPatients.Visibility = Visibility.Collapsed;
                    btnDoctors.Visibility = Visibility.Collapsed;
                    btnBilling.Visibility = Visibility.Collapsed;
                    btnPharmacy.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashboardPage(_currentUser));
        }

        private void BtnPatients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PatientsPage());
        }

        private void BtnDoctors_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DoctorsPage());
        }

        private void BtnBilling_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BillingPage());
        }

        private void BtnPharmacy_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PharmacyPage());
        }

        private void BtnLab_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LabPage());
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReportsPage());
        }
    }
}