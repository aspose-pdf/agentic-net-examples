using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // <-- added for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (implements IDisposable) within a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Set the page range (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 5;
            extractor.EndPage   = 10;

            // Perform image extraction for the specified pages
            extractor.ExtractImage();

            int imageCounter = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build output file path (PNG format)
                string outputPath = Path.Combine(outputFolder, $"image_{imageCounter}.png");

                // Save the current image as PNG
                extractor.GetNextImage(outputPath, ImageFormat.Png);

                imageCounter++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
