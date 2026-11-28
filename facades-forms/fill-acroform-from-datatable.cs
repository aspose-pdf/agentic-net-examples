using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template and the output PDF
        const string templatePath = "AcroFormTemplate.pdf";
        const string outputPath   = "FilledForm.pdf";

        // Verify that the template file exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Create a synthetic DataTable with column names matching the form fields
        DataTable formData = new DataTable();
        formData.Columns.Add("FullName");   // Must match a field PartialName in the PDF
        formData.Columns.Add("BirthDate"); // Must match a field PartialName in the PDF
        formData.Columns.Add("Email");     // Must match a field PartialName in the PDF

        // Add a single row of sample data
        formData.Rows.Add("John Doe", DateTime.Today.ToString("yyyy-MM-dd"), "john.doe@example.com");

        // Initialize the AutoFiller facade and bind the template PDF
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);          // Load the template
            autoFiller.ImportDataTable(formData);      // Populate fields from the DataTable

            // Save the filled PDF into a MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                autoFiller.Save(ms);                  // Write PDF bytes to the stream
                ms.Position = 0;                       // Reset stream position before reading
                File.WriteAllBytes(outputPath, ms.ToArray());
            }

            // Close the facade (disposes the underlying Document)
            autoFiller.Close();
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
