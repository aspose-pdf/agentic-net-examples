using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample image file (1x1 PNG)
        string imagePath = "sample.png";
        byte[] pngBytes = new byte[] {
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
        File.WriteAllBytes(imagePath, pngBytes);

        // Create a sample PDF with a single page
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the PDF and add an upside‑down image stamp
        using (Document pdfDoc = new Document("input.pdf"))
        {
            Page page = pdfDoc.Pages[1];
            ImageStamp imgStamp = new ImageStamp(imagePath);
            imgStamp.Rotate = Rotation.on180; // upside‑down rotation
            imgStamp.XIndent = 100;
            imgStamp.YIndent = 100;
            page.AddStamp(imgStamp);
            pdfDoc.Save("output.pdf");
        }
    }
}
