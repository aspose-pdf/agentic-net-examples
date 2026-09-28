using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal of the Document.
        using (Document pdfDoc = new Document(inputPath))
        {
            // TiffDevice does not implement IDisposable, so instantiate it without a using block.
            // The parameter‑less constructor uses the default resolution (300 DPI) and default compression.
            TiffDevice tiffDevice = new TiffDevice();

            // Create a FileStream for the output TIFF; the stream is disposable and therefore wrapped in a using block.
            using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Convert all pages of the PDF (from page 1 to the last page) into a multi‑page TIFF.
                tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, tiffStream);
            }
        }

        Console.WriteLine($"PDF pages have been converted to a multi‑page TIFF: {outputPath}");
    }
}
