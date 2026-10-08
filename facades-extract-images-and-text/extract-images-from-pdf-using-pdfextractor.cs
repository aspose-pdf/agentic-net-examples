using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable, so we can use a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF
                extractor.BindPdf(inputPath);

                // Extract images from the whole document
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Get the current image via a stream (newer Aspose API overload)
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream); // writes image to the stream
                        byte[] imageBytes = imgStream.ToArray();

                        // Save the image (using PNG extension as a generic format)
                        string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
                        File.WriteAllBytes(outPath, imageBytes);
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
