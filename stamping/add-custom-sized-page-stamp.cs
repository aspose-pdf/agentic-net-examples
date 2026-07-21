using System;
using System.IO;
using Aspose.Pdf; // Core API (Document, Page, PdfPageStamp)

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "stamped_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // Create a PdfPageStamp using an existing page as the stamp source.
            // Here we use page 1 of the same document as the stamp content.
            // ------------------------------------------------------------
            Page stampSourcePage = doc.Pages[1]; // 1‑based indexing
            PdfPageStamp pageStamp = new PdfPageStamp(stampSourcePage);

            // ------------------------------------------------------------
            // Configure custom size (Width, Height) and position (XIndent, YIndent)
            // These values are in points (1 point = 1/72 inch).
            // ------------------------------------------------------------
            pageStamp.Width   = 200; // Desired stamp width
            pageStamp.Height  = 100; // Desired stamp height
            pageStamp.XIndent = 50;  // Horizontal offset from the left edge of the target page
            pageStamp.YIndent = 50;  // Vertical offset from the bottom edge of the target page

            // Optional: set other visual properties
            pageStamp.Opacity = 0.8;          // Slightly transparent
            pageStamp.Background = false;    // Stamp appears on top of page content

            // ------------------------------------------------------------
            // Apply the stamp to a specific target page.
            // For example, stamp page 2 of the document.
            // ------------------------------------------------------------
            Page targetPage = doc.Pages[2]; // Ensure the document has at least 2 pages
            targetPage.AddStamp(pageStamp);

            // Save the modified document (PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}