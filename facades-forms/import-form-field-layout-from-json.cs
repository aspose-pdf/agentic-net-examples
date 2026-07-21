using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfFormImportExample
{
    // Represents a single field definition from the JSON layout.
    public class FieldDefinition
    {
        public string FieldName { get; set; }          // Fully qualified field name
        public string FieldType { get; set; }          // e.g., "Text", "CheckBox", "Radio", etc.
        public int PageNumber { get; set; }            // 1‑based page index
        public float Llx { get; set; }                 // Lower‑left X
        public float Lly { get; set; }                 // Lower‑left Y
        public float Urx { get; set; }                 // Upper‑right X
        public float Ury { get; set; }                 // Upper‑right Y
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath   = "fieldLayout.json";   // JSON describing fields
            const string outputPdf  = "output.pdf";

            // Load field definitions from JSON.
            List<FieldDefinition> fields = LoadFieldDefinitions(jsonPath);

            // Create a new blank PDF with a single page (add more pages if needed).
            using (Document doc = new Document())
            {
                // Ensure at least one page exists.
                doc.Pages.Add();

                // Initialize FormEditor and bind it to the newly created document.
                FormEditor formEditor = new FormEditor();
                formEditor.BindPdf(doc);

                // Add each field according to the JSON layout.
                foreach (FieldDefinition f in fields)
                {
                    // Convert string representation to the FieldType enum.
                    if (!Enum.TryParse(f.FieldType, true, out FieldType fieldType))
                    {
                        Console.WriteLine($"Unsupported field type: {f.FieldType}");
                        continue;
                    }

                    // Add the field. The AddField method returns true on success.
                    bool added = formEditor.AddField(
                        fieldType,
                        f.FieldName,
                        f.PageNumber,
                        f.Llx,
                        f.Lly,
                        f.Urx,
                        f.Ury);

                    if (!added)
                    {
                        Console.WriteLine($"Failed to add field: {f.FieldName}");
                    }
                }

                // Save the resulting PDF.
                formEditor.Save(outputPdf);
            }

            Console.WriteLine($"PDF with imported fields saved to '{outputPdf}'.");
        }

        // Reads the JSON file and deserializes it into a list of FieldDefinition objects.
        private static List<FieldDefinition> LoadFieldDefinitions(string path)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"JSON file not found: {path}");
                return new List<FieldDefinition>();
            }

            string jsonContent = File.ReadAllText(path);
            JsonSerializerOptions options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                return JsonSerializer.Deserialize<List<FieldDefinition>>(jsonContent, options) ?? new List<FieldDefinition>();
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"Error parsing JSON: {ex.Message}");
                return new List<FieldDefinition>();
            }
        }
    }
}