using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalBillingSystem.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string DoctorId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string Specialization { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Qualification { get; set; } = string.Empty;
        
        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        public decimal ConsultationFee { get; set; }
        
        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;
        
        public DateTime JoinDate { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
    }
}
