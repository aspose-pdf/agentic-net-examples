using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf.Facades;

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

        // Create a temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeExtractedImages");
        Directory.CreateDirectory(tempFolder);

        // Use PdfExtractor (facade) to extract images from pages 2‑5
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Define the page range (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 2;
            extractor.EndPage   = 5;

            // Prepare for image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as PNG in the temporary folder
                string outputPath = Path.Combine(tempFolder, $"image-{imageIndex}.png");
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine($"Images extracted to: {tempFolder}");
    }
}