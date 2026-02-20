# Hospital Billing System - Build Instructions

## Quick Start Guide

### Step 1: Prerequisites

Ensure you have the following installed on your Windows machine:

1. **.NET 8.0 SDK** or later
   - Download from: https://dotnet.microsoft.com/download/dotnet/8.0
   - Verify installation: Open Command Prompt and run `dotnet --version`

2. **Git** (to clone the repository)
   - Download from: https://git-scm.com/downloads

3. **Visual Studio 2022** (Optional, but recommended)
   - Download Community Edition: https://visualstudio.microsoft.com/downloads/
   - Select ".NET desktop development" workload during installation

### Step 2: Clone the Repository

```bash
git clone https://github.com/Vijay7-tech/VijayProjects.git
cd VijayProjects/HospitalBillingSystem
```

### Step 3: Restore Dependencies

```bash
dotnet restore
```

This command downloads all required NuGet packages:
- Microsoft.EntityFrameworkCore.Sqlite (8.0.0)
- iTextSharp.LGPLv2.Core (3.4.23)
- ClosedXML (0.104.2)
- Microsoft.EntityFrameworkCore.Tools (8.0.0)

### Step 4: Build the Project

#### Option A: Using Command Line
```bash
dotnet build --configuration Release
```

#### Option B: Using Visual Studio
1. Open `HospitalBillingSystem.sln` (or the folder)
2. Press `Ctrl + Shift + B` or go to Build → Build Solution
3. Select "Release" configuration from the toolbar

### Step 5: Run the Application

#### Option A: Using Command Line
```bash
dotnet run
```

Or navigate to the build output:
```bash
cd bin/Release/net8.0-windows
./HospitalBillingSystem.exe
```

#### Option B: Using Visual Studio
1. Press `F5` or click the "Start" button
2. Or press `Ctrl + F5` to run without debugging

### Step 6: First Login

When the application starts:
1. The database `hospital.db` will be automatically created
2. Default user accounts will be seeded
3. Use these credentials to log in:

**Admin Account:**
- Username: `admin`
- Password: `admin123`
- Role: `Admin`

## Publishing for Distribution

To create a standalone executable that can run on any Windows machine without .NET SDK:

### Self-Contained Deployment

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

This creates a single EXE file in:
```
bin/Release/net8.0-windows/win-x64/publish/HospitalBillingSystem.exe
```

### Framework-Dependent Deployment (Smaller Size)

```bash
dotnet publish -c Release -r win-x64 --self-contained false
```

Recipients will need .NET 8.0 Runtime installed.

## Project Structure Overview

```
HospitalBillingSystem/
│
├── Models/                      # Database entities
│   ├── User.cs                  # User authentication
│   ├── Patient.cs               # Patient records
│   ├── Doctor.cs                # Doctor profiles
│   ├── Billing.cs               # Billing/invoices
│   ├── Medicine.cs              # Pharmacy inventory
│   ├── LabTest.cs               # Lab test orders
│   └── PharmacySale.cs          # Pharmacy transactions
│
├── Data/
│   └── HospitalDbContext.cs     # EF Core database context
│
├── Views/                       # WPF user interface
│   ├── LoginWindow.xaml         # Login screen
│   ├── DashboardPage.xaml       # Main dashboard
│   ├── PatientsPage.xaml        # Patient management
│   ├── DoctorsPage.xaml         # Doctor management
│   ├── BillingPage.xaml         # Billing module
│   ├── PharmacyPage.xaml        # Pharmacy module
│   ├── LabPage.xaml             # Laboratory module
│   ├── ReportsPage.xaml         # Reports & analytics
│   └── [Various Dialog Windows] # Add/Edit dialogs
│
├── Services/                    # Business logic
│   ├── DatabaseInitializer.cs  # Database seeding
│   ├── PdfExportService.cs     # PDF generation
│   └── ExcelExportService.cs   # Excel export
│
├── Utilities/
│   └── PasswordHelper.cs        # Password hashing
│
├── App.xaml                     # Application entry point
├── MainWindow.xaml              # Main window/dashboard
└── README.md                    # Documentation
```

## Build Configuration

