using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;                     // PDF core API
using Aspose.Pdf.Forms;               // Form handling

class Program
{
    // Minimal class that matches the JSON structure produced by Aspose.Pdf
    private class FormField
    {
        public string FullName { get; set; }
        public string Value    { get; set; }
    }

    static void Main()
    {
        const string pdfPath  = "input.pdf";      // source PDF with form fields
        const string jsonPath = "formData.json";  // intermediate JSON file
        const string csvPath  = "formData.csv";   // final CSV output

        // ------------------------------------------------------------
        // 1. Export PDF form fields to JSON (Aspose.Pdf lifecycle)
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (must be inside a using block for proper disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Export all form fields to a JSON file
            // ExportFieldsToJsonOptions can be omitted for default behaviour
            pdfDoc.Form.ExportToJson(jsonPath);
        }

        // ------------------------------------------------------------
        // 2. Read the exported JSON and convert it to CSV
        // ------------------------------------------------------------
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        // The JSON produced by Aspose.Pdf looks like:
        // [
        //   { "FullName":"field1", "Value":"value1", ... },
        //   { "FullName":"field2", "Value":"value2", ... }
        // ]
        // We'll deserialize only the needed properties.
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<FormField> fields;
        using (FileStream jsonStream = File.OpenRead(jsonPath))
        {
            fields = JsonSerializer.Deserialize<List<FormField>>(jsonStream, jsonOptions);
        }

        if (fields == null)
        {
            Console.Error.WriteLine("Failed to parse JSON.");
            return;
        }

        // Write CSV: first line is header, then each field as a row
        using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("FieldName,Value"); // CSV header

            foreach (var field in fields)
            {
                // Escape commas and quotes in the value
                string escapedValue = field.Value?.Replace("\"", "\"\"") ?? string.Empty;
                if (escapedValue.Contains(",") || escapedValue.Contains("\""))
                {
                    escapedValue = $"\"{escapedValue}\"";
                }

                string escapedName = field.FullName?.Replace("\"", "\"\"") ?? string.Empty;
                if (escapedName.Contains(",") || escapedName.Contains("\""))
                {
                    escapedName = $"\"{escapedName}\"";
                }

                writer.WriteLine($"{escapedName},{escapedValue}");
            }
        }

        Console.WriteLine($"Form data exported to CSV: {csvPath}");
    }
}
