using System;
using System.Linq;
using System.Windows;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;

namespace HospitalBillingSystem.Views
{
    public partial class LabTestDialog : Window
    {
        private int? _testId;

        public LabTestDialog(int? testId = null)
        {
            InitializeComponent();
            _testId = testId;

            using (var context = new HospitalDbContext())
            {
                cmbPatient.ItemsSource = context.Patients.Where(p => p.IsActive).ToList();
                cmbDoctor.ItemsSource = context.Doctors.Where(d => d.IsActive).ToList();
            }

            if (_testId.HasValue)
            {
                txtTitle.Text = "Update Lab Test";
                LoadTest(_testId.Value);
            }
            else
            {
                txtTestCode.Text = GenerateTestCode();
            }
        }

        private string GenerateTestCode()
        {
            using (var context = new HospitalDbContext())
            {
                int count = context.LabTests.Count() + 1;
                return $"TEST{count:D5}";
            }
        }

        private void LoadTest(int testId)
        {
            using (var context = new HospitalDbContext())
            {
                var test = context.LabTests.Find(testId);
                if (test != null)
                {
                    txtTestCode.Text = test.TestCode;
                    cmbTestName.Text = test.TestName;
                    cmbPatient.SelectedValue = test.PatientId;
                    cmbDoctor.SelectedValue = test.DoctorId;
                    txtTestCost.Text = test.TestCost.ToString();
                    cmbStatus.Text = test.Status;
                    txtResult.Text = test.Result;
                    txtNotes.Text = test.Notes;
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbTestName.Text))
            {
                MessageBox.Show("Please enter test name.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cmbPatient.SelectedValue == null)
            {
                MessageBox.Show("Please select a patient.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtTestCost.Text, out decimal cost))
            {
                MessageBox.Show("Please enter a valid test cost.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new HospitalDbContext())
            {
                LabTest test;
                if (_testId.HasValue)
                {
                    test = context.LabTests.Find(_testId.Value);
                    if (test == null) return;
                }
                else
                {
                    test = new LabTest();
                    context.LabTests.Add(test);
                }

                test.TestCode = txtTestCode.Text;
                test.TestName = cmbTestName.Text;
                test.PatientId = (int)cmbPatient.SelectedValue;
                test.DoctorId = cmbDoctor.SelectedValue as int?;
                test.TestCost = cost;
                test.Status = cmbStatus.Text;
                test.Result = txtResult.Text;
                test.Notes = txtNotes.Text;

                context.SaveChanges();
                
                MessageBox.Show("Lab test saved successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
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
