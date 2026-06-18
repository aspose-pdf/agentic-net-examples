using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tif";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document and convert all pages to a single multi‑page TIFF
        using (Document pdfDocument = new Document(inputPath))
        {
            // TiffDevice with default settings (default resolution, no compression, etc.)
            TiffDevice tiffDevice = new TiffDevice();

            // Process the whole document and save the result as a TIFF file
            tiffDevice.Process(pdfDocument, outputPath);
        }

        Console.WriteLine($"TIFF saved to '{outputPath}'.");
    }
}