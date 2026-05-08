using System;
using System.IO;
using Aspose.Pdf.Facades;               // PdfExtractor
using System.Drawing;                  // Bitmap, Graphics, Font, Brush, Color
using System.Drawing.Imaging;          // ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputDir  = "ExtractedImages";    // folder for watermarked images
        const string watermark  = "Sample Watermark";   // text to overlay

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Extract images from the PDF using PdfExtractor (Facades API)
            // -----------------------------------------------------------------
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file
                extractor.BindPdf(inputPdf);

                // Extract all images from the document
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream (default JPEG format)
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0; // reset for reading

                        // ---------------------------------------------------------
                        // 2. Load the image into System.Drawing.Bitmap for watermarking
                        // ---------------------------------------------------------
                        using (Bitmap bitmap = new Bitmap(imgStream))
                        {
                            // Create graphics object for drawing
                            using (Graphics graphics = Graphics.FromImage(bitmap))
                            {
                                // Set high quality rendering options
                                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                                // Define watermark appearance
                                Font watermarkFont = new Font("Arial", 36, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);
                                Color watermarkColor = Color.FromArgb(128, 255, 255, 255); // semi‑transparent white
                                using (Brush brush = new SolidBrush(watermarkColor))
                                {
                                    // Position watermark at the center of the image
                                    SizeF textSize = graphics.MeasureString(watermark, watermarkFont);
                                    float x = (bitmap.Width  - textSize.Width)  / 2f;
                                    float y = (bitmap.Height - textSize.Height) / 2f;

                                    // Draw the watermark text
                                    graphics.DrawString(watermark, watermarkFont, brush, x, y);
                                }
                            }

                            // ---------------------------------------------------------
                            // 3. Save the watermarked image to a file (PNG preserves transparency)
                            // ---------------------------------------------------------
                            string outputPath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                            bitmap.Save(outputPath, ImageFormat.Png);
                            Console.WriteLine($"Saved watermarked image: {outputPath}");
                        }
                    }

                    imageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}