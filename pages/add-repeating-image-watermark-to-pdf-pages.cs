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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired size of each watermark image (adjust as needed)
                const double stampWidth  = 100;   // points
                const double stampHeight = 50;    // points

                // Spacing between repeated images (adjust as needed)
                const double stepX = 150; // horizontal step
                const double stepY = 120; // vertical step

                // Loop to place stamps in a grid
                for (double y = 0; y < pageHeight; y += stepY)
                {
                    for (double x = 0; x < pageWidth; x += stepX)
                    {
                        // Create a new ImageStamp for each position
                        ImageStamp stamp = new ImageStamp(watermarkImage);

                        // Set size of the stamp
                        stamp.Width  = stampWidth;
                        stamp.Height = stampHeight;

                        // Position of the stamp (origin is bottom‑left)
                        stamp.XIndent = x;
                        stamp.YIndent = y;

                        // Make the watermark semi‑transparent and place it on top
                        stamp.Opacity   = 0.2f;   // 0 = fully transparent, 1 = opaque
                        stamp.Background = false;

                        // Add the stamp to the current page
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}