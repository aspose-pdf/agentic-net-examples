using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form.pdf";
        const string outputJsonPath = "form_data.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Export form field values to JSON using the Document.Form API (ExportToJson)
        using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
        {
            // ExportFieldsToJsonOptions may not expose an Indent property in all versions.
            // Use the default options (or set other supported options) and let Aspose handle formatting.
            var jsonOptions = new ExportFieldsToJsonOptions();
            // If a pretty‑printed JSON is required and the current version supports it,
            // the Indent property can be set conditionally:
            // if (jsonOptions.GetType().GetProperty("Indent") != null)
            //     jsonOptions.GetType().GetProperty("Indent").SetValue(jsonOptions, true);
            pdfDocument.Form.ExportToJson(jsonStream, jsonOptions);
        }

        // Verify the exported JSON structure
        if (!File.Exists(outputJsonPath))
        {
            Console.Error.WriteLine($"Export failed, JSON not created: {outputJsonPath}");
            return;
        }

        try
        {
            string jsonContent = File.ReadAllText(outputJsonPath);
            using JsonDocument doc = JsonDocument.Parse(jsonContent);
            JsonElement root = doc.RootElement;

            if (root.ValueKind != JsonValueKind.Object)
            {
                Console.Error.WriteLine("Invalid JSON structure: root element is not an object.");
                return;
            }

            Console.WriteLine("Form field values exported to JSON:");
            foreach (JsonProperty property in root.EnumerateObject())
            {
                // Each property name is the form field name; its value is the field value
                string fieldName = property.Name;
                string fieldValue = property.Value.GetString() ?? property.Value.ToString();
                Console.WriteLine($"  {fieldName}: {fieldValue}");
            }
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Error parsing JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
