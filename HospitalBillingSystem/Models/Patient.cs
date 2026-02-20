using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalBillingSystem.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string PatientId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        public DateTime DateOfBirth { get; set; }
        
        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;
        
        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string BloodGroup { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string MedicalHistory { get; set; } = string.Empty;
        
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
    }
}
