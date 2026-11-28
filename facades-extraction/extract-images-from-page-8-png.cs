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

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Restrict extraction to page 8 only (1‑based index)
            extractor.StartPage = 8;
            extractor.EndPage   = 8;

            // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // The default extraction extracts all images on the selected pages, which satisfies the requirement.
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed; // <-- removed for compatibility

            // Start image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all extracted images
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputFolder, $"page8_image_{imageIndex}.png");
                // Save the current image as PNG (ImageFormat determines the format)
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                Console.WriteLine($"Saved image: {imagePath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
