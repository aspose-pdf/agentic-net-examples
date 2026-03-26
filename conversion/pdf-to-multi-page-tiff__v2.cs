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

        using (Document pdfDoc = new Document(inputPdf))
        {
            // TiffDevice with default settings (default compression)
            TiffDevice tiffDevice = new TiffDevice();
            tiffDevice.Process(pdfDoc, outputTiff);
        }

        Console.WriteLine($"Multi‑page TIFF created: {outputTiff}");
    }
}