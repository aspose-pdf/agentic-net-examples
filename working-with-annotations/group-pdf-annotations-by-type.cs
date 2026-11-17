using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to group annotations by their concrete type name.
            // Store both the annotation instance and the page number it belongs to.
            var groups = new Dictionary<string, List<(Annotation annotation, int pageNumber)>>();

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Each page has an Annotations collection
                foreach (Annotation annot in page.Annotations)
                {
                    string typeName = annot.GetType().Name; // e.g., TextAnnotation, LinkAnnotation

                    if (!groups.TryGetValue(typeName, out var list))
                    {
                        list = new List<(Annotation, int)>();
                        groups[typeName] = list;
                    }

                    list.Add((annot, i)); // keep the current page index
                }
            }

            // Reporting: print count and basic info for each group
            Console.WriteLine("Annotation Report:");
            foreach (var kvp in groups)
            {
                string type = kvp.Key;
                var annots = kvp.Value;

                Console.WriteLine($"- {type}: {annots.Count} instance(s)");
                foreach (var (annotation, pageNumber) in annots)
                {
                    // Common properties: Rect (position) and Contents (text)
                    Aspose.Pdf.Rectangle rect = annotation.Rect;
                    string contents = annotation.Contents ?? string.Empty;

                    Console.WriteLine($"    Page {pageNumber}, Rect [{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}], Contents: \"{contents}\"");
                }
            }
        }
    }
}
