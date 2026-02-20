using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;

namespace HospitalBillingSystem.Views
{
    public partial class PharmacyPage : Page
    {
        public PharmacyPage()
        {
            InitializeComponent();
            LoadMedicines();
        }

        private void LoadMedicines()
        {
            using (var context = new HospitalDbContext())
            {
                dgMedicines.ItemsSource = context.Medicines.Where(m => m.IsActive).ToList();
            }
        }

        private void BtnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MedicineDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadMedicines();
            }
        }

        private void BtnNewSale_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PharmacySaleDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadMedicines();
            }
        }

        private void BtnViewSales_Click(object sender, RoutedEventArgs e)
        {
            var window = new PharmacySalesWindow();
            window.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int medicineId = (int)button.Tag;
                var dialog = new MedicineDialog(medicineId);
                if (dialog.ShowDialog() == true)
                {
                    LoadMedicines();
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int medicineId = (int)button.Tag;
                var result = MessageBox.Show("Are you sure you want to delete this medicine?", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new HospitalDbContext())
                    {
                        var medicine = context.Medicines.Find(medicineId);
                        if (medicine != null)
                        {
                            medicine.IsActive = false;
                            context.SaveChanges();
                            LoadMedicines();
                        }
                    }
                }
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadMedicines();
        }
    }
}
