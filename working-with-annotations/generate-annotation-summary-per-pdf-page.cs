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
        const string outputPath = "annotation_summary.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Prepare a list to hold per‑page annotation summaries
            var pagesSummary = new List<object>();

            // Iterate pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Count annotation types on the current page
                var typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (Annotation ann in page.Annotations)
                {
                    string typeName = ann.AnnotationType.ToString();
                    if (typeCounts.ContainsKey(typeName))
                        typeCounts[typeName]++;
                    else
                        typeCounts[typeName] = 1;
                }

                // Add the page summary to the collection
                pagesSummary.Add(new
                {
                    PageNumber = i,
                    Annotations = typeCounts
                });
            }

            // Serialize the summary to JSON (indented for readability)
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(pagesSummary, jsonOptions);

            // Save JSON to file (standard file I/O, not Document.Save)
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Annotation summary saved to '{outputPath}'.");
    }
}