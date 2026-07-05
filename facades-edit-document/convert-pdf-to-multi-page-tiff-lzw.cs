using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfConverter
using Aspose.Pdf.Devices;          // CompressionType, TiffSettings

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
            // Initialize the converter and bind the source PDF
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(inputPdf);
                converter.DoConvert();

                // Configure TIFF settings with LZW compression
                TiffSettings tiffSettings = new TiffSettings
                {
                    Compression = CompressionType.LZW
                };

                // Save all pages as a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff, tiffSettings);
            }

            Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}