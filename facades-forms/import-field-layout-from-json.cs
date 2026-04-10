using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Represents a field definition coming from the JSON layout file
    private class FieldLayout
    {
        public string Name { get; set; }          // field name
        public string Type { get; set; }          // e.g., "Text", "CheckBox", "Radio"
        public int Page { get; set; }             // 1‑based page number
        public float Llx { get; set; }            // lower‑left X
        public float Lly { get; set; }            // lower‑left Y
        public float Urx { get; set; }            // upper‑right X
        public float Ury { get; set; }            // upper‑right Y
    }

    static void Main()
    {
        const string jsonPath   = "fieldLayout.json";   // input JSON describing fields
        const string outputPath = "output.pdf";         // resulting PDF with positioned fields

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON layout file not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into a list of field definitions
        List<FieldLayout> fields;
        using (FileStream fs = File.OpenRead(jsonPath))
        {
            fields = JsonSerializer.Deserialize<List<FieldLayout>>(fs);
        }

        // Create a new blank PDF document and add a page for each distinct page number referenced
        using (Document doc = new Document())
        {
            // Determine the highest page number required
            int maxPage = 0;
            foreach (var f in fields)
                if (f.Page > maxPage) maxPage = f.Page;

            // Ensure the document has enough pages (Pages are 1‑based)
            for (int i = 1; i <= maxPage; i++)
                doc.Pages.Add();

            // Bind the document to a FormEditor facade for field manipulation
            FormEditor formEditor = new FormEditor(doc);

            // Add each field according to its layout definition
            foreach (var f in fields)
            {
                // Convert the string type to the FieldType enum (case‑insensitive)
                if (!Enum.TryParse<FieldType>(f.Type, true, out FieldType fieldType))
                {
                    Console.Error.WriteLine($"Unsupported field type '{f.Type}' for field '{f.Name}'. Skipping.");
                    continue;
                }

                // Add the field; AddField returns true on success
                bool added = formEditor.AddField(
                    fieldType,
                    f.Name,
                    f.Page,
                    f.Llx,
                    f.Lly,
                    f.Urx,
                    f.Ury);

                if (!added)
                {
                    Console.Error.WriteLine($"Failed to add field '{f.Name}' on page {f.Page}.");
                }
            }

            // Save the populated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported field layout saved to '{outputPath}'.");
    }
}