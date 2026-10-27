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

        // Load the PDF document; using ensures deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Define the resolution (default 300 DPI). The TiffDevice constructor expects a Resolution object.
            Resolution resolution = new Resolution(300);

            // Create a TiffDevice with default compression (LZW) using the resolution.
            TiffDevice tiffDevice = new TiffDevice(resolution);

            // Create a file stream for the TIFF output and render all pages into a multi‑page TIFF.
            using (FileStream tiffStream = new FileStream(outputTiff, FileMode.Create, FileAccess.Write))
            {
                tiffDevice.Process(pdfDoc, tiffStream);
            }
        }

        Console.WriteLine($"Multi‑page TIFF created at '{outputTiff}'.");
    }
}
