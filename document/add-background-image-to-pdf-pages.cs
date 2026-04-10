using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PdfPageStamp)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath    = "input.pdf";               // PDF to receive the background
        const string templatePdfPath = "background_template.pdf"; // PDF containing the background image (single page)
        const string outputPdfPath   = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        // Load the target document and the template (both inside using blocks for deterministic disposal)
        using (Document targetDoc = new Document(inputPdfPath))
        using (Document templateDoc = new Document(templatePdfPath))
        {
            // The template is expected to have a single page that contains the background image.
            // Retrieve that page once – it will be reused for all stamps.
            Page templatePage = templateDoc.Pages[1]; // 1‑based indexing

            // Iterate over every page of the target document.
            foreach (Page page in targetDoc.Pages)
            {
                // Create a stamp that uses the template page as its source.
                PdfPageStamp stamp = new PdfPageStamp(templatePage);

                // Place the stamp behind existing page content.
                stamp.Background = true;

                // Ensure the stamp covers the whole page.
                // Page.Rect returns the page rectangle (media box or crop box).
                stamp.Width  = page.Rect.Width;
                stamp.Height = page.Rect.Height;

                // Remove any margins so the stamp aligns exactly with the page edges.
                stamp.LeftMargin   = 0;
                stamp.BottomMargin = 0;
                stamp.RightMargin  = 0;
                stamp.TopMargin    = 0;

                // Apply the stamp to the current page.
                page.AddStamp(stamp);
            }

            // Save the modified document. No SaveOptions are required because the output format is PDF.
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image applied to all pages. Saved as '{outputPdfPath}'.");
    }
}