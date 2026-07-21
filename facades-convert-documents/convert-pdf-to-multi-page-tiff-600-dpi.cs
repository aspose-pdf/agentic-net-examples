using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Convert PDF to a multi‑page TIFF with 600 DPI resolution
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPath);

            // Set the desired resolution (600 DPI)
            converter.Resolution = new Aspose.Pdf.Devices.Resolution(600);

            // Initialize conversion
            converter.DoConvert();

            // Save all pages as a single TIFF file
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}'.");
    }
}