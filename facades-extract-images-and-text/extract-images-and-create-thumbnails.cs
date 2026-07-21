using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputDir = "Thumbnails";        // folder for thumbnails

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Enable image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all extracted images
            while (extractor.HasNextImage())
            {
                // Extract the raw image into a memory stream
                using (MemoryStream rawStream = new MemoryStream())
                {
                    extractor.GetNextImage(rawStream);
                    rawStream.Position = 0;

                    using (Image original = Image.FromStream(rawStream))
                    {
                        // Create a thumbnail that fits within 200x200 while preserving aspect ratio
                        using (Image thumb = ResizeImage(original, 200, 200))
                        {
                            string thumbPath = Path.Combine(outputDir, $"thumb_{imageIndex}.png");
                            thumb.Save(thumbPath, ImageFormat.Png);
                            Console.WriteLine($"Created thumbnail: {thumbPath}");
                        }
                    }
                }
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and thumbnail generation completed.");
    }

    /// <summary>
    /// Resizes an image so that it fits within the specified maximum width and height,
    /// preserving the original aspect ratio.
    /// </summary>
    private static Image ResizeImage(Image image, int maxWidth, int maxHeight)
    {
        // Compute scaling factor while preserving aspect ratio
        double ratioX = (double)maxWidth / image.Width;
        double ratioY = (double)maxHeight / image.Height;
        double ratio = Math.Min(ratioX, ratioY);

        int newWidth = (int)(image.Width * ratio);
        int newHeight = (int)(image.Height * ratio);

        var thumbnail = new Bitmap(newWidth, newHeight);
        using (var graphics = Graphics.FromImage(thumbnail))
        {
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return thumbnail;
    }
}
