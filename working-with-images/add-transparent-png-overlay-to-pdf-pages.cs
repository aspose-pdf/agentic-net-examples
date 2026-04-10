using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp (belongs to Aspose.Pdf namespace, but ImageStamp is in Aspose.Pdf)

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
            Console.Error.WriteLine($"Overlay image not found: {overlayPng}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp for the overlay image
                ImageStamp stamp = new ImageStamp(overlayPng)
                {
                    // Ensure the stamp is placed on top of existing content
                    Background = false,          // false = top layer (default, kept for clarity)
                    Opacity = 1.0f,              // full opacity; PNG's own alpha channel provides transparency
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
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