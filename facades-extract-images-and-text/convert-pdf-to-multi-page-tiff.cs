using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfConverter implements IDisposable, wrap in using for deterministic cleanup
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Prepare the converter (required before extracting images)
                converter.DoConvert();

                // Configure TIFF settings for lossless archival (no compression)
                TiffSettings tiffSettings = new TiffSettings
                {
                    Compression = CompressionType.None
                };

                // Save all pages as a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff, tiffSettings);
            }

            Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}