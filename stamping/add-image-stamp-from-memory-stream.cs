using System;
using System.IO;
using Aspose.Pdf;

namespace AddImageStampFromMemoryStreamExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF file (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Load the PDF we just created
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Prepare a tiny PNG image in a memory stream (1x1 pixel transparent PNG)
                string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+X9WcAAAAASUVORK5CYII=";
                byte[] imageBytes = Convert.FromBase64String(base64Png);
                using (MemoryStream imageStream = new MemoryStream(imageBytes))
                {
                    // Create an ImageStamp from the memory stream
                    ImageStamp imageStamp = new ImageStamp(imageStream);
                    // Optional: set size and alignment of the stamp
                    imageStamp.Width = 100;
                    imageStamp.Height = 100;
                    imageStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    imageStamp.VerticalAlignment = VerticalAlignment.Center;

                    // Add the stamp to the first page (page indexing is 1‑based)
                    pdfDoc.Pages[1].AddStamp(imageStamp);
                }

                // Save the resulting PDF
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
