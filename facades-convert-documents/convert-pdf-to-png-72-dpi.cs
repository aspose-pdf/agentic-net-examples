using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for PNG images
        const string outputDir = "OutputImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (implements IDisposable) to convert pages to PNG
        using (PdfConverter converter = new PdfConverter())
        {
            // Set low resolution (72 DPI) for quick preview generation
            converter.Resolution = new Resolution(72);

            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageIndex = 1;
            // Loop through all pages and save each as a PNG file
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.png");
                // The format is inferred from the .png extension; no ImageFormat needed
                converter.GetNextImage(outputPath);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
