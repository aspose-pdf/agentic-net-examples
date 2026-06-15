using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF (simulating a signed PDF)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Create a simple PNG image for stamping (1x1 transparent pixel)
        byte[] pngBytes = new byte[]
        {
            0x89,0x50,0x4E,0x47,0x0D,0x0A,0x1A,0x0A,
            0x00,0x00,0x00,0x0D,0x49,0x48,0x44,0x52,
            0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,
            0x08,0x06,0x00,0x00,0x00,0x1F,0x15,0xC4,
            0x89,0x00,0x00,0x00,0x0A,0x49,0x44,0x41,
            0x54,0x78,0x9C,0x63,0x60,0x00,0x00,0x00,
            0x02,0x00,0x01,0xE2,0x21,0xBC,0x33,0x00,
            0x00,0x00,0x00,0x49,0x45,0x4E,0x44,0xAE,
            0x42,0x60,0x82
        };
        File.WriteAllBytes("stamp.png", pngBytes);

        // Open the PDF (assumed to be digitally signed)
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Create an image stamp
            ImageStamp imageStamp = new ImageStamp("stamp.png");
            // Position the stamp (example: 100 points from left, 100 points from bottom)
            imageStamp.XIndent = 100;
            imageStamp.YIndent = 100;
            // Set opacity to 50% (0.5f)
            imageStamp.Opacity = 0.5f;

            // Add the stamp to each page
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imageStamp);
            }

            pdfDoc.Save("output.pdf");
        }
    }
}