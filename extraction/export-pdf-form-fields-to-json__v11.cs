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
        const string inputPath = "FormD.pdf";
        const string outputPath = "FormD_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect field names and values
            var fields = new Dictionary<string, string>();

            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                string name = field.PartialName ?? string.Empty;
                string value = field.Value?.ToString() ?? string.Empty;
                fields[name] = value;
            }

            // Serialize the dictionary to JSON with indentation
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fields, options);

            // Write the JSON output to a file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Form fields exported to '{outputPath}'.");
    }
}