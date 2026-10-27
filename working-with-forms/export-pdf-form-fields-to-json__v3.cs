using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_schema.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the PDF contains form fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Collect field definitions into a list
            var fields = new List<FormFieldDefinition>();

            foreach (Field field in doc.Form)
            {
                FormFieldDefinition definition = new FormFieldDefinition {
                    // PartialName is the programmatic name of the field
                    Name = field.PartialName,
                    // Use the concrete class name (e.g., TextBoxField, CheckBoxField) as the type identifier
                    Type = field.GetType().Name,
                    // Convert the field value to a string; handle nulls safely
                    Value = field.Value?.ToString(),
                    // Aspose.Pdf does not expose a Required flag directly; default to false
                    Required = false
                };

                fields.Add(definition);
            }

            // Serialize the list to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fields, jsonOptions);

            // Write the JSON schema to the output file
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Form schema exported to '{outputJson}'.");
        }
    }

    // DTO representing a form field in the JSON schema
    private class FormFieldDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Required { get; set; }
    }
}