using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // CompressionType enum resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Path to source PDF
        const string outputTiff = "output.tiff"; // Desired TIFF output path

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfConverter implements Facade and supports IDisposable; use a using block for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Prepare the converter (required before saving)
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF using LZW compression
            converter.SaveAsTIFF(outputTiff, CompressionType.LZW);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}