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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // TiffDevice does not implement IDisposable – instantiate it directly
            TiffDevice tiffDevice = new TiffDevice();

            // Create the output stream for the TIFF file
            using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Convert all PDF pages (1 .. Pages.Count) into a multi‑page TIFF using default settings
                tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, tiffStream);
            }
        }

        Console.WriteLine($"PDF converted to multi‑page TIFF: {outputPath}");
    }
}