using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Initialize the extractor (Facades API)
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF document to the extractor
        extractor.BindPdf(inputPdfPath);

        // Configure extractor to pull images only
        extractor.ExtractImage();

        int imageCounter = 1;

        // Iterate over all extracted images
        while (extractor.HasNextImage())
        {
            // Generate a GUID for a unique filename (no hyphens)
            string guidFileName = Guid.NewGuid().ToString("N") + ".png";

            // Full path for the extracted image
            string outputPath = Path.Combine(outputFolder, guidFileName);

            // Save the current image to the file stream
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                extractor.GetNextImage(fs);
            }

            Console.WriteLine($"Image {imageCounter} saved as {outputPath}");
            imageCounter++;
        }

        Console.WriteLine("Image extraction completed.");
    }
}