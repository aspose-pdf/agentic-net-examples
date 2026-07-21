using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string overlayImg = "overlay.png";
        const string outputPdf  = "output.pdf";

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

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the overlay image
            ImageStamp stamp = new ImageStamp(overlayImg)
            {
                // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.3,          // 30 % opacity
                Background = false      // place on top of page content
            };

            // Size the stamp to cover the whole page (assumes all pages have same size)
            stamp.Width  = doc.Pages[1].PageInfo.Width;
            stamp.Height = doc.Pages[1].PageInfo.Height;
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Flatten transparency so the overlay becomes part of the page raster
            doc.FlattenTransparency();

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}