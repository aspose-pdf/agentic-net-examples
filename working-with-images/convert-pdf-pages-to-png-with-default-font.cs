using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PageImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Set DPI and default font for rendering
        var resolution = new Resolution(300);
        var renderOpts = new RenderingOptions { DefaultFontName = "Times New Roman" };

        // PngDevice constructor takes a Resolution; assign RenderingOptions separately
        var pngDevice = new PngDevice(resolution);
        pngDevice.RenderingOptions = renderOpts;

        // Load the PDF with deterministic disposal
        using (var doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputDir, $"page_{i}.png");
                pngDevice.Process(doc.Pages[i], outPath);
                Console.WriteLine($"Saved page {i} as image: {outPath}");
            }
        }

        Console.WriteLine("All pages have been rendered to images.");
    }
}
