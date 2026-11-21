using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the PDF converter (facade API)
            using var converter = new PdfConverter();

            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Prepare the converter for conversion
            converter.DoConvert();

            // Convert all pages and save as a multi‑page TIFF file using LZW compression
            converter.SaveAsTIFF(outputTiff, CompressionType.LZW);

            Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
