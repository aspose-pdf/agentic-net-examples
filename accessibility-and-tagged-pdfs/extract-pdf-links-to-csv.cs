using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExtractLinksToCsv
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Collect all URLs from LinkAnnotations across all pages
            List<string> urls = new List<string>();

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                foreach (Annotation annotation in page.Annotations)
                {
                    // We are interested only in link annotations
                    if (annotation is LinkAnnotation linkAnno && linkAnno.Action != null)
                    {
                        // Go‑to URI actions contain the target URL
                        if (linkAnno.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                        {
                            urls.Add(uriAction.URI);
                        }
                    }
                }
            }

            // Write the collected URLs to a CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, System.Text.Encoding.UTF8))
            {
                // CSV header
                writer.WriteLine("URL");

                foreach (string url in urls)
                {
                    // Escape double quotes by doubling them, then wrap the field in quotes
                    string escaped = $"\"{url.Replace("\"", "\"\"")}\"";
                    writer.WriteLine(escaped);
                }
            }

            Console.WriteLine($"Extracted {urls.Count} link(s) to '{outputCsvPath}'.");
        }
    }
}
