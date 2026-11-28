using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Directory where extracted images will be saved
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable – wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPdf);
                // Prepare the extractor to retrieve images
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate over all images in the PDF
                while (extractor.HasNextImage())
                {
                    // Build a file name with an incremental number
                    string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                    // Save the current image to the specified path
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Saved image {imageIndex} to {imagePath}");
                    imageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}