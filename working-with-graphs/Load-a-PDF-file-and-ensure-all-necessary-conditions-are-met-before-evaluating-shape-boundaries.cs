using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Ensure the document contains at least one page (necessary pre‑condition)
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Validate page dimensions before any shape‑boundary calculations
                double width = page.PageInfo.Width;
                double height = page.PageInfo.Height;

                if (width <= 0 || height <= 0)
                {
                    Console.Error.WriteLine($"Page {i} has invalid dimensions (Width={width}, Height={height}).");
                    continue;
                }

                // Check that the page actually contains vector graphics before evaluating boundaries
                if (!page.HasVectorGraphics())
                {
                    Console.WriteLine($"Page {i} does not contain vector graphics; skipping boundary evaluation.");
                    continue;
                }

                // Placeholder: evaluate shape boundaries here
                Console.WriteLine($"Page {i}: dimensions OK ({width}x{height}), ready for shape boundary evaluation.");
            }
        }
    }
}