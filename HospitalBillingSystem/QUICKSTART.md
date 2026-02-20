# Hospital Billing System - Quick Start Guide

## 🚀 Get Started in 3 Minutes

### Step 1: Prerequisites (1 minute)
Download and install **.NET 8.0 SDK**: https://dotnet.microsoft.com/download/dotnet/8.0

### Step 2: Get the Code (30 seconds)
```bash
git clone https://github.com/Vijay7-tech/VijayProjects.git
cd VijayProjects/HospitalBillingSystem
```

### Step 3: Build & Run (1 minute)
```bash
dotnet run
```

That's it! The application will:
- ✅ Auto-download all dependencies
- ✅ Auto-create the database
- ✅ Auto-seed test users
- ✅ Launch the login window

## 🔐 First Login

Use these credentials:

**Admin Login:**
- Username: `admin`
- Password: `admin123`  
- Role: Select `Admin`

Then click **Login** button.

## 📋 Quick Feature Tour

After logging in, try these in order:

### 1️⃣ Add a Patient (1 minute)
1. Click **Patients** in the sidebar
2. Click **Add New Patient**
3. Fill in:
   - Name: "John Doe"
   - Date of Birth: Select any date
   - Gender: "Male"
   - Phone: "1234567890"
   - Blood Group: "A+"
4. Click **Save**

### 2️⃣ Add a Doctor (1 minute)
1. Click **Doctors** in sidebar
2. Click **Add New Doctor**
3. Fill in:
   - Name: "Dr. Sarah Smith"
   - Specialization: "Cardiology"
   - Consultation Fee: "500"
4. Click **Save**

### 3️⃣ Create a Bill (2 minutes)
1. Click **Billing** in sidebar
2. Click **New OP Billing**
3. Select the patient and doctor you just created
4. Enter charges:
   - Consultation Charges: 500
   - Medicine Charges: 200
   - Lab Charges: 300
5. Watch total auto-calculate to ₹1000
6. Click **Save**

### 4️⃣ Export Invoice (30 seconds)
1. In the billing list, find your invoice
2. Click the **PDF** button
3. PDF invoice opens automatically!

### 5️⃣ View Dashboard (30 seconds)
1. Click **Dashboard** in sidebar
2. See all metrics updated with your data!

### 6️⃣ Generate Report (1 minute)
1. Click **Reports** in sidebar
2. Select report type: "Billing Summary"
3. Click **Generate Report**
4. Click **Export to Excel** or **Export to PDF**
5. File opens automatically!

## 🎯 All Features

| Module | What You Can Do |
|--------|----------------|
| 👥 **Patients** | Add, edit, delete, search patients |
| 👨‍⚕️ **Doctors** | Manage doctor profiles |
| 💰 **Billing** | Create OP/IP bills, export invoices |
| 💊 **Pharmacy** | Manage medicine inventory, make sales |
| 🔬 **Lab** | Order tests, enter results |
| 📊 **Reports** | Generate 6 types of reports, export data |
| 📈 **Dashboard** | View real-time metrics |

## 👥 All User Accounts

Try logging in with different roles to see different menus:

| Username | Password | Role | What They See |
|----------|----------|------|---------------|
| admin | admin123 | Admin | Everything |
| doctor1 | doctor123 | Doctor | Patients, Doctors, Billing, Lab, Reports |
| receptionist | recep123 | Receptionist | Patients, Doctors, Billing, Reports |
| pharmacist | pharma123 | Pharmacist | Dashboard, Pharmacy, Reports |
| labtech | lab123 | LabTechnician | Dashboard, Lab, Reports |

## 📁 Where Are Files Saved?

- **Database**: Same folder as the executable (`hospital.db`)
- **PDFs & Excel**: Your Documents folder

## ⚙️ Advanced Commands

### Build for Distribution
```bash
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
```
Creates a single `.exe` file in:  
`bin/Release/net8.0-windows/win-x64/publish/HospitalBillingSystem.exe`

### Clean Build
```bash
dotnet clean
dotnet build
```

### View Database
Use **DB Browser for SQLite** to open `hospital.db` file.

## 🔧 Troubleshooting

**Problem**: "dotnet command not found"  
**Solution**: Install .NET 8.0 SDK from Microsoft

**Problem**: Login fails  
**Solution**: Make sure you selected the correct role dropdown

**Problem**: Can't save files  
**Solution**: Run as Administrator or check folder permissions

**Problem**: Database not created  
**Solution**: Check write permissions in application folder

## 📚 Need More Help?

- **Full Documentation**: See `README.md`
- **Build Guide**: See `BUILD_INSTRUCTIONS.md`
- **Implementation Details**: See `SUMMARY.md`
- **Code Comments**: Check the source files

## 🎓 Learning Path

**Beginner?** Follow this order:
1. Read this Quick Start Guide (you're here!)
2. Try the feature tour above
3. Read `README.md` for feature overview
4. Explore the code in `Views/` folder

**Developer?** Customize it:
1. Read `BUILD_INSTRUCTIONS.md`
2. Read `SUMMARY.md` for architecture
3. Modify models in `Models/` folder
4. Add new pages in `Views/` folder

## ✨ Key Features Highlight

🔐 **Secure Login** - SHA256 password hashing  
📊 **Live Dashboard** - 8 real-time metrics  
💳 **Smart Billing** - Auto-calculated totals  
📄 **PDF Invoices** - Professional output  
📊 **Excel Reports** - One-click export  
🔍 **Quick Search** - Find patients/doctors fast  
💊 **Stock Tracking** - Low stock alerts  
🏥 **Role-Based Access** - 5 user types  

## 🎯 Quick Commands Cheat Sheet

```bash
# Clone
git clone https://github.com/Vijay7-tech/VijayProjects.git

# Navigate
cd VijayProjects/HospitalBillingSystem

# Run (Dev)
dotnet run

# Build (Release)
dotnet build --configuration Release

# Publish (Standalone)
dotnet publish -c Release -r win-x64 --self-contained

# Clean
dotnet clean
```

## 📞 Support

Found a bug or have questions?  
Create an issue on GitHub: https://github.com/Vijay7-tech/VijayProjects

---

**Ready to go?** Run `dotnet run` and start exploring! 🚀
