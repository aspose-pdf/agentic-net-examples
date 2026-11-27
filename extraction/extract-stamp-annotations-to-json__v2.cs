using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "FormC.pdf";
        const string outputPath = "FormC_allStamps.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Collect all stamp annotation texts
        List<string> stampTexts = new List<string>();

        // Document must be disposed deterministically
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        // The textual content of a stamp annotation is stored in the "Contents" property.
                        if (!string.IsNullOrEmpty(stamp.Contents))
                        {
                            stampTexts.Add(stamp.Contents);
                        }
                    }
                }
            }
        }

        // Serialize the collected texts to JSON
        string json = JsonSerializer.Serialize(stampTexts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Extracted {stampTexts.Count} stamp annotations to '{outputPath}'.");
    }
}
