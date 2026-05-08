using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (implements IDisposable) inside a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Prepare the extractor to pull images
            extractor.ExtractImage();

            // Iterate over all images in the document
            while (extractor.HasNextImage())
            {
                // Generate a unique file name using a GUID
                string guidFileName = Guid.NewGuid().ToString() + ".png"; // extension can be any supported type

                // Full path for the extracted image
                string outputPath = Path.Combine(outputDir, guidFileName);

                // Save the image. The overload without ImageFormat saves the image in its original format.
                extractor.GetNextImage(outputPath);
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
