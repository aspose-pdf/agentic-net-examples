using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string extractedFolder = "extracted_images";
        const string watermarkedFolder = "watermarked_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure output directories exist
        Directory.CreateDirectory(extractedFolder);
        Directory.CreateDirectory(watermarkedFolder);

        // ---------------------------------------------------------------------
        // 1. Extract images from the PDF using PdfExtractor (recommended API).
        // ---------------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            int imageIndex = 0;
            while (extractor.HasNextImage())
            {
                imageIndex++;
                string outPath = Path.Combine(extractedFolder, $"image_{imageIndex}.png");
                // Save each image as PNG – ImageFormat comes from System.Drawing.Imaging
                extractor.GetNextImage(outPath, ImageFormat.Png);
                Console.WriteLine($"Extracted image saved: {outPath}");
            }
        }

        // ---------------------------------------------------------------------
        // 2. Apply a text watermark to each extracted image.
        // ---------------------------------------------------------------------
        foreach (string imagePath in Directory.GetFiles(extractedFolder))
        {
            using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imagePath))
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
            {
                const string watermarkText = "Sample Watermark";
                using (System.Drawing.Font font = new System.Drawing.Font(
                    "Arial", 20f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel))
                {
                    // Measure text size to position it at bottom‑right
                    System.Drawing.SizeF textSize = graphics.MeasureString(watermarkText, font);
                    float x = bitmap.Width - textSize.Width - 10;
                    float y = bitmap.Height - textSize.Height - 10;

                    // Semi‑transparent white brush for the watermark
                    using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(
                        System.Drawing.Color.FromArgb(128, 255, 255, 255)))
                    {
                        graphics.DrawString(watermarkText, font, brush, x, y);
                    }
                }

                // Save the watermarked image (PNG preserves quality)
                string fileName = Path.GetFileName(imagePath);
                string outPath = Path.Combine(watermarkedFolder, fileName);
                bitmap.Save(outPath, ImageFormat.Png);
                Console.WriteLine($"Watermarked image saved: {outPath}");
            }
        }

        Console.WriteLine("Image extraction and watermarking completed.");
    }
}
