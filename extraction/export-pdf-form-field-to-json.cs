using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "fieldValue.json";
        const string fieldName = "myField";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF via FileStream inside a using block for deterministic disposal
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        using (Document doc = new Document(pdfStream))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field before using.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Extract the field value as a string
            string value = field.Value?.ToString() ?? string.Empty;

            // Create a simple JSON object
            var jsonObject = new { field = fieldName, value };
            string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file using a FileStream
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(jsonStream))
            {
                writer.Write(json);
            }

            Console.WriteLine($"Exported field value to '{jsonPath}'.");
        }
    }
}
