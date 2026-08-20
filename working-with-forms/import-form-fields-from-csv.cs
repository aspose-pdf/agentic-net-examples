using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Simple DTO to hold CSV field metadata
    private class FieldMeta
    {
        public int PageNumber;          // 1‑based page index
        public string? FieldName;        // Full name of the field (nullable to satisfy compiler)
        public string? FieldType;        // e.g., "TextBox", "CheckBox", "RadioButton" (nullable)
        public double X;                // Lower‑left X coordinate
        public double Y;                // Lower‑left Y coordinate
        public double Width;            // Field width
        public double Height;           // Field height
        public string? DefaultValue;     // Initial value (optional, nullable)
        public bool ReadOnly;           // Read‑only flag
    }

    static void Main()
    {
        const string inputPdfPath = "template.pdf";   // PDF to which fields will be added
        const string csvPath      = "fields.csv";     // CSV containing field definitions
        const string outputPdfPath = "filled.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Parse CSV into a list of FieldMeta objects
        List<FieldMeta> fields = ParseCsv(csvPath);

        // Load the PDF, add fields, and save
        using (Document doc = new Document(inputPdfPath))
        {
            foreach (FieldMeta meta in fields)
            {
                // Ensure the page number is valid
                if (meta.PageNumber < 1 || meta.PageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {meta.PageNumber} for field {meta.FieldName}");
                    continue;
                }

                // Get the target page (Aspose.Pdf uses 1‑based indexing for Pages collection)
                Page page = doc.Pages[meta.PageNumber];

                // Define the rectangle for the field (lower‑left X,Y and upper‑right X+Width, Y+Height)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    meta.X,
                    meta.Y,
                    meta.X + meta.Width,
                    meta.Y + meta.Height);

                // Create the appropriate field based on the type string
                Field? field = CreateField(page, meta, rect);
                if (field == null)
                {
                    Console.Error.WriteLine($"Unsupported field type '{meta.FieldType}' for field {meta.FieldName}");
                    continue;
                }

                // Set common properties
                field.PartialName = meta.FieldName ?? string.Empty;
                field.ReadOnly = meta.ReadOnly;
                if (!string.IsNullOrEmpty(meta.DefaultValue))
                {
                    field.Value = meta.DefaultValue;
                }

                // Add the field to the form (the page is already bound via the constructor)
                doc.Form.Add(field, meta.PageNumber);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPdfPath}'.");
    }

    // Parses a CSV file where each line has:
    // PageNumber,FieldName,FieldType,X,Y,Width,Height,DefaultValue,ReadOnly
    private static List<FieldMeta> ParseCsv(string csvPath)
    {
        var list = new List<FieldMeta>();
        foreach (string line in File.ReadAllLines(csvPath))
        {
            // Skip empty lines or comments
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            string[] parts = line.Split(',');
            if (parts.Length < 8)
                continue; // malformed line

            FieldMeta meta = new FieldMeta
            {
                PageNumber   = int.Parse(parts[0].Trim()),
                FieldName    = parts[1].Trim(),
                FieldType    = parts[2].Trim(),
                X            = double.Parse(parts[3].Trim()),
                Y            = double.Parse(parts[4].Trim()),
                Width        = double.Parse(parts[5].Trim()),
                Height       = double.Parse(parts[6].Trim()),
                DefaultValue = parts[7].Trim(),
                ReadOnly     = parts.Length > 8 && bool.Parse(parts[8].Trim())
            };
            list.Add(meta);
        }
        return list;
    }

    // Factory method that creates a concrete Field instance based on the type string.
    // All Aspose.Pdf form fields are constructed with (Page, Rectangle).
    private static Field? CreateField(Page page, FieldMeta meta, Aspose.Pdf.Rectangle rect)
    {
        if (meta.FieldType == null)
            return null;

        switch (meta.FieldType.Trim().ToLowerInvariant())
        {
            case "textbox":
                return new TextBoxField(page, rect);
            case "checkbox":
                return new CheckboxField(page, rect);
            case "radiobutton":
                // RadioButtonField constructor expects only the Page; the rectangle is set via the widget.
                RadioButtonField radio = new RadioButtonField(page);
                radio.Rect = rect;
                return radio;
            case "listbox":
                return new ListBoxField(page, rect);
            case "combobox":
                return new ComboBoxField(page, rect);
            case "signature":
                return new SignatureField(page, rect);
            default:
                return null; // unsupported type
        }
    }
}
