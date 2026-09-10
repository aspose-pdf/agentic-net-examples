using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "stamps.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            var stampTexts = new List<string>();

            // Iterate over all pages and their annotations
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    // StampAnnotation represents rubber‑stamp annotations
                    if (annotation is StampAnnotation stamp)
                    {
                        // The visible text of a stamp is stored in the Contents property
                        if (!string.IsNullOrEmpty(stamp.Contents))
                        {
                            stampTexts.Add(stamp.Contents);
                        }
                    }
                }
            }

            // Serialize the collected strings to a JSON array
            string json = JsonSerializer.Serialize(stampTexts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);

            Console.WriteLine($"Extracted {stampTexts.Count} stamp text(s) to '{outputJsonPath}'.");
        }
    }
}