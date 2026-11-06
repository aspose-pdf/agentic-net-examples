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
        // Input PDF files – adjust the list as needed
        string[] pdfFiles = new string[]
        {
            "form1.pdf",
            "form2.pdf",
            "form3.pdf"
        };

        // Output JSON file
        const string jsonOutputPath = "merged_form_data.json";

        // Dictionary to hold form data per PDF (key = file name, value = field dictionary)
        var allFormData = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    var fieldData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                    // Ensure the document actually contains a form
                    if (doc.Form != null && doc.Form.Fields != null)
                    {
                        foreach (Field field in doc.Form.Fields)
                        {
                            // Prefer FullName; fallback to Name if FullName is null
                            string name = field?.FullName ?? field?.Name ?? string.Empty;
                            // Convert the field value to string; treat null as empty string
                            string value = field?.Value?.ToString() ?? string.Empty;
                            fieldData[name] = value;
                        }
                    }

                    // Store the collected data using the file name (without directory) as the key
                    string fileKey = Path.GetFileName(pdfPath);
                    allFormData[fileKey] = fieldData;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        try
        {
            // Serialize the aggregated form data to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(allFormData, jsonOptions);
            File.WriteAllText(jsonOutputPath, json);
            Console.WriteLine($"Merged form data saved to '{jsonOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write JSON file: {ex.Message}");
        }
    }
}
