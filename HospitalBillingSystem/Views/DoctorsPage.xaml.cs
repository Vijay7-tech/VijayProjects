using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;

namespace HospitalBillingSystem.Views
{
    public partial class DoctorsPage : Page
    {
        public DoctorsPage()
        {
            InitializeComponent();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            using (var context = new HospitalDbContext())
            {
                dgDoctors.ItemsSource = context.Doctors.Where(d => d.IsActive).ToList();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            using (var context = new HospitalDbContext())
            {
                var doctors = context.Doctors
                    .Where(d => d.IsActive && (
                        d.DoctorId.ToLower().Contains(searchTerm) ||
                        d.FullName.ToLower().Contains(searchTerm) ||
                        d.Specialization.ToLower().Contains(searchTerm)))
                    .ToList();
                dgDoctors.ItemsSource = doctors;
            }
        }

        private void BtnAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DoctorDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadDoctors();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int doctorId = (int)button.Tag;
                var dialog = new DoctorDialog(doctorId);
                if (dialog.ShowDialog() == true)
                {
                    LoadDoctors();
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int doctorId = (int)button.Tag;
                var result = MessageBox.Show("Are you sure you want to delete this doctor?", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new HospitalDbContext())
                    {
                        var doctor = context.Doctors.Find(doctorId);
                        if (doctor != null)
                        {
                            doctor.IsActive = false;
                            context.SaveChanges();
                            LoadDoctors();
                            MessageBox.Show("Doctor deleted successfully!", "Success", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            LoadDoctors();
        }
    }
}
