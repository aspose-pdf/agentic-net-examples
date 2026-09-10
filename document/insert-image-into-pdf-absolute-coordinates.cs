using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";

        // Create a new PDF document with a single page
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();

            // Define absolute coordinates for the image placement
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            double llx = 100;   // left
            double lly = 500;   // bottom
            double width = 300; // desired width
            double height = 200; // desired height
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, llx + width, lly + height);

            // Create a minimal in‑memory PNG image (1x1 pixel) to avoid external file dependencies
            byte[] pngBytes = new byte[]
            {
                0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A,
                0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
                0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
                0x08, 0x06, 0x00, 0x00, 0x00, 0x1F, 0x15, 0xC4,
                0x89, 0x00, 0x00, 0x00, 0x0A, 0x49, 0x44, 0x41,
                0x54, 0x78, 0x9C, 0x63, 0x60, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x01, 0xE2, 0x21, 0xBC, 0x33, 0x00,
                0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE,
                0x42, 0x60, 0x82
            };

            using (MemoryStream imgStream = new MemoryStream(pngBytes))
            {
                // Insert the image using the in‑memory stream
                pdfDoc.Pages[1].AddImage(imgStream, rect);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdf}'.");
    }
}
