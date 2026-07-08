using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Prepare for image extraction
            extractor.ExtractImage();

            // Iterate through all images
            while (extractor.HasNextImage())
            {
                // Generate a unique filename using GUID
                string uniqueName = Guid.NewGuid().ToString() + ".png"; // extension can be any; Aspose saves in original format
                string outputPath = Path.Combine(outputDir, uniqueName);

                // Save the next image. Use overload without ImageFormat to stay cross‑platform.
                extractor.GetNextImage(outputPath);
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
