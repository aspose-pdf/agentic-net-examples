using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the PdfConverter
            PdfConverter converter = new PdfConverter();
            // Set the desired DPI (600) for detailed graphics extraction
            converter.Resolution = new Resolution(600);
            // Bind the source PDF
            converter.BindPdf(inputPdf);
            // Perform the conversion preparation step
            converter.DoConvert();
            // Save all pages as a single multi‑page TIFF using LZW compression (optional)
            converter.SaveAsTIFF(outputTiff, CompressionType.LZW);
            // Release resources held by the converter
            converter.Close();

            Console.WriteLine($"PDF successfully converted to TIFF at 600 DPI: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
