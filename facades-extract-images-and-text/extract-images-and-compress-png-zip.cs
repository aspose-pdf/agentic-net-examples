using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where extracted PNG images will be saved
        const string outputDir = "ExtractedImages";

        // Path of the final ZIP archive (lossless compression of PNG files)
        const string zipPath = "images.zip";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // -----------------------------------------------------------------
        // 1. Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        // -----------------------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF document to the extractor
        extractor.BindPdf(inputPdf);

        // Instruct the extractor to look for images
        extractor.ExtractImage();

        int imageIndex = 1;

        // Loop through all extracted images
        while (extractor.HasNextImage())
        {
            // Retrieve the current image into a memory stream
            using (MemoryStream imgStream = new MemoryStream())
            {
                // GetNextImage fills the provided stream with the image bytes
                extractor.GetNextImage(imgStream);

                // Build a file name for the PNG image
                string pngPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                // The extracted image bytes are already in PNG format (Aspose extracts
                // the original image data). Write them directly to disk.
                File.WriteAllBytes(pngPath, imgStream.ToArray());

                imageIndex++;
            }
        }

        // -----------------------------------------------------------------
        // 2. Compress the PNG files using a lossless ZIP archive
        // -----------------------------------------------------------------
        // Delete any existing archive to avoid exceptions
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a ZIP archive containing all PNG files.
        // CompressionLevel.Optimal provides lossless compression.
        ZipFile.CreateFromDirectory(outputDir, zipPath, CompressionLevel.Optimal, false);

        Console.WriteLine($"Extracted {imageIndex - 1} image(s) to '{outputDir}'.");
        Console.WriteLine($"Compressed images into '{zipPath}'.");
    }
}