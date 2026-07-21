using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Collection of HTML fragments to be turned into PDF pages
        List<string> htmlFragments = new List<string>
        {
            "<html><body><h1>Page 1</h1><p>First fragment.</p></body></html>",
            "<html><body><h1>Page 2</h1><p>Second fragment.</p></body></html>"
        };

        const string outputPdfPath = "combined.pdf";

        // Create an empty target document that will hold all pages
        using (Document targetDoc = new Document())
        {
            // Custom rendering options for loading HTML strings
            HtmlLoadOptions loadOpts = new HtmlLoadOptions
            {
                // Embed fonts into the resulting PDF
                IsEmbedFonts = true,
                // Render each HTML fragment as separate pages (default behavior)
                IsRenderToSinglePage = false
                // Additional options can be set here as needed
            };

            // Process each HTML string
            foreach (string html in htmlFragments)
            {
                // Convert the HTML string to a stream (UTF‑8 encoding)
                using (MemoryStream htmlStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                // Load the HTML into a temporary Document using the custom options
                using (Document tempDoc = new Document(htmlStream, loadOpts))
                {
                    // Append all pages from the temporary document to the target document
                    targetDoc.Pages.Add(tempDoc.Pages);
                }
            }

            // Save the combined PDF document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created successfully at '{outputPdfPath}'.");
    }
}