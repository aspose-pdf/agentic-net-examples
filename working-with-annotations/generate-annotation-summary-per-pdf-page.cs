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
        const string inputPdf = "input.pdf";
        const string outputJson = "annotation_summary.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the recommended using pattern
        using (Document doc = new Document(inputPdf))
        {
            var summary = new List<PageAnnotationSummary>();

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Annotations collection is also 1‑based
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];
                    string typeName = ann.AnnotationType.ToString();

                    if (counts.ContainsKey(typeName))
                        counts[typeName]++;
                    else
                        counts[typeName] = 1;
                }

                summary.Add(new PageAnnotationSummary
                {
                    PageNumber = i,
                    AnnotationCounts = counts
                });
            }

            // Serialize the summary to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(summary, jsonOptions);
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Annotation summary saved to '{outputJson}'.");
    }
}

// Helper class representing the annotation count per page
public class PageAnnotationSummary
{
    public int PageNumber { get; set; }
    public Dictionary<string, int> AnnotationCounts { get; set; }
}