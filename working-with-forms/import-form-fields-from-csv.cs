using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";   // PDF that will receive the fields
        const string csvPath = "fields.csv";          // CSV containing field metadata
        const string outputPdfPath = "filled.pdf";    // Resulting PDF

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

        // Load the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Expected CSV header:
            // Name,Type,Page,LLX,LLY,URX,URY,Value,ReadOnly,Required
            string[] lines = File.ReadAllLines(csvPath);
            for (int i = 1; i < lines.Length; i++) // skip header line
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 9)
                    continue; // insufficient data, skip

                string fieldName   = parts[0].Trim();
                string fieldType   = parts[1].Trim().ToLower(); // textbox, checkbox, radiobutton, etc.
                int    pageNumber  = int.Parse(parts[2].Trim()); // 1‑based indexing (page-indexing-one-based rule)
                double llx         = double.Parse(parts[3].Trim());
                double lly         = double.Parse(parts[4].Trim());
                double urx         = double.Parse(parts[5].Trim());
                double ury         = double.Parse(parts[6].Trim());
                string value       = parts[7].Trim();
                bool   readOnly    = bool.Parse(parts[8].Trim());
                bool   required    = parts.Length > 9 ? bool.Parse(parts[9].Trim()) : false;

                // Get the target page (Aspose pages collection is 1‑based)
                Page page = doc.Pages[pageNumber];
                // Define the field rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                Field field = null;
                switch (fieldType)
                {
                    case "textbox":
                        field = new TextBoxField(page, rect);
                        break;
                    case "checkbox":
                        field = new CheckboxField(page, rect);
                        break;
                    case "radiobutton":
                        // RadioButtonField constructor accepts only a Page object.
                        // Rectangle is assigned via the Rect property after construction.
                        var radio = new RadioButtonField(page);
                        radio.Rect = rect;
                        field = radio;
                        break;
                    case "listbox":
                        field = new ListBoxField(page, rect);
                        break;
                    case "combobox":
                        field = new ComboBoxField(page, rect);
                        break;
                    default:
                        Console.WriteLine($"Unsupported field type '{fieldType}' on line {i + 1}");
                        continue;
                }

                // Set common properties
                field.PartialName = fieldName; // field name within the form
                field.Value       = value;     // initial value
                field.ReadOnly    = readOnly;
                field.Required    = required;

                // Add the field to the document's form
                doc.Form.Add(field);
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPdfPath}'.");
    }
}
