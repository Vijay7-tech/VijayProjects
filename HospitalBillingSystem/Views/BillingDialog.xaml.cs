using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalBillingSystem.Views
{
    public partial class BillingDialog : Window
    {
        private string _billingType;
        private int? _billingId;
        private bool _isViewMode;

        public BillingDialog(string billingType)
        {
            InitializeComponent();
            _billingType = billingType;
            _isViewMode = false;
            Initialize();
        }

        public BillingDialog(int billingId)
        {
            InitializeComponent();
            _billingId = billingId;
            _isViewMode = true;
            Initialize();
            LoadBilling(billingId);
        }

        private void Initialize()
        {
            using (var context = new HospitalDbContext())
            {
                cmbPatient.ItemsSource = context.Patients.Where(p => p.IsActive).ToList();
                cmbDoctor.ItemsSource = context.Doctors.Where(d => d.IsActive).ToList();
            }

            if (!_isViewMode)
            {
                txtTitle.Text = $"New {_billingType} Billing";
                txtBillingType.Text = $"Type: {_billingType}";
                txtInvoiceNumber.Text = GenerateInvoiceNumber();
                
                if (_billingType == "OP")
                {
                    txtRoomCharges.IsEnabled = false;
                }
            }
            else
            {
                txtTitle.Text = "View Billing";
                btnSave.Visibility = Visibility.Collapsed;
                DisableAllControls();
            }
        }

        private void LoadBilling(int billingId)
        {
            using (var context = new HospitalDbContext())
            {
                var billing = context.Billings
                    .Include(b => b.Patient)
                    .Include(b => b.Doctor)
                    .FirstOrDefault(b => b.Id == billingId);

                if (billing != null)
                {
                    _billingType = billing.BillingType;
                    txtBillingType.Text = $"Type: {billing.BillingType}";
                    txtInvoiceNumber.Text = billing.InvoiceNumber;
                    cmbPatient.SelectedValue = billing.PatientId;
                    cmbDoctor.SelectedValue = billing.DoctorId;
                    txtConsultationCharges.Text = billing.ConsultationCharges.ToString();
                    txtMedicineCharges.Text = billing.MedicineCharges.ToString();
                    txtLabCharges.Text = billing.LabCharges.ToString();
                    txtRoomCharges.Text = billing.RoomCharges.ToString();
                    txtOtherCharges.Text = billing.OtherCharges.ToString();
                    txtDiscount.Text = billing.Discount.ToString();
                    cmbPaymentMethod.Text = billing.PaymentMethod;
                    cmbPaymentStatus.Text = billing.PaymentStatus;
                    txtNotes.Text = billing.Notes;
                    CalculateTotal(null, null);
                }
            }
        }

        private void DisableAllControls()
        {
            txtInvoiceNumber.IsEnabled = false;
            cmbPatient.IsEnabled = false;
            cmbDoctor.IsEnabled = false;
            txtConsultationCharges.IsEnabled = false;
            txtMedicineCharges.IsEnabled = false;
            txtLabCharges.IsEnabled = false;
            txtRoomCharges.IsEnabled = false;
            txtOtherCharges.IsEnabled = false;
            txtDiscount.IsEnabled = false;
            cmbPaymentMethod.IsEnabled = false;
            cmbPaymentStatus.IsEnabled = false;
            txtNotes.IsEnabled = false;
        }

        private string GenerateInvoiceNumber()
        {
            using (var context = new HospitalDbContext())
            {
                int count = context.Billings.Count() + 1;
                return $"INV{DateTime.Now:yyyyMMdd}{count:D4}";
            }
        }

        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            decimal consultation = decimal.TryParse(txtConsultationCharges.Text, out decimal c) ? c : 0;
            decimal medicine = decimal.TryParse(txtMedicineCharges.Text, out decimal m) ? m : 0;
            decimal lab = decimal.TryParse(txtLabCharges.Text, out decimal l) ? l : 0;
            decimal room = decimal.TryParse(txtRoomCharges.Text, out decimal r) ? r : 0;
            decimal other = decimal.TryParse(txtOtherCharges.Text, out decimal o) ? o : 0;
            decimal discount = decimal.TryParse(txtDiscount.Text, out decimal d) ? d : 0;

            decimal total = consultation + medicine + lab + room + other - discount;
            txtTotalAmount.Text = $"Total Amount: ₹{total:N2}";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPatient.SelectedValue == null)
            {
                MessageBox.Show("Please select a patient.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new HospitalDbContext())
            {
                var billing = new Billing
                {
                    InvoiceNumber = txtInvoiceNumber.Text,
                    BillingType = _billingType,
                    PatientId = (int)cmbPatient.SelectedValue,
                    DoctorId = cmbDoctor.SelectedValue as int?,
                    ConsultationCharges = decimal.Parse(txtConsultationCharges.Text),
                    MedicineCharges = decimal.Parse(txtMedicineCharges.Text),
                    LabCharges = decimal.Parse(txtLabCharges.Text),
                    RoomCharges = decimal.Parse(txtRoomCharges.Text),
                    OtherCharges = decimal.Parse(txtOtherCharges.Text),
                    Discount = decimal.Parse(txtDiscount.Text),
                    TotalAmount = decimal.Parse(txtConsultationCharges.Text) + 
                                 decimal.Parse(txtMedicineCharges.Text) +
                                 decimal.Parse(txtLabCharges.Text) +
                                 decimal.Parse(txtRoomCharges.Text) +
                                 decimal.Parse(txtOtherCharges.Text) -
                                 decimal.Parse(txtDiscount.Text),
                    PaymentMethod = cmbPaymentMethod.Text,
                    PaymentStatus = cmbPaymentStatus.Text,
                    Notes = txtNotes.Text
                };

                context.Billings.Add(billing);
                context.SaveChanges();
                
                MessageBox.Show("Billing saved successfully!", "Success", 
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
