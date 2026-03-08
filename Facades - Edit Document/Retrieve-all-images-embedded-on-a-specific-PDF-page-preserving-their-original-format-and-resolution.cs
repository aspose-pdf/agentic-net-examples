using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for extraction
using Aspose.Pdf;          // For ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // Source PDF
        const string outputDir  = "ExtractedImages";    // Folder for extracted images
        const int    targetPage = 2;                    // Page to extract images from (1‑based)

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – use using for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Restrict extraction to the desired page
            extractor.StartPage = targetPage;
            extractor.EndPage   = targetPage;

            // Extract images exactly as stored (original format & resolution)
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Perform the extraction operation
            extractor.ExtractImage();

            int imageCount = 1;
            // Iterate over all images found on the page
            while (extractor.HasNextImage())
            {
                // Preserve original format by not specifying an ImageFormat
                string outPath = Path.Combine(outputDir,
                    $"page{targetPage}_image{imageCount}.pdf");

                // Save the image; returns true if successful
                extractor.GetNextImage(outPath);
                imageCount++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}