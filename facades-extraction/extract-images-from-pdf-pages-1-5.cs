using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF
        const string outputDir = "ExtractedImages";        // folder for saved images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Extract images from pages 1‑5
        for (int page = 1; page <= 5; page++)
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPdf);

                // Restrict extraction to the current page
                extractor.StartPage = page;
                extractor.EndPage   = page;

                // Perform the extraction (no ExtractImageMode property in current versions)
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate over all extracted images on this page
                while (extractor.HasNextImage())
                {
                    string outFile = Path.Combine(outputDir,
                        $"Page{page}_Image{imageIndex}.jpg");

                    // Save the image as JPEG
                    extractor.GetNextImage(outFile, ImageFormat.Jpeg);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
