using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";   // PDF to add fields to
        const string csvPath = "fields.csv";          // CSV with field metadata
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the AcroForm instance exists (null‑forgiving operator for nullable warnings)
            var form = doc.Form!;

            // Read CSV line by line
            using (StreamReader reader = new StreamReader(csvPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Expected CSV columns:
                    // Name,Type,Page,LLX,LLY,URX,URY,Value
                    // Example: CustomerName,TextBox,1,100,700,300,720,John Doe
                    var parts = line.Split(',');

                    if (parts.Length < 7)
                    {
                        Console.Error.WriteLine($"Invalid CSV line (not enough columns): {line}");
                        continue;
                    }

                    string fieldName = parts[0].Trim();
                    string fieldType = parts[1].Trim(); // e.g., TextBox, CheckBox
                    if (!int.TryParse(parts[2].Trim(), out int pageNumber) || pageNumber < 1)
                    {
                        Console.Error.WriteLine($"Invalid page number in line: {line}");
                        continue;
                    }

                    // Parse rectangle coordinates (all values are in points)
                    if (!double.TryParse(parts[3].Trim(), out double llx) ||
                        !double.TryParse(parts[4].Trim(), out double lly) ||
                        !double.TryParse(parts[5].Trim(), out double urx) ||
                        !double.TryParse(parts[6].Trim(), out double ury))
                    {
                        Console.Error.WriteLine($"Invalid rectangle coordinates in line: {line}");
                        continue;
                    }

                    // Optional value (may be missing)
                    string fieldValue = parts.Length > 7 ? parts[7].Trim() : string.Empty;

                    // Ensure the requested page exists
                    if (pageNumber > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
                        continue;
                    }

                    // Create the appropriate form field
                    Field field = null;
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    switch (fieldType.ToLowerInvariant())
                    {
                        case "textbox":
                            // Constructors expect a Document, not a Form
                            TextBoxField txt = new TextBoxField(doc);
                            txt.PartialName = fieldName;
                            txt.Rect = rect;
                            txt.Value = fieldValue;
                            field = txt;
                            break;

                        case "checkbox":
                            CheckboxField chk = new CheckboxField(doc);
                            chk.PartialName = fieldName;
                            chk.Rect = rect;
                            // For checkboxes, value "On" means checked, anything else means unchecked
                            chk.Checked = string.Equals(fieldValue, "On", StringComparison.OrdinalIgnoreCase);
                            field = chk;
                            break;

                        // Additional field types can be added here (e.g., RadioButton, ListBox, ComboBox)

                        default:
                            Console.Error.WriteLine($"Unsupported field type '{fieldType}' in line: {line}");
                            continue;
                    }

                    // Add the field to the form
                    form.Add(field);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields imported and PDF saved to '{outputPdfPath}'.");
    }
}
