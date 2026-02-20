using System;
using System.Linq;
using System.Windows;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;

namespace HospitalBillingSystem.Views
{
    public partial class DoctorDialog : Window
    {
        private int? _doctorId;

        public DoctorDialog(int? doctorId = null)
        {
            InitializeComponent();
            _doctorId = doctorId;

            if (_doctorId.HasValue)
            {
                txtTitle.Text = "Edit Doctor";
                LoadDoctor(_doctorId.Value);
            }
            else
            {
                txtDoctorId.Text = GenerateDoctorId();
            }
        }

        private string GenerateDoctorId()
        {
            using (var context = new HospitalDbContext())
            {
                int count = context.Doctors.Count() + 1;
                return $"DOC{count:D5}";
            }
        }

        private void LoadDoctor(int doctorId)
        {
            using (var context = new HospitalDbContext())
            {
                var doctor = context.Doctors.Find(doctorId);
                if (doctor != null)
                {
                    txtDoctorId.Text = doctor.DoctorId;
                    txtFullName.Text = doctor.FullName;
                    txtSpecialization.Text = doctor.Specialization;
                    txtQualification.Text = doctor.Qualification;
                    txtPhone.Text = doctor.Phone;
                    txtEmail.Text = doctor.Email;
                    txtConsultationFee.Text = doctor.ConsultationFee.ToString();
                    txtDepartment.Text = doctor.Department;
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter doctor name.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtConsultationFee.Text, out decimal fee))
            {
                MessageBox.Show("Please enter a valid consultation fee.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new HospitalDbContext())
            {
                Doctor doctor;
                if (_doctorId.HasValue)
                {
                    doctor = context.Doctors.Find(_doctorId.Value);
                    if (doctor == null) return;
                }
                else
                {
                    doctor = new Doctor();
                    context.Doctors.Add(doctor);
                }

                doctor.DoctorId = txtDoctorId.Text;
                doctor.FullName = txtFullName.Text;
                doctor.Specialization = txtSpecialization.Text;
                doctor.Qualification = txtQualification.Text;
                doctor.Phone = txtPhone.Text;
                doctor.Email = txtEmail.Text;
                doctor.ConsultationFee = fee;
                doctor.Department = txtDepartment.Text;

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
