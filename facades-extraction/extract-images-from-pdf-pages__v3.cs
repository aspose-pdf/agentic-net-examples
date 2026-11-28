using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor implements IDisposable – use a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(pdfPath);

            // Configure extraction: only pages 10‑12
            extractor.StartPage = 10;
            extractor.EndPage   = 12;

            // The ExtractImageMode property does not exist in the current Aspose.Pdf.Facades versions.
            // Image extraction is performed by calling ExtractImage(), which extracts all images on the
            // selected pages by default.
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image and save as JPEG with incremental suffix
            while (extractor.HasNextImage())
            {
                string outFile = Path.Combine(outputFolder,
                    $"image_page10_12_{imageIndex}.jpg");
                // GetNextImage(string) saves the image directly to the supplied path
                extractor.GetNextImage(outFile);
                Console.WriteLine($"Saved image {imageIndex} to '{outFile}'");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
