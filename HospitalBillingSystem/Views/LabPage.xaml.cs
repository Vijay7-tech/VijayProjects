using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalBillingSystem.Views
{
    public partial class LabPage : Page
    {
        public LabPage()
        {
            InitializeComponent();
            LoadLabTests();
        }

        private void LoadLabTests()
        {
            using (var context = new HospitalDbContext())
            {
                dgLabTests.ItemsSource = context.LabTests
                    .Include(t => t.Patient)
                    .Include(t => t.Doctor)
                    .OrderByDescending(t => t.TestDate)
                    .ToList();
            }
        }

        private void BtnNewTest_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new LabTestDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadLabTests();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int testId = (int)button.Tag;
                var dialog = new LabTestDialog(testId);
                if (dialog.ShowDialog() == true)
                {
                    LoadLabTests();
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int testId = (int)button.Tag;
                var result = MessageBox.Show("Are you sure you want to delete this test?", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new HospitalDbContext())
                    {
                        var test = context.LabTests.Find(testId);
                        if (test != null)
                        {
                            context.LabTests.Remove(test);
                            context.SaveChanges();
                            LoadLabTests();
                        }
                    }
                }
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLabTests();
        }
    }
}
