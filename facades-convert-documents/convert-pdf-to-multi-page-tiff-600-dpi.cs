using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfConverter
using Aspose.Pdf.Devices;          // Resolution

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF file
        const string outputTiff = "output.tif";        // destination multi‑page TIFF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfConverter implements IDisposable, so wrap it in a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF
                converter.BindPdf(inputPdf);

                // Set the desired resolution (600 DPI)
                converter.Resolution = new Resolution(600);

                // Prepare the converter
                converter.DoConvert();

                // Convert all pages to a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff);
            }

            Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}