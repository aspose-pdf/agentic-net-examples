using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

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

        // PdfExtractor is a Facade that implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Define the page range (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 5;
            extractor.EndPage   = 10;

            // Perform the image extraction for the specified pages
            extractor.ExtractImage();

            int imageCounter = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image_{imageCounter}.png");
                // Save each image as PNG
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                imageCounter++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}