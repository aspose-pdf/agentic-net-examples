using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jsonPath = "form_schema.json";
        const string outputPdf = "output.pdf";

        // Verify that the JSON schema file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add a default blank page (A4 size)
            doc.Pages.Add();

            // Import AcroForm fields from the JSON schema
            doc.Form.ImportFromJson(jsonPath);

            // Save the generated PDF with the imported form fields
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPdf}'.");
    }
}