using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution and CompressionType enums

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PDF converter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF document
            converter.BindPdf(inputPdf);

            // Set the resolution (DPI) for the output TIFF
            converter.Resolution = new Resolution(300);

            // Perform the conversion
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF using LZW compression.
            converter.SaveAsTIFF(outputTiff, CompressionType.LZW);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}
