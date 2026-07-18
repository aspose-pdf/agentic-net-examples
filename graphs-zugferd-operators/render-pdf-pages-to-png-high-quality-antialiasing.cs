using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create rendering options with high‑quality interpolation (anti‑aliasing)
            RenderingOptions renderOpts = new RenderingOptions
            {
                InterpolationHighQuality = true
            };

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputDir, $"page_{i}.png");

                // Save each page as a PNG image using the rendering options
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    PngDevice pngDevice = new PngDevice();
                    pngDevice.RenderingOptions = renderOpts;
                    pngDevice.Process(doc.Pages[i], outStream);
                }
            }
        }

        Console.WriteLine("Pages saved as PNG images with high‑quality anti‑aliasing.");
    }
}