using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfFormFromJson
{
    // Represents a single field definition as described in the JSON input.
    public class FieldDefinition
    {
        public string FieldType { get; set; }   // e.g., "Text", "CheckBox", "Radio", etc.
        public string Name { get; set; }        // Full field name.
        public int Page { get; set; }           // 1‑based page number.
        public float Llx { get; set; }          // Lower‑left X coordinate.
        public float Lly { get; set; }          // Lower‑left Y coordinate.
        public float Urx { get; set; }          // Upper‑right X coordinate.
        public float Ury { get; set; }          // Upper‑right Y coordinate.
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath   = "fields.json";      // Input JSON file with field definitions.
            const string outputPath = "output.pdf";       // Resulting PDF file.

            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            // Deserialize the JSON array into a list of FieldDefinition objects.
            List<FieldDefinition> fields;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                fields = JsonSerializer.Deserialize<List<FieldDefinition>>(jsonContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            // Create a new blank PDF document and add a single page (more pages will be added on demand).
            using (Document doc = new Document())
            {
                // Ensure at least one page exists; additional pages will be created as needed.
                doc.Pages.Add();

                // FormEditor works on the Document instance.
                FormEditor formEditor = new FormEditor(doc);

                foreach (var def in fields)
                {
                    // Convert the string representation of the field type to the Aspose enum.
                    if (!Enum.TryParse(typeof(FieldType), def.FieldType, true, out var enumValue))
                    {
                        Console.Error.WriteLine($"Unsupported field type: {def.FieldType}");
                        continue;
                    }

                    // Ensure the target page exists; add blank pages if necessary.
                    while (doc.Pages.Count < def.Page)
                    {
                        doc.Pages.Add();
                    }

                    // Add the field to the document.
                    bool added = formEditor.AddField((FieldType)enumValue,
                                                     def.Name,
                                                     def.Page,
                                                     def.Llx,
                                                     def.Lly,
                                                     def.Urx,
                                                     def.Ury);
                    if (!added)
                    {
                        Console.Error.WriteLine($"Failed to add field: {def.Name}");
                    }
                }

                // Save the resulting PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
        }
    }
}