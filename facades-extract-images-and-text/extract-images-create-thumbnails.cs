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

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage(); // Prepare image extraction

            int imageIndex = 1;
            const int maxDimension = 200; // maximum width or height for thumbnail

            while (extractor.HasNextImage())
            {
                // Extract the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream); // overload that writes image to a stream
                    imgStream.Position = 0;

                    using (Image original = Image.FromStream(imgStream))
                    {
                        // Calculate thumbnail size while preserving aspect ratio
                        int thumbWidth, thumbHeight;
                        if (original.Width > original.Height)
                        {
                            thumbWidth = maxDimension;
                            thumbHeight = (int)(original.Height * (maxDimension / (float)original.Width));
                        }
                        else
                        {
                            thumbHeight = maxDimension;
                            thumbWidth = (int)(original.Width * (maxDimension / (float)original.Height));
                        }

                        // Create the thumbnail bitmap
                        using (Bitmap thumb = new Bitmap(thumbWidth, thumbHeight))
                        {
                            using (Graphics g = Graphics.FromImage(thumb))
                            {
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.DrawImage(original, 0, 0, thumbWidth, thumbHeight);
                            }

                            // Save thumbnail as PNG
                            string outputPath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                            thumb.Save(outputPath, ImageFormat.Png);
                        }
                    }
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image thumbnails have been generated.");
    }
}
