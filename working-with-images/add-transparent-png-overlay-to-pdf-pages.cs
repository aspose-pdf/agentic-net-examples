using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, ImageStamp)

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string overlayPng = "overlay.png";        // transparent PNG to overlay
        const string outputPdf  = "output.pdf";         // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(overlayPng))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayPng}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the transparent PNG
                ImageStamp stamp = new ImageStamp(overlayPng)
                {
                    // Ensure the stamp is placed on top of existing content
                    Background = false,          // false = top layer (default, kept for clarity)

                    // Optional: control opacity (0.0 = fully transparent, 1.0 = opaque)
                    Opacity = 0.8f,

                    // Position the stamp to cover the whole page
                    // Width/Height are in user units (points). Page.Rect gives the page size.
                    Width   = page.Rect.Width,
                    Height  = page.Rect.Height,
                    XIndent = 0,
                    YIndent = 0
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}