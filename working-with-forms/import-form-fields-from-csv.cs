using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string csvPath = "fields.csv";
        const string outputPdf = "output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // CSV format: Name,Type,Page,LLX,LLY,URX,URY,Value,Color (Color optional)
        var fieldDefs = new List<FieldDefinition>();
        foreach (var line in File.ReadLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
            var parts = line.Split(',');
            if (parts.Length < 8) continue; // insufficient data

            FieldDefinition def = new FieldDefinition
            {
                Name = parts[0].Trim(),
                Type = parts[1].Trim(),
                PageNumber = int.Parse(parts[2].Trim()),
                Rect = new Aspose.Pdf.Rectangle(
                    double.Parse(parts[3].Trim(), CultureInfo.InvariantCulture),
                    double.Parse(parts[4].Trim(), CultureInfo.InvariantCulture),
                    double.Parse(parts[5].Trim(), CultureInfo.InvariantCulture),
                    double.Parse(parts[6].Trim(), CultureInfo.InvariantCulture)),
                Value = parts[7].Trim(),
                Color = parts.Length > 8 ? parts[8].Trim() : null
            };
            fieldDefs.Add(def);
        }

        // Create a new PDF and ensure required pages exist
        using (Document doc = new Document())
        {
            int maxPage = 0;
            foreach (var f in fieldDefs) if (f.PageNumber > maxPage) maxPage = f.PageNumber;
            for (int i = 1; i <= maxPage; i++) doc.Pages.Add();

            foreach (var f in fieldDefs)
            {
                Page page = doc.Pages[f.PageNumber]; // 1‑based indexing

                switch (f.Type.ToLower())
                {
                    case "textbox":
                        TextBoxField tb = new TextBoxField(page, f.Rect);
                        tb.PartialName = f.Name;
                        tb.Value = f.Value;
                        // BackgroundColor is not available on TextBoxField in core API; omitted.
                        doc.Form.Add(tb);
                        break;

                    case "checkbox":
                        CheckboxField cb = new CheckboxField(page, f.Rect);
                        cb.PartialName = f.Name;
                        cb.Checked = f.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
                        doc.Form.Add(cb);
                        break;

                    case "radiobutton":
                        // Create a radio button option field. Checked state is managed by the group,
                        // so we only set the option name here.
                        RadioButtonOptionField rb = new RadioButtonOptionField(page, f.Rect);
                        rb.PartialName = f.Name;
                        rb.OptionName = f.Name; // use the field name as the option identifier
                        doc.Form.Add(rb);
                        break;

                    case "combobox":
                        ComboBoxField combo = new ComboBoxField(page, f.Rect);
                        combo.PartialName = f.Name;
                        // Expect items separated by '|'
                        var items = f.Value.Split('|');
                        foreach (var item in items)
                        {
                            combo.AddOption(item.Trim());
                        }
                        doc.Form.Add(combo);
                        break;

                    default:
                        Console.WriteLine($"Unsupported field type: {f.Type}");
                        break;
                }
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with form fields saved to '{outputPdf}'.");
    }

    // Convert simple color names or hex strings to Aspose.Pdf.Color (kept for potential future use)
    static Aspose.Pdf.Color ParseColor(string colorStr)
    {
        if (string.IsNullOrWhiteSpace(colorStr)) return Aspose.Pdf.Color.Transparent;

        if (colorStr.StartsWith("#"))
        {
            var hex = colorStr.Substring(1);
            if (hex.Length == 6 &&
                int.TryParse(hex.Substring(0, 2), NumberStyles.HexNumber, null, out int r) &&
                int.TryParse(hex.Substring(2, 2), NumberStyles.HexNumber, null, out int g) &&
                int.TryParse(hex.Substring(4, 2), NumberStyles.HexNumber, null, out int b))
            {
                return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
            }
        }

        return colorStr.ToLower() switch
        {
            "red" => Aspose.Pdf.Color.Red,
            "green" => Aspose.Pdf.Color.Green,
            "blue" => Aspose.Pdf.Color.Blue,
            "lightgray" => Aspose.Pdf.Color.LightGray,
            _ => Aspose.Pdf.Color.Transparent
        };
    }

    // DTO representing a CSV row
    class FieldDefinition
    {
        public string Name;
        public string Type;
        public int PageNumber;
        public Aspose.Pdf.Rectangle Rect;
        public string Value;
        public string Color;
    }
}
