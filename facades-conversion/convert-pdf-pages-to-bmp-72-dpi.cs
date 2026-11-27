using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputDir  = "ConvertedPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Initialize the PdfConverter facade
        PdfConverter converter = new PdfConverter();

        // Bind the source PDF
        converter.BindPdf(inputPath);

        // Set the page range to convert (pages 3 to 6, inclusive)
        converter.StartPage = 3;
        converter.EndPage   = 6;

        // Set the rendering resolution to 72 DPI
        converter.Resolution = new Resolution(72);

        // Perform the conversion
        converter.DoConvert();

        // Retrieve each rendered page as a BMP image
        int pageNumber = converter.StartPage;
        while (converter.HasNextImage())
        {
            string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
            // Use fully qualified ImageFormat to avoid ambiguity with System.Drawing.Image
            converter.GetNextImage(outputPath, System.Drawing.Imaging.ImageFormat.Bmp);
            pageNumber++;
        }

        Console.WriteLine("Pages 3‑6 have been converted to BMP images.");
    }
}