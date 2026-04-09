using System;
using System.IO;
using System.Collections.Generic;
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

        // Collect URLs from all link annotations in the document
        List<string> urls = new List<string>();

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the page (1‑based indexing)
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // We are interested only in LinkAnnotation objects
                    if (annotation is LinkAnnotation linkAnn)
                    {
                        // The URL is stored in the Action if it is a GoToURIAction
                        if (linkAnn.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                        {
                            urls.Add(uriAction.URI);
                        }
                    }
                }
            }
        }

        // Write the collected URLs to a CSV file
        try
        {
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // CSV header
                writer.WriteLine("URL");

                // Write each URL on a separate line
                foreach (string url in urls)
                {
                    // Escape double quotes by doubling them and wrap the field in quotes
                    string escaped = $"\"{url.Replace("\"", "\"\"")}\"";
                    writer.WriteLine(escaped);
                }
            }

            Console.WriteLine($"Extracted {urls.Count} link(s) to '{outputCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing CSV: {ex.Message}");
        }
    }
}