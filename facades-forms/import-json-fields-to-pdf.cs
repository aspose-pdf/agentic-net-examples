using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfFormFromJson
{
    // Represents a field definition read from the JSON file.
    public class FieldDefinition
    {
        public string FieldType { get; set; }   // e.g., "Text", "CheckBox", "Radio", etc.
        public string Name { get; set; }        // Full field name.
        public int Page { get; set; }           // 1‑based page number.
        public float Llx { get; set; }          // Lower‑left X.
        public float Lly { get; set; }          // Lower‑left Y.
        public float Urx { get; set; }          // Upper‑right X.
        public float Ury { get; set; }          // Upper‑right Y.
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath   = "fields.json";          // Input JSON with field definitions.
            const string outputPath = "output.pdf";           // Result PDF file.

            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            // Deserialize the JSON into a list of field definitions.
            List<FieldDefinition> fields;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                fields = JsonSerializer.Deserialize<List<FieldDefinition>>(jsonContent);
                if (fields == null)
                {
                    Console.Error.WriteLine("No field definitions found in JSON.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read/parse JSON: {ex.Message}");
                return;
            }

            // Create a new blank PDF document with a single page.
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // Adds the first (and only) page.

                // Use FormEditor (Facades API) to add fields to the document.
                using (FormEditor formEditor = new FormEditor(doc))
                {
                    foreach (var def in fields)
                    {
                        // Convert the string representation of the field type to the enum.
                        if (!Enum.TryParse<Aspose.Pdf.Facades.FieldType>(def.FieldType, true, out var fieldType))
                        {
                            Console.Error.WriteLine($"Unsupported field type: {def.FieldType}");
                            continue;
                        }

                        // Add the field to the specified page and rectangle.
                        bool added = formEditor.AddField(
                            fieldType,
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

                    // Save the modified document directly via the FormEditor facade.
                    formEditor.Save(outputPath);
                }

                // The document is already saved by FormEditor; the following line is optional.
                // doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
        }
    }
}