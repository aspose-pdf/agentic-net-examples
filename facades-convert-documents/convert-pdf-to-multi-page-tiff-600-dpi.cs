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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfConverter is a Facade that handles PDF‑to‑image conversion.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF.
            converter.BindPdf(inputPdf);

            // Set the resolution to 600 DPI for high‑quality graphics.
            converter.Resolution = new Resolution(600);

            // Initialize the conversion process.
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file.
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}
