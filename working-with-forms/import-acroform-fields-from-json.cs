using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jsonPath   = "formFields.json";
        const string outputPath = "generated.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Create a new PDF document (contains a default page)
        using (Document doc = new Document())
        {
            // Import AcroForm fields from the JSON schema
            doc.Form.ImportFromJson(jsonPath);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported form fields saved to '{outputPath}'.");
    }
}