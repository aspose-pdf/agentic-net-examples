using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(inputPdfPath);

        // Create a TiffDevice with the desired resolution.
        // The overload without a compression argument uses the default compression (CCITT4).
        Resolution resolution = new Resolution(300); // DPI
        TiffDevice tiffDevice = new TiffDevice(resolution);

        // Write all pages to a single multi‑page TIFF file.
        using (FileStream tiffStream = new FileStream(outputTiffPath, FileMode.Create, FileAccess.Write))
        {
            tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, tiffStream);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiffPath}");
    }
}
