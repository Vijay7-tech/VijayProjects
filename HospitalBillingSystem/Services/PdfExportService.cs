using System;
using System.IO;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using HospitalBillingSystem.Models;

namespace HospitalBillingSystem.Services
{
    public class PdfExportService
    {
        public void ExportInvoiceToPDF(Billing billing)
        {
            string fileName = $"Invoice_{billing.InvoiceNumber}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            
            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // Hospital Header
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                Paragraph title = new Paragraph("Hospital Billing System", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                document.Add(new Paragraph(" "));

                // Invoice Details
                document.Add(new Paragraph($"Invoice Number: {billing.InvoiceNumber}", headerFont));
                document.Add(new Paragraph($"Date: {billing.BillingDate:yyyy-MM-dd}", normalFont));
                document.Add(new Paragraph($"Type: {billing.BillingType}", normalFont));
                document.Add(new Paragraph($"Patient: {billing.Patient?.FullName}", normalFont));
                document.Add(new Paragraph($"Doctor: {billing.Doctor?.FullName}", normalFont));

                document.Add(new Paragraph(" "));

                // Charges Table
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;

                table.AddCell(new PdfPCell(new Phrase("Description", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Amount", headerFont)));

                table.AddCell("Consultation Charges");
                table.AddCell($"₹{billing.ConsultationCharges:N2}");

                table.AddCell("Medicine Charges");
                table.AddCell($"₹{billing.MedicineCharges:N2}");

                table.AddCell("Lab Charges");
                table.AddCell($"₹{billing.LabCharges:N2}");

                table.AddCell("Room Charges");
                table.AddCell($"₹{billing.RoomCharges:N2}");

                table.AddCell("Other Charges");
                table.AddCell($"₹{billing.OtherCharges:N2}");

                table.AddCell("Discount");
                table.AddCell($"₹{billing.Discount:N2}");

                table.AddCell(new PdfPCell(new Phrase("Total Amount", headerFont)));
                table.AddCell(new PdfPCell(new Phrase($"₹{billing.TotalAmount:N2}", headerFont)));

                document.Add(table);

                document.Add(new Paragraph(" "));
                document.Add(new Paragraph($"Payment Method: {billing.PaymentMethod}", normalFont));
                document.Add(new Paragraph($"Payment Status: {billing.PaymentStatus}", normalFont));

                if (!string.IsNullOrEmpty(billing.Notes))
                {
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph($"Notes: {billing.Notes}", normalFont));
                }

                document.Close();
                
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception)
            {
                if (document.IsOpen())
                    document.Close();
                throw;
            }
        }

        public void ExportReportToPDF(IEnumerable data, string reportTitle)
        {
            string fileName = $"Report_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            Document document = new Document(PageSize.A4.Rotate(), 50, 50, 25, 25);
            
            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                Paragraph title = new Paragraph(reportTitle, titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                document.Add(new Paragraph(" "));

                // Convert data to table
                var dataList = data as System.Collections.IList;
                if (dataList != null && dataList.Count > 0)
                {
                    var firstItem = dataList[0];
                    var properties = firstItem.GetType().GetProperties();

                    PdfPTable table = new PdfPTable(properties.Length);
                    table.WidthPercentage = 100;

                    // Headers
                    foreach (var prop in properties)
                    {
                        table.AddCell(new PdfPCell(new Phrase(prop.Name, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10))));
                    }

                    // Data
                    foreach (var item in dataList)
                    {
                        foreach (var prop in properties)
                        {
                            var value = prop.GetValue(item)?.ToString() ?? "";
                            table.AddCell(value);
                        }
                    }

                    document.Add(table);
                }

                document.Close();
                
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception)
            {
                if (document.IsOpen())
                    document.Close();
                throw;
            }
        }
    }
}
