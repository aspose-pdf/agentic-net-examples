using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the JSON definition and the output PDF
        const string jsonPath   = "form_definition.json";
        const string outputPath = "generated_form.pdf";

        // Verify that the JSON file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add a blank page – required because form fields need a page context
            doc.Pages.Add();

            // Open the JSON file as a stream and import the form fields
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                // ImportFromJson adds the AcroForm fields defined in the JSON to the document
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the resulting PDF with the imported form fields
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPath}'.");
    }
}