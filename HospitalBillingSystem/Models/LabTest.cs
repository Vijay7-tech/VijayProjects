using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalBillingSystem.Models
{
    public class LabTest
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string TestCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string TestName { get; set; } = string.Empty;
        
        public int PatientId { get; set; }
        
        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        
        public int? DoctorId { get; set; }
        
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }
        
        public DateTime TestDate { get; set; } = DateTime.Now;
        
        public decimal TestCost { get; set; }
        
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled
        
        [MaxLength(500)]
        public string Result { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Notes { get; set; } = string.Empty;
    }
}
