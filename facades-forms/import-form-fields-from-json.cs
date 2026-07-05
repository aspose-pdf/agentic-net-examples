using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Represents a field definition read from JSON
    private class FieldDefinition
    {
        public string? Name { get; set; }          // Full field name (nullable for safety)
        public string? Type { get; set; }          // e.g., "Text", "CheckBox", "Radio", "ListBox"
        public int Page { get; set; }              // 1‑based page number
        public float Llx { get; set; }             // Lower‑left X
        public float Lly { get; set; }             // Lower‑left Y
        public float Urx { get; set; }             // Upper‑right X
        public float Ury { get; set; }             // Upper‑right Y
    }

    static void Main()
    {
        const string jsonPath   = "fields.json";          // JSON file with field definitions
        const string outputPath = "output.pdf";           // Result PDF

        // ---------------------------------------------------------------------
        // Ensure the JSON file exists. If it does not, create a minimal example
        // so the program can run without external preparation.
        // ---------------------------------------------------------------------
        if (!File.Exists(jsonPath))
        {
            var sampleFields = new List<FieldDefinition>
            {
                new FieldDefinition
                {
                    Name = "SampleText",
                    Type = "Text",
                    Page = 1,
                    Llx = 100,
                    Lly = 700,
                    Urx = 300,
                    Ury = 720
                },
                new FieldDefinition
                {
                    Name = "SampleCheck",
                    Type = "CheckBox",
                    Page = 1,
                    Llx = 100,
                    Lly = 650,
                    Urx = 120,
                    Ury = 670
                }
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(sampleFields, options);
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Sample JSON file created at '{jsonPath}'.");
        }

        // Load field definitions from JSON
        List<FieldDefinition>? fields;
        using (FileStream fs = File.OpenRead(jsonPath))
        {
            fields = JsonSerializer.Deserialize<List<FieldDefinition>>(fs);
        }

        if (fields == null || fields.Count == 0)
        {
            Console.Error.WriteLine("No field definitions were found – the JSON file may be empty or malformed.");
            return;
        }

        // Create a blank PDF document with enough pages for the highest page index
        using (Document doc = new Document())
        {
            int maxPage = 0;
            foreach (var f in fields)
                if (f.Page > maxPage) maxPage = f.Page;

            // Aspose.Pdf starts with one page by default; add additional pages if needed
            for (int i = doc.Pages.Count + 1; i <= maxPage; i++)
                doc.Pages.Add();

            // Bind the document to FormEditor (Facades API)
            using (FormEditor formEditor = new FormEditor(doc))
            {
                foreach (var f in fields)
                {
                    // Guard against missing name or type
                    if (string.IsNullOrWhiteSpace(f.Name))
                        throw new ArgumentException("Field definition is missing a valid Name.");

                    FieldType fieldType = MapFieldType(f.Type);

                    // Add the field to the specified page and rectangle
                    formEditor.AddField(
                        fieldType,
                        f.Name,
                        f.Page,
                        f.Llx,
                        f.Lly,
                        f.Urx,
                        f.Ury);
                }

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }

    // Helper to convert a textual type name to the corresponding FieldType enum value
    private static FieldType MapFieldType(string? typeName)
    {
        return typeName?.ToLowerInvariant() switch
        {
            "text"       => FieldType.Text,
            "checkbox"   => FieldType.CheckBox,
            "radio"      => FieldType.Radio,
            "listbox"    => FieldType.ListBox,
            "combobox"   => FieldType.ComboBox,
            "pushbutton" => FieldType.PushButton,
            _ => throw new ArgumentException($"Unsupported field type: {typeName ?? "null"}")
        };
    }
}
