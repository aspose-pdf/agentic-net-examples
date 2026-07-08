using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Simple DTO for CSV rows – use auto‑properties with defaults to satisfy non‑nullable warnings
    private class FieldMetadata
    {
        public string Name { get; set; } = string.Empty;          // Full field name
        public string Type { get; set; } = string.Empty;          // e.g., TextBox, CheckBox, RadioButton, ComboBox, ListBox
        public int PageNumber { get; set; } = 1;                  // 1‑based page index
        public double Llx { get; set; }
        public double Lly { get; set; }
        public double Urx { get; set; }
        public double Ury { get; set; }
        public bool Required { get; set; }
        public bool ReadOnly { get; set; }
    }

    static void Main()
    {
        const string pdfPath   = "template.pdf";   // input PDF with (or without) existing form
        const string csvPath   = "fields.csv";     // CSV containing field definitions
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(pdfPath))
        {
            // Parse CSV into a list of FieldMetadata
            List<FieldMetadata> fields = ParseCsv(csvPath);

            // Create form fields according to metadata
            foreach (var meta in fields)
            {
                // Ensure the requested page exists
                if (meta.PageNumber < 1 || meta.PageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {meta.PageNumber} for field {meta.Name}");
                    continue;
                }

                Page page = doc.Pages[meta.PageNumber];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(meta.Llx, meta.Lly, meta.Urx, meta.Ury);

                // Create the appropriate field type
                Field? field = CreateField(meta, page, rect);
                if (field == null)
                {
                    Console.Error.WriteLine($"Unsupported field type '{meta.Type}' for field {meta.Name}");
                    continue;
                }

                // Common properties
                field.Name = meta.Name;               // Full name
                field.PartialName = meta.Name;        // For simplicity use same value
                field.Required = meta.Required;
                field.ReadOnly = meta.ReadOnly;

                // Add the field to the document form
                doc.Form.Add(field);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Form fields created and saved to '{outputPdf}'.");
    }

    // Parses a CSV file where each line has:
    // Name,Type,PageNumber,LLX,LLY,URX,URY,Required,ReadOnly
    private static List<FieldMetadata> ParseCsv(string csvPath)
    {
        var list = new List<FieldMetadata>();
        using (StreamReader reader = new StreamReader(csvPath))
        {
            string line;
            bool firstLine = true;
            while ((line = reader.ReadLine()) != null)
            {
                // Skip header row
                if (firstLine)
                {
                    firstLine = false;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');
                if (parts.Length < 9)
                    continue; // malformed line

                FieldMetadata meta = new FieldMetadata
                {
                    Name       = parts[0].Trim(),
                    Type       = parts[1].Trim(),
                    PageNumber = int.Parse(parts[2].Trim()),
                    Llx        = double.Parse(parts[3].Trim()),
                    Lly        = double.Parse(parts[4].Trim()),
                    Urx        = double.Parse(parts[5].Trim()),
                    Ury        = double.Parse(parts[6].Trim()),
                    Required   = bool.Parse(parts[7].Trim()),
                    ReadOnly   = bool.Parse(parts[8].Trim())
                };
                list.Add(meta);
            }
        }
        return list;
    }

    // Factory method that creates a concrete Field based on the type string
    private static Field? CreateField(FieldMetadata meta, Page page, Aspose.Pdf.Rectangle rect)
    {
        switch (meta.Type.ToLowerInvariant())
        {
            case "textbox":
                return new TextBoxField(page, rect);
            case "checkbox":
                return new CheckboxField(page, rect);
            case "radiobutton":
                // RadioButtonField constructor takes only the page; rectangle is set via the Rect property
                var radio = new RadioButtonField(page);
                radio.Rect = rect;
                return radio;
            case "combobox":
                return new ComboBoxField(page, rect);
            case "listbox":
                return new ListBoxField(page, rect);
            case "signature":
                return new SignatureField(page, rect);
            // Add more field types as needed
            default:
                return null; // unsupported type
        }
    }
}
