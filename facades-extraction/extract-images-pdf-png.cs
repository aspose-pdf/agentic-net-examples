using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the source PDF
            extractor.BindPdf(pdfPath);

            // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // Image extraction is performed by default when calling ExtractImage(), so the line is omitted.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // removed for compatibility

            // Perform the extraction for all pages
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
                // Save each image as PNG
                extractor.GetNextImage(outPath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted and saved as PNG files.");
    }
}
