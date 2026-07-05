using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (a Facade) inside a using block for proper disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Prepare the extractor to work with images
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build the output file name
                string outputPath = Path.Combine(outputDir, $"thumb_{imageIndex}.png");

                // Extract the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0;

                    // Load the image, create a thumbnail, and save as PNG
                    using (Image original = Image.FromStream(imgStream))
                    using (Image thumb = ResizeImage(original, 200, 200))
                    {
                        thumb.Save(outputPath, ImageFormat.Png);
                    }
                }

                imageIndex++;
            }

            Console.WriteLine($"Extracted {imageIndex - 1} thumbnail(s) to \"{outputDir}\".");
        }
    }

    /// <summary>
    /// Resizes an image so that its longest side does not exceed maxDimension while preserving aspect ratio.
    /// </summary>
    private static Image ResizeImage(Image image, int maxWidth, int maxHeight)
    {
        // Determine the scaling factor to keep aspect ratio
        double ratioX = (double)maxWidth / image.Width;
        double ratioY = (double)maxHeight / image.Height;
        double ratio = Math.Min(ratioX, ratioY);

        int newWidth = (int)(image.Width * ratio);
        int newHeight = (int)(image.Height * ratio);

        var thumb = new Bitmap(newWidth, newHeight);
        using (Graphics g = Graphics.FromImage(thumb))
        {
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return thumb;
    }
}
