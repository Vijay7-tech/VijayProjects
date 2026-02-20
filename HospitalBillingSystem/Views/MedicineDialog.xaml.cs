using System;
using System.Linq;
using System.Windows;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;

namespace HospitalBillingSystem.Views
{
    public partial class MedicineDialog : Window
    {
        private int? _medicineId;

        public MedicineDialog(int? medicineId = null)
        {
            InitializeComponent();
            _medicineId = medicineId;

            if (_medicineId.HasValue)
            {
                txtTitle.Text = "Edit Medicine";
                LoadMedicine(_medicineId.Value);
            }
            else
            {
                txtMedicineCode.Text = GenerateMedicineCode();
                dpExpiryDate.SelectedDate = DateTime.Today.AddYears(2);
            }
        }

        private string GenerateMedicineCode()
        {
            using (var context = new HospitalDbContext())
            {
                int count = context.Medicines.Count() + 1;
                return $"MED{count:D5}";
            }
        }

        private void LoadMedicine(int medicineId)
        {
            using (var context = new HospitalDbContext())
            {
                var medicine = context.Medicines.Find(medicineId);
                if (medicine != null)
                {
                    txtMedicineCode.Text = medicine.MedicineCode;
                    txtName.Text = medicine.Name;
                    txtGenericName.Text = medicine.GenericName;
                    txtManufacturer.Text = medicine.Manufacturer;
                    txtCategory.Text = medicine.Category;
                    txtPrice.Text = medicine.Price.ToString();
                    txtStockQuantity.Text = medicine.StockQuantity.ToString();
                    txtReorderLevel.Text = medicine.ReorderLevel.ToString();
                    dpExpiryDate.SelectedDate = medicine.ExpiryDate;
                    txtDescription.Text = medicine.Description;
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter medicine name.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtStockQuantity.Text, out int stock))
            {
                MessageBox.Show("Please enter a valid stock quantity.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new HospitalDbContext())
            {
                Medicine medicine;
                if (_medicineId.HasValue)
                {
                    medicine = context.Medicines.Find(_medicineId.Value);
                    if (medicine == null) return;
                }
                else
                {
                    medicine = new Medicine();
                    context.Medicines.Add(medicine);
                }

                medicine.MedicineCode = txtMedicineCode.Text;
                medicine.Name = txtName.Text;
                medicine.GenericName = txtGenericName.Text;
                medicine.Manufacturer = txtManufacturer.Text;
                medicine.Category = txtCategory.Text;
                medicine.Price = price;
                medicine.StockQuantity = stock;
                medicine.ReorderLevel = int.Parse(txtReorderLevel.Text);
                medicine.ExpiryDate = dpExpiryDate.SelectedDate ?? DateTime.Today.AddYears(2);
                medicine.Description = txtDescription.Text;

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
