using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_page1.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create → load)
        using (Document doc = new Document(inputPath))
        {
            // Configure rendering options for high‑quality interpolation (anti‑aliasing)
            RenderingOptions renderOpts = new RenderingOptions
            {
                InterpolationHighQuality = true // enables smoother lines and curves
            };

            // PngDevice does NOT implement IDisposable, so instantiate it without a using block
            PngDevice pngDevice = new PngDevice();
            pngDevice.RenderingOptions = renderOpts;

            // Export the first page as a PNG image using the configured options (lifecycle: save)
            pngDevice.Process(doc.Pages[1], outputPath);
        }

        Console.WriteLine($"Page rendered with enhanced anti‑aliasing saved to '{outputPath}'.");
    }
}
