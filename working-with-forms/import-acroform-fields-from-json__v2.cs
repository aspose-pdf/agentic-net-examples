using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the JSON file that defines the AcroForm fields
        const string jsonPath = "form_schema.json";
        // Output PDF file that will contain the generated form
        const string outputPdf = "output.pdf";

        // Verify that the JSON schema file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Create a new PDF document and import the form fields from the JSON schema
        using (Document doc = new Document())
        {
            // Import AcroForm fields defined in the JSON file
            doc.Form.ImportFromJson(jsonPath);

            // Save the resulting PDF with the imported form fields
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPdf}'.");
    }
}