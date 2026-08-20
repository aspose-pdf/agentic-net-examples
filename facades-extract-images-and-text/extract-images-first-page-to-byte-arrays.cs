using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // ------------------------------------------------------------
        // 1. Create a minimal PDF with an embedded image (self‑contained)
        // ------------------------------------------------------------
        // Create a 1×1 pixel BMP in memory (no external files required)
        byte[] bmpBytes = new byte[] {
            0x42, 0x4D, 0x3E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x3E, 0x00, 0x00, 0x00, 0x28, 0x00, 0x00, 0x00, 0x01, 0x00,
            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x18, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0xFF, 0x00, 0x00, 0x00
        };

        // Keep the image stream alive until the PDF is saved.
        MemoryStream imgStream = new MemoryStream(bmpBytes);
        imgStream.Position = 0; // ensure stream is at the beginning

        // Build the PDF that contains the image
        using (var doc = new Document())
        {
            Page page = doc.Pages.Add();

            var img = new Aspose.Pdf.Image { ImageStream = imgStream };
            page.Paragraphs.Add(img);

            doc.Save(pdfPath);
        }

        // Dispose the image stream after the document has been saved.
        imgStream.Dispose();

        // ------------------------------------------------------------
        // 2. Extract images from the first page into byte arrays
        // ------------------------------------------------------------
        List<byte[]> extractedImages = new List<byte[]>();

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.StartPage = 1;
            extractor.EndPage   = 1;
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bool ok = extractor.GetNextImage(ms);
                    if (ok)
                    {
                        extractedImages.Add(ms.ToArray());
                    }
                }
            }
        }

        Console.WriteLine($"Extracted {extractedImages.Count} image(s) from page 1.");
        if (extractedImages.Count > 0)
        {
            File.WriteAllBytes("firstPageImage1.bmp", extractedImages[0]);
            Console.WriteLine("First extracted image saved as 'firstPageImage1.bmp'.");
        }
    }
}
