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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a TiffDevice with default settings (uses default compression)
            TiffDevice tiffDevice = new TiffDevice();

            // Convert all pages of the PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputTiff);
        }

        Console.WriteLine($"Multi‑page TIFF saved to '{outputTiff}'.");
    }
}