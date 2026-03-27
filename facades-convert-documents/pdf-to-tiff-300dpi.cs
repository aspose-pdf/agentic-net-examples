using System;
using System.IO;
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

        // Use a using block to ensure resources are released
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(inputPath);
            // Set resolution to 300 DPI for high‑quality output (Resolution object required)
            converter.Resolution = new Resolution(300);
            converter.DoConvert();
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputPath}");
    }
}