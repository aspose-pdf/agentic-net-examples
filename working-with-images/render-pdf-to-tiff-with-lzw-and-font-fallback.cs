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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Set a default font to be used when a glyph is missing in the original font.
                // This is done via RenderingOptions.DefaultFontName.
                var renderingOptions = new RenderingOptions
                {
                    DefaultFontName = "Arial" // change to any font that is guaranteed to be present
                };

                // Configure TIFF settings with LZW compression
                var tiffSettings = new TiffSettings
                {
                    Compression = CompressionType.LZW
                };

                // Create a TiffDevice with desired DPI (e.g., 300) and the settings above
                var tiffDevice = new TiffDevice(new Resolution(300), tiffSettings)
                {
                    RenderingOptions = renderingOptions
                };

                // Render the whole PDF to a multi‑page TIFF file
                tiffDevice.Process(doc, outputPath);
            }

            Console.WriteLine($"PDF rendered to TIFF with LZW compression: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
