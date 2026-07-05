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
        const string jsonPath = "annotation_summary.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            var report = new List<PageAnnotationSummary>();

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                var typeCounts = new Dictionary<string, int>();

                // Annotations collection is also 1‑based
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];
                    string typeName = ann.AnnotationType.ToString();

                    if (typeCounts.ContainsKey(typeName))
                        typeCounts[typeName]++;
                    else
                        typeCounts[typeName] = 1;
                }

                report.Add(new PageAnnotationSummary
                {
                    PageNumber = i,
                    AnnotationCounts = typeCounts
                });
            }

            // Serialize the summary to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(report, jsonOptions);
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Annotation summary saved to '{jsonPath}'.");
    }

    // Helper class representing the JSON structure for each page
    private class PageAnnotationSummary
    {
        public int PageNumber { get; set; }
        public Dictionary<string, int> AnnotationCounts { get; set; }
    }
}