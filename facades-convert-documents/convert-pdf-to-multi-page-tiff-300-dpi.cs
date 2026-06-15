using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfConverter, PdfFileInfo, etc.
using Aspose.Pdf.Devices;          // Resolution

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output multi‑page TIFF file path
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a PdfConverter, set the desired DPI (300) and perform conversion.
        using (PdfConverter converter = new PdfConverter())
        {
            // Set resolution – higher DPI yields higher quality images.
            converter.Resolution = new Resolution(300);

            // Bind the source PDF document.
            converter.BindPdf(inputPdf);

            // Prepare the converter (required before extracting images).
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file.
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outputTiff}");
    }
}