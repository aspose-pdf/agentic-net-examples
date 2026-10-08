using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_data.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Ensure the document contains a form with at least one field
                if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
                {
                    Console.WriteLine("No AcroForm fields found in the document.");
                    return;
                }

                // Collect field names and their string values
                var fieldData = new Dictionary<string, string>();

                foreach (Field field in doc.Form.Fields)
                {
                    // PartialName is the field's identifier (fallback to empty string if null)
                    string name = field.PartialName ?? string.Empty;

                    // Value may be null; convert to string safely
                    string value = field.Value?.ToString() ?? string.Empty;

                    fieldData[name] = value;
                }

                // Serialize the dictionary to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(fieldData, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Form data extracted to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
