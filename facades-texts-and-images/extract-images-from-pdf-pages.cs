using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: The file '{inputPdf}' was not found.");
            return;
        }

        // Create a unique temporary folder for the extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(),
                                         "ExtractedImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Use PdfExtractor (facade) to extract images from pages 2‑5
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);          // Load the PDF
            extractor.StartPage = 2;              // First page to process (1‑based)
            extractor.EndPage   = 5;              // Last page to process
            extractor.ExtractImage();             // Prepare image extraction

            int imgIndex = 1;
            while (extractor.HasNextImage())
            {
                string outFile = Path.Combine(tempFolder, $"image-{imgIndex}.png");
                // Save each image using the overload that preserves the original format.
                // This avoids the Windows‑only ImageFormat.Png warning.
                extractor.GetNextImage(outFile);
                imgIndex++;
            }
        }

        Console.WriteLine($"Images from pages 2‑5 have been saved to: {tempFolder}");
    }
}
