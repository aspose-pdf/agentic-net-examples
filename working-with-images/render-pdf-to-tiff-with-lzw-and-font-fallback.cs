using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdfPath = "input.pdf";
        // Output TIFF path
        const string outputTiffPath = "output.tif";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Set a default font fallback for characters whose fonts are missing.
            // This uses PdfSaveOptions.DefaultFontName which tells Aspose.Pdf which
            // font to substitute when the original font cannot be found.
            // ------------------------------------------------------------
            PdfSaveOptions fontFallbackOptions = new PdfSaveOptions
            {
                DefaultFontName = "Arial" // Choose a font that is guaranteed to exist on the system
            };

            // Apply the fallback options by saving to a temporary stream (no file is created).
            // This forces the document to resolve missing fonts using the specified fallback.
            using (MemoryStream tempStream = new MemoryStream())
            {
                pdfDocument.Save(tempStream, fontFallbackOptions);
                // Reset the stream position so the document can continue to be used.
                tempStream.Position = 0;
                // Reload the document from the stream to ensure the fallback is applied.
                // Dispose the original instance manually because we are inside a using block.
                pdfDocument.Dispose();
                // Re‑assign a new Document instance from the stream.
                Document reloaded = new Document(tempStream);
                // Continue processing with the reloaded document.
                // ------------------------------------------------------------
                // Configure TIFF rendering: 300 DPI resolution and LZW compression.
                // ------------------------------------------------------------
                Resolution resolution = new Resolution(300); // 300 DPI
                TiffSettings tiffSettings = new TiffSettings
                {
                    Compression = CompressionType.LZW // LZW compression
                };

                // Create the TIFF device with the specified resolution and settings
                TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                // Render the entire PDF document to a single multi‑page TIFF file
                tiffDevice.Process(reloaded, outputTiffPath);
            }
        }

        Console.WriteLine($"PDF has been rendered to TIFF with LZW compression: {outputTiffPath}");
    }
}
