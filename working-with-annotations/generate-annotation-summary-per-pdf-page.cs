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
        const string outputJsonPath = "annotations_summary.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdf = new Document(inputPdfPath))
        {
            // List that will hold per‑page annotation summaries.
            var pagesSummary = new List<PageAnnotationInfo>();

            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= pdf.Pages.Count; i++)
            {
                Page page = pdf.Pages[i];
                var typeCounts = new Dictionary<string, int>(StringComparer.Ordinal);

                // Each page has an Annotations collection.
                foreach (Annotation annotation in page.Annotations)
                {
                    string typeName = annotation.GetType().Name;
                    if (typeCounts.ContainsKey(typeName))
                        typeCounts[typeName]++;
                    else
                        typeCounts[typeName] = 1;
                }

                pagesSummary.Add(new PageAnnotationInfo
                {
                    PageNumber = i,
                    Annotations = typeCounts
                });
            }

            // Serialize the summary to JSON with indentation for readability.
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(pagesSummary, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Annotation summary saved to '{outputJsonPath}'.");
    }

    // Helper class that represents the JSON structure for a single page.
    private class PageAnnotationInfo
    {
        public int PageNumber { get; set; }
        public Dictionary<string, int> Annotations { get; set; }
    }
}