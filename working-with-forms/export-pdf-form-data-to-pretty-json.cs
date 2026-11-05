using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "formdata.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect form field names and their values
            var formData = new Dictionary<string, object>();

            // Iterate over the form fields collection (not WidgetAnnotation)
            foreach (Field field in doc.Form.Fields)
            {
                // Prefer FullName, fall back to Name if FullName is null
                string name = field.FullName ?? field.Name ?? string.Empty;

                // Retrieve the field value as a string (handles different field types)
                string value = field.Value?.ToString() ?? string.Empty;

                formData[name] = value;
            }

            // Serialize the dictionary to JSON with pretty‑printing (indented)
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(formData, jsonOptions);

            // Write the JSON to the output file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Form data exported to '{outputPath}'.");
    }
}
