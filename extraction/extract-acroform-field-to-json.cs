using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormA.pdf";
        const string outputPath = "FormA_CustomerName.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the AcroForm field titled "CustomerName"
            // The Form indexer returns a WidgetAnnotation, so cast to Field to access the Value property
            Field field = doc.Form["CustomerName"] as Field;
            string fieldValue = field?.Value?.ToString() ?? string.Empty;

            // Create a simple JSON object with the field value
            var jsonObject = new { CustomerName = fieldValue };
            string json = JsonSerializer.Serialize(jsonObject);

            // Save the JSON to the specified file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"CustomerName saved to '{outputPath}'.");
    }
}
