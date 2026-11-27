using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class FormBReport
{
    public List<FormField> Fields { get; set; } = new();
    public string FirstStampText { get; set; } = string.Empty;
}

class FormField
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

class Program
{
    static void Main()
    {
        const string inputPath = "FormB.pdf";
        const string outputPath = "FormB_report.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        FormBReport report = new FormBReport();

        // Load PDF and extract data
        using (Document doc = new Document(inputPath))
        {
            // Extract form fields
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field field in doc.Form.Fields)
                {
                    report.Fields.Add(new FormField
                    {
                        Name = field.FullName,
                        Value = field.Value?.ToString() ?? string.Empty
                    });
                }
            }

            // Extract first stamp annotation text (use Contents property)
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is StampAnnotation stamp && !string.IsNullOrEmpty(stamp.Contents))
                    {
                        report.FirstStampText = stamp.Contents;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(report.FirstStampText))
                    break;
            }
        }

        // Serialize report to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(report, jsonOptions);
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Report generated: {outputPath}");
    }
}
