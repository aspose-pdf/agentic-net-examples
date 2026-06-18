using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // Source PDF
        const string outputDir = "ExtractedImages";    // Folder for JPEGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to pull images from page 5 only
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.StartPage = 5;   // 1‑based page index
            extractor.EndPage   = 5;
            extractor.ExtractImage();  // Prepare image extraction

            int imgIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream srcStream = new MemoryStream())
                {
                    extractor.GetNextImage(srcStream);
                    srcStream.Position = 0; // Reset for reading

                    // Load the image into a Bitmap
                    using (Bitmap original = new Bitmap(srcStream))
                    {
                        // Create a grayscale bitmap of the same size
                        using (Bitmap gray = new Bitmap(original.Width, original.Height))
                        {
                            using (Graphics g = Graphics.FromImage(gray))
                            {
                                // Grayscale color matrix
                                ColorMatrix matrix = new ColorMatrix(new float[][]
                                {
                                    new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                                    new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                                    new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                                    new float[] {0,      0,      0,      1, 0},
                                    new float[] {0,      0,      0,      0, 1}
                                });

                                using (ImageAttributes attributes = new ImageAttributes())
                                {
                                    attributes.SetColorMatrix(matrix);

                                    // Draw the original image onto the grayscale bitmap
                                    g.DrawImage(
                                        original,
                                        new Rectangle(0, 0, original.Width, original.Height),
                                        0, 0, original.Width, original.Height,
                                        GraphicsUnit.Pixel,
                                        attributes);
                                }
                            }

                            // Save the grayscale bitmap as JPEG
                            string outPath = Path.Combine(outputDir, $"page5_image_{imgIndex}.jpg");
                            gray.Save(outPath, ImageFormat.Jpeg);
                        }
                    }

                    imgIndex++;
                }
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}
