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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Dictionary where key = page number, value = dictionary of annotation type counts
            var summary = new Dictionary<int, Dictionary<AnnotationType, int>>();

            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var typeCounts = new Dictionary<AnnotationType, int>();

                // Iterate over annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    AnnotationType type = annotation.AnnotationType;

                    if (typeCounts.ContainsKey(type))
                        typeCounts[type]++;
                    else
                        typeCounts[type] = 1;
                }

                summary[pageIndex] = typeCounts;
            }

            // Serialize the summary dictionary to pretty‑printed JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(summary, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Annotation summary saved to '{outputJsonPath}'.");
    }
}