using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "page1.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Configure TIFF settings – 300 DPI and CCITT Group 4 compression
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.CCITT4
            };

            // Create a TiffDevice with the desired resolution and settings.
            // TiffDevice does NOT implement IDisposable, so we do NOT wrap it in a using statement.
            TiffDevice tiffDevice = new TiffDevice(new Resolution(300), tiffSettings);

            // Process only the first page (page numbers are 1‑based) and write the result to the output file.
            tiffDevice.Process(pdfDoc, 1, 1, outputTiffPath);
        }

        Console.WriteLine($"Single‑page TIFF created at: {outputTiffPath}");
    }
}
