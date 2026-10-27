using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string jsonPath  = "formData.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Prepare a dictionary to hold field name/value pairs
            var formData = new Dictionary<string, object>();

            // Iterate over all form fields in the document
            foreach (var field in doc.Form.Fields)
            {
                // Some fields may have complex values (e.g., list boxes). Use ToString() for simplicity
                formData[field.FullName] = field.Value?.ToString() ?? string.Empty;
            }

            // Serialize the dictionary to a JSON string (indented for readability)
            string json = JsonSerializer.Serialize(formData, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON string to a text file
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Form data exported to JSON file: {jsonPath}");
    }
}