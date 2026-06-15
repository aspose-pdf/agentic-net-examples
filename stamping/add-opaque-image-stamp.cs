using System;
using System.IO;
using Aspose.Pdf;

namespace AddOpaqueImageStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF file
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Step 2: Prepare a small PNG image to use as a stamp
            string imagePath = "stamp.png";
            if (!File.Exists(imagePath))
            {
                // 1x1 pixel transparent PNG (base64 encoded)
                string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+X3eUAAAAASUVORK5CYII=";
                byte[] pngBytes = Convert.FromBase64String(base64Png);
                File.WriteAllBytes(imagePath, pngBytes);
            }

            // Step 3: Open the PDF and add the image stamp to each page
            using (Document pdfDoc = new Document("input.pdf"))
            {
                ImageStamp imgStamp = new ImageStamp(imagePath);
                imgStamp.Opacity = 1.0f; // fully opaque
                imgStamp.Background = true; // place behind page content (optional for watermark effect)
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment = VerticalAlignment.Center;

                int pageCount = pdfDoc.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    pdfDoc.Pages[pageIndex].AddStamp(imgStamp);
                }

                pdfDoc.Save("output.pdf");
            }
        }
    }
}
