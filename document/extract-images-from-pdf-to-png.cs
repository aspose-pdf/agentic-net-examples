using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Path to source PDF
        const string outputDir = "ExtractedImages";    // Directory for PNG files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            int imageIndex = 0;

            // Iterate through all pages (1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate through all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;
                    string outPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                    // Save the XImage to a memory stream first (original resolution)
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);               // XImage.Save expects a Stream
                        ms.Position = 0;            // Reset stream position for reading

                        // Load the image with System.Drawing and re‑save as PNG preserving resolution
                        using (Bitmap bmp = new Bitmap(ms))
                        {
                            bmp.Save(outPath, ImageFormat.Png);
                        }
                    }

                    Console.WriteLine($"Saved image {imageIndex} → {outPath}");
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
