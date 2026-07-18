using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Text.Json;

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

        // Collect the text of all stamp annotations
        List<string> stampTexts = new List<string>();

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Annotation collections are also 1‑based
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    // We are interested only in StampAnnotation objects
                    if (ann is StampAnnotation stamp)
                    {
                        // The visible text of a stamp is stored in the Contents property
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

        Console.WriteLine($"Extracted {stampTexts.Count} stamp texts to '{outputJson}'.");
    }
}