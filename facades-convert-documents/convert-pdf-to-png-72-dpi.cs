using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Set low resolution (72 DPI) for quick preview images
            converter.Resolution = new Resolution(72);

            // Prepare the converter (initial work before extracting images)
            converter.DoConvert();

            int pageNumber = 1;
            // Extract each page as a PNG image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                // GetNextImage infers the image format from the file extension, no ImageFormat needed
                converter.GetNextImage(outputPath);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
