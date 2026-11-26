using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Thumbnails";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page width in points (1 point = 1/72 inch)
                double pageWidthPoints = page.PageInfo.Width;

                // Desired thumbnail width in pixels
                const double thumbWidthPx = 120.0;

                // Compute DPI so that the rendered image has the required pixel width
                double dpiDouble = thumbWidthPx * 72.0 / pageWidthPoints;
                int dpi = (int)Math.Round(dpiDouble);

                // Create a PNG device with the calculated resolution (devices are not disposable)
                var pngDevice = new PngDevice(new Resolution(dpi))
                {
                    TransparentBackground = true
                };

                string outPath = Path.Combine(outputDir, $"thumb_page_{i}.png");
                // Render the page to a PNG file
                pngDevice.Process(page, outPath);
            }
        }

        Console.WriteLine("PNG thumbnails created successfully.");
    }
}
