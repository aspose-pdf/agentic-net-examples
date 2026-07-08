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
        const string inputPdf   = "input.pdf";          // source PDF with form fields
        const string outputJson = "filtered_fields.json"; // result JSON file
        const string prefix     = "Customer_";          // only fields starting with this prefix

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Collect matching fields
            List<Field> matchingFields = new List<Field>();
            foreach (Field field in doc.Form.Fields)
            {
                if (!string.IsNullOrEmpty(field?.FullName) && field.FullName.StartsWith(prefix, StringComparison.Ordinal))
                {
                    matchingFields.Add(field);
                }
            }

            // Prepare a simple DTO for JSON serialization
            var fieldData = new List<object>();
            foreach (Field field in matchingFields)
            {
                // Export the raw value of the field
                object value = GetFieldValue(field);
                fieldData.Add(new { Name = field.FullName, Value = value });
            }

            // Serialize to JSON (indented for readability)
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldData, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Exported {matchingFields.Count} fields to '{outputJson}'.");
        }
    }

    // Helper to extract the value of a form field in a generic way
    private static object GetFieldValue(Field field)
    {
        // Different field types expose their value via different properties
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                return chk.Checked;
            case RadioButtonField rad:
                return rad.Value;
            case ListBoxField lst:
                return lst.SelectedItems;
            case ComboBoxField cmb:
                return cmb.Value;
            case ButtonField btn:
                return btn.Value; // usually empty for buttons
            default:
                return field.Value; // fallback for other field types
        }
    }
}