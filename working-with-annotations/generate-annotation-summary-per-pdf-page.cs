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
        const string inputPdf = "input.pdf";
        const string outputJson = "annotation_summary.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Dictionary: page number -> (annotation type name -> count)
        var summary = new Dictionary<int, Dictionary<string, int>>();

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based (global rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var typeCounts = new Dictionary<string, int>();

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];
                    string typeName = ann.AnnotationType.ToString();

                    if (typeCounts.ContainsKey(typeName))
                        typeCounts[typeName]++;
                    else
                        typeCounts[typeName] = 1;
                }

                summary[pageIndex] = typeCounts;
            }
        }

        // Serialize the summary to pretty‑printed JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(summary, jsonOptions);

        // Save JSON to file (standard .NET I/O)
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Annotation summary saved to '{outputJson}'.");
    }
}