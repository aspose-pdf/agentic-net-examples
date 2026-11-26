using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF document; using ensures deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // TiffDevice does NOT implement IDisposable, so instantiate without a using block.
            // The constructor expects a Resolution (default 300 DPI is typical).
            TiffDevice tiffDevice = new TiffDevice(new Resolution(300));

            // Convert all pages of the PDF to a multi‑page TIFF.
            // Use the overload that takes (Document, fromPage, toPage, outputPath).
            tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, outputPath);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputPath}");
    }
}
