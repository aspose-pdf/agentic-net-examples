using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfConverter
using Aspose.Pdf.Devices;          // CompressionType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create and configure the PDF converter
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Prepare the converter (required before saving)
                converter.DoConvert();

                // Save all pages as a multi‑page TIFF using LZW compression
                converter.SaveAsTIFF(outputTiff, CompressionType.LZW);
            }

            Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}