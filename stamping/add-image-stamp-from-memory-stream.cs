using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output.pdf";

        // A 1x1 pixel PNG image encoded in Base64 (transparent). Replace with your own image if needed.
        const string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=";
        byte[] imageBytes = Convert.FromBase64String(base64Png);

        // Create a PDF document in memory (no external file required)
        using (Document pdfDoc = new Document())
        {
            // Add a single blank page so we have something to stamp onto
            pdfDoc.Pages.Add();

            // Create an ImageStamp directly from the memory stream
            using (MemoryStream imageStream = new MemoryStream(imageBytes))
            {
                ImageStamp stamp = new ImageStamp(imageStream)
                {
                    Background = false,               // place on top of page content
                    Opacity = 0.5,                    // 50% transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Apply the stamp to each page in the document
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(stamp);
                }
            }

            // Save the stamped PDF to disk
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}
