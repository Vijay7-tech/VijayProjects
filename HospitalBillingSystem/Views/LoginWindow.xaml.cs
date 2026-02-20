using System.Linq;
using System.Windows;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Utilities;

namespace HospitalBillingSystem.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;
            string role = ((System.Windows.Controls.ComboBoxItem)cmbRole.SelectedItem).Content.ToString() ?? "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                txtError.Text = "Please enter username and password";
                return;
            }

            using (var context = new HospitalDbContext())
            {
                string passwordHash = PasswordHelper.HashPassword(password);
                var user = context.Users.FirstOrDefault(u => 
                    u.Username == username && 
                    u.PasswordHash == passwordHash && 
                    u.Role == role && 
                    u.IsActive);

                if (user != null)
                {
                    // Login successful
                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    txtError.Text = "Invalid username, password, or role";
                }
            }
        }
    }
}
