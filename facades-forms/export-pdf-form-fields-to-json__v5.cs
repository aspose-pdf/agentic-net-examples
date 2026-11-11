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

        // Load PDF document (Document provides access to form fields)
        Document pdfDoc = new Document(inputPdf);

        // Verify that the document actually contains a form with fields
        // NOTE: Form.Fields is an array, so use Length instead of Count.
        if (pdfDoc.Form == null || pdfDoc.Form.Fields == null || pdfDoc.Form.Fields.Length == 0)
        {
            Console.WriteLine("No form fields found in the PDF.");
            return;
        }

        // Convert each field to a serializable DTO
        var fieldInfos = new List<FormFieldInfo>();
        foreach (Field field in pdfDoc.Form.Fields)
        {
            fieldInfos.Add(new FormFieldInfo
            {
                Name = field.Name,
                // The concrete type name (e.g., TextBoxField, CheckboxField) identifies the field kind
                Type = field.GetType().Name,
                Value = field.Value?.ToString(),
                IsReadOnly = field.ReadOnly,
                IsRequired = field.Required
            });
        }

        // Serialize the DTO list to indented JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(fieldInfos, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Form field definitions exported to '{outputJson}'.");
    }

    // DTO used for JSON output
    private class FormFieldInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsRequired { get; set; }
    }
}
