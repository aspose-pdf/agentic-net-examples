using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    // Define a simple POCO that matches the expected JSON structure
    private class FieldDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }          // e.g., "Text", "CheckBox", "Radio", "ComboBox", "ListBox"
        public int Page { get; set; }             // 1‑based page number
        public float Llx { get; set; }            // lower‑left X
        public float Lly { get; set; }            // lower‑left Y
        public float Urx { get; set; }            // upper‑right X
        public float Ury { get; set; }            // upper‑right Y
    }

    static void Main()
    {
        const string jsonPath   = "fieldLayout.json";   // JSON file describing fields
        const string outputPath = "output.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Read and deserialize the JSON file
        List<FieldDefinition> fields;
        using (FileStream fs = File.OpenRead(jsonPath))
        {
            fields = JsonSerializer.Deserialize<List<FieldDefinition>>(fs);
        }

        // Create a new PDF document with a single blank page (add more pages if needed)
        using (Document doc = new Document())
        {
            // Ensure at least one page exists; additional pages can be added later if JSON references them
            doc.Pages.Add();

            // Bind the document to FormEditor (Facades API)
            FormEditor formEditor = new FormEditor(doc);

            // Iterate over each field definition and add it to the PDF
            foreach (var f in fields)
            {
                // Ensure the target page exists; add blank pages as necessary
                while (doc.Pages.Count < f.Page)
                {
                    doc.Pages.Add();
                }

                // Map string type to FieldType enum
                FieldType fieldType = MapFieldType(f.Type);

                // Add the field using FormEditor
                bool added = formEditor.AddField(fieldType, f.Name, f.Page, f.Llx, f.Lly, f.Urx, f.Ury);
                if (!added)
                {
                    Console.Error.WriteLine($"Failed to add field '{f.Name}' on page {f.Page}.");
                }
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }

    // Helper method to convert a string representation to the corresponding FieldType enum value
    private static FieldType MapFieldType(string type)
    {
        return type?.ToLowerInvariant() switch
        {
            "text"      => FieldType.Text,
            "checkbox"  => FieldType.CheckBox,
            "radio"     => FieldType.Radio,
            "combobox"  => FieldType.ComboBox,
            "listbox"   => FieldType.ListBox,
            "signature" => FieldType.Signature,
            "button"    => FieldType.PushButton,
            _           => FieldType.Text   // default fallback
        };
    }
}