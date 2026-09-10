using System;
using System.IO;
using Aspose.Pdf;                     // Core API (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string watermarkImg = "watermark.png"; // PNG with transparent background
        const string outputPdf  = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkImg))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImg}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp that will be used as the overlay
            ImageStamp stamp = new ImageStamp(watermarkImg)
            {
                // Semi‑transparent (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.5f,

                // Place the stamp on top of page content (false = foreground)
                Background = false,

                // Center the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}