using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormF.pdf";
        const string outputPath = "FormF_report.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Extract form fields
            var formFields = new List<Dictionary<string, string>>();
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field field in doc.Form.Fields)
                {
                    var entry = new Dictionary<string, string>
                    {
                        ["Name"] = field.Name,
                        ["Value"] = field.Value?.ToString() ?? string.Empty
                    };
                    formFields.Add(entry);
                }
            }

            // Extract the text of the first stamp annotation
            string firstStampText = null;
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based page indexing
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        // Stamp text may be stored in Title or Contents
                        firstStampText = stamp.Title ?? stamp.Contents;
                        break;
                    }
                }
                if (firstStampText != null)
                    break;
            }

            // Build the report object
            var report = new
            {
                FormFields = formFields,
                FirstStampText = firstStampText ?? string.Empty
            };

            // Serialize to JSON with indentation
            string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Report generated: {outputPath}");
    }
}