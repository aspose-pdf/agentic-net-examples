using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable – wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF file
                extractor.BindPdf(inputPdf);

                // Enable image extraction
                extractor.ExtractImage();

                int imageIndex = 1;

                // Iterate over all images in the document
                while (extractor.HasNextImage())
                {
                    // Retrieve the current image into a memory stream
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imageStream);

                        // Reset stream position before reading
                        imageStream.Position = 0;

                        // Define the output PNG file name
                        string outputPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                        // Write the image bytes to the PNG file
                        File.WriteAllBytes(outputPath, imageStream.ToArray());

                        Console.WriteLine($"Saved image {imageIndex} to '{outputPath}'");
                        imageIndex++;
                    }
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}