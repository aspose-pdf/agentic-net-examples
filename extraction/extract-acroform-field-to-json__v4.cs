using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormD.pdf";
        const string jsonPath = "FormD_CustomerName.json";
        const string fieldName = "CustomerName";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field safely
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"AcroForm field \"{fieldName}\" not found or is not a form field.");
                return;
            }

            // Get the field value as a string (null‑safe)
            string fieldValue = field.Value?.ToString() ?? string.Empty;

            // Prepare a simple object for JSON serialization
            var jsonObject = new { CustomerName = fieldValue };

            // Serialize to JSON
            string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Customer name saved to \"{jsonPath}\".");
    }
}
