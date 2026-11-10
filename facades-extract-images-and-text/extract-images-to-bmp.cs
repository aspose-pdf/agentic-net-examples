using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor implements IDisposable – use a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // The ExtractImageMode property is not available in the referenced version of Aspose.Pdf.Facades.
            // Image extraction is performed by calling ExtractImage() which extracts all images by default.
            extractor.ExtractImage();

            int index = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a BMP file name preserving the original order
                string outputPath = Path.Combine(outputFolder, $"image_{index}.bmp");

                // Save the current image directly as BMP – this retains the image's native resolution and color depth
                extractor.GetNextImage(outputPath, ImageFormat.Bmp);
                index++;
            }
        }

        Console.WriteLine($"Extraction complete. BMP files saved to '{outputFolder}'.");
    }
}
