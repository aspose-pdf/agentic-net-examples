using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jsonPath   = "form_fields.json";   // JSON schema describing AcroForm fields
        const string outputPath = "generated_form.pdf";

        // Verify JSON file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Create a new empty PDF document
            using (Document doc = new Document())
            {
                // Import AcroForm fields from the JSON schema
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                {
                    // The Form object provides ImportFromJson overloads for Stream and file name
                    doc.Form.ImportFromJson(jsonStream);
                }

                // Save the newly generated PDF with the imported form fields
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with AcroForm fields created: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}