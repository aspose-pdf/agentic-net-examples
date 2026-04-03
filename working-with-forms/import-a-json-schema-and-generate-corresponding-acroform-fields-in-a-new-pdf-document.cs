using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the JSON schema that defines the AcroForm fields
        const string jsonPath = "form_schema.json";

        // Output PDF file that will contain the generated form fields
        const string outputPdf = "output.pdf";

        // Verify that the JSON file exists before proceeding
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Create a new PDF document (contains a default blank page)
        using (Document doc = new Document())
        {
            // Import AcroForm fields from the JSON schema into the document
            // The ImportFromJson method reads the JSON file and creates the corresponding form fields
            doc.Form.ImportFromJson(jsonPath);

            // Save the resulting PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPdf}'.");
    }
}