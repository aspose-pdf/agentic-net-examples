using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputCsv = "links.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                var records = new List<(int PageNumber, string Url)>();

                // Pages are 1‑based
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Annotations collection is also 1‑based
                    for (int j = 1; j <= page.Annotations.Count; j++)
                    {
                        Annotation ann = page.Annotations[j];

                        // We are interested only in link annotations
                        if (ann is LinkAnnotation link)
                        {
                            string url = null;

                            // Extract URL from GoToURIAction if present
                            if (link.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                            {
                                url = uriAction.URI;
                            }

                            if (!string.IsNullOrEmpty(url))
                            {
                                records.Add((i, url));
                            }
                        }
                    }
                }

                // Write results to CSV
                using (StreamWriter writer = new StreamWriter(outputCsv, false))
                {
                    writer.WriteLine("Page,URL");
                    foreach (var rec in records)
                    {
                        // Escape commas in URL if any
                        string escapedUrl = rec.Url.Contains(",") ? $"\"{rec.Url}\"" : rec.Url;
                        writer.WriteLine($"{rec.PageNumber},{escapedUrl}");
                    }
                }

                Console.WriteLine($"Extracted {records.Count} link(s) to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
