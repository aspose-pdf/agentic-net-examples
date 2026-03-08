using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat enum

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string pdfPath = "input.pdf";

        // Page from which images will be extracted (1‑based index)
        const int pageNumber = 2;

        // Folder to store extracted images
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facade) to work with the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Limit extraction to the specified page
            extractor.StartPage = pageNumber;
            extractor.EndPage   = pageNumber;

            // Perform the image extraction operation
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all images found on the page
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each extracted image
                string outPath = Path.Combine(
                    outputFolder,
                    $"page{pageNumber}_image{imageIndex}.png");

                // Save the image as PNG (other formats are possible via ImageFormat)
                extractor.GetNextImage(outPath, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}