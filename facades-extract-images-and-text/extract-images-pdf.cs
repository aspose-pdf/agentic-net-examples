using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        string inputPdfPath = "sample.pdf";
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), "extracted-images-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Initialize the PDF extractor facade
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdfPath);
        // Use default extraction mode (DefinedInResources)
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string outputImagePath = Path.Combine(tempFolder, "image-" + imageIndex + ".png");
            extractor.GetNextImage(outputImagePath);
            imageIndex++;
        }

        extractor.Close();
        Console.WriteLine($"Images have been extracted to: {tempFolder}");
    }
}