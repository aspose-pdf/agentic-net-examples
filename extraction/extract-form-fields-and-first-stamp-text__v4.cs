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
        const string inputPdf = "FormH.pdf";
        const string outputJson = "FormH_report.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Extract all form fields
            var formFields = new List<object>();
            foreach (Field field in doc.Form)
            {
                // Some fields may have null values; handle gracefully
                string value = field.Value?.ToString() ?? string.Empty;
                formFields.Add(new { Name = field.FullName, Value = value });
            }

            // Find the first stamp annotation and get its text (Contents)
            string firstStampText = null;
            bool stampFound = false;
            // Pages are 1‑based; iterate accordingly
            for (int i = 1; i <= doc.Pages.Count && !stampFound; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        firstStampText = stamp.Contents ?? string.Empty;
                        stampFound = true;
                        break;
                    }
                }
            }

            // Prepare the report object
            var report = new
            {
                FormFields = formFields,
                FirstStampText = firstStampText
            };

            // Serialize to JSON with indentation for readability
            string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Report generated: {outputJson}");
    }
}