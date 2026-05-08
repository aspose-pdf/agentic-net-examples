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

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a TiffDevice with default settings (default compression)
            TiffDevice tiffDevice = new TiffDevice();

            // Convert all pages of the PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDoc, outputPath);
        }

        Console.WriteLine($"Multi‑page TIFF saved to '{outputPath}'.");
    }
}