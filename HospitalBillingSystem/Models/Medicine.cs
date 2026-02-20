using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalBillingSystem.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string MedicineCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string GenericName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string Manufacturer { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; }
        
        public int ReorderLevel { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
    }
}
