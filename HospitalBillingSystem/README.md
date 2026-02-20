# Hospital Billing System - Complete C# WPF Application

A comprehensive hospital management and billing software built with C# WPF, Entity Framework Core, and SQLite.

## Features

### User Management & Authentication
- **Role-based Login System** with 5 user roles:
  - Admin (Full access)
  - Doctor
  - Receptionist
  - Pharmacist
  - Lab Technician
- Secure password hashing using SHA256
- Role-based menu visibility

### Patient Management
- Complete CRUD operations for patient records
- Patient registration with:
  - Demographics (Name, DOB, Gender, Contact)
  - Medical history
  - Blood group
  - Auto-generated Patient IDs
- Search functionality
- Soft delete (mark as inactive)

### Doctor Management
- Doctor profile management
- Specialization and qualification tracking
- Consultation fee management
- Department assignment
- Auto-generated Doctor IDs

### Billing Module (OP/IP)
- **Outpatient (OP) Billing**: Quick consultation billing
- **Inpatient (IP) Billing**: Comprehensive billing with room charges
- Invoice generation with auto-generated numbers
- Itemized billing:
  - Consultation charges
  - Medicine charges
  - Lab charges
  - Room charges (IP only)
  - Other charges
  - Discount support
- Multiple payment methods (Cash, Card, UPI, Insurance)
- Payment status tracking (Paid, Pending, Partial)
- **PDF Invoice Export** - Generate professional PDF invoices

### Pharmacy Management
- Medicine inventory management
- Stock tracking with reorder levels
- Low stock alerts on dashboard
- Medicine expiry date tracking
- Point of Sale (POS) system
- Automatic stock updates on sale
- Sales history tracking
- Multiple payment methods

### Laboratory Management
- Lab test ordering system
- Pre-defined common tests (CBC, Blood Sugar, etc.)
- Custom test support
- Test status tracking (Pending, Completed, Cancelled)
- Result entry
- Cost tracking
- Doctor and patient linking

### Reports & Analytics
- **Dashboard** with key metrics:
  - Total patients and doctors
  - Today's revenue
  - Pending tests
  - Low stock medicines
  - OP/IP billing counts
  - Pharmacy sales
- **Multiple Report Types**:
  - Billing Summary
  - Patient List
  - Doctor Performance
  - Pharmacy Sales
  - Lab Tests
  - Revenue Report
- Date range filtering
- **Excel Export** - Export reports to Excel
- **PDF Export** - Export reports to PDF

## Technology Stack

- **Framework**: .NET 8.0 (C#)
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Database**: SQLite
- **ORM**: Entity Framework Core 8.0
- **PDF Generation**: iTextSharp.LGPLv2.Core 3.4.23
- **Excel Export**: ClosedXML 0.104.2

## Prerequisites

- .NET 8.0 SDK or later
- Windows OS (WPF is Windows-only)
- Visual Studio 2022 or Visual Studio Code (optional)

## Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/Vijay7-tech/VijayProjects.git
cd VijayProjects/HospitalBillingSystem
```

### 2. Restore NuGet Packages
```bash
dotnet restore
```

### 3. Build the Project
```bash
dotnet build
```

### 4. Run the Application
```bash
dotnet run
```

Or build and run in release mode:
```bash
dotnet build --configuration Release
cd bin/Release/net8.0-windows
./HospitalBillingSystem.exe
```

## Default Login Credentials

The system comes with pre-seeded user accounts for testing:

| Username | Password | Role |
|----------|----------|------|
| admin | admin123 | Admin |
| doctor1 | doctor123 | Doctor |
| receptionist | recep123 | Receptionist |
| pharmacist | pharma123 | Pharmacist |
| labtech | lab123 | LabTechnician |

**Note**: Change these credentials in production!

## Database

- Database file: `hospital.db` (created automatically in the application directory)
- The database is auto-created on first run with seeded data
- Uses SQLite for portability and ease of deployment

### Database Schema

The application includes the following entities:
- **Users** - Authentication and role management
- **Patients** - Patient records
- **Doctors** - Doctor profiles
- **Billing** - Invoice and billing records
- **Medicines** - Pharmacy inventory
- **LabTests** - Laboratory test orders
- **PharmacySales** - Pharmacy sales transactions

## Project Structure

```
HospitalBillingSystem/
├── Models/              # Entity models
│   ├── User.cs
│   ├── Patient.cs
│   ├── Doctor.cs
│   ├── Billing.cs
│   ├── Medicine.cs
│   ├── LabTest.cs
│   └── PharmacySale.cs
├── Data/                # Database context
│   └── HospitalDbContext.cs
├── Views/               # WPF windows and pages
│   ├── LoginWindow.xaml
│   ├── DashboardPage.xaml
│   ├── PatientsPage.xaml
│   ├── DoctorsPage.xaml
│   ├── BillingPage.xaml
│   ├── PharmacyPage.xaml
│   ├── LabPage.xaml
│   ├── ReportsPage.xaml
│   └── ... (dialogs)
├── Services/            # Business logic and utilities
│   ├── DatabaseInitializer.cs
│   ├── PdfExportService.cs
│   └── ExcelExportService.cs
├── Utilities/           # Helper classes
│   └── PasswordHelper.cs
└── MainWindow.xaml      # Main dashboard
```

## Features by User Role

### Admin
- Full access to all modules
- User management
- View all reports
- System configuration

### Doctor
- View patients
- View doctors
- Create billing
- View lab tests
- View reports

### Receptionist
- Patient management
- Doctor management
- Billing (OP/IP)
- View reports

### Pharmacist
- Pharmacy management only
- Medicine inventory
- Sales management
- View pharmacy reports

### Lab Technician
- Laboratory management only
- Test ordering
- Result entry
- View lab reports

## Exported Files Location

All exported files (PDF invoices, Excel reports) are saved to:
- **Windows**: `C:\Users\[Username]\Documents\`

## Customization

### Changing Hospital Name
Edit `Services/PdfExportService.cs` and update the hospital name in the PDF generation methods.

### Adding New Tests
Edit `Views/LabTestDialog.xaml` and add items to the test name ComboBox.

### Modifying Reports
Edit `Views/ReportsPage.xaml.cs` to customize report queries and data.

## Troubleshooting

### Database Not Created
- Ensure write permissions in the application directory
- Check if SQLite package is properly installed

### Login Fails
- Verify the database was created successfully
- Check that default users were seeded
- Ensure role selection matches the user's role

### Export Fails
- Ensure Documents folder has write permissions
- Check that PDF/Excel packages are installed
- Verify file is not open in another program

## Development

### Adding a New Module
1. Create model in `Models/`
2. Add DbSet to `HospitalDbContext.cs`
3. Create migration (if using migrations)
4. Create page in `Views/`
5. Add menu item in `MainWindow.xaml`
6. Add navigation handler in `MainWindow.xaml.cs`

### Building for Production
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

This creates a standalone executable in `bin/Release/net8.0-windows/win-x64/publish/`

## Security Notes

- Passwords are hashed using SHA256 (consider bcrypt for production)
- No password complexity enforcement (implement for production)
- Default credentials should be changed
- SQL injection protected by Entity Framework
- Implement HTTPS if deploying as web application

## License

This is a demonstration project. Modify as needed for your use case.

## Support

For issues and questions, please create an issue in the GitHub repository.

## Future Enhancements

- Appointment scheduling
- Email notifications
- SMS integration
- Backup and restore
- Barcode scanning for medicines
- Insurance claim management
- Multi-branch support
- Web-based access (ASP.NET Core)
- Mobile app integration

---

**Version**: 1.0.0  
**Last Updated**: 2026-02-20
