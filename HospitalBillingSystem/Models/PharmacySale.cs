using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalBillingSystem.Models
{
    public class PharmacySale
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string SaleNumber { get; set; } = string.Empty;
        
        public int? PatientId { get; set; }
        
        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        
        public int MedicineId { get; set; }
        
        [ForeignKey("MedicineId")]
        public Medicine? Medicine { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public DateTime SaleDate { get; set; } = DateTime.Now;
        
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Notes { get; set; } = string.Empty;
    }
}
