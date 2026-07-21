using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputCsvPath = "links.csv";  // CSV file to write URLs

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // List to collect extracted URLs
        List<string> extractedUrls = new List<string>();

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate through all annotations on the page
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // We're interested only in link annotations
                    if (annotation is LinkAnnotation linkAnnotation)
                    {
                        // Extract the URL from a GoToURIAction if present
                        if (linkAnnotation.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                        {
                            extractedUrls.Add(uriAction.URI);
                        }
                        // In older PDFs the Hyperlink property may hold a URI, but the property
                        // no longer exposes a URI member in recent Aspose.Pdf versions. If needed,
                        // you could inspect other properties of Hyperlink, but for this task we
                        // rely solely on GoToURIAction.
                    }
                }
            }
        }

        // Write the URLs to a CSV file (simple one‑column CSV)
        using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
        {
            writer.WriteLine("URL"); // header
            foreach (string url in extractedUrls)
            {
                // Escape double quotes by doubling them (CSV rule)
                string escaped = url.Replace("\"", "\"\"");
                writer.WriteLine($"\"{escaped}\"");
            }
        }

        Console.WriteLine($"Extracted {extractedUrls.Count} link(s) to '{outputCsvPath}'.");
    }
}
