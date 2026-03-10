using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF with form fields
        const string outputJsonPath = "form_fields.json"; // Output JSON file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Export all form field values to JSON
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                // ExportToJson writes the field names and their current values in JSON format
                doc.Form.ExportToJson(jsonStream);
            }
        }

        Console.WriteLine($"Form field values exported to '{outputJsonPath}'.");
    }
}