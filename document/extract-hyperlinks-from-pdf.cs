using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "hyperlinks.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var extractedLinks = new List<string>();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // We are interested only in link annotations
                    if (annotation is LinkAnnotation link)
                    {
                        // External hyperlinks are represented by GoToURIAction
                        if (link.Action is GoToURIAction uriAction)
                        {
                            extractedLinks.Add(uriAction.URI);
                        }
                        // Internal destinations can be handled here if needed
                    }
                }
            }
        }

        // Export the list of URLs to a plain‑text file (one URL per line)
        File.WriteAllLines(outputPath, extractedLinks);
        Console.WriteLine($"Extracted {extractedLinks.Count} hyperlink(s) to '{outputPath}'.");
    }
}