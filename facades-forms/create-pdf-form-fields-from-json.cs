using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfFormFromJson
{
    // Represents a field definition read from the JSON file
    public class FieldDefinition
    {
        public string Name { get; set; }          // Full field name
        public string Type { get; set; }          // e.g., "Text", "CheckBox", "Radio", "ListBox", "ComboBox"
        public int Page { get; set; }             // 1‑based page number
        public float Llx { get; set; }            // Lower‑left X
        public float Lly { get; set; }            // Lower‑left Y
        public float Urx { get; set; }            // Upper‑right X
        public float Ury { get; set; }            // Upper‑right Y
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath   = "fields.json";          // Input JSON with field definitions
            const string outputPath = "output.pdf";           // Resulting PDF file

            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            // Deserialize JSON into a list of field definitions
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

            // Create a new PDF document and add a blank page for each distinct page number used
            using (Document doc = new Document())
            {
                // Determine the highest page number required
                int maxPage = 0;
                foreach (var f in fields)
                    if (f.Page > maxPage) maxPage = f.Page;

                // Ensure the document has enough pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= maxPage; i++)
                    doc.Pages.Add();

                // Use FormEditor (Facades API) to add fields to the document
                using (FormEditor formEditor = new FormEditor(doc))
                {
                    foreach (var f in fields)
                    {
                        // Map string type to FieldType enum; default to Text if unknown
                        FieldType fieldType = f.Type?.ToLowerInvariant() switch
                        {
                            "textbox"   => FieldType.Text,
                            "text"      => FieldType.Text,
                            "checkbox"  => FieldType.CheckBox,
                            "check"     => FieldType.CheckBox,
                            "radio"     => FieldType.Radio,
                            "listbox"   => FieldType.ListBox,
                            "combobox"  => FieldType.ComboBox,
                            _ => FieldType.Text
                        };

                        // Add the field; AddField returns bool indicating success
                        bool added = formEditor.AddField(fieldType,
                                                         f.Name,
                                                         f.Page,
                                                         f.Llx,
                                                         f.Lly,
                                                         f.Urx,
                                                         f.Ury);
                        if (!added)
                        {
                            Console.Error.WriteLine($"Failed to add field '{f.Name}' on page {f.Page}");
                        }
                    }

                    // No explicit Save on FormEditor; saving the underlying Document persists changes
                }

                // Save the resulting PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
        }
    }
}