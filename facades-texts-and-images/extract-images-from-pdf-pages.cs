using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a unique temporary directory for the extracted images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposePdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Use PdfExtractor (Facade) to extract images from pages 2‑5
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Set the page range (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 2;
            extractor.EndPage   = 5;

            // Perform the image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build the output file path (PNG format)
                string outFile = Path.Combine(tempDir, $"image_{imageIndex}.png");

                // Save the next image; GetNextImage returns a bool indicating success
                extractor.GetNextImage(outFile, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine($"Images extracted to temporary folder: {tempDir}");
    }
}