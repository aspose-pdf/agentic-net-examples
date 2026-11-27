using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormB.pdf";
        const string jsonPath = "FormB_fields.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Prepare a dictionary to hold field name/value pairs
            var fieldData = new Dictionary<string, string>();

            // Ensure the document contains a form
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (var field in doc.Form.Fields)
                {
                    // Some fields may have null values; convert to empty string for JSON safety
                    string value = field.Value?.ToString() ?? string.Empty;
                    fieldData[field.PartialName] = value;
                }
            }

            // Serialize the dictionary to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldData, jsonOptions);

            // Write the JSON content to the output file
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Form fields exported to '{jsonPath}'.");
    }
}