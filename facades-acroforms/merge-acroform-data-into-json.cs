using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files containing form data
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file path
        const string jsonOutput = "merged_form_data.json";

        // Collection to hold form data from each PDF
        var allFormData = new List<Dictionary<string, string>>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document – Document implements IDisposable
            using (var document = new Document(pdfPath))
            {
                var fieldsDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // Access form fields via Document.Form.Fields collection
                if (document?.Form?.Fields != null)
                {
                    foreach (var field in document.Form.Fields)
                    {
                        // Prefer FullName; fallback to Name if FullName is null
                        string name = field?.FullName ?? field?.Name ?? string.Empty;
                        // Convert the field value to string safely
                        string value = field?.Value?.ToString() ?? string.Empty;
                        fieldsDict[name] = value;
                    }
                }

                allFormData.Add(fieldsDict);
            }
        }

        // Serialize the aggregated form data to JSON (indented for readability)
        string json = JsonSerializer.Serialize(allFormData, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to the output file
        File.WriteAllText(jsonOutput, json);

        Console.WriteLine($"Merged form data saved to '{jsonOutput}'.");
    }
}
