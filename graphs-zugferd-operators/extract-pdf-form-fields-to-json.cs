using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        // Verify the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Collect form field names and their values
            var fieldValues = new Dictionary<string, string>();
            foreach (var field in pdfDocument.Form.Fields)
            {
                // FullName gives the hierarchical name of the field
                string name = field.FullName;
                // Value may be null, convert to empty string in that case
                string value = field.Value?.ToString() ?? string.Empty;
                fieldValues[name] = value;
            }

            // Serialize the dictionary to a nicely formatted JSON string
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldValues, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
        }

        // Optional: read the generated JSON and output it to the console
        if (File.Exists(outputJsonPath))
        {
            string jsonContent = File.ReadAllText(outputJsonPath);
            Console.WriteLine(jsonContent);
        }
        else
        {
            Console.Error.WriteLine("Failed to create JSON output.");
        }
    }
}