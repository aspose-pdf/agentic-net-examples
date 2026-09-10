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
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "filtered_fields.json";
        const string fieldPrefix = "Customer_";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a dictionary to hold the filtered field names and their values
            var filteredFields = new Dictionary<string, object>();

            // Iterate over all form fields in the document
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Check if the field name starts with the desired prefix
                if (!string.IsNullOrEmpty(field.Name) && field.Name.StartsWith(fieldPrefix, StringComparison.Ordinal))
                {
                    // Store the field value (null-safe) in the dictionary
                    filteredFields[field.Name] = field.Value ?? string.Empty;
                }
            }

            // Serialize the filtered fields to JSON with indentation
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonContent = JsonSerializer.Serialize(filteredFields, jsonOptions);

            // Write the JSON output to the specified file
            File.WriteAllText(outputJsonPath, jsonContent);
        }

        Console.WriteLine($"Filtered fields exported to '{outputJsonPath}'.");
    }
}