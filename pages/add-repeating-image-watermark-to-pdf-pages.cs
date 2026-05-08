using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "watermarked.pdf";    // result PDF
        const string watermarkImage = "logo.png";      // image to repeat

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Page dimensions in points
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired size of each watermark instance
                const double stampWidth  = 100; // points
                const double stampHeight = 50;  // points

                // Spacing between watermarks (you can adjust as needed)
                const double horizontalGap = 20;
                const double verticalGap   = 20;

                // Loop to place stamps in a grid pattern
                for (double y = 0; y < pageHeight; y += stampHeight + verticalGap)
                {
                    for (double x = 0; x < pageWidth; x += stampWidth + horizontalGap)
                    {
                        // Create a new ImageStamp for each position
                        ImageStamp stamp = new ImageStamp(watermarkImage)
                        {
                            // Set size of the stamp
                            Width  = stampWidth,
                            Height = stampHeight,

                            // Position on the page (origin is bottom‑left)
                            XIndent = x,
                            YIndent = y,

                            // Optional visual settings
                            Opacity = 0.3,               // semi‑transparent
                            Background = false           // draw over page content
                        };

                        // Add the stamp to the current page
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}