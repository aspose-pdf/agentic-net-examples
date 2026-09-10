using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for TIFF

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor implements IDisposable – wrap in using for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Enable image extraction mode
            extractor.ExtractImage();

            int imageNumber = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image_{imageNumber}.tiff");
                // Save each image as TIFF (lossless archival format)
                extractor.GetNextImage(outputPath, ImageFormat.Tiff);
                imageNumber++;
            }
        }

        Console.WriteLine($"Image extraction completed. TIFF files saved to '{outputFolder}'.");
    }
}