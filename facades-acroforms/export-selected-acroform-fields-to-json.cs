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
        const string pdfPath = "input.pdf";
        const string jsonPath = "selected_fields.json";

        // List of form field names to export
        string[] selectedFields = new[] { "FirstName", "LastName", "Email" };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(pdfPath);

            // Collect only the requested fields
            var selectedData = new Dictionary<string, string>();

            foreach (Field field in doc.Form?.Fields ?? Array.Empty<Field>())
            {
                // Prefer the Name property; fallback to FullName if Name is null
                string fieldName = field.Name ?? field.FullName;
                if (Array.IndexOf(selectedFields, fieldName) >= 0)
                {
                    selectedData[fieldName] = field.Value?.ToString() ?? string.Empty;
                }
            }

            // Serialize the selected fields to JSON (indented for readability)
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(selectedData, jsonOptions);
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"Selected form fields exported to '{jsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}