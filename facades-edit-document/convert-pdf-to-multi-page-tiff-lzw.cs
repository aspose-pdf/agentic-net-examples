using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // CompressionType enum for TIFF

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "output.tiff";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfConverter, bind the PDF, perform conversion, and save as multi‑page TIFF with LZW compression
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(inputPdf);   // Load PDF
            converter.DoConvert();         // Prepare conversion
            converter.SaveAsTIFF(outputTiff, CompressionType.LZW); // Save TIFF with LZW compression
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}