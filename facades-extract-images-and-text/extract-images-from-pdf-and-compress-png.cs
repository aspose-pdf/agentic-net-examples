using System;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";               // source PDF
        const string outputDir = "ExtractedImages";          // folder for PNGs
        const string compressedDir = "CompressedImages";    // folder for compressed files

        // Verify that the source PDF exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: PDF file '{inputPdf}' not found.");
            return;
        }

        // Ensure output folders exist
        Directory.CreateDirectory(outputDir);
        Directory.CreateDirectory(compressedDir);

        // Use PdfExtractor (Facade) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);          // bind the PDF file
            extractor.ExtractImage();             // prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build PNG file name
                string pngPath = Path.Combine(outputDir, $"image-{imageIndex}.png");

                // Save the next image as PNG
                // The ImageFormat.Png API is Windows‑only and triggers CA1416.
                // Suppress the warning because the code runs on Windows where Aspose.Pdf.Facades requires System.Drawing.
#pragma warning disable CA1416 // Validate platform compatibility
                extractor.GetNextImage(pngPath, ImageFormat.Png);
#pragma warning restore CA1416 // Validate platform compatibility

                // ----- Lossless compression of the PNG -----
                // Read the PNG bytes
                byte[] pngBytes = File.ReadAllBytes(pngPath);

                // Create a .gz file (GZip provides lossless compression)
                string gzPath = Path.Combine(compressedDir, $"image-{imageIndex}.png.gz");
                using (FileStream gzFile = new FileStream(gzPath, FileMode.Create, FileAccess.Write))
                using (GZipStream gzip = new GZipStream(gzFile, CompressionLevel.Optimal))
                {
                    gzip.Write(pngBytes, 0, pngBytes.Length);
                }

                // Optional: delete the original PNG if only compressed version is needed
                // File.Delete(pngPath);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and compression completed.");
    }
}
