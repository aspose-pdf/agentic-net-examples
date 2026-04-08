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
        const string inputPath = "input.pdf";
        const string outputJson = "stamps.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var stampTexts = new List<string>();

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Annotations collection is also 1‑based
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // StampAnnotation represents a rubber‑stamp annotation
                    if (ann is StampAnnotation stamp)
                    {
                        // The visible text of the stamp is stored in the Contents property
                        if (!string.IsNullOrEmpty(stamp.Contents))
                        {
                            stampTexts.Add(stamp.Contents);
                        }
                    }
                }
            }
        }

        // Serialize the collected strings to a JSON array
        string json = JsonSerializer.Serialize(stampTexts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Extracted {stampTexts.Count} stamp text(s) to '{outputJson}'.");
    }
}