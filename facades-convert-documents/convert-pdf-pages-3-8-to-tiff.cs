using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "pages3to8.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document; using ensures proper disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // TiffDevice with default resolution and default CropBox coordinate type.
            TiffDevice tiffDevice = new TiffDevice();

            // Convert pages 3 through 8 (1‑based indexing) to a single TIFF file.
            tiffDevice.Process(pdfDoc, 3, 8, outputTiff);
        }

        Console.WriteLine($"Pages 3‑8 have been saved to TIFF file: {outputTiff}");
    }
}