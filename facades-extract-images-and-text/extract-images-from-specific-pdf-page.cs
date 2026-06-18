using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Page number to extract images from (1‑based indexing)
        const int pageNumber = 2;

        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfExtractor implements IDisposable, so use a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document
                extractor.BindPdf(inputPdf);

                // Limit extraction to a single page
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;

                // Extract images from the specified page range
                extractor.ExtractImage();

                int imageIndex = 1;
                // Retrieve each extracted image and save it
                while (extractor.HasNextImage())
                {
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"page{pageNumber}_image{imageIndex}.jpg");

                    // GetNextImage saves the image in JPEG format by default
                    extractor.GetNextImage(outputPath);
                    Console.WriteLine($"Saved image: {outputPath}");
                    imageIndex++;
                }

                // If no images were found, inform the user
                if (imageIndex == 1)
                {
                    Console.WriteLine($"No images found on page {pageNumber}.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}