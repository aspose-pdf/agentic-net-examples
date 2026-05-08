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
        const string inputPdf  = "input.pdf";          // source PDF with form fields
        const string outputJson = "filtered_fields.json"; // destination JSON file
        const string prefix = "Customer_";            // only fields whose names start with this

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Collect fields whose names begin with the specified prefix
            var filtered = new Dictionary<string, object>();

            foreach (Field field in doc.Form.Fields)
            {
                // Field.Name may be null for some internal fields; guard against it
                if (!string.IsNullOrEmpty(field.Name) && field.Name.StartsWith(prefix, StringComparison.Ordinal))
                {
                    // Most field types expose a Value property; use it if available
                    object value = GetFieldValue(field);
                    filtered[field.Name] = value;
                }
            }

            // Serialize the filtered collection to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using (FileStream fs = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize(fs, filtered, jsonOptions);
            }

            Console.WriteLine($"Exported {filtered.Count} fields to '{outputJson}'.");
        }
    }

    // Helper to extract a field's value in a generic way
    private static object GetFieldValue(Field field)
    {
        // Most concrete field types inherit the Value property.
        // If the field does not expose a value, return null.
        try
        {
            // The Value property returns an object; for checkboxes/radio buttons it may be a bool or string.
            return field.Value;
        }
        catch
        {
            return null;
        }
    }
}