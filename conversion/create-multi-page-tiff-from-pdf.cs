using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output multi‑page TIFF file path
        const string outputTiff = "output.tif";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a TiffDevice with default settings (default compression)
            TiffDevice tiffDevice = new TiffDevice();

            // Convert the entire PDF (all pages) to a single multi‑page TIFF file
            tiffDevice.Process(pdfDoc, outputTiff);
        }

        Console.WriteLine($"Multi‑page TIFF saved to '{outputTiff}'.");
    }
}