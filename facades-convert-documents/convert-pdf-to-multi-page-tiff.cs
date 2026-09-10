using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // OPTIONAL: Font substitution (Symbol → Arial Unicode MS)
            // Aspose.Pdf performs font substitution automatically when the
            // requested font is missing. Ensure that "Arial Unicode MS"
            // is installed on the system; the Symbol font will be mapped
            // to it during rendering.
            // ------------------------------------------------------------

            // Create a resolution for the output TIFF (300 DPI is a common choice)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings (no compression, default color depth, landscape)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Initialise the TIFF device with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert the entire PDF document to a single multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputTiffPath);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: '{outputTiffPath}'");
    }
}