using System;
using System.IO;
using Aspose.Pdf;

namespace AddImageStampCustomPosition
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a simple PNG image (1x1 pixel, black)
            byte[] pngBytes = new byte[]
            {
                0x89,0x50,0x4E,0x47,0x0D,0x0A,0x1A,0x0A,
                0x00,0x00,0x00,0x0D,0x49,0x48,0x44,0x52,
                0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,
                0x08,0x02,0x00,0x00,0x00,0x90,0x77,0x53,
                0xDE,0x00,0x00,0x00,0x0A,0x49,0x44,0x41,
                0x54,0x08,0xD7,0x63,0xF8,0xCF,0xC0,0x00,
                0x00,0x04,0x00,0x01,0xE2,0x26,0x05,0x9B,
                0x00,0x00,0x00,0x00,0x49,0x45,0x4E,0x44,
                0xAE,0x42,0x60,0x82
            };
            File.WriteAllBytes("stamp.png", pngBytes);

            // Create a sample PDF
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Load the PDF and add image stamp with custom coordinates
            using (Document pdfDoc = new Document("input.pdf"))
            {
                ImageStamp stamp = new ImageStamp("stamp.png");
                stamp.XIndent = 100.0;
                stamp.YIndent = 150.0;
                stamp.Width = 50.0;
                stamp.Height = 50.0;

                pdfDoc.Pages[1].AddStamp(stamp);
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
