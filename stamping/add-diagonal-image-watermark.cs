using System;
using System.IO;
using Aspose.Pdf;

namespace AddDiagonalImageWatermarkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with a single page (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Open the created PDF and add a rotated image stamp as a diagonal watermark
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // A 1x1 pixel PNG image encoded in Base64 (acts as a placeholder watermark image)
                string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+X6ZcAAAAASUVORK5CYII=";
                byte[] pngBytes = Convert.FromBase64String(base64Png);

                using (MemoryStream imgStream = new MemoryStream(pngBytes))
                {
                    // Create an ImageStamp from the image stream
                    ImageStamp imageStamp = new ImageStamp(imgStream);
                    // Set stamp properties for a watermark effect
                    imageStamp.Background = true;                     // place behind page content
                    imageStamp.Opacity = 0.5f;                        // semi‑transparent
                    imageStamp.Rotate = Rotation.on90;                // rotate 90 degrees for diagonal appearance
                    imageStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    imageStamp.VerticalAlignment = VerticalAlignment.Center;
                    // Optionally set size (here we let it use its original size)

                    // Apply the stamp to each page (only one page in this example)
                    foreach (Page page in pdfDoc.Pages)
                    {
                        page.AddStamp(imageStamp);
                    }
                }

                // Save the resulting PDF with the watermark
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
