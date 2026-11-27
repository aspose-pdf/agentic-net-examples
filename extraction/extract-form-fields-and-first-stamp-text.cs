using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormA.pdf";
        const string outputPath = "FormA_report.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Extract all form fields using the proper Form.Fields collection
            var fields = new Dictionary<string, string>();
            foreach (Field field in doc.Form.Fields)
            {
                // Prefer FullName, then PartialName, then Name
                string name = field.FullName ?? field.PartialName ?? field.Name ?? string.Empty;
                string value = field.Value?.ToString() ?? string.Empty;
                fields[name] = value;
            }

            // Find the first stamp annotation and retrieve its text
            string firstStampText = null;
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based page indexing
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        firstStampText = stamp.Contents;
                        break;
                    }
                }
                if (firstStampText != null) break;
            }

            // Build the report object
            var report = new
            {
                FormFields = fields,
                FirstStampText = firstStampText
            };

            // Serialize the report to formatted JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(report, jsonOptions);

            // Write the JSON to the output file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Report generated: {outputPath}");
    }
}
