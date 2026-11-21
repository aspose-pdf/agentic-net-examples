using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Added to resolve the Field type

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "formdata.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Collect form field names and their values
            var formData = new Dictionary<string, string>();

            // Iterate over all form fields. The collection contains concrete field types
            // (TextBoxField, CheckBoxField, RadioButtonField, etc.) that inherit from the
            // abstract Aspose.Pdf.Forms.Field class.
            foreach (var field in doc.Form.Fields)
            {
                // Some fields may have null values; treat them as empty strings
                string value = field.Value?.ToString() ?? string.Empty;
                formData[field.FullName] = value;
            }

            // Serialize the dictionary to a formatted JSON string
            string json = JsonSerializer.Serialize(formData, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Save the JSON string to a text file
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Form data exported to '{outputJsonPath}'.");
    }
}
