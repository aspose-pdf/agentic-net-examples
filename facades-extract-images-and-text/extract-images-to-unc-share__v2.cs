using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file (local path)
        const string inputPdf = @"C:\Docs\sample.pdf";

        // UNC output directory (e.g., \\Server\Share\Images)
        const string uncOutputDir = @"\\Server\Share\Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the UNC directory exists
        try
        {
            Directory.CreateDirectory(uncOutputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{uncOutputDir}': {ex.Message}");
            return;
        }

        try
        {
            // Use PdfExtractor facade to extract images
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);          // Load the PDF
                extractor.ExtractImage();             // Prepare image extraction

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build UNC file name for each extracted image
                    string outputFile = Path.Combine(
                        uncOutputDir,
                        $"image-{imageIndex}.png");

                    // Save the image in PNG format
                    bool success = extractor.GetNextImage(outputFile, ImageFormat.Png);
                    if (!success)
                    {
                        Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                    }

                    imageIndex++;
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