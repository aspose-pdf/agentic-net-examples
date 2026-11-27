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
        const string inputPdfPath = "FormA.pdf";
        const string outputJsonPath = "FormA_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare a list to hold field name/value pairs
            var fieldData = new List<Dictionary<string, object>>();

            // Enumerate all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Some fields may have null values; handle gracefully
                var entry = new Dictionary<string, object>
                {
                    { "PartialName", field.PartialName },
                    { "Value", field.Value ?? string.Empty }
                };
                fieldData.Add(entry);
            }

            // Serialize the list to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldData, jsonOptions);

            // Write the JSON output to the specified file
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Form fields exported to '{outputJsonPath}'.");
    }
}