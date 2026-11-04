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
        const string inputPath = "input.pdf";
        const string outputJson = "form_schema.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build a dictionary where each key is the field name and the value describes the field
            var schema = new Dictionary<string, object>();

            // Iterate over all form fields in the document
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field field in doc.Form.Fields)
                {
                    var fieldDef = new Dictionary<string, object>
                    {
                        // Map Aspose field type to a JSON schema type
                        ["type"] = GetJsonFieldType(field),
                        // Whether the field is required
                        ["required"] = field.Required
                    };

                    // Add maximum length constraint for text boxes (MaxLen) if defined
                    if (field is TextBoxField txt && txt.MaxLen > 0)
                        fieldDef["maxLength"] = txt.MaxLen;

                    // Include a default value if the field already has one
                    if (field.Value != null && !string.IsNullOrEmpty(field.Value.ToString()))
                        fieldDef["default"] = field.Value.ToString();

                    // Store the definition using the field's partial name as the key
                    schema[field.PartialName] = fieldDef;
                }
            }

            // Serialize the schema dictionary to pretty‑printed JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(schema, jsonOptions);
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Form schema saved to {outputJson}");
        }
    }

    // Helper to translate Aspose.Pdf.Forms field concrete types to a JSON schema type string
    static string GetJsonFieldType(Field field)
    {
        // Determine the concrete field class name
        string typeName = field.GetType().Name;
        return typeName switch
        {
            // CheckboxField represents a boolean value
            nameof(CheckboxField) => "boolean",
            // RadioButtonField (or RadioButtonGroup) is represented as a string
            nameof(RadioButtonField) => "string",
            // TextBoxField, ComboBoxField are strings
            nameof(TextBoxField) => "string",
            nameof(ComboBoxField) => "string",
            // ListBoxField can hold multiple selections => array
            nameof(ListBoxField) => "array",
            // SignatureField is stored as a string placeholder
            nameof(SignatureField) => "string",
            // Fallback for any other field types
            _ => "string"
        };
    }
}
