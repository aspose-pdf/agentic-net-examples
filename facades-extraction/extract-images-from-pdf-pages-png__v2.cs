using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor implements IDisposable – wrap in using for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // NOTE: The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // The default behavior extracts all images, which satisfies the requirement.
            // If a newer version provides a mode, you could use the fully‑qualified enum, e.g.:
            // extractor.ExtractImageMode = Aspose.Pdf.Facades.ExtractImageMode.ActuallyUsed;

            // Limit extraction to pages 6 through 8 (1‑based indexing)
            extractor.StartPage = 6;
            extractor.EndPage   = 8;

            // Perform the extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image sequentially
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");
                // GetNextImage writes the image directly to the file using the supplied format
                extractor.GetNextImage(outPath, ImageFormat.Png);
                Console.WriteLine($"Saved: {outPath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
