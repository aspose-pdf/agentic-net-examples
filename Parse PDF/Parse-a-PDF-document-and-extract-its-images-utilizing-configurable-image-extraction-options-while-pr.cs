using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <exe> <input-pdf> <output-folder> [mode]");
            Console.Error.WriteLine("  mode: 0 = DefinedInResources (default), 1 = ActuallyUsed");
            return;
        }

        string inputPdfPath = args[0];
        string outputFolder = args[1];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Determine extraction mode (optional third argument)
        ExtractImageMode mode = ExtractImageMode.DefinedInResources; // default
        if (args.Length >= 3 && int.TryParse(args[2], out int modeValue))
        {
            mode = modeValue == 1 ? ExtractImageMode.ActuallyUsed : ExtractImageMode.DefinedInResources;
        }

        // Use PdfExtractor (facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Configure extraction options
            extractor.ExtractImageMode = mode;          // Choose extraction mode
            extractor.Resolution = 300;                // Higher resolution for better quality (optional)

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name for each extracted image
                string imagePath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                // Save the next image; using .png preserves lossless quality for most raster images
                extractor.GetNextImage(imagePath);

                Console.WriteLine($"Extracted image #{imageIndex} to: {imagePath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}