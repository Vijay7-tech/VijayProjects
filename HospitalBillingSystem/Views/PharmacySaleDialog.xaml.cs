using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;

namespace HospitalBillingSystem.Views
{
    public partial class PharmacySaleDialog : Window
    {
        public PharmacySaleDialog()
        {
            InitializeComponent();
            
            using (var context = new HospitalDbContext())
            {
                cmbPatient.ItemsSource = context.Patients.Where(p => p.IsActive).ToList();
                cmbMedicine.ItemsSource = context.Medicines.Where(m => m.IsActive && m.StockQuantity > 0).ToList();
            }

            txtSaleNumber.Text = GenerateSaleNumber();
        }

        private string GenerateSaleNumber()
        {
            using (var context = new HospitalDbContext())
            {
                int count = context.PharmacySales.Count() + 1;
                return $"SAL{DateTime.Now:yyyyMMdd}{count:D4}";
            }
        }

        private void CmbMedicine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMedicine.SelectedValue != null)
            {
                using (var context = new HospitalDbContext())
                {
                    int medicineId = (int)cmbMedicine.SelectedValue;
                    var medicine = context.Medicines.Find(medicineId);
                    if (medicine != null)
                    {
                        txtUnitPrice.Text = medicine.Price.ToString("N2");
                        CalculateTotal(null, null);
                    }
                }
            }
        }

        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) &&
                int.TryParse(txtQuantity.Text, out int quantity))
            {
                decimal total = unitPrice * quantity;
                txtTotalPrice.Text = $"Total: ₹{total:N2}";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMedicine.SelectedValue == null)
            {
                MessageBox.Show("Please select a medicine.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new HospitalDbContext())
            {
                int medicineId = (int)cmbMedicine.SelectedValue;
                var medicine = context.Medicines.Find(medicineId);

                if (medicine == null)
                {
                    MessageBox.Show("Medicine not found.", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (medicine.StockQuantity < quantity)
                {
                    MessageBox.Show($"Insufficient stock. Available: {medicine.StockQuantity}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var sale = new PharmacySale
                {
                    SaleNumber = txtSaleNumber.Text,
                    PatientId = cmbPatient.SelectedValue as int?,
                    MedicineId = medicineId,
                    Quantity = quantity,
                    UnitPrice = medicine.Price,
                    TotalPrice = medicine.Price * quantity,
                    PaymentMethod = cmbPaymentMethod.Text,
                    Notes = txtNotes.Text
                };

                // Update medicine stock
                medicine.StockQuantity -= quantity;

                context.PharmacySales.Add(sale);
                context.SaveChanges();

                MessageBox.Show("Sale completed successfully!", "Success", 
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
