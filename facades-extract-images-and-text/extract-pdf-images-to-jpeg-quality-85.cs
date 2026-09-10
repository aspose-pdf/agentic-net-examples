using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory to store extracted JPEG images
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Create and use PdfConverter (facade) to extract images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int imageIndex = 1;

            // Iterate through all images in the PDF
            while (converter.HasNextImage())
            {
                // Build output file path for each image
                string outputFile = Path.Combine(outputDir, $"image{imageIndex}.jpg");

                // Export the current image as JPEG with quality 85
                converter.GetNextImage(outputFile, ImageFormat.Jpeg, 85);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}