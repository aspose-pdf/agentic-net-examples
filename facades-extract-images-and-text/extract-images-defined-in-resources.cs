using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Set the extraction mode to DefinedInResources (default, but set explicitly as requested)
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Perform the image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image and save it to the output folder
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                extractor.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}