using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfExtractor
using System.Drawing;            // Bitmap, Graphics
using System.Drawing.Imaging;    // ImageFormat, ColorMatrix, ImageAttributes

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputDir  = "ExtractedImages";    // folder for JPEGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to extract images from page 5 only
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.StartPage = 5;   // 1‑based page index
            extractor.EndPage   = 5;
            extractor.ExtractImage();  // prepare extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Temporary JPEG file for the extracted image
                string jpegPath = Path.Combine(outputDir,
                    $"page5_image_{imageIndex}.jpg");

                // Save the extracted image as JPEG
                extractor.GetNextImage(jpegPath, ImageFormat.Jpeg);

                // Convert the saved JPEG to grayscale and overwrite the file
                using (Bitmap original = new Bitmap(jpegPath))
                using (Bitmap grayBmp = new Bitmap(original.Width, original.Height))
                using (Graphics g = Graphics.FromImage(grayBmp))
                using (ImageAttributes ia = new ImageAttributes())
                {
                    // Grayscale color matrix
                    ColorMatrix cm = new ColorMatrix(new float[][]
                    {
                        new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                        new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                        new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                        new float[] {0,      0,      0,      1, 0},
                        new float[] {0,      0,      0,      0, 1}
                    });

                    ia.SetColorMatrix(cm);

                    // Draw the original image onto the grayscale bitmap
                    g.DrawImage(original,
                        new Rectangle(0, 0, original.Width, original.Height),
                        0, 0, original.Width, original.Height,
                        GraphicsUnit.Pixel, ia);

                    // Save the grayscale bitmap, overwriting the original JPEG
                    grayBmp.Save(jpegPath, ImageFormat.Jpeg);
                }

                Console.WriteLine($"Extracted and grayscaled image saved to: {jpegPath}");
                imageIndex++;
            }
        }
    }
}
