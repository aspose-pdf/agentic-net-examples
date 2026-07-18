using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputTxt = "hyperlinks.txt";     // plain‑text list

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            var links = new List<string>();

            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Annotations collection is also 1‑based
                for (int idx = 1; idx <= page.Annotations.Count; idx++)
                {
                    Annotation ann = page.Annotations[idx];

                    // We are interested only in LinkAnnotation objects
                    if (ann is LinkAnnotation linkAnn)
                    {
                        // External hyperlinks are represented by GoToURIAction
                        if (linkAnn.Action is GoToURIAction uriAction)
                        {
                            // The URI property holds the URL string
                            links.Add(uriAction.URI);
                        }
                    }
                }
            }

            // Write the collected URLs to a plain‑text file (one per line)
            File.WriteAllLines(outputTxt, links);
        }

        Console.WriteLine($"Extracted {File.ReadAllLines(outputTxt).Length} hyperlink(s) to '{outputTxt}'.");
    }
}