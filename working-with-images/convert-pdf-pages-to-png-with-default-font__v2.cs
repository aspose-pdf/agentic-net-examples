using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a concrete PNG device. Devices are not IDisposable, so do not wrap them in a using block.
            var pngDevice = new PngDevice();

            // Configure rendering options for the device. RenderingOptions is an instance class; set the default font here.
            pngDevice.RenderingOptions = new RenderingOptions
            {
                DefaultFontName = "Times New Roman"
            };

            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{i}.png");
                // Render the page to an image file.
                pngDevice.Process(doc.Pages[i], outPath);
            }
        }

        Console.WriteLine("PDF pages have been converted to images.");
    }
}
