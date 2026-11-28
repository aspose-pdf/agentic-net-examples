using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file.
            extractor.BindPdf(inputPdf);

            // Define the page range (1‑based indexing).
            extractor.StartPage = 1;
            extractor.EndPage   = 3;

            // The ExtractImageMode property does not exist in the referenced
            // Aspose.Pdf.Facades version. Image extraction works with the default
            // behavior, so the line is removed.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Extract images from the specified pages.
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.jpg");
                // Save the next image as JPEG.
                extractor.GetNextImage(outPath, ImageFormat.Jpeg);
                Console.WriteLine($"Saved image {imageIndex} → {outPath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
