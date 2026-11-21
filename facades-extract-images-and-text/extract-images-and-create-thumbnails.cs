using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "Thumbnails";
        const int maxDimension = 200; // maximum width or height for thumbnails

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(pdfPath);

            // Enable image extraction
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0; // reset stream position

                    // Load the image using System.Drawing
                    using (Image original = Image.FromStream(imgStream))
                    {
                        // Determine thumbnail size while preserving aspect ratio
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
                        using (Bitmap thumbnail = new Bitmap(thumbWidth, thumbHeight))
                        {
                            using (Graphics g = Graphics.FromImage(thumbnail))
                            {
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.DrawImage(original, 0, 0, thumbWidth, thumbHeight);
                            }

                            // Save the thumbnail as PNG
                            string outPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");
                            thumbnail.Save(outPath, System.Drawing.Imaging.ImageFormat.Png);
                            Console.WriteLine($"Saved thumbnail: {outPath}");
                        }
                    }
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and thumbnail generation completed.");
    }
}