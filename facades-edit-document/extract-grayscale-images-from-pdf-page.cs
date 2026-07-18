using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Use PdfExtractor (Facade) to extract images from a specific page range
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(doc);
                extractor.StartPage = 5; // 1‑based page index
                extractor.EndPage   = 5;
                extractor.ExtractImage(); // Prepare extraction

                int imageIndex = 1;

                // Iterate over all extracted images on page 5
                while (extractor.HasNextImage())
                {
                    // Extract the image into a memory stream as JPEG
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream, ImageFormat.Jpeg);
                        imgStream.Position = 0; // Reset stream position for reading

                        // Load the JPEG into a System.Drawing.Bitmap for pixel manipulation
                        using (Bitmap bitmap = new Bitmap(imgStream))
                        {
                            // Convert each pixel to grayscale
                            for (int y = 0; y < bitmap.Height; y++)
                            {
                                for (int x = 0; x < bitmap.Width; x++)
                                {
                                    System.Drawing.Color pixel = bitmap.GetPixel(x, y);
                                    int gray = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                                    System.Drawing.Color grayColor = System.Drawing.Color.FromArgb(gray, gray, gray);
                                    bitmap.SetPixel(x, y, grayColor);
                                }
                            }

                            // Save the grayscale image as JPEG
                            string outPath = Path.Combine(outputFolder, $"page5_image_{imageIndex}.jpg");
                            bitmap.Save(outPath, ImageFormat.Jpeg);
                            Console.WriteLine($"Saved grayscale image: {outPath}");
                        }
                    }

                    imageIndex++;
                }
            }
        }
    }
}