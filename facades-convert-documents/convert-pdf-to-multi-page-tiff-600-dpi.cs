using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution class

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (Facade) to convert PDF pages to a single multi‑page TIFF.
        // Wrap in using to ensure resources are released.
        using (PdfConverter converter = new PdfConverter())
        {
            // Set desired resolution (600 DPI) for detailed graphics extraction.
            converter.Resolution = new Resolution(600);

            // Bind the source PDF file.
            converter.BindPdf(inputPdf);

            // Prepare the converter (required before conversion).
            converter.DoConvert();

            // Save all pages as a single TIFF file.
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"PDF converted to TIFF at {outputTiff}");
    }
}