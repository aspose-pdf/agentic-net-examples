using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string watermarkImagePath = "watermark.png"; // image to use as watermark
        const string outputDir = "ExtractedImages";        // folder for extracted images

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // -----------------------------------------------------------------
        // Step 1: Create a temporary PDF with the watermark applied to each page
        // -----------------------------------------------------------------
        string tempWatermarkedPdf = Path.Combine(Path.GetTempPath(), $"watermarked_{Guid.NewGuid()}.pdf");

        try
        {
            // Load the original PDF to obtain page dimensions
            using (Document srcDoc = new Document(inputPdfPath))
            {
                int pageCount = srcDoc.Pages.Count;

                // Initialize the facade that can add images (watermark) to pages
                using (PdfFileMend mend = new PdfFileMend())
                {
                    mend.BindPdf(inputPdfPath);

                    // Add the watermark image to every page, covering the whole page
                    for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                    {
                        // Retrieve page size (width & height) for the current page
                        float pageWidth = (float)srcDoc.Pages[pageNum].PageInfo.Width;
                        float pageHeight = (float)srcDoc.Pages[pageNum].PageInfo.Height;

                        // lower‑left (0,0) to upper‑right (width,height) places the image over the entire page
                        mend.AddImage(watermarkImagePath, pageNum, 0f, 0f, pageWidth, pageHeight);
                    }

                    // Save the watermarked PDF to a temporary location
                    mend.Save(tempWatermarkedPdf);
                }
            }

            // -----------------------------------------------------------------
            // Step 2: Extract images from the watermarked PDF
            // -----------------------------------------------------------------
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(tempWatermarkedPdf);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outputImagePath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                    // Save each extracted image as PNG
                    extractor.GetNextImage(outputImagePath, ImageFormat.Png);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Extraction complete. Images saved to '{outputDir}'.");
        }
        finally
        {
            // Clean up the temporary watermarked PDF
            if (File.Exists(tempWatermarkedPdf))
            {
                try { File.Delete(tempWatermarkedPdf); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}
