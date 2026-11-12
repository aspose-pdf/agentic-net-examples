using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable – wrap in using for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the source PDF
                extractor.BindPdf(inputPdf);

                // Extract all images from the document
                extractor.ExtractImage();

                int imageIndex = 1;

                // Iterate through each extracted image
                while (extractor.HasNextImage())
                {
                    // Retrieve the image into a memory stream (no temporary file)
                    using (var imgStream = new MemoryStream())
                    {
                        // GetNextImage requires a destination stream (or path). Using the stream overload.
                        extractor.GetNextImage(imgStream);

                        // Build a PNG file name for the current image
                        string outPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                        // Write the image bytes to disk as a PNG file
                        File.WriteAllBytes(outPath, imgStream.ToArray());
                        Console.WriteLine($"Saved image {imageIndex} to {outPath}");
                    }

                    imageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
