using System.Linq;
using System.Windows;
using HospitalBillingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalBillingSystem.Views
{
    public partial class PharmacySalesWindow : Window
    {
        public PharmacySalesWindow()
        {
            InitializeComponent();
            LoadSales();
        }

        private void LoadSales()
        {
            using (var context = new HospitalDbContext())
            {
                dgSales.ItemsSource = context.PharmacySales
                    .Include(s => s.Patient)
                    .Include(s => s.Medicine)
                    .OrderByDescending(s => s.SaleDate)
                    .ToList();
            }
        }
    }
}
