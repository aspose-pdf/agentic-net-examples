using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class FieldDefinition
{
    // Mark properties as required to satisfy non‑nullable warnings (C# 11+)
    public required string Name { get; set; }
    public required string Type { get; set; }          // "TextBox", "CheckBox", "RadioButton", etc.
    public int Page { get; set; }                     // 1‑based page number
    public double Llx { get; set; }                   // lower‑left X
    public double Lly { get; set; }                   // lower‑left Y
    public double Urx { get; set; }                   // upper‑right X
    public double Ury { get; set; }                   // upper‑right Y
}

class Program
{
    static void Main()
    {
        const string jsonPath   = "layout.json";
        const string outputPath = "output.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON layout file not found: {jsonPath}");
            return;
        }

        // Deserialize field layout
        List<FieldDefinition>? maybeFields;
        try
        {
            string json = File.ReadAllText(jsonPath);
            maybeFields = JsonSerializer.Deserialize<List<FieldDefinition>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        if (maybeFields == null)
        {
            Console.Error.WriteLine("JSON deserialization returned null.");
            return;
        }

        List<FieldDefinition> fields = maybeFields;

        // Create a new PDF document with enough pages
        using (Document doc = new Document())
        {
            // Determine the highest page number required
            int maxPage = 0;
            foreach (var f in fields)
                if (f.Page > maxPage) maxPage = f.Page;

            // Add blank pages as needed (Aspose.Pdf starts with one page by default)
            while (doc.Pages.Count < maxPage)
                doc.Pages.Add();

            foreach (var field in fields)
            {
                // Build rectangle for the field
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(field.Llx, field.Lly, field.Urx, field.Ury);

                // Create the appropriate form field and add it to the document form
                switch (field.Type?.Trim().ToLowerInvariant())
                {
                    case "textbox":
                        var txt = new TextBoxField(doc.Pages[field.Page], rect)
                        {
                            PartialName = field.Name
                        };
                        doc.Form.Add(txt, field.Page);
                        break;

                    case "checkbox":
                        // Correct class name is CheckboxField (lowercase 'b')
                        var chk = new CheckboxField(doc.Pages[field.Page], rect)
                        {
                            PartialName = field.Name
                        };
                        doc.Form.Add(chk, field.Page);
                        break;

                    case "radiobutton":
                        // RadioButtonField constructor takes only the Page; set rectangle afterwards
                        var rad = new RadioButtonField(doc.Pages[field.Page])
                        {
                            PartialName = field.Name,
                            Rect = rect
                        };
                        doc.Form.Add(rad, field.Page);
                        break;

                    case "listbox":
                        var lst = new ListBoxField(doc.Pages[field.Page], rect)
                        {
                            PartialName = field.Name
                        };
                        doc.Form.Add(lst, field.Page);
                        break;

                    case "combobox":
                        var cmb = new ComboBoxField(doc.Pages[field.Page], rect)
                        {
                            PartialName = field.Name
                        };
                        doc.Form.Add(cmb, field.Page);
                        break;

                    default:
                        Console.WriteLine($"Unsupported field type '{field.Type}' for field '{field.Name}'. Skipping.");
                        break;
                }
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form fields saved to '{outputPath}'.");
    }
}
