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
        const string inputPdf = "input.pdf";
        const string outputJson = "form_fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document. Document implements IDisposable, so we use a using block.
        using var doc = new Document(inputPdf);

        // The form fields are available via doc.Form.Fields collection.
        var fieldDefinitions = new List<object>();

        foreach (Field field in doc.Form.Fields)
        {
            // Determine a readable type name – the concrete class name (e.g., TextBoxField, CheckBoxField, etc.)
            string fieldType = field.GetType().Name;

            // Most field types expose a Value property; if not, we fall back to null.
            string value = null;
            try
            {
                // Value is defined in the base Field class as an object.
                value = field.Value?.ToString();
            }
            catch { /* ignore – some field types may not support Value */ }

            // Tooltip handling: Aspose.Pdf does not expose a Tooltip property on Field.
            // For button fields the tooltip is stored in AlternateCaption.
            // For other fields the tooltip can be retrieved from the underlying annotation's Contents property.
            string tooltip = null;
            if (field is ButtonField buttonField)
            {
                tooltip = buttonField.AlternateCaption;
            }
            else
            {
                tooltip = field.Contents; // Contents works for most form field types.
            }

            var definition = new
            {
                Name = field.Name,               // Full name (may be same as PartialName)
                PartialName = field.PartialName, // Short name used in the PDF form
                Type = fieldType,
                Value = value,
                IsReadOnly = field.ReadOnly,
                IsRequired = field.Required,
                Tooltip = tooltip
            };
            fieldDefinitions.Add(definition);
        }

        // Serialize the list to formatted JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(fieldDefinitions, jsonOptions);

        // Write the JSON output to a file
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Form field definitions exported to '{outputJson}'.");
    }
}
