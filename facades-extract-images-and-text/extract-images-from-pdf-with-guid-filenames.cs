using System;
using System.IO;
using System.Drawing.Imaging;
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

        // Use PdfExtractor to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Prepare for image extraction
            extractor.ExtractImage();

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Generate a unique file name using GUID
                string uniqueName = Guid.NewGuid().ToString();
                string outputPath = Path.Combine(outputDir, uniqueName + ".png");

                // Save the next image as PNG
                extractor.GetNextImage(outputPath, ImageFormat.Png);
            }
        }

        Console.WriteLine($"Images extracted to '{outputDir}'.");
    }
}