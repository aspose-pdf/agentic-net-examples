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

        // Use PdfConverter facade inside a using block to ensure proper disposal
        using (PdfConverter converter = new PdfConverter())
        {
            // Set the desired resolution (400 DPI) for high‑resolution output.
            // PdfConverter.Resolution expects a Aspose.Pdf.Devices.Resolution object.
            converter.Resolution = new Resolution(400);

            // Bind the source PDF file
            converter.BindPdf(inputPath);

            // Initialize the converter before conversion
            converter.DoConvert();

            // Convert all pages to a single multi‑page TIFF file
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"PDF successfully converted to TIFF at '{outputPath}' with 400 DPI.");
    }
}
