using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "links.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare CSV writer
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath))
            {
                // Write CSV header
                csvWriter.WriteLine("Page,URL");

                // Pages are 1‑based (global rule)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Iterate over all annotations on the page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // We are interested only in link annotations
                        if (annotation is LinkAnnotation link)
                        {
                            string url = null;

                            // Preferred way: the Action is a GoToURIAction containing the URI
                            if (link.Action is GoToURIAction uriAction)
                            {
                                url = uriAction.URI;
                            }

                            // If a URL was found, write it to CSV
                            if (!string.IsNullOrEmpty(url))
                            {
                                // Escape commas in the URL to keep CSV well‑formed
                                string escapedUrl = url.Contains(",") ? $"\"{url}\"" : url;
                                csvWriter.WriteLine($"{pageIndex},{escapedUrl}");
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Link URLs have been extracted to '{outputCsvPath}'.");
    }
}
