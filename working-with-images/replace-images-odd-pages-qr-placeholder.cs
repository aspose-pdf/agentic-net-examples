using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample image file to be used in the PDF
        string sampleImagePath = "sample.png";
        CreateSampleImage(sampleImagePath);

        // Create a sample PDF with two pages, each containing the sample image
        using (Document doc = new Document())
        {
            // First page (odd)
            Page page1 = doc.Pages.Add();
            using (FileStream imgStream1 = new FileStream(sampleImagePath, FileMode.Open, FileAccess.Read))
            {
                Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
                page1.AddImage(imgStream1, rect1);
            }

            // Second page (even)
            Page page2 = doc.Pages.Add();
            using (FileStream imgStream2 = new FileStream(sampleImagePath, FileMode.Open, FileAccess.Read))
            {
                Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
                page2.AddImage(imgStream2, rect2);
            }

            doc.Save("input.pdf");
        }

        // Open the PDF and replace images on odd pages with a placeholder (sample image) that would represent a QR code
        using (Document doc = new Document("input.pdf"))
        {
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                if (pageIndex % 2 == 1) // odd page
                {
                    Page page = doc.Pages[pageIndex];
                    int imageCount = page.Resources.Images.Count;
                    for (int imgIndex = 1; imgIndex <= imageCount; imgIndex++)
                    {
                        // Original image source URL (demo purpose)
                        string originalUrl = "http://example.com/image" + pageIndex + "_" + imgIndex + ".png";

                        // In a real scenario a QR code would be generated from originalUrl.
                        // For this self‑contained example we reuse the sample image as a placeholder.
                        using (FileStream placeholderStream = new FileStream(sampleImagePath, FileMode.Open, FileAccess.Read))
                        {
                            MemoryStream qrPlaceholder = new MemoryStream();
                            placeholderStream.CopyTo(qrPlaceholder);
                            qrPlaceholder.Position = 0;
                            page.Resources.Images.Replace(imgIndex, qrPlaceholder);
                        }
                    }
                }
            }

            doc.Save("output.pdf");
        }
    }

    private static void CreateSampleImage(string path)
    {
        // A minimal 1x1 pixel PNG (transparent)
        byte[] pngBytes = new byte[]
        {
            0x89,0x50,0x4E,0x47,0x0D,0x0A,0x1A,0x0A,
            0x00,0x00,0x00,0x0D,0x49,0x48,0x44,0x52,
            0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,
            0x08,0x06,0x00,0x00,0x00,0x1F,0x15,0xC4,
            0x89,0x00,0x00,0x00,0x0A,0x49,0x44,0x41,
            0x54,0x78,0x9C,0x63,0x00,0x01,0x00,0x00,
            0x05,0x00,0x01,0x0D,0x0A,0x2D,0xB4,0x00,
            0x00,0x00,0x00,0x49,0x45,0x4E,0x44,0xAE,
            0x42,0x60,0x82
        };
        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            fs.Write(pngBytes, 0, pngBytes.Length);
        }
    }
}
