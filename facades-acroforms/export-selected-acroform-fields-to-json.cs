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
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "selected_fields.json";

        // List of fully qualified field names to export
        var fieldsToExport = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "CustomerName",
            "OrderDate",
            "TotalAmount"
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (var document = new Document(inputPdfPath))
        {
            // Prepare a dictionary to hold selected field values
            var selectedData = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            // Iterate over all form fields
            foreach (Field field in document.Form.Fields)
            {
                // Use the full field name for matching
                string fullName = field.FullName;

                if (fieldsToExport.Contains(fullName))
                {
                    // Some fields may have null values; handle gracefully
                    object value = field.Value ?? string.Empty;
                    selectedData[fullName] = value;
                }
            }

            // Serialize the selected fields to JSON with indentation
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(selectedData, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, jsonString);
        }

        Console.WriteLine($"Selected form fields exported to '{outputJsonPath}'.");
    }
}