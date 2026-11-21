using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // needed for form field classes

namespace PdfFieldLayoutImport
{
    // Represents a single field definition from the JSON layout file
    public class FieldDefinition
    {
        public required string Name { get; set; }          // Field name (unique identifier)
        public required string Type { get; set; }          // Field type, e.g., "TextBox", "CheckBox", "RadioButton"
        public int Page { get; set; }                     // 1‑based page number where the field should appear
        public double Llx { get; set; }                    // Lower‑left X coordinate (points)
        public double Lly { get; set; }                    // Lower‑left Y coordinate (points)
        public double Urx { get; set; }                    // Upper‑right X coordinate (points)
        public double Ury { get; set; }                    // Upper‑right Y coordinate (points)
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath   = "fieldLayout.json";   // Input JSON describing fields
            const string outputPath = "output.pdf";         // Resulting PDF with positioned fields

            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"JSON layout file not found: {jsonPath}");
                return;
            }

            // Deserialize JSON into a list of field definitions
            List<FieldDefinition> fields;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                fields = JsonSerializer.Deserialize<List<FieldDefinition>>(jsonContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (fields == null) throw new InvalidOperationException("Deserialized field list is null.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read or parse JSON: {ex.Message}");
                return;
            }

            // Create a new PDF document and add enough blank pages.
            using (Document pdfDoc = new Document())
            {
                int maxPage = 0;
                foreach (var f in fields) if (f.Page > maxPage) maxPage = f.Page;
                for (int i = 1; i <= maxPage; i++) pdfDoc.Pages.Add();

                // Iterate over the field definitions and add the appropriate form field.
                foreach (var field in fields)
                {
                    // Ensure the target page exists (Aspose pages are 1‑based).
                    Page targetPage = pdfDoc.Pages[field.Page];

                    // Build the rectangle for the field.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                        field.Llx, field.Lly, field.Urx, field.Ury);

                    switch (field.Type?.Trim().ToLowerInvariant())
                    {
                        case "textbox":
                            var txt = new TextBoxField(targetPage, rect) { PartialName = field.Name };
                            pdfDoc.Form.Add(txt, field.Page);
                            break;

                        case "checkbox":
                            var chk = new CheckboxField(targetPage, rect) { PartialName = field.Name };
                            pdfDoc.Form.Add(chk, field.Page);
                            break;

                        case "radiobutton":
                            var rad = new RadioButtonField(targetPage) { PartialName = field.Name };
                            // For a simple radio button without options we still need a rectangle.
                            // Aspose.Pdf requires options to be added; here we add a single option that
                            // occupies the same rectangle as the field definition.
                            rad.AddOption(field.Name, rect);
                            pdfDoc.Form.Add(rad, field.Page);
                            break;

                        default:
                            Console.WriteLine($"Unsupported field type '{field.Type}' for field '{field.Name}'. Skipping.");
                            break;
                    }
                }

                // Save the resulting PDF.
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
        }
    }
}
