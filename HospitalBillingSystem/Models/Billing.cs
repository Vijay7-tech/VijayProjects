using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalBillingSystem.Models
{
    public class Billing
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string InvoiceNumber { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(10)]
        public string BillingType { get; set; } = string.Empty; // OP (OutPatient) or IP (InPatient)
        
        public int PatientId { get; set; }
        
        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        
        public int? DoctorId { get; set; }
        
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }
        
        public DateTime BillingDate { get; set; } = DateTime.Now;
        
        public decimal ConsultationCharges { get; set; }
        
        public decimal MedicineCharges { get; set; }
        
        public decimal LabCharges { get; set; }
        
        public decimal RoomCharges { get; set; }
        
        public decimal OtherCharges { get; set; }
        
        public decimal Discount { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Partial
        
        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;
    }
}
