using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF
        const string outputFolder = "ExtractedImages";     // folder for JPEGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facade) to extract images from page 5 only
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.StartPage = 5;   // 1‑based page index
            extractor.EndPage   = 5;
            extractor.ExtractImage(); // prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream (default JPEG format)
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0; // reset for reading

                    // Load the image, convert to grayscale, and save as JPEG
                    using (Bitmap bitmap = new Bitmap(imgStream))
                    {
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            for (int x = 0; x < bitmap.Width; x++)
                            {
                                Color pixel = bitmap.GetPixel(x, y);
                                int gray = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                                Color grayColor = Color.FromArgb(gray, gray, gray);
                                bitmap.SetPixel(x, y, grayColor);
                            }
                        }

                        string outPath = Path.Combine(outputFolder,
                            $"page5_image_{imageIndex}.jpg");
                        bitmap.Save(outPath, ImageFormat.Jpeg);
                    }
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and grayscale conversion completed.");
    }
}