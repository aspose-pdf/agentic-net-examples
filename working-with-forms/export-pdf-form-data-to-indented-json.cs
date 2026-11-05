using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputJson = "formData.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Check if the document contains any form fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Collect form field names and values into a dictionary
            var formData = new Dictionary<string, object>();
            foreach (var field in doc.Form.Fields)
            {
                // field.Name gives the field identifier; field.Value holds its current value
                formData[field.Name] = field.Value;
            }

            // Serialize the dictionary to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(formData, jsonOptions);

            // Write the formatted JSON to the output file
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Form data exported to '{outputJson}'.");
        }
    }
}