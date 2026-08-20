using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // Source PDF
        const string watermarkImgPath = "watermark.png";       // Watermark image to overlay
        const string tempWatermarkedPdf = "temp_watermarked.pdf"; // Intermediate PDF with watermark
        const string outputImageDir = "ExtractedImages";       // Folder for extracted images

        // -----------------------------------------------------------------
        // Ensure we have a minimal input PDF (self‑contained example)
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using var seed = new Document();
            seed.Pages.Add();
            // Add a sample image to the page so there is something to extract later
            var sampleImg = CreateSampleImage();
            var img = new Aspose.Pdf.Image { ImageStream = new MemoryStream(sampleImg) };
            seed.Pages[1].Paragraphs.Add(img);
            seed.Save(inputPdfPath);
        }

        // -----------------------------------------------------------------
        // Ensure we have a simple watermark image (self‑contained example)
        // -----------------------------------------------------------------
        if (!File.Exists(watermarkImgPath))
        {
            using var bmp = new Bitmap(200, 50);
            using var g = Graphics.FromImage(bmp);
            g.Clear(System.Drawing.Color.Transparent);
            using var font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold);
            g.DrawString("WATERMARK", font, System.Drawing.Brushes.Red, new PointF(0, 0));
            bmp.Save(watermarkImgPath, ImageFormat.Png);
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputImageDir);

        // -----------------------------------------------------------------
        // Step 1: Add watermark image to each page of the PDF using PdfFileMend
        // -----------------------------------------------------------------
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdfPath);

            // Retrieve page count via Document (PdfFileMend does not expose it directly)
            int pageCount;
            using (Document doc = new Document(inputPdfPath))
            {
                pageCount = doc.Pages.Count;
            }

            // Add the watermark image to every page.
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Position the watermark at the bottom‑right corner (adjust as needed)
                mend.AddImage(watermarkImgPath, pageNum, 400, 10, 600, 60);
            }

            mend.Save(tempWatermarkedPdf);
        }

        // -----------------------------------------------------------------
        // Step 2: Extract images from the watermarked PDF using PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(tempWatermarkedPdf);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputImagePath = Path.Combine(outputImageDir, $"image_{imageIndex}.png");
                // Use the overload without ImageFormat to avoid platform‑specific warnings.
                extractor.GetNextImage(outputImagePath);
                imageIndex++;
            }
        }

        // Optional: clean up the temporary watermarked PDF
        if (File.Exists(tempWatermarkedPdf))
        {
            File.Delete(tempWatermarkedPdf);
        }

        Console.WriteLine("Image extraction with watermark overlay completed.");
    }

    // Helper: creates a 1×1 pixel PNG (used for the sample PDF image)
    private static byte[] CreateSampleImage()
    {
        using var bmp = new Bitmap(1, 1);
        bmp.SetPixel(0, 0, System.Drawing.Color.Blue);
        using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
}
