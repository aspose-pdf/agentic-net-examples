using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF with no form fields
        const string csvPath = "fields.csv";      // CSV containing field metadata
        const string outputPdfPath = "output.pdf"; // PDF with generated form fields

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Read CSV line by line (skip header if present)
            using (StreamReader reader = new StreamReader(csvPath))
            {
                bool firstLine = true;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Optional: skip header row that contains column names
                    if (firstLine && line.StartsWith("FieldName", StringComparison.OrdinalIgnoreCase))
                    {
                        firstLine = false;
                        continue;
                    }

                    // Expected CSV format:
                    // FieldName,FieldType,PageNumber,LLX,LLY,URX,URY,Required,ReadOnly
                    string[] parts = line.Split(',');

                    if (parts.Length < 9)
                    {
                        Console.Error.WriteLine($"Invalid CSV line (expected 9 columns): {line}");
                        continue;
                    }

                    string fieldName = parts[0].Trim();
                    string fieldType = parts[1].Trim(); // e.g., TextBox, CheckBox, RadioButton, etc.

                    if (!int.TryParse(parts[2].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int pageNumber) || pageNumber < 1)
                    {
                        Console.Error.WriteLine($"Invalid page number for field '{fieldName}': {parts[2]}");
                        continue;
                    }

                    // Parse rectangle coordinates (LLX, LLY, URX, URY) individually to avoid short‑circuit issues
                    bool llxOk = double.TryParse(parts[3].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double llx);
                    bool llyOk = double.TryParse(parts[4].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lly);
                    bool urxOk = double.TryParse(parts[5].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double urx);
                    bool uryOk = double.TryParse(parts[6].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double ury);
                    if (!llxOk || !llyOk || !urxOk || !uryOk)
                    {
                        Console.Error.WriteLine($"Invalid rectangle for field '{fieldName}': {line}");
                        continue;
                    }

                    bool required = bool.TryParse(parts[7].Trim(), out bool req) && req;
                    bool readOnly = bool.TryParse(parts[8].Trim(), out bool ro) && ro;

                    // Get the target page (Aspose uses 1‑based indexing)
                    Page page = doc.Pages[pageNumber];

                    // Create the appropriate field instance based on the type identifier
                    Field field = CreateFieldByType(page, fieldType, llx, lly, urx, ury);
                    if (field == null)
                    {
                        Console.Error.WriteLine($"Unsupported or unavailable field type '{fieldType}' for field '{fieldName}'.");
                        continue;
                    }

                    // Set common properties
                    field.Name = fieldName;               // full name
                    field.PartialName = fieldName;        // partial name (same as full for simple cases)
                    field.Required = required;
                    field.ReadOnly = readOnly;

                    // Add the field to the form on the specified page
                    doc.Form.Add(field, pageNumber);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdfPath}'.");
    }

    // Factory method that creates a concrete Field based on a textual type identifier.
    // For fields that require a Page + Rectangle constructor we use those; for others we fall back to the Document‑based constructor.
    private static Field CreateFieldByType(Page page, string fieldType, double llx, double lly, double urx, double ury)
    {
        Rectangle rect = new Rectangle(llx, lly, urx, ury);
        switch (fieldType.Trim().ToLowerInvariant())
        {
            case "textbox":
                // TextBoxField has a (Page, Rectangle) ctor – use it directly.
                return new TextBoxField(page, rect);

            case "checkbox":
                return new CheckboxField(page, rect);

            case "radiobutton":
                // RadioButtonField does not accept a rectangle in its ctor; set Rect afterwards.
                RadioButtonField radio = new RadioButtonField(page);
                radio.Rect = rect;
                return radio;

            case "listbox":
                // ListBoxField only has a (Page, Rectangle) ctor.
                return new ListBoxField(page, rect);

            case "combobox":
                return new ComboBoxField(page, rect);

            case "signature":
                return new SignatureField(page, rect);

            case "fileselectbox":
                // FileSelectBoxField has no public ctor – use a TextBoxField as a placeholder.
                return new TextBoxField(page, rect);

            default:
                return null;
        }
    }
}
