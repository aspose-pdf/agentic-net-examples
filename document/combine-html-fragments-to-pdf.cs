using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf; // Core PDF API (HtmlLoadOptions resides in this namespace)

class Program
{
    static void Main()
    {
        // Collection of HTML fragments to be converted.
        List<string> htmlFragments = new List<string>
        {
            "<html><body><h1>First Document</h1><p>Hello World!</p></body></html>",
            "<html><body><h2>Second Document</h2><p>Another paragraph.</p></body></html>"
            // Add more HTML strings as needed.
        };

        // Output PDF path.
        const string outputPdf = "CombinedOutput.pdf";

        // Custom rendering options for HTML → PDF conversion.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            // Example: render each HTML fragment as a separate page.
            IsRenderToSinglePage = false,

            // Example: embed fonts from the HTML into the resulting PDF.
            IsEmbedFonts = true
        };

        // Create the target PDF document.
        using (Document targetPdf = new Document())
        {
            // Remove the default empty page created by the parameterless constructor.
            if (targetPdf.Pages.Count > 0)
                targetPdf.Pages.Delete(1);

            // Process each HTML fragment.
            foreach (string html in htmlFragments)
            {
                // Convert the HTML string to a memory stream (UTF‑8 encoding).
                byte[] htmlBytes = Encoding.UTF8.GetBytes(html);
                using (MemoryStream htmlStream = new MemoryStream(htmlBytes))
                {
                    // Load the HTML into a temporary PDF document using the custom options.
                    using (Document tempPdf = new Document(htmlStream, loadOptions))
                    {
                        // Append all pages from the temporary document to the target PDF.
                        targetPdf.Pages.Add(tempPdf.Pages);
                    }
                }
            }

            // Save the combined PDF.
            targetPdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully at '{outputPdf}'.");
    }
}
