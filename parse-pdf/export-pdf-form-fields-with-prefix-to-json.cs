using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "filtered_fields.json";
        const string prefix = "Customer_"; // fields whose names start with this prefix

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Prepare a list to hold the filtered field data
            var filtered = new List<Dictionary<string, object>>();

            // Iterate over all form fields
            foreach (Field field in doc.Form.Fields)
            {
                // Some field types may not have a Name property; use FullName if available
                string fieldName = field?.FullName ?? field?.Name;
                if (string.IsNullOrEmpty(fieldName))
                    continue;

                // Keep only fields whose name starts with the specified prefix
                if (fieldName.StartsWith(prefix, StringComparison.Ordinal))
                {
                    // Retrieve the field value; handle nulls safely
                    object value = field?.Value;
                    // Store name and value in a simple dictionary
                    filtered.Add(new Dictionary<string, object>
                    {
                        { "Name", fieldName },
                        { "Value", value ?? string.Empty }
                    });
                }
            }

            // Serialize the filtered collection to JSON with indentation
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(filtered, jsonOptions);

            // Write the JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Filtered fields exported to '{outputJson}'.");
    }
}