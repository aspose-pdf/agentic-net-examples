using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string tempFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Prepare a clean folder for extracted images
        if (Directory.Exists(tempFolder))
            Directory.Delete(tempFolder, true);
        Directory.CreateDirectory(tempFolder);

        // Extract images using PdfExtractor (Facades API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // The ExtractImageMode property does not exist in the current Aspose.Pdf version.
            // Calling ExtractImage() extracts all images by default.
            extractor.ExtractImage();

            int index = 1;
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(tempFolder, $"Image_{index}.png");
                // Save each image as PNG
                extractor.GetNextImage(outPath, ImageFormat.Png);
                index++;
            }
        }

        // Determine if any image files were produced
        bool hasImages = Directory.GetFiles(tempFolder).Length > 0;

        if (hasImages)
            Console.WriteLine("PDF contains images (not text-only).");
        else
            Console.WriteLine("PDF is text-only (no images found).");
    }
}
