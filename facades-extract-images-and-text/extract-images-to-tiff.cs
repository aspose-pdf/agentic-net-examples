using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade APIs for extraction and conversion
using System.Drawing.Imaging;      // ImageFormat enum for GetNextImage
using System.Runtime.Versioning;   // For platform‑specific attribute

class Program
{
    // Suppress the CA1416 warning because TIFF support is Windows‑only.
    // The code will run on Windows platforms where System.Drawing is fully supported.
    [SupportedOSPlatform("windows")]
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Source PDF containing images
        const string outputDir = "ExtractedImages";    // Folder to store TIFF files

        // Verify that the source PDF exists before proceeding.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: The file '{inputPdf}' was not found. Please place the PDF in the executable's directory or provide a correct path.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to pull images out of the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Optional: set a higher resolution for better quality (default is 150 DPI)
            extractor.Resolution = 300;

            // Tell the extractor that we only want images
            extractor.ExtractImage();

            int imageIndex = 1;

            // Loop through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a file name for each image (e.g., image-1.tiff, image-2.tiff, ...)
                string outputPath = Path.Combine(outputDir, $"image-{imageIndex}.tiff");

                // Save the current image directly as TIFF (lossless)
                // ImageFormat.Tiff is Windows‑only; the method is marked with SupportedOSPlatform("windows")
                extractor.GetNextImage(outputPath, ImageFormat.Tiff);

                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction complete. TIFF files saved to '{outputDir}'.");
    }
}
