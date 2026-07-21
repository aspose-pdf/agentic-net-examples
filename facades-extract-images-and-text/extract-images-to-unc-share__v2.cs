using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = @"C:\Docs\sample.pdf";               // Local PDF file
        const string outputUncDir = @"\\SERVER\Share\ExtractedImages";   // UNC folder

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Ensure the UNC output directory exists
            Directory.CreateDirectory(outputUncDir);

            // Use PdfExtractor (Facade) to extract images
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);          // Load the PDF
                extractor.ExtractImage();                 // Prepare image extraction

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outputFile = Path.Combine(outputUncDir, $"image-{imageIndex}.png");
                    // Save each image as PNG; returns true if successful
                    bool saved = extractor.GetNextImage(outputFile, ImageFormat.Png);
                    if (!saved)
                    {
                        Console.Error.WriteLine($"Failed to extract image {imageIndex}");
                    }
                    imageIndex++;
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}