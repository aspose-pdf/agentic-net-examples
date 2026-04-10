using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "watermark.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a stamp based on the image. This stamp will be reused for all positions.
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                // Make the watermark semi‑transparent and draw it on top of page content
                Opacity = 0.3f,
                Background = false
            };

            // Desired size of each watermark instance (adjust as needed)
            const float stampWidth  = 80f;
            const float stampHeight = 80f;
            stamp.Width  = stampWidth;
            stamp.Height = stampHeight;

            // Spacing between watermarks (adjust as needed)
            const double horizontalSpacing = 150.0;
            const double verticalSpacing   = 150.0;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Page dimensions (points) – PageInfo returns double values
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Tile the stamp across the page surface
                for (double y = 0; y < pageHeight; y += verticalSpacing)
                {
                    for (double x = 0; x < pageWidth; x += horizontalSpacing)
                    {
                        // ImageStamp properties expect float, so cast the double coordinates
                        stamp.XIndent = (float)x;
                        stamp.YIndent = (float)y;
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
