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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Open CSV for writing
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                csvWriter.WriteLine("Page,URL");

                // Iterate pages (1‑based indexing rule)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Iterate annotations on the page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // We're interested only in LinkAnnotation instances
                        if (annotation is LinkAnnotation linkAnnotation)
                        {
                            string url = null;

                            // If the link uses a GoToURIAction, extract the URI
                            if (linkAnnotation.Action is GoToURIAction uriAction)
                            {
                                url = uriAction.URI;
                            }
                            // Fallback: some links expose a Hyperlink object
                            else if (linkAnnotation.Hyperlink != null)
                            {
                                // Hyperlink does not expose a direct URL property in this API version.
                                // If needed, additional handling can be added here.
                            }

                            if (!string.IsNullOrEmpty(url))
                            {
                                // Escape double quotes for CSV compliance
                                string escapedUrl = url.Replace("\"", "\"\"");
                                csvWriter.WriteLine($"{pageIndex},\"{escapedUrl}\"");
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Link URLs have been extracted to '{outputCsvPath}'.");
    }
}