using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Extract images using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Get the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0;

                    // Load the image with System.Drawing (GDI+). The PNG encoder used by GDI+ is lossless.
                    using (Image image = Image.FromStream(imgStream))
                    {
                        string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.png");

                        // Save as PNG – GDI+ automatically applies lossless compression.
                        image.Save(outPath, ImageFormat.Png);
                    }
                }

                Console.WriteLine($"Saved image {imageIndex}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and compression completed.");
    }
}
