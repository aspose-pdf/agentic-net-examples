using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExtractLinksToCsv
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output CSV file path
        const string outputCsvPath = "links.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // List to hold extracted URLs
        List<string> linkUrls = new List<string>();

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // We are interested only in LinkAnnotation objects
                    if (annotation is LinkAnnotation linkAnnotation)
                    {
                        // The Action of a LinkAnnotation can be a GoToURIAction containing the URL
                        if (linkAnnotation.Action is GoToURIAction uriAction)
                        {
                            string uri = uriAction.URI;
                            if (!string.IsNullOrEmpty(uri))
                            {
                                linkUrls.Add(uri);
                            }
                        }
                    }
                }
            }
        }

        // Write the collected URLs to a CSV file (one URL per line)
        try
        {
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                foreach (string url in linkUrls)
                {
                    // Simple CSV line – URLs are written as‑is; commas are unlikely in URLs
                    writer.WriteLine(url);
                }
            }

            Console.WriteLine($"Extracted {linkUrls.Count} link(s) to '{outputCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing CSV: {ex.Message}");
        }
    }
}
