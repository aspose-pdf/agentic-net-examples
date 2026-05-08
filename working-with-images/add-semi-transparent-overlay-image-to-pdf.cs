using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, ImageStamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string overlayImg = "overlay.png";   // semi‑transparent image to overlay
        const string outputPdf = "output.pdf";     // result PDF

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(overlayImg))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the overlay image
            ImageStamp stamp = new ImageStamp(overlayImg)
            {
                // Place the stamp over the page content (false = top layer)
                Background = false,

                // Semi‑transparent (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.5f,

                // Center the image on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Scale the image to fit the page while preserving aspect ratio
                // (Zoom = 1.0 means original size; adjust if needed)
                Zoom = 1.0f
            };

            // Apply the stamp to every page (pages are 1‑based)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Optional: replace any remaining transparency with opaque graphics
            // doc.FlattenTransparency();

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}