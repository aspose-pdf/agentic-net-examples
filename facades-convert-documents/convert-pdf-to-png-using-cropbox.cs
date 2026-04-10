using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter facade to convert each page to PNG.
        // Aspose.Pdf uses the CropBox area by default, so no explicit CoordinateType is required.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Optional: set resolution if needed (default is 96 DPI)
            // converter.Resolution = new Resolution(150);

            // Initialize conversion process
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save them as PNG images
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                converter.GetNextImage(outputPath, ImageFormat.Png);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
