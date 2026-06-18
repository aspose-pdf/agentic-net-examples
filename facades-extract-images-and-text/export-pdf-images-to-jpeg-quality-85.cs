using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create and configure the PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF
            converter.BindPdf(inputPdf);
            // Perform initial conversion setup
            converter.DoConvert();

            int imageCount = 1;
            // Iterate through all images in the PDF
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"image_{imageCount}.jpg");
                // Export the current image as JPEG with quality set to 85
                converter.GetNextImage(outputPath, ImageFormat.Jpeg, 85);
                imageCount++;
            }
        }

        Console.WriteLine("All images have been exported as JPEG with quality 85.");
    }
}