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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a temporary folder for the extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Use PdfExtractor to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Default mode (DefinedInResources) extracts all images defined in resources
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(tempFolder, $"image-{imageIndex}.png");
                // Extract the next image as PNG
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine($"Images have been extracted to: {tempFolder}");
    }
}