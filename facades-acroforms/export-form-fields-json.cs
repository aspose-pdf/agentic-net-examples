using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form-data.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and export its form fields to JSON
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export all form fields to a JSON file (indented output by default)
            pdfDocument.Form.ExportToJson(outputJsonPath);
        }

        // Verify that the exported JSON is a valid JSON object and display basic info
        if (!File.Exists(outputJsonPath))
        {
            Console.Error.WriteLine($"Failed to create JSON file: {outputJsonPath}");
            return;
        }

        using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Open, FileAccess.Read))
        using (JsonDocument jsonDocument = JsonDocument.Parse(jsonStream))
        {
            JsonElement root = jsonDocument.RootElement;
            Console.WriteLine($"JSON root kind: {root.ValueKind}");
            if (root.ValueKind == JsonValueKind.Object)
            {
                int propertyCount = root.EnumerateObject().Count();
                Console.WriteLine($"Number of top‑level properties in JSON: {propertyCount}");
            }
            else
            {
                Console.WriteLine("Unexpected JSON structure – expected an object.");
            }
        }
    }
}