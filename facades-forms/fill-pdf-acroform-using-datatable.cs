using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the AcroForm template and the output PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // Verify that the template exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Create a synthetic DataTable with sample data
        DataTable data = new DataTable();
        data.Columns.Add("FirstName");
        data.Columns.Add("LastName");
        data.Columns.Add("EmailAddress");
        data.Rows.Add("John", "Doe", "john.doe@example.com");

        // Rename columns to exactly match the AcroForm field names (case‑sensitive)
        // Example field names in the PDF: "first_name", "last_name", "email"
        data.Columns["FirstName"].ColumnName = "first_name";
        data.Columns["LastName"].ColumnName  = "last_name";
        data.Columns["EmailAddress"].ColumnName = "email";

        // Fill the PDF form using AutoFiller
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);          // Load the template
            filler.ImportDataTable(data);          // Import the DataTable values
            filler.Save(outputPath);               // Save the filled PDF
        }

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }
}