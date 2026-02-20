using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;

namespace HospitalBillingSystem.Views
{
    public partial class PatientDialog : Window
    {
        private int? _patientId;

        public PatientDialog(int? patientId = null)
        {
            InitializeComponent();
            _patientId = patientId;

            if (_patientId.HasValue)
            {
                txtTitle.Text = "Edit Patient";
                LoadPatient(_patientId.Value);
            }
            else
            {
                txtPatientId.Text = GeneratePatientId();
            }
        }

        private string GeneratePatientId()
        {
            using (var context = new HospitalDbContext())
            {
                int count = context.Patients.Count() + 1;
                return $"PAT{count:D5}";
            }
        }

        private void LoadPatient(int patientId)
        {
            using (var context = new HospitalDbContext())
            {
                var patient = context.Patients.Find(patientId);
                if (patient != null)
                {
                    txtPatientId.Text = patient.PatientId;
                    txtFullName.Text = patient.FullName;
                    dpDateOfBirth.SelectedDate = patient.DateOfBirth;
                    cmbGender.Text = patient.Gender;
                    txtPhone.Text = patient.Phone;
                    txtEmail.Text = patient.Email;
                    txtAddress.Text = patient.Address;
                    cmbBloodGroup.Text = patient.BloodGroup;
                    txtMedicalHistory.Text = patient.MedicalHistory;
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter patient name.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!dpDateOfBirth.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select date of birth.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new HospitalDbContext())
            {
                Patient patient;
                if (_patientId.HasValue)
                {
                    patient = context.Patients.Find(_patientId.Value);
                    if (patient == null) return;
                }
                else
                {
                    patient = new Patient();
                    context.Patients.Add(patient);
                }

                patient.PatientId = txtPatientId.Text;
                patient.FullName = txtFullName.Text;
                patient.DateOfBirth = dpDateOfBirth.SelectedDate.Value;
                patient.Gender = cmbGender.Text;
                patient.Phone = txtPhone.Text;
                patient.Email = txtEmail.Text;
                patient.Address = txtAddress.Text;
                patient.BloodGroup = cmbBloodGroup.Text;
                patient.MedicalHistory = txtMedicalHistory.Text;

                context.SaveChanges();
                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
