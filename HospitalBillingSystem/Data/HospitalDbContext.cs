using Microsoft.EntityFrameworkCore;
using HospitalBillingSystem.Models;
using System.IO;

namespace HospitalBillingSystem.Data
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<PharmacySale> PharmacySales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "hospital.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure precision for decimal properties
            modelBuilder.Entity<Doctor>()
                .Property(d => d.ConsultationFee)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Billing>()
                .Property(b => b.ConsultationCharges)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Billing>()
                .Property(b => b.MedicineCharges)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Billing>()
                .Property(b => b.LabCharges)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Billing>()
                .Property(b => b.RoomCharges)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Billing>()
                .Property(b => b.OtherCharges)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Billing>()
                .Property(b => b.Discount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Billing>()
                .Property(b => b.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Medicine>()
                .Property(m => m.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LabTest>()
                .Property(l => l.TestCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PharmacySale>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PharmacySale>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);
        }
    }
}
