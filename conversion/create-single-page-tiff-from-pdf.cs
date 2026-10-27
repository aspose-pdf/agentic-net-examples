using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set the resolution for the TIFF output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create the TiffDevice with the custom resolution
            TiffDevice tiffDevice = new TiffDevice(resolution);

            // Convert only the first page (page numbers are 1‑based) to a single‑page TIFF
            // The core API uses the Process method; no Compression property is available in older versions.
            tiffDevice.Process(pdfDoc, 1, 1, outputTiff);
        }

        Console.WriteLine($"TIFF created: {outputTiff}");
    }
}
