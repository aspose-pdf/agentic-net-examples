using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputTiff = "pages3to8.tiff"; // resulting multi‑page TIFF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfConverter is a Facade class that can convert PDF pages to images.
        // It implements IDisposable, so we use a using block for deterministic cleanup.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file.
            converter.BindPdf(inputPdf);

            // Set the page range to convert (pages are 1‑based).
            converter.StartPage = 3;
            converter.EndPage   = 8;

            // CropBox is the default coordinate type; no explicit setting is required.
            // converter.CoordinateType = CoordinateType.CropBox; // <-- removed

            // Use the default resolution (150 DPI) – no need to set it explicitly.

            // Convert the selected pages to a single multi‑page TIFF file.
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"Pages 3‑8 have been saved to TIFF: {outputTiff}");
    }
}
