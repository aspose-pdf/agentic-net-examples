using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class ExportFormFieldsToJson
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "form_fields.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF and bind it to the Form facade
        using (Form form = new Form(pdfPath))
        {
            // Export all form fields to a JSON file (indented for readability)
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportJson(jsonStream, indented: true);
            }
        }

        // Verify the JSON structure by parsing it
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            using (JsonDocument doc = JsonDocument.Parse(jsonContent))
            {
                // The exported JSON is expected to be an array of field objects
                if (doc.RootElement.ValueKind != JsonValueKind.Array)
                {
                    Console.Error.WriteLine("Unexpected JSON format: root element is not an array.");
                }
                else
                {
                    Console.WriteLine($"Exported {doc.RootElement.GetArrayLength()} form fields to JSON.");
                    // Optionally, display the name of each field
                    foreach (JsonElement field in doc.RootElement.EnumerateArray())
                    {
                        if (field.TryGetProperty("FullName", out JsonElement name))
                        {
                            Console.WriteLine($"Field: {name.GetString()}");
                        }
                    }
                }
            }
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}