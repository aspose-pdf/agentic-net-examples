using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Create and populate a DataTable whose column names match the
        // AcroForm field names in the PDF.
        // ------------------------------------------------------------
        DataTable formData = new DataTable();
        formData.Columns.Add("FirstName", typeof(string));
        formData.Columns.Add("LastName",  typeof(string));
        formData.Columns.Add("Email",     typeof(string));
        formData.Columns.Add("Phone",     typeof(string));
        formData.Rows.Add("John", "Doe", "john.doe@example.com", "555-1234");

        // ------------------------------------------------------------
        // Fill the form using AutoFiller (the modern API).
        // ------------------------------------------------------------
        using (AutoFiller filler = new AutoFiller())
        {
            // Bind the template PDF
            filler.BindPdf(inputPdfPath);

            // Import the DataTable – column names are matched to field names
            filler.ImportDataTable(formData);

            // Save the filled PDF
            filler.Save(outputPdfPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdfPath}'.");
    }
}
