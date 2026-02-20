using System;
using System.Linq;
using System.Windows.Controls;
using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;

namespace HospitalBillingSystem.Views
{
    public partial class DashboardPage : Page
    {
        public DashboardPage(User currentUser)
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            using (var context = new HospitalDbContext())
            {
                // Total Patients
                txtTotalPatients.Text = context.Patients.Count(p => p.IsActive).ToString();

                // Total Doctors
                txtTotalDoctors.Text = context.Doctors.Count(d => d.IsActive).ToString();

                // Today's Revenue
                var today = DateTime.Today;
                var todayRevenue = context.Billings
                    .Where(b => b.BillingDate.Date == today)
                    .Sum(b => (decimal?)b.TotalAmount) ?? 0;
                txtTodayRevenue.Text = $"₹{todayRevenue:N2}";

                // Pending Tests
                txtPendingTests.Text = context.LabTests.Count(t => t.Status == "Pending").ToString();

                // Low Stock Medicines
                txtLowStockMeds.Text = context.Medicines
                    .Count(m => m.StockQuantity <= m.ReorderLevel && m.IsActive).ToString();

                // OP Billing Today
                txtOPBilling.Text = context.Billings
                    .Count(b => b.BillingDate.Date == today && b.BillingType == "OP").ToString();

                // IP Billing Today
                txtIPBilling.Text = context.Billings
                    .Count(b => b.BillingDate.Date == today && b.BillingType == "IP").ToString();

                // Pharmacy Sales Today
                var pharmacySales = context.PharmacySales
                    .Where(p => p.SaleDate.Date == today)
                    .Sum(p => (decimal?)p.TotalPrice) ?? 0;
                txtPharmacySales.Text = $"₹{pharmacySales:N2}";
            }
        }
    }
}
