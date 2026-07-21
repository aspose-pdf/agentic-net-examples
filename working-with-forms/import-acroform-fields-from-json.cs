using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string jsonPath   = "form_schema.json"; // JSON defining AcroForm fields
        const string outputPath = "generated_form.pdf";

        // Ensure the JSON file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Create a new PDF document and add a blank page (required for form fields)
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // first (and only) page for the form

            // Import AcroForm fields from the JSON schema
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                // The Form class provides ImportFromJson to deserialize fields
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the resulting PDF with the recreated form fields
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPath}'.");
    }
}