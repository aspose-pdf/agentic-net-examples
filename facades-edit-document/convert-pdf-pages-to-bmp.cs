using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade API for conversion
using System.Drawing.Imaging;      // ImageFormat enum for BMP

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where BMP images will be saved
        const string outputDir = "BmpPages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Initialize the PdfConverter (Facades) and bind the PDF
        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPdf);
        converter.DoConvert();   // Prepare for image extraction

        int pageIndex = 1;
        // Extract each page as a BMP image (24‑bit by default)
        while (converter.HasNextImage())
        {
            string bmpPath = Path.Combine(outputDir, $"page_{pageIndex}.bmp");
            // ImageFormat.Bmp produces a standard 24‑bit BMP file
            converter.GetNextImage(bmpPath, ImageFormat.Bmp);
            pageIndex++;
        }

        // Release resources held by the converter
        converter.Close();

        Console.WriteLine($"Conversion completed. BMP files saved to '{outputDir}'.");
    }
}