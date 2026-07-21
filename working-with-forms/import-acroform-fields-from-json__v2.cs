using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jsonPath = "form_schema.json";
        const string outputPdf = "generated_form.pdf";

        // Verify that the JSON schema file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema file not found: {jsonPath}");
            return;
        }

        // Create a new PDF document and import AcroForm fields from the JSON schema
        using (Document doc = new Document())
        {
            // Import form fields defined in the JSON file
            doc.Form.ImportFromJson(jsonPath);

            // Save the resulting PDF with the imported form fields
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPdf}'.");
    }
}