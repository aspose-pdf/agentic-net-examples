using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string csvPath = "fields.csv";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Process each non‑empty line of the CSV file
            foreach (var line in File.ReadLines(csvPath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue; // skip blanks/comments

                // Expected CSV columns:
                // Name,Type,Page,LLX,LLY,URX,URY,Required,ReadOnly
                var parts = line.Split(',');

                if (parts.Length < 7)
                    continue; // not enough data

                string fieldName = parts[0].Trim();
                string fieldType = parts[1].Trim().ToLowerInvariant();
                int pageNumber = int.Parse(parts[2].Trim()); // 1‑based indexing
                double llx = double.Parse(parts[3].Trim());
                double lly = double.Parse(parts[4].Trim());
                double urx = double.Parse(parts[5].Trim());
                double ury = double.Parse(parts[6].Trim());

                // Optional boolean flags
                bool required = parts.Length > 7 && bool.Parse(parts[7].Trim());
                bool readOnly = parts.Length > 8 && bool.Parse(parts[8].Trim());

                // Retrieve the target page (Aspose.Pdf uses 1‑based page indexing)
                Page page = doc.Pages[pageNumber];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the appropriate form field instance
                Field field = CreateField(page, rect, fieldType);
                if (field == null)
                    continue; // unsupported field type

                // Set common field properties
                field.PartialName = fieldName;
                field.Required = required;
                field.ReadOnly = readOnly;

                // Add the field to the document's form on the specified page
                doc.Form.Add(field, pageNumber);
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdf}'.");
    }

    // Factory method that returns a concrete Field based on the requested type
    static Field CreateField(Page page, Aspose.Pdf.Rectangle rect, string type)
    {
        switch (type)
        {
            case "textbox":
                // TextBoxField has a (Page, Rectangle) ctor
                return new TextBoxField(page, rect);
            case "checkbox":
                // Correct class name is CheckboxField (lowercase 'b')
                return new CheckboxField(page, rect);
            case "radiobutton":
                // RadioButtonField only accepts a Page in its ctor; set the rectangle afterwards
                var radio = new RadioButtonField(page);
                radio.Rect = rect;
                return radio;
            case "listbox":
                return new ListBoxField(page, rect);
            case "combobox":
                return new ComboBoxField(page, rect);
            default:
                // Unsupported or unknown field type
                Console.Error.WriteLine($"Unsupported field type: {type}");
                return null; // returning null is intentional for unsupported types
        }
    }
}
