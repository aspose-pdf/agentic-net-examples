using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputJson = "form_data.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (var document = new Document(inputPdf))
            {
                // Export form fields to JSON using the correct API
                using (var jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
                {
                    document.Form.ExportToJson(jsonStream);
                }
            }

            // Verify the JSON structure
            string jsonText = File.ReadAllText(outputJson);
            using JsonDocument doc = JsonDocument.Parse(jsonText);
            JsonElement root = doc.RootElement;

            if (root.ValueKind != JsonValueKind.Array)
            {
                Console.WriteLine("Unexpected JSON format: root element is not an array.");
                return;
            }

            Console.WriteLine("Exported form fields:");
            foreach (JsonElement field in root.EnumerateArray())
            {
                // Each field is expected to have "name" and "value" properties
                string name = field.TryGetProperty("name", out JsonElement nameProp) ? nameProp.GetString() ?? string.Empty : string.Empty;
                string value = field.TryGetProperty("value", out JsonElement valueProp) ? valueProp.GetString() ?? string.Empty : string.Empty;
                Console.WriteLine($"- {name}: {value}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
