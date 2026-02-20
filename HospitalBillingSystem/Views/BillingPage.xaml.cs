using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalBillingSystem.Views
{
    public partial class BillingPage : Page
    {
        public BillingPage()
        {
            InitializeComponent();
            LoadBillings();
        }

        private void LoadBillings()
        {
            using (var context = new HospitalDbContext())
            {
                dgBillings.ItemsSource = context.Billings
                    .Include(b => b.Patient)
                    .Include(b => b.Doctor)
                    .OrderByDescending(b => b.BillingDate)
                    .ToList();
            }
        }

        private void BtnNewOP_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BillingDialog("OP");
            if (dialog.ShowDialog() == true)
            {
                LoadBillings();
            }
        }

        private void BtnNewIP_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BillingDialog("IP");
            if (dialog.ShowDialog() == true)
            {
                LoadBillings();
            }
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int billingId = (int)button.Tag;
                var dialog = new BillingDialog(billingId);
                dialog.ShowDialog();
            }
        }

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int billingId = (int)button.Tag;
                ExportInvoiceToPDF(billingId);
            }
        }

        private void ExportInvoiceToPDF(int billingId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var billing = context.Billings
                        .Include(b => b.Patient)
                        .Include(b => b.Doctor)
                        .FirstOrDefault(b => b.Id == billingId);

                    if (billing != null)
                    {
                        var pdfService = new Services.PdfExportService();
                        pdfService.ExportInvoiceToPDF(billing);
                        MessageBox.Show("Invoice exported to PDF successfully!", "Success", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error exporting PDF: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadBillings();
        }
    }
}
