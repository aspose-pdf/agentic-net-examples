using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "links.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        var extractedLinks = new List<(int Page, int AnnotationIndex, string Url)>();

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            int pageNumber = 1;
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing (global rule)
                for (int idx = 1; idx <= page.Annotations.Count; idx++)
                {
                    Annotation ann = page.Annotations[idx];
                    if (ann is LinkAnnotation linkAnn)
                    {
                        // Preferred extraction: use GoToURIAction.URI (Fix: use-gotouriaction-uri-for-link-url)
                        if (linkAnn.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                        {
                            extractedLinks.Add((pageNumber, idx, uriAction.URI));
                        }
                        // No fallback to Hyperlink.Url – the property does not exist in current Aspose.Pdf versions.
                    }
                }
                pageNumber++;
            }
        }

        // Write results to CSV (no special SaveOptions needed – plain file I/O)
        using (var writer = new StreamWriter(outputCsv))
        {
            writer.WriteLine("Page,AnnotationIndex,URL");
            foreach (var entry in extractedLinks)
            {
                // Escape commas in URL if necessary
                string escapedUrl = entry.Url.Contains(",") ? $"\"{entry.Url}\"" : entry.Url;
                writer.WriteLine($"{entry.Page},{entry.AnnotationIndex},{escapedUrl}");
            }
        }

        Console.WriteLine($"Extracted {extractedLinks.Count} link(s) to '{outputCsv}'.");
    }
}
