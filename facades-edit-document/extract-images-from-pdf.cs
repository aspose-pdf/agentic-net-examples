using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor facade to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Extract images from the bound PDF
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build output file name (e.g., image-1.png, image-2.png, ...)
                string outputFile = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Retrieve the next image and save it as PNG
                // GetNextImage(string, ImageFormat) returns void; it writes the file directly
                extractor.GetNextImage(outputFile, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputFolder}'.");
    }
}