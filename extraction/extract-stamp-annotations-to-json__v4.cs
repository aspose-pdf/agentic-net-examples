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
        const string inputPath = "FormE.pdf";
        const string outputPath = "FormE_allStamps.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        var stamps = new List<StampInfo>();

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        // Prefer Title; fall back to Contents
                        string text = !string.IsNullOrEmpty(stamp.Title) ? stamp.Title : stamp.Contents;
                        stamps.Add(new StampInfo
                        {
                            PageNumber = i,
                            Text = text ?? string.Empty
                        });
                    }
                }
            }
        }

        // Serialize to formatted JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(stamps, jsonOptions);

        // Save JSON file
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Extracted {stamps.Count} stamp annotations to '{outputPath}'.");
    }

    // DTO for JSON output
    private class StampInfo
    {
        public int PageNumber { get; set; }
        public string Text { get; set; }
    }
}