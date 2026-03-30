using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfExcelFormMapping
{
    class Program
    {
        static void Main()
        {
            const string pdfTemplatePath = "template.pdf";
            const string outputPdfPath = "filled_form.pdf";

            if (!File.Exists(pdfTemplatePath))
            {
                Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
                return;
            }

            // Sample data that would normally come from an Excel sheet.
            DataTable dataTable = GetSampleDataTable();

            // Fill PDF form fields using the DataTable where column names match field names
            var autoFiller = new AutoFiller();
            autoFiller.BindPdf(pdfTemplatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPdfPath);

            Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
        }

        // Returns a DataTable with column names taken from the first row of a hypothetical Excel sheet.
        private static DataTable GetSampleDataTable()
        {
            var table = new DataTable();

            // Define column headers – these would correspond to the first row in the Excel file.
            table.Columns.Add("Name");
            table.Columns.Add("Date");
            table.Columns.Add("Amount");

            // Add a single data row – replace with as many rows as needed.
            var row = table.NewRow();
            row["Name"] = "John Doe";
            row["Date"] = DateTime.Today.ToShortDateString();
            row["Amount"] = "123.45";
            table.Rows.Add(row);

            return table;
        }
    }
}
