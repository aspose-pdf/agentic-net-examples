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
        const string inputPath = "FormF.pdf";
        const string outputPath = "FormF_allStamps.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var stampInfos = new List<StampInfo>();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        // StampAnnotation stores its textual content in the Contents property
                        string text = stamp.Contents ?? string.Empty;
                        stampInfos.Add(new StampInfo
                        {
                            PageNumber = i,
                            Text = text
                        });
                    }
                }
            }
        }

        // Serialize the collected stamp data to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(stampInfos, jsonOptions);
        File.WriteAllText(outputPath, json);
        Console.WriteLine($"Extracted {stampInfos.Count} stamp(s) to '{outputPath}'.");
    }

    // DTO used for JSON output
    private class StampInfo
    {
        public int PageNumber { get; set; }
        public string Text { get; set; }
    }
}