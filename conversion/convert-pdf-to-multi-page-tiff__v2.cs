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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a TiffDevice with default settings
            TiffDevice tiffDevice = new TiffDevice();

            // Convert all pages of the PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputPath);
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}'.");
    }
}