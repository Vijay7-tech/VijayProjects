using System;
using System.IO;
using System.Collections;
using ClosedXML.Excel;

namespace HospitalBillingSystem.Services
{
    public class ExcelExportService
    {
        public void ExportToExcel(IEnumerable data, string sheetName)
        {
            string fileName = $"Report_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                var dataList = data as System.Collections.IList;
                if (dataList != null && dataList.Count > 0)
                {
                    var firstItem = dataList[0];
                    var properties = firstItem.GetType().GetProperties();

                    // Headers
                    for (int i = 0; i < properties.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = properties[i].Name;
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    }

                    // Data
                    int row = 2;
                    foreach (var item in dataList)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            var value = properties[i].GetValue(item);
                            worksheet.Cell(row, i + 1).Value = value?.ToString() ?? "";
                        }
                        row++;
                    }

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();
                }

                workbook.SaveAs(filePath);
                
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }
    }
}
