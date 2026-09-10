using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // UNC path to the source PDF
        const string pdfPath = @"\\server\share\input\sample.pdf";

        // UNC folder where extracted images will be saved
        const string outputFolder = @"\\server\share\output\images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the destination directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to pull images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all images found in the PDF
            while (extractor.HasNextImage())
            {
                // Build the UNC file name for each extracted image
                string outputFile = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Save the current image as PNG; GetNextImage returns a bool indicating success
                extractor.GetNextImage(outputFile, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}