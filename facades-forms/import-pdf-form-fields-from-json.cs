using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Represents a field definition in the JSON input
    private class FieldDefinition
    {
        public string Type { get; set; }          // e.g., "Text", "CheckBox", "Radio", "ListBox"
        public string Name { get; set; }          // full field name
        public int Page { get; set; }             // 1‑based page number
        public float Llx { get; set; }            // lower‑left X
        public float Lly { get; set; }            // lower‑left Y
        public float Urx { get; set; }            // upper‑right X
        public float Ury { get; set; }            // upper‑right Y
    }

    static void Main()
    {
        const string jsonPath   = "fields.json";          // JSON file with field definitions
        const string outputPath = "output.pdf";           // Result PDF with created fields

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Read and deserialize the JSON array of field definitions
        List<FieldDefinition> fields;
        using (FileStream fs = File.OpenRead(jsonPath))
        {
            fields = JsonSerializer.Deserialize<List<FieldDefinition>>(fs);
        }

        if (fields == null || fields.Count == 0)
        {
            Console.Error.WriteLine("No field definitions found in JSON.");
            return;
        }

        // Create a new blank PDF document
        using (Document doc = new Document())
        {
            // Ensure at least one page exists (FormEditor expects a page for field placement)
            doc.Pages.Add();

            // Bind the document to FormEditor (Facades API)
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // Add each field defined in the JSON
                foreach (var f in fields)
                {
                    // Convert string type to FieldType enum (case‑insensitive)
                    if (!Enum.TryParse<FieldType>(f.Type, true, out var fieldType))
                    {
                        Console.Error.WriteLine($"Unsupported field type: {f.Type}");
                        continue;
                    }

                    // Add the field; FormEditor.AddField returns bool indicating success
                    bool added = formEditor.AddField(
                        fieldType,
                        f.Name,
                        f.Page,          // 1‑based page index as required by Aspose.Pdf
                        f.Llx,
                        f.Lly,
                        f.Urx,
                        f.Ury);

                    if (!added)
                    {
                        Console.Error.WriteLine($"Failed to add field: {f.Name}");
                    }
                }

                // Save the modified document to the desired output file
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }
}