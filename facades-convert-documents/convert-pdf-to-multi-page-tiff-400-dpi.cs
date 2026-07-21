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

        // Use PdfConverter (Facade) to convert PDF pages to a single multi‑page TIFF
        using (PdfConverter converter = new PdfConverter())
        {
            // Set high‑resolution (400 DPI) for archival quality.
            // The Resolution property expects an Aspose.Pdf.Devices.Resolution instance.
            converter.Resolution = new Resolution(400);

            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Prepare the converter (required before saving)
            converter.DoConvert();

            // Save all pages as one TIFF file
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}
