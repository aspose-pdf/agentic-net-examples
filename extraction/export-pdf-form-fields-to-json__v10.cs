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
        const string inputPath = "FormC.pdf";
        const string outputPath = "FormC_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a list to hold field name/value pairs
            var fields = new List<Dictionary<string, string>>();

            // Enumerate all form fields in the document
            foreach (var item in doc.Form)
            {
                // The collection returns objects of type Field (abstract) – cast safely
                var field = item as Field;
                if (field == null) continue;

                // Some fields may have null values; handle gracefully
                string value = field.Value?.ToString() ?? string.Empty;

                // Store the partial name and its value
                fields.Add(new Dictionary<string, string>
                {
                    { "PartialName", field.PartialName },
                    { "Value", value }
                });
            }

            // Serialize the list to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fields, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Form fields exported to '{outputPath}'.");
    }
}