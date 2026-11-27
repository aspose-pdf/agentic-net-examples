using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple DTO for JSON output
    private class StampInfo
    {
        public int Page { get; set; }
        public string Text { get; set; }
    }

    static void Main()
    {
        const string inputPdf = "FormB.pdf";
        const string outputJson = "FormB_allStamps.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        var stamps = new List<StampInfo>();

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Iterate all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Process only StampAnnotation instances
                    if (annotation is StampAnnotation stamp)
                    {
                        // StampAnnotation may contain text in the Contents property
                        string text = stamp.Contents ?? string.Empty;
                        stamps.Add(new StampInfo { Page = i, Text = text });
                    }
                }
            }
        }

        // Serialize the collected stamp data to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(stamps, jsonOptions);

        // Write JSON to output file
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Extracted {stamps.Count} stamp annotations to '{outputJson}'.");
    }
}