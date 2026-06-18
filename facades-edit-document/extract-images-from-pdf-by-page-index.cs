using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

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

        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable, so use a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Get total page count from the underlying Document
            int pageCount = extractor.Document.Pages.Count;

            // Iterate through each page to extract images per page
            for (int page = 1; page <= pageCount; page++)
            {
                extractor.StartPage = page;
                extractor.EndPage   = page;

                // Extract images from the current page
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build the output file name using the required pattern
                    string outputPath = Path.Combine(
                        outputDir,
                        $"Image_Page{page}_Index{imageIndex}.png");

                    // Save the next image as PNG
                    extractor.GetNextImage(outputPath, ImageFormat.Png);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputDir}'.");
    }
}