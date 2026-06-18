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
        const string inputPdf = "input.pdf";
        const string outputJson = "filtered_fields.json";
        const string prefix = "Customer_"; // only fields whose names start with this prefix will be exported

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Collect field names and values that match the prefix
            var filtered = new Dictionary<string, object>();

            foreach (Field field in doc.Form.Fields)
            {
                // Field.Name may be null for some internal fields; guard against it
                if (!string.IsNullOrEmpty(field.Name) && field.Name.StartsWith(prefix, StringComparison.Ordinal))
                {
                    // Retrieve the field value; most field types expose a Value property
                    // For checkboxes/radio buttons the value may be a boolean or string
                    object value = GetFieldValue(field);
                    filtered[field.Name] = value;
                }
            }

            // Serialize the filtered collection to JSON with indentation
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(filtered, jsonOptions);

            // Write the JSON to the output file
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Filtered fields exported to '{outputJson}'.");
        }
    }

    // Helper method to extract a field's value in a generic way
    private static object GetFieldValue(Field field)
    {
        // Most concrete field types inherit from Field and expose a Value property.
        // Use reflection to read it safely without depending on a specific subclass.
        var valueProp = field.GetType().GetProperty("Value");
        if (valueProp != null)
        {
            return valueProp.GetValue(field);
        }

        // Fallback for fields that may not have a Value property (e.g., Button)
        return null;
    }
}