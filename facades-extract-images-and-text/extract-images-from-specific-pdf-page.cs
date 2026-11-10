using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Directory where extracted images will be saved
        const string outputDir = "ExtractedImages";
        // Page to extract images from (1‑based index)
        const int pageNumber = 2;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Limit extraction to a single page
            extractor.StartPage = pageNumber; // inclusive start page (1‑based)
            extractor.EndPage   = pageNumber; // inclusive end page (same as start)

            // Extract images from the specified page range
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string imagePath = Path.Combine(outputDir, $"Page{pageNumber}_Image{imageIndex}.png");
                // Save the current image to the file system
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image: {imagePath}");
                imageIndex++;
            }

            if (imageIndex == 1)
                Console.WriteLine($"No images found on page {pageNumber}.");
        }
    }
}