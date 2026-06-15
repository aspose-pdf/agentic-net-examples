using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jsonPath = "form_fields.json";
        const string outputPdf = "generated.pdf";

        // Verify that the JSON schema file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Ensure the document has at least one page
            doc.Pages.Add();

            // Import AcroForm fields from the JSON schema
            doc.Form.ImportFromJson(jsonPath);

            // Save the PDF with the recreated form fields
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPdf}'.");
    }
}