The project is configured with:
- **Target Framework**: .NET 8.0 Windows
- **Output Type**: WinExe (Windows Application)
- **Platform**: Windows only (uses WPF)
- **Nullable**: Enabled
- **ImplicitUsings**: Enabled

## Database

- **Type**: SQLite
- **File**: `hospital.db` (created in application directory)
- **Auto-created**: Yes, on first run
- **Migrations**: Not used (EnsureCreated approach)

### Database Location
The database file is created in the same folder as the executable:
- Development: `HospitalBillingSystem/bin/Debug/net8.0-windows/hospital.db`
- Release: `HospitalBillingSystem/bin/Release/net8.0-windows/hospital.db`
- Published: Same folder as `.exe` file

## Troubleshooting

### Issue: "Could not execute because the specified command or file was not found"
**Solution**: Install .NET 8.0 SDK from Microsoft's website.

### Issue: "The name 'InitializeComponent' does not exist"
**Solution**: Clean and rebuild the solution:
```bash
dotnet clean
dotnet build
```

### Issue: NuGet package restore fails
**Solution**: Clear NuGet cache and restore:
```bash
dotnet nuget locals all --clear
dotnet restore
```

### Issue: "EnableWindowsTargeting" error on Linux/Mac
**Solution**: This is a Windows-only application. Use a Windows machine or VM.

### Issue: Database file not created
**Solution**: 
- Ensure write permissions in the application directory
- Run as administrator if needed
- Check antivirus is not blocking file creation

### Issue: Login fails with correct credentials
**Solution**:
- Verify database was created successfully
- Check `hospital.db` file exists
- Delete `hospital.db` and restart to recreate with default data

## Testing the Application

### Quick Test Workflow

1. **Login** (as admin/admin123)
2. **Add a Patient**:
   - Go to Patients → Add New Patient
   - Fill in details and save
3. **Add a Doctor**:
   - Go to Doctors → Add New Doctor
   - Fill in details and save
4. **Create a Billing**:
   - Go to Billing → New OP Billing
   - Select patient and doctor
   - Enter charges and save
5. **Export Invoice**:
   - Click PDF button on the billing record
   - PDF opens automatically
6. **View Dashboard**:
   - Return to Dashboard to see updated statistics
7. **Generate Report**:
   - Go to Reports
   - Select date range and report type
   - Click Generate Report
   - Export to Excel or PDF

## Development

### Adding New Dependencies

```bash
dotnet add package [PackageName] --version [Version]
```

### Viewing Database

Use any SQLite browser:
- DB Browser for SQLite: https://sqlitebrowser.org/
- Open the `hospital.db` file

### Code Style

- Follow Microsoft C# coding conventions
- Use meaningful variable names
- Add XML comments for public methods
- Keep UI logic in code-behind minimal

## Performance Optimization

For production deployment:
1. Enable ReadyToRun compilation:
   ```bash
   dotnet publish -c Release -r win-x64 -p:PublishReadyToRun=true
   ```

2. Enable assembly trimming (careful with reflection):
   ```bash
   dotnet publish -c Release -r win-x64 -p:PublishTrimmed=true
   ```

## Security Recommendations

Before deploying to production:

1. **Change Default Passwords**: Update all default user passwords
2. **Implement Password Policy**: Add minimum length, complexity requirements
3. **Use Stronger Hashing**: Consider bcrypt instead of SHA256
4. **Enable Logging**: Add audit trail for critical operations
5. **Data Backup**: Implement automated database backup
6. **SSL/TLS**: If networked, use encrypted connections

## Support & Resources

- **Project Repository**: https://github.com/Vijay7-tech/VijayProjects
- **.NET Documentation**: https://docs.microsoft.com/dotnet/
- **WPF Tutorial**: https://docs.microsoft.com/wpf/
- **Entity Framework Core**: https://docs.microsoft.com/ef/core/

## License & Usage

This is a demonstration project. You are free to modify and use it for:
- Learning purposes
- Commercial applications (with modifications)
- Educational institutions
- Healthcare facilities

No warranty is provided. Test thoroughly before production use.

---

**Last Updated**: February 2026  
**Version**: 1.0.0
