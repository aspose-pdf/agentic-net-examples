using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Thumbnails";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage(); // Prepare the extractor for image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Temporary path for the extracted image (original format)
                string tempPath = Path.Combine(outputDir, $"image_{imageIndex}_orig");
                extractor.GetNextImage(tempPath); // overload with only the file path

                // Load the extracted image
                using (Image original = Image.FromFile(tempPath))
                {
                    // Determine scaling factor to keep max dimension 200px
                    const int maxDim = 200;
                    double ratio = Math.Min((double)maxDim / original.Width, (double)maxDim / original.Height);
                    // If the image is already smaller than the max dimension, keep original size
                    if (ratio > 1) ratio = 1;

                    int thumbWidth = (int)(original.Width * ratio);
                    int thumbHeight = (int)(original.Height * ratio);

                    using (Bitmap thumb = new Bitmap(thumbWidth, thumbHeight))
                    {
                        using (Graphics g = Graphics.FromImage(thumb))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.DrawImage(original, 0, 0, thumbWidth, thumbHeight);
                        }

                        // Final thumbnail path (PNG)
                        string thumbPath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                        thumb.Save(thumbPath, ImageFormat.Png);
                    }
                }

                // Delete the temporary original image file
                File.Delete(tempPath);

                imageIndex++;
            }
        }

        Console.WriteLine("Thumbnail extraction completed.");
    }
}
