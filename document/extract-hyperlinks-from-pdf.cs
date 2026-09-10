using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "hyperlinks.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            List<string> extractedLinks = new List<string>();

            // Pages are 1‑based (global rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // We're interested only in LinkAnnotation objects
                    if (ann is LinkAnnotation linkAnn)
                    {
                        // Extract the URL from a GoToURIAction attached to the link
                        if (linkAnn.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                        {
                            extractedLinks.Add(uriAction.URI);
                        }
                    }
                }
            }

            // Export the list of URLs to a plain‑text file (standard .NET I/O)
            File.WriteAllLines(outputPath, extractedLinks);
        }

        Console.WriteLine($"Hyperlinks extracted to '{outputPath}'.");
    }
}
