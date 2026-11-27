using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Create a PdfConverter instance (no IDisposable, so no using block)
            PdfConverter converter = new PdfConverter();

            // Bind the source PDF file to the converter
            converter.BindPdf(inputPdfPath);

            // Prepare the converter for conversion
            converter.DoConvert();

            // Convert all pages to a multi‑page TIFF image
            converter.SaveAsTIFF(outputTiffPath);

            // Release all allocated resources
            converter.Close();

            Console.WriteLine($"PDF successfully converted to TIFF: {outputTiffPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}