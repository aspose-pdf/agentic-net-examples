using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPdf);

                // Extract all images from the PDF
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through each extracted image
                while (extractor.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
                    // Save the current image as PNG
                    extractor.GetNextImage(outPath);
                    Console.WriteLine($"Saved {outPath}");
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