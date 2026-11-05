using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tif";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the output file stream inside a using block
            using (FileStream tiffStream = new FileStream(outputTiffPath, FileMode.Create, FileAccess.Write))
            {
                // TiffDevice with default settings (default compression)
                TiffDevice tiffDevice = new TiffDevice();

                // Convert all pages of the PDF to a multi‑page TIFF
                tiffDevice.Process(pdfDoc, tiffStream);
            }
        }

        Console.WriteLine($"Multi‑page TIFF created at '{outputTiffPath}'.");
    }
}