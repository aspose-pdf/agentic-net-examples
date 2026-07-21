using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";   // PDF with AcroForm fields
        const string jsonDataPath    = "data.json";      // JSON file containing field values
        const string outputPdfPath   = "filled.pdf";     // Resulting PDF with pre‑filled fields

        // Verify input files exist
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load JSON into a dictionary (key = field name, value = field value)
        Dictionary<string, string> fieldValues;
        try
        {
            string jsonContent = File.ReadAllText(jsonDataPath);
            fieldValues = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
            if (fieldValues == null)
                throw new InvalidOperationException("Deserialized JSON is null.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse JSON: {ex.Message}");
            return;
        }

        // Open the PDF template using the Facades Form class
        using (Form form = new Form(templatePdfPath))
        {
            // Iterate over each key/value pair and fill the matching form field
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // Ensure the field exists in the PDF before attempting to fill it
                if (form.FieldNames.Contains(kvp.Key))
                {
                    // Fill the field with the provided value
                    form.FillField(kvp.Key, kvp.Value);
                }
                else
                {
                    Console.WriteLine($"Warning: Field '{kvp.Key}' not found in the PDF template.");
                }
            }

            // Save the updated PDF to the output path
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with pre‑filled fields saved to '{outputPdfPath}'.");
    }
}