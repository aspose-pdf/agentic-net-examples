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
        const string inputPdfPath = "FormG.pdf";
        const string outputJsonPath = "FormG_report.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Extract all form fields
            var formFields = new List<Dictionary<string, string>>();
            foreach (Field field in doc.Form.Fields) // Field is the correct base type
            {
                string value = string.Empty;
                // Resolve the actual value based on concrete field type
                switch (field)
                {
                    case TextBoxField txt:
                        value = txt.Value?.ToString() ?? string.Empty;
                        break;
                    case CheckboxField cb:
                        value = cb.Checked ? "Checked" : "Unchecked";
                        break;
                    case RadioButtonField rb:
                        // Selected is 1‑based index of the chosen option; 0 means none selected
                        value = rb.Selected > 0 ? rb.Selected.ToString() : string.Empty;
                        break;
                    case ComboBoxField combo:
                        value = combo.Value?.ToString() ?? string.Empty;
                        break;
                    case ListBoxField list:
                        value = list.Value?.ToString() ?? string.Empty;
                        break;
                    case SignatureField sig:
                        // Signature fields do not expose a simple string value; leave empty or add custom info
                        value = string.Empty;
                        break;
                    default:
                        // For any other field types, attempt a generic Value property via dynamic (may be null)
                        try
                        {
                            var dyn = field as dynamic;
                            value = dyn.Value?.ToString() ?? string.Empty;
                        }
                        catch { /* ignore */ }
                        break;
                }

                var fieldInfo = new Dictionary<string, string>
                {
                    { "Name", field.FullName },
                    { "Value", value },
                    { "Type", field.GetType().Name }
                };
                formFields.Add(fieldInfo);
            }

            // Find the first Stamp annotation (if any) and get its text
            string firstStampText = null;
            // Pages are 1‑based; iterate accordingly
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is StampAnnotation stamp)
                    {
                        firstStampText = stamp.Contents;
                        break;
                    }
                }
                if (firstStampText != null)
                    break;
            }

            // Prepare the report object
            var report = new
            {
                FormFields = formFields,
                FirstStampText = firstStampText
            };

            // Serialize to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(report, jsonOptions);

            // Write the JSON to the output file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Report generated: {outputJsonPath}");
        }
    }
}
