using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "watermarked.pdf";    // result PDF
        const string watermarkImage = "watermark.png"; // image to repeat

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // Load the PDF document (using rule: load)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired size of each watermark instance
                const double stampWidth  = 100; // points
                const double stampHeight = 100; // points

                // Spacing between watermarks
                const double stepX = 150; // horizontal step
                const double stepY = 150; // vertical step

                // Grid loop: place a stamp at each (x, y) coordinate
                for (double y = 0; y < pageHeight; y += stepY)
                {
                    for (double x = 0; x < pageWidth; x += stepX)
                    {
                        // Create a new ImageStamp for each position
                        ImageStamp stamp = new ImageStamp(watermarkImage)
                        {
                            // Draw behind page content
                            Background = true,
                            // Semi‑transparent
                            Opacity    = 0.2,
                            // Fixed size
                            Width      = stampWidth,
                            Height     = stampHeight,
                            // Position on the page
                            XIndent    = x,
                            YIndent    = y
                        };

                        // Add the stamp to the current page (per‑page call)
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified PDF (using rule: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}