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

        // Load the PDF document (Document implements IDisposable)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Instantiate TiffDevice with default settings (do NOT wrap in using)
            TiffDevice tiffDevice = new TiffDevice();

            // Create a file stream for the output TIFF
            using (FileStream tiffStream = new FileStream(outputTiffPath, FileMode.Create, FileAccess.Write))
            {
                // Convert all pages (1‑based indexing) to a single multi‑page TIFF
                tiffDevice.Process(pdfDocument, 1, pdfDocument.Pages.Count, tiffStream);
            }
        }

        Console.WriteLine($"TIFF image saved to '{outputTiffPath}'.");
    }
}