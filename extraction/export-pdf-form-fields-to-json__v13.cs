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
        const string inputPath = "FormF.pdf";
        const string outputPath = "FormF_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a list to hold field name/value pairs
            var fields = new List<FieldInfo>();

            // Enumerate all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Some fields may have null values; handle gracefully
                string value = field.Value?.ToString() ?? string.Empty;

                fields.Add(new FieldInfo
                {
                    PartialName = field.PartialName,
                    Value = value
                });
            }

            // Serialize the list to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fields, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Form fields exported to '{outputPath}'.");
    }

    // Simple DTO for JSON serialization
    private class FieldInfo
    {
        public string PartialName { get; set; }
        public string Value { get; set; }
    }
}