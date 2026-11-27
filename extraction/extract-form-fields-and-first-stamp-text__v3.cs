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
        const string inputPath = "FormD.pdf";
        const string outputPath = "FormD_report.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -------- Extract form fields --------
            var fieldNames = new List<string>();
            if (doc.Form != null && doc.Form.Count > 0)
            {
                foreach (Field field in doc.Form)
                {
                    // Use the fully qualified name of each field
                    fieldNames.Add(field.FullName);
                }
            }

            // -------- Extract first stamp text (TextAnnotation) --------
            string firstStampText = string.Empty;
            // Pages are 1‑based; iterate until a stamp is found
            for (int i = 1; i <= doc.Pages.Count && string.IsNullOrEmpty(firstStampText); i++)
            {
                Page page = doc.Pages[i];
                if (page.Annotations != null && page.Annotations.Count > 0)
                {
                    foreach (Annotation annot in page.Annotations)
                    {
                        if (annot is TextAnnotation textAnnot)
                        {
                            firstStampText = textAnnot.Contents ?? string.Empty;
                            break;
                        }
                    }
                }
            }

            // -------- Build report object --------
            var report = new
            {
                FormFields = fieldNames,
                FirstStampText = firstStampText
            };

            // Serialize to pretty‑printed JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(report, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Report saved to '{outputPath}'.");
    }
}