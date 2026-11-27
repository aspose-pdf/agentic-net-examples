using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms; // <-- required for form field types

class Program
{
    static void Main()
    {
        const string inputPath = "FormE.pdf";
        const string outputPath = "FormE_report.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Report report = new Report();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -------- Extract form fields ----------
            // doc.Form may be null if there are no form fields
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field field in doc.Form.Fields) // Field is from Aspose.Pdf.Forms
                {
                    FormFieldInfo info = new FormFieldInfo
                    {
                        Name = field.FullName,
                        Type = field.GetType().Name,
                        Value = GetFieldValue(field)
                    };
                    report.FormFields.Add(info);
                }
            }

            // -------- Extract first stamp annotation text ----------
            bool stampFound = false;
            // Pages are 1‑based; iterate accordingly
            for (int i = 1; i <= doc.Pages.Count && !stampFound; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        // Prefer Contents, fall back to Title
                        report.FirstStampText = stamp.Contents ?? stamp.Title ?? string.Empty;
                        stampFound = true;
                        break;
                    }
                }
            }
        }

        // Serialize the collected data to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(report, jsonOptions);
        File.WriteAllText(outputPath, json);
        Console.WriteLine($"Report saved to {outputPath}");
    }

    // Helper to read the value of different field types
    static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value ?? string.Empty;
            case CheckboxField chk:
                return chk.Checked.ToString();
            case RadioButtonField rad:
                // RadioButtonField does not expose a direct value; Selected returns the 1‑based index of the chosen option
                return rad.Selected > 0 ? rad.Selected.ToString() : string.Empty;
            case ListBoxField list:
                // SelectedItems returns a collection of selected indices (1‑based). Use the field's Value which concatenates selected export values.
                return list.Value ?? string.Empty;
            case ComboBoxField combo:
                // The selected value is exposed via the Value property.
                return combo.Value ?? string.Empty;
            default:
                return string.Empty;
        }
    }

    // DTO for JSON output
    class Report
    {
        public List<FormFieldInfo> FormFields { get; set; } = new List<FormFieldInfo>();
        public string FirstStampText { get; set; } = string.Empty;
    }

    class FormFieldInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
