using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string jsonPath   = "fields_definition.json"; // JSON describing form fields
        const string outputPath = "generated_form.pdf";

        // Verify JSON file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Create a new blank PDF document
        using (Document doc = new Document())
        {
            // Add a single blank page – required for a PDF with a form
            doc.Pages.Add();

            // Import form field definitions from the JSON file.
            // The ImportFromJson method creates the fields in the document.
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the resulting PDF with the newly created fields
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }
}