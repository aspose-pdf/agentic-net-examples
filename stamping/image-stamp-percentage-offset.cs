using System;
using System.IO;
using Aspose.Pdf;

namespace ImageStampPercentageOffset
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Create a simple PNG image file to use as stamp
            // 1x1 pixel transparent PNG (base64 encoded)
            string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XbZcAAAAASUVORK5CYII=";
            byte[] pngBytes = Convert.FromBase64String(base64Png);
            File.WriteAllBytes("stamp.png", pngBytes);

            // Open the PDF and add the image stamp
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = pdfDoc.Pages[1];

                // Create the image stamp from the PNG file
                ImageStamp stamp = new ImageStamp("stamp.png");

                // Optionally set size of the stamp (e.g., 100 points width)
                stamp.Width = 100f;
                // Height will be scaled proportionally if not set

                // Compute percentage based offsets (10% from left, 20% from bottom)
                float leftOffset = (float)(page.PageInfo.Width * 0.10);
                float bottomOffset = (float)(page.PageInfo.Height * 0.20);

                stamp.XIndent = leftOffset;
                stamp.YIndent = bottomOffset;

                // Add the stamp to the page
                page.AddStamp(stamp);

                // Save the result
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
