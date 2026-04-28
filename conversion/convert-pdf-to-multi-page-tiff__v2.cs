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

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a TiffDevice with default settings (no parameters)
            TiffDevice tiffDevice = new TiffDevice();

            // Convert all pages of the PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputPath);
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}'.");
    }
}