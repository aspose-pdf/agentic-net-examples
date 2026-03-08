using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Define the page range from which images will be extracted (1‑based indexing)
        const int startPage = 2;
        const int endPage   = 5;

        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor is a Facades class that implements IDisposable
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Set the page range for image extraction
            extractor.StartPage = startPage;
            extractor.EndPage   = endPage;

            // Perform the image extraction operation
            extractor.ExtractImage();

            int imageIndex = 1;

            // Retrieve each extracted image and save it to a file
            while (extractor.HasNextImage())
            {
                // Build a file name for the current image
                string imagePath = Path.Combine(outputFolder, $"image_{imageIndex}.jpg");

                // Save the image; without specifying format it defaults to JPEG
                extractor.GetNextImage(imagePath);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}