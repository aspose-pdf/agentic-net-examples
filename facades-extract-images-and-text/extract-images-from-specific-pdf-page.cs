using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string pdfPath = "input.pdf";

        // Directory to store extracted images
        const string outputDir = "ExtractedImages";

        // Page number to extract images from (1‑based indexing)
        const int pageNumber = 2;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor and limit extraction to a single page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Limit extraction to the desired page (same start and end page)
            extractor.StartPage = pageNumber; // first page to extract (1‑based)
            extractor.EndPage   = pageNumber; // last page to extract (same as start)

            // NOTE: The ExtractImageMode property is not available in the current
            // version of Aspose.Pdf.Facades. The default behavior extracts all images,
            // so we simply call ExtractImage() without setting a mode.

            // Perform the extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputDir, $"page{pageNumber}_image{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'.");
                imageIndex++;
            }

            Console.WriteLine($"Extracted {imageIndex - 1} image(s) from page {pageNumber}.");
        }
    }
}
