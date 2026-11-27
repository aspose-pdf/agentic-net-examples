using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Report
{
    // Initialise properties to avoid nullable warnings
    public Dictionary<string, string> FormFields { get; set; } = new Dictionary<string, string>();
    public string? FirstStampText { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPath = "FormC.pdf";
        const string outputPath = "FormC_report.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Report report = new Report();

        // Load PDF and extract data
        using (Document doc = new Document(inputPath))
        {
            // Extract all form fields
            var fields = new Dictionary<string, string>();
            foreach (Field field in doc.Form.Fields)
            {
                string name = field.FullName;
                string value = string.Empty;

                switch (field)
                {
                    case TextBoxField tb:
                        value = tb.Value;
                        break;
                    case CheckboxField cb:
                        value = cb.Checked.ToString();
                        break;
                    case RadioButtonField rb:
                        // RadioButtonField stores the selected option index (1‑based)
                        if (rb.Selected > 0 && rb.Options != null && rb.Selected <= rb.Options.Count)
                        {
                            // Retrieve the option name for readability
                            value = rb.Options[rb.Selected - 1].Name;
                        }
                        else
                        {
                            value = string.Empty;
                        }
                        break;
                    case ListBoxField lb:
                        // SelectedItems returns an array of selected indices (1‑based)
                        if (lb.SelectedItems != null && lb.SelectedItems.Length > 0)
                        {
                            var selectedNames = new List<string>();
                            foreach (int idx in lb.SelectedItems)
                            {
                                // idx is 1‑based
                                if (lb.Options != null && idx > 0 && idx <= lb.Options.Count)
                                    selectedNames.Add(lb.Options[idx - 1].Name);
                            }
                            value = string.Join(",", selectedNames);
                        }
                        break;
                    default:
                        // Fallback for other field types
                        value = field.Value?.ToString() ?? string.Empty;
                        break;
                }

                fields[name] = value;
            }
            report.FormFields = fields;

            // Extract the first stamp annotation text (if any)
            string? firstStampText = null;
            for (int i = 1; i <= doc.Pages.Count && firstStampText == null; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        // StampAnnotation does not expose a Text property; use the Icon name as a surrogate.
                        firstStampText = stamp.Icon.ToString();
                        break;
                    }
                }
            }
            report.FirstStampText = firstStampText;
        }

        // Serialize report to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(report, jsonOptions);
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Report saved to '{outputPath}'.");
    }
}
