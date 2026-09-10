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
        const string outputJsonPath = "annotation_summary.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            var pagesSummary = new List<object>();

            // Pages are 1‑based (global rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var typeCounts = new Dictionary<string, int>();

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    string typeName = annotation.AnnotationType.ToString();

                    if (typeCounts.ContainsKey(typeName))
                        typeCounts[typeName]++;
                    else
                        typeCounts[typeName] = 1;
                }

                pagesSummary.Add(new
                {
                    pageNumber = pageIndex,
                    annotations = typeCounts
                });
            }

            // Serialize the summary to JSON (no Aspose.Pdf SaveOptions needed)
            var summaryObject = new { pages = pagesSummary };
            string json = JsonSerializer.Serialize(summaryObject, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Annotation summary saved to '{outputJsonPath}'.");
        }
    }
}