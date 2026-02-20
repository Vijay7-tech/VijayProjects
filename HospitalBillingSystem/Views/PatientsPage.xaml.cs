using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalBillingSystem.Views
{
    public partial class PatientsPage : Page
    {
        public PatientsPage()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            using (var context = new HospitalDbContext())
            {
                dgPatients.ItemsSource = context.Patients.Where(p => p.IsActive).ToList();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            using (var context = new HospitalDbContext())
            {
                var patients = context.Patients
                    .Where(p => p.IsActive && (
                        p.PatientId.ToLower().Contains(searchTerm) ||
                        p.FullName.ToLower().Contains(searchTerm) ||
                        p.Phone.Contains(searchTerm)))
                    .ToList();
                dgPatients.ItemsSource = patients;
            }
        }

        private void BtnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PatientDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadPatients();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int patientId = (int)button.Tag;
                var dialog = new PatientDialog(patientId);
                if (dialog.ShowDialog() == true)
                {
                    LoadPatients();
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int patientId = (int)button.Tag;
                var result = MessageBox.Show("Are you sure you want to delete this patient?", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new HospitalDbContext())
                    {
                        var patient = context.Patients.Find(patientId);
                        if (patient != null)
                        {
                            patient.IsActive = false;
                            context.SaveChanges();
                            LoadPatients();
                            MessageBox.Show("Patient deleted successfully!", "Success", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            LoadPatients();
        }
    }
}
