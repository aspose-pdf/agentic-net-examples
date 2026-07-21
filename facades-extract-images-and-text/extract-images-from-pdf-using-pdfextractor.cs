using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable, so a using block ensures it is disposed automatically
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF file into the extractor
                extractor.BindPdf(inputPdf);

                // Perform the image extraction operation
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate over all extracted images
                while (extractor.HasNextImage())
                {
                    // Build a file name for each image
                    string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.png");

                    // Save the current image to the file system
                    extractor.GetNextImage(imagePath);

                    imageIndex++;
                }
            }

            Console.WriteLine($"All images have been extracted to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}