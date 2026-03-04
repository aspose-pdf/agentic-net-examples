using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where extracted images will be saved
        const string outputDir = "ExtractedImages";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (it implements IDisposable) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Optional: extract images only from a specific page.
            // Set both StartPage and EndPage to the same page number (1‑based indexing).
            // Uncomment and change the page number as needed.
            // extractor.StartPage = 2;
            // extractor.EndPage   = 2;

            // Choose extraction mode.
            // DefinedInResources extracts all images defined in resources.
            // ActuallyUsed extracts only images that are rendered on the page.
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Perform the extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build output file name: image-1.jpg, image-2.jpg, etc.
                string outputPath = Path.Combine(outputDir, $"image-{imageIndex}.jpg");

                // Save the extracted image. You can also specify a different ImageFormat if required.
                extractor.GetNextImage(outputPath);

                Console.WriteLine($"Extracted image saved to: {outputPath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}