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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor is a facade that implements IDisposable, so use a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Set the extraction mode to DefinedInResources (extract all images defined in resources)
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Start the image extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image until none remain
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                // Save the image to the specified file (format inferred from extension)
                extractor.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}