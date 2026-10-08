using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputDir  = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – wrap in using
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPath);

            // Set the page range (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 5;
            extractor.EndPage   = 10;

            // Extract images from the specified pages
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Get the next image into a memory stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // GetNextImage requires a stream argument
                    extractor.GetNextImage(imageStream);

                    // Reset stream position before writing
                    imageStream.Position = 0;

                    // Build a unique file name for each image
                    string outputPath = Path.Combine(
                        outputDir,
                        $"Page5to10_Image_{imageIndex}.png");

                    // Write the image bytes to a PNG file
                    using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        imageStream.CopyTo(file);
                    }

                    Console.WriteLine($"Saved image {imageIndex} → {outputPath}");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}