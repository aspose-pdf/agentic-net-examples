using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Simple DTO that matches the expected JSON structure: [{"Name":"field1","Value":"value1"}, ...]
    private class FormFieldData
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonPath}");
            return;
        }

        // Read the JSON array containing field objects
        string json = File.ReadAllText(jsonPath);

        // Deserialize the JSON into a list of name/value pairs
        List<FormFieldData> fieldData;
        try
        {
            fieldData = JsonSerializer.Deserialize<List<FormFieldData>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // If the JSON was parsed successfully, apply the values to the matching form fields
                if (fieldData != null)
                {
                    foreach (var item in fieldData)
                    {
                        // Find the field by its name (case‑sensitive)
                        var field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == item.Name);
                        if (field != null)
                        {
                            field.Value = item.Value;
                        }
                    }
                }

                // Save the updated PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}