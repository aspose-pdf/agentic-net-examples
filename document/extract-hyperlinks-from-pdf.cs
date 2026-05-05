using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputTxtPath = "hyperlinks.txt"; // output plain‑text list

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Collect all hyperlink URLs found in the document
            List<string> links = new List<string>();

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over annotations on the current page
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // We are interested only in LinkAnnotation objects
                    if (ann is LinkAnnotation link)
                    {
                        // The hyperlink is stored in the Action property.
                        // For external URLs the Action is a GoToURIAction.
                        if (link.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                        {
                            links.Add(uriAction.URI);
                        }
                    }
                }
            }

            // Write the collected URLs to a plain‑text file (one URL per line)
            File.WriteAllLines(outputTxtPath, links);
        }

        Console.WriteLine($"Extracted {File.ReadAllLines(outputTxtPath).Length} hyperlink(s) to '{outputTxtPath}'.");
    }
}