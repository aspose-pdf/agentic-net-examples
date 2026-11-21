using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load JSON into a dictionary of field name/value pairs
        Dictionary<string, string> fieldMap;
        try
        {
            string json = File.ReadAllText(jsonPath);
            fieldMap = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read JSON: {ex.Message}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(templatePath))
        {
            // Enable automatic recalculation of fields (optional but recommended)
            doc.Form.AutoRecalculate = true;

            foreach (var kvp in fieldMap)
            {
                // Find the field with the matching name
                var field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == kvp.Key);
                if (field != null)
                {
                    // Set the field value
                    field.Value = kvp.Value;
                }
                else
                {
                    Console.WriteLine($"Warning: field '{kvp.Key}' not found in template.");
                }
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with pre‑filled fields saved to '{outputPath}'.");
    }
}
