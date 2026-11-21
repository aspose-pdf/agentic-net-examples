using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable – wrap in using for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPath);

                // NOTE: The ExtractImageMode property does not exist in the current Aspose.Pdf version.
                // Image extraction is performed by calling ExtractImage() for the selected pages.

                int imageCounter = 1;

                // Loop through the required page range (5‑10 inclusive)
                for (int page = 5; page <= 10; page++)
                {
                    // Set the page range to a single page for each iteration
                    extractor.StartPage = page;
                    extractor.EndPage   = page;

                    // Extract images from the current page
                    extractor.ExtractImage();

                    // Save each extracted image as PNG
                    while (extractor.HasNextImage())
                    {
                        string fileName = $"page{page}_img{imageCounter}.png";
                        string outPath  = Path.Combine(outputDir, fileName);

                        // GetNextImage overload that specifies output path and format
                        extractor.GetNextImage(outPath, ImageFormat.Png);

                        Console.WriteLine($"Saved: {outPath}");
                        imageCounter++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
