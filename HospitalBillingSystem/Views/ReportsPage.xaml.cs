using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Services;

namespace HospitalBillingSystem.Views
{
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
            dpFromDate.SelectedDate = DateTime.Today.AddMonths(-1);
            dpToDate.SelectedDate = DateTime.Today;
        }

        private void BtnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            var fromDate = dpFromDate.SelectedDate ?? DateTime.Today.AddMonths(-1);
            var toDate = dpToDate.SelectedDate ?? DateTime.Today;
            var reportType = (cmbReportType.SelectedItem as ComboBoxItem)?.Content.ToString();

            using (var context = new HospitalDbContext())
            {
                switch (reportType)
                {
                    case "Billing Summary":
                        var billings = context.Billings
                            .Where(b => b.BillingDate >= fromDate && b.BillingDate <= toDate)
                            .Select(b => new
                            {
                                b.InvoiceNumber,
                                b.BillingType,
                                Patient = b.Patient!.FullName,
                                Doctor = b.Doctor!.FullName,
                                b.BillingDate,
                                b.TotalAmount,
                                b.PaymentStatus
                            })
                            .ToList();
                        dgReport.ItemsSource = billings;
                        txtReportTitle.Text = $"Billing Summary ({fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd})";
                        break;

                    case "Patient List":
                        var patients = context.Patients
                            .Where(p => p.IsActive)
                            .Select(p => new
                            {
                                p.PatientId,
                                p.FullName,
                                p.Gender,
                                p.Phone,
                                p.Email,
                                p.BloodGroup,
                                p.RegistrationDate
                            })
                            .ToList();
                        dgReport.ItemsSource = patients;
                        txtReportTitle.Text = "Active Patients List";
                        break;

                    case "Pharmacy Sales":
                        var sales = context.PharmacySales
                            .Where(s => s.SaleDate >= fromDate && s.SaleDate <= toDate)
                            .Select(s => new
                            {
                                s.SaleNumber,
                                Medicine = s.Medicine!.Name,
                                s.Quantity,
                                s.UnitPrice,
                                s.TotalPrice,
                                s.SaleDate
                            })
                            .ToList();
                        dgReport.ItemsSource = sales;
                        txtReportTitle.Text = $"Pharmacy Sales ({fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd})";
                        break;

                    case "Lab Tests":
                        var tests = context.LabTests
                            .Where(t => t.TestDate >= fromDate && t.TestDate <= toDate)
                            .Select(t => new
                            {
                                t.TestCode,
                                t.TestName,
                                Patient = t.Patient!.FullName,
                                t.TestDate,
                                t.TestCost,
                                t.Status
                            })
                            .ToList();
                        dgReport.ItemsSource = tests;
                        txtReportTitle.Text = $"Lab Tests ({fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd})";
                        break;

                    case "Revenue Report":
                        var revenue = new[]
                        {
                            new { Category = "Billing Revenue", Amount = context.Billings.Where(b => b.BillingDate >= fromDate && b.BillingDate <= toDate).Sum(b => (decimal?)b.TotalAmount) ?? 0 },
                            new { Category = "Pharmacy Sales", Amount = context.PharmacySales.Where(s => s.SaleDate >= fromDate && s.SaleDate <= toDate).Sum(s => (decimal?)s.TotalPrice) ?? 0 },
                            new { Category = "Lab Tests", Amount = context.LabTests.Where(t => t.TestDate >= fromDate && t.TestDate <= toDate).Sum(t => (decimal?)t.TestCost) ?? 0 }
                        };
                        dgReport.ItemsSource = revenue.ToList();
                        txtReportTitle.Text = $"Revenue Report ({fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd})";
                        break;
                }
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            if (dgReport.ItemsSource == null)
            {
                MessageBox.Show("Please generate a report first.", "No Data", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var excelService = new ExcelExportService();
                excelService.ExportToExcel(dgReport.ItemsSource, txtReportTitle.Text);
                MessageBox.Show("Report exported to Excel successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnExportPDF_Click(object sender, RoutedEventArgs e)
        {
            if (dgReport.ItemsSource == null)
            {
                MessageBox.Show("Please generate a report first.", "No Data", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var pdfService = new PdfExportService();
                pdfService.ExportReportToPDF(dgReport.ItemsSource, txtReportTitle.Text);
                MessageBox.Show("Report exported to PDF successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
