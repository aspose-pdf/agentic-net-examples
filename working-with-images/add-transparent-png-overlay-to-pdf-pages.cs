using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string overlayPng = "overlay.png";       // transparent PNG to overlay
        const string outputPdf = "output.pdf";         // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(overlayPng))
        {
            Console.Error.WriteLine($"Overlay PNG not found: {overlayPng}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate over all pages (foreach works with 1‑based indexing internally)
            foreach (Page page in pdfDoc.Pages)
            {
                // Create an ImageStamp from the transparent PNG
                ImageStamp stamp = new ImageStamp(overlayPng)
                {
                    // Ensure the stamp is drawn on top of existing page content
                    Background = false   // false = overlay (above), true = background (below)
                };

                // Optionally stretch the overlay to cover the whole page
                stamp.Width  = page.PageInfo.Width;
                stamp.Height = page.PageInfo.Height;

                // Add the stamp to the current page; AddStamp works per‑page (not on the collection)
                page.AddStamp(stamp);
            }

            // Save the modified PDF; Document.Save writes PDF regardless of extension
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}
