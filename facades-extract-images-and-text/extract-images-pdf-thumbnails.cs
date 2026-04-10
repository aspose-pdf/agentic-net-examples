using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Aspose.Pdf.Facades; // PdfExtractor facade

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF
        const string outputFolder = "Thumbnails";          // folder for thumbnails

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (facade) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Prepare for image extraction
            extractor.ExtractImage();

            int imageCount = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Build output file name
                string outputPath = Path.Combine(outputFolder, $"image_{imageCount}.png");

                // Extract the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0;

                    // Load the image using System.Drawing
                    using (Image original = Image.FromStream(imgStream))
                    {
                        // Determine scaling factor to keep max dimension 200px
                        const int maxDim = 200;
                        int width = original.Width;
                        int height = original.Height;
                        float scale = Math.Min((float)maxDim / width, (float)maxDim / height);
                        if (scale > 1) scale = 1; // do not upscale small images
                        int thumbWidth = (int)(width * scale);
                        int thumbHeight = (int)(height * scale);

                        // Create thumbnail bitmap
                        using (Bitmap thumb = new Bitmap(thumbWidth, thumbHeight))
                        {
                            using (Graphics g = Graphics.FromImage(thumb))
                            {
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.CompositingQuality = CompositingQuality.HighQuality;
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.DrawImage(original, 0, 0, thumbWidth, thumbHeight);
                            }

                            // Save thumbnail as PNG
                            thumb.Save(outputPath, ImageFormat.Png);
                        }
                    }
                }

                imageCount++;
            }
        }

        Console.WriteLine("Thumbnail extraction completed.");
    }
}
