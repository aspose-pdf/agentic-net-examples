using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure TIFF settings – each PDF page will become a separate frame in the TIFF
                TiffSettings tiffSettings = new TiffSettings
                {
                    // Example: use LZW compression (optional)
                    Compression = CompressionType.LZW,
                    // You can adjust other settings such as Depth, SkipBlankPages, etc., if needed
                };

                // Create a TIFF device with desired resolution (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);
                TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                // Process the document – this generates a multi‑page TIFF where each PDF page is a separate image layer
                tiffDevice.Process(pdfDoc, outputTiff);
            }

            Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
