using HospitalBillingSystem.Data;
using HospitalBillingSystem.Models;
using HospitalBillingSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HospitalBillingSystem.Services
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using (var context = new HospitalDbContext())
            {
                // Create database if it doesn't exist
                context.Database.EnsureCreated();

                // Check if we already have users
                if (context.Users.Any())
                {
                    return; // Database has been seeded
                }

                // Seed default users
                var users = new[]
                {
                    new User
                    {
                        Username = "admin",
                        PasswordHash = PasswordHelper.HashPassword("admin123"),
                        Role = "Admin",
                        FullName = "System Administrator",
                        Email = "admin@hospital.com",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "doctor1",
                        PasswordHash = PasswordHelper.HashPassword("doctor123"),
                        Role = "Doctor",
                        FullName = "Dr. John Smith",
                        Email = "doctor@hospital.com",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "receptionist",
                        PasswordHash = PasswordHelper.HashPassword("recep123"),
                        Role = "Receptionist",
                        FullName = "Jane Doe",
                        Email = "reception@hospital.com",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "pharmacist",
                        PasswordHash = PasswordHelper.HashPassword("pharma123"),
                        Role = "Pharmacist",
                        FullName = "Bob Johnson",
                        Email = "pharmacy@hospital.com",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "labtech",
                        PasswordHash = PasswordHelper.HashPassword("lab123"),
                        Role = "LabTechnician",
                        FullName = "Alice Williams",
                        Email = "lab@hospital.com",
                        IsActive = true
                    }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
