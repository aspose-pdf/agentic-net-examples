using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";   // PDF to which fields will be added
        const string csvPath = "fields.csv";         // CSV containing field metadata
        const string outputPdfPath = "filled.pdf";   // Resulting PDF

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

        // Expected CSV header (comma‑separated):
        // Name,Type,Page,LLX,LLY,URX,URY,Required,ReadOnly
        // Example line:
        // CustomerName,TextBox,1,100,500,300,520,true,false

        try
        {
            using (Document doc = new Document(inputPdfPath))
            {
                // Read all lines, skip empty lines and the header row
                var csvLines = File.ReadAllLines(csvPath)
                                   .Where(l => !string.IsNullOrWhiteSpace(l))
                                   .Skip(1);

                foreach (var line in csvLines)
                {
                    var fields = line.Split(',')
                                     .Select(f => f.Trim())
                                     .ToArray();

                    if (fields.Length < 9)
                    {
                        Console.Error.WriteLine($"Invalid CSV line (expected 9 columns): {line}");
                        continue;
                    }

                    string fieldName = fields[0];
                    string fieldType = fields[1];
                    int pageNumber = int.Parse(fields[2]);          // 1‑based indexing
                    double llx = double.Parse(fields[3]);
                    double lly = double.Parse(fields[4]);
                    double urx = double.Parse(fields[5]);
                    double ury = double.Parse(fields[6]);
                    bool isRequired = bool.Parse(fields[7]);
                    bool isReadOnly = bool.Parse(fields[8]);

                    // Get the target page (Aspose.Pdf uses 1‑based indexing for Pages collection)
                    Page page = doc.Pages[pageNumber];

                    // Create rectangle for the field position
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Create the appropriate field based on the Type column
                    Field pdfField = null;

                    switch (fieldType.ToLowerInvariant())
                    {
                        case "textbox":
                        case "text":
                            pdfField = new TextBoxField(page, rect);
                            break;

                        case "checkbox":
                            pdfField = new CheckboxField(page, rect);
                            break;

                        case "radiobutton":
                            // RadioButtonField constructor expects only the Page object.
                            // The rectangle is assigned via the Rect property after construction.
                            var radio = new RadioButtonField(page);
                            radio.Rect = rect;
                            pdfField = radio;
                            break;

                        case "listbox":
                            pdfField = new ListBoxField(page, rect);
                            break;

                        case "combobox":
                            pdfField = new ComboBoxField(page, rect);
                            break;

                        case "signature":
                            pdfField = new SignatureField(page, rect);
                            break;

                        default:
                            Console.Error.WriteLine($"Unsupported field type '{fieldType}' for field '{fieldName}'. Skipping.");
                            continue;
                    }

                    // Set common properties
                    pdfField.Name = fieldName;          // Full name of the field
                    pdfField.Required = isRequired;    // Mark as required if needed
                    pdfField.ReadOnly = isReadOnly;    // Set read‑only flag

                    // Add the field to the form on the specified page
                    doc.Form.Add(pdfField, pageNumber);
                }

                // Save the modified PDF
                doc.Save(outputPdfPath);
                Console.WriteLine($"PDF with imported fields saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
