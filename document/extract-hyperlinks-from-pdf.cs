using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExtractHyperlinks
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "hyperlinks.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDoc = new Document(inputPdfPath);

        // Collect all hyperlink URLs
        List<string> urls = new List<string>();

        // Pages are 1‑based indexed
        for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
        {
            Page page = pdfDoc.Pages[pageIndex];

            // Annotations collection is also 1‑based
            for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
            {
                Annotation ann = page.Annotations[annIndex];

                // We are interested only in LinkAnnotation objects
                if (ann is LinkAnnotation link)
                {
                    // The hyperlink is represented by a GoToURIAction
                    if (link.Action is GoToURIAction uriAction)
                    {
                        string url = uriAction.URI;
                        if (!string.IsNullOrEmpty(url))
                        {
                            urls.Add(url);
                        }
                    }
                }
            }
        }

        // Write the collected URLs to a plain‑text file (one per line)
        using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
        {
            foreach (string url in urls)
            {
                writer.WriteLine(url);
            }
        }

        Console.WriteLine($"Extracted {urls.Count} hyperlink(s) to '{outputTxtPath}'.");
    }
}