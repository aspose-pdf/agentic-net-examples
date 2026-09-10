using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Set the extraction mode to retrieve images defined in resources
            extractor.ExtractImageMode = Aspose.Pdf.ExtractImageMode.DefinedInResources;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image and save it to the output folder
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}