using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat for specifying output image type
using Aspose.Pdf.Facades;   // Facade classes for PDF operations

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Page number to extract images from (1‑based indexing)
        const int pageNumber = 2;

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (implements IDisposable) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Limit extraction to a single page by setting StartPage and EndPage to the same value
            extractor.StartPage = pageNumber;
            extractor.EndPage   = pageNumber;

            // Perform the image extraction operation
            extractor.ExtractImage();

            int imageIndex = 1;

            // Retrieve each extracted image and save it to the output folder
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(
                    outputFolder,
                    $"page{pageNumber}_image{imageIndex}.jpg");

                // Save the image as JPEG (you can choose other formats supported by ImageFormat)
                extractor.GetNextImage(outputPath, ImageFormat.Jpeg);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}