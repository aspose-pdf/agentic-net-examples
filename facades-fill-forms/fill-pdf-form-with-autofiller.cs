using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - path to the PDF template that contains form fields
        // 1 - path to the XLSX file with data (for demo purposes we will not actually read the XLSX)
        // 2 - optional output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <template.pdf> <data.xlsx> [output.pdf]");
            return;
        }

        string templatePath = args[0];
        string excelPath    = args[1];
        string outputPath   = args.Length > 2 ? args[2] : "filled_output.pdf";

        // -----------------------------------------------------------------
        // Build a DataTable that matches the field names in the PDF template.
        // In a real scenario you would read the XLSX file (e.g., via
        // ExcelDataReader, ClosedXML, or OleDb) and populate the table.
        // Here we create a single row with dummy data for illustration.
        // -----------------------------------------------------------------
        DataTable dataTable = new DataTable("MailMerge");
        dataTable.Columns.Add("CompanyName", typeof(string));
        dataTable.Columns.Add("ContactName", typeof(string));
        dataTable.Columns.Add("Address",     typeof(string));
        dataTable.Columns.Add("PostalCode",  typeof(string));
        dataTable.Columns.Add("City",        typeof(string));
        dataTable.Columns.Add("Country",     typeof(string));
        dataTable.Columns.Add("Heading",     typeof(string));

        dataTable.Rows.Add(
            "Acme Corp",               // CompanyName
            "John Doe",                // ContactName
            "123 Main St",             // Address
            "12345",                   // PostalCode
            "Metropolis",              // City
            "USA",                     // Country
            "Dear Acme Corp,"          // Heading
        );

        // -----------------------------------------------------------------
        // Use Aspose.Pdf.Facades.AutoFiller to bind the template, import the
        // DataTable, and generate the filled PDF.
        // -----------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF template file.
            autoFiller.BindPdf(templatePath);

            // Import the data. Column names must exactly match the field names
            // in the PDF (case‑sensitive).
            autoFiller.ImportDataTable(dataTable);

            // Save the resulting PDF. This creates a single merged PDF stream.
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}