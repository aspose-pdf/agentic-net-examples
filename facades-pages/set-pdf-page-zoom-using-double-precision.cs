using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ZoomedPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Desired zoom factor (double for high‑precision scaling)
        const double zoomFactor = 2.0; // 200% scaling
        // Aspose.Pdf uses DPI to control image size; the default export DPI is 96.
        const int baseDpi = 96;
        var resolution = new Resolution((int)(baseDpi * zoomFactor), (int)(baseDpi * zoomFactor));

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            for (int i = 1; i <= pageCount; i++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{i}.png");
                using (var outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // Render the page as PNG using the calculated resolution (i.e., zoom)
                    var pngDevice = new PngDevice(resolution);
                    pngDevice.Process(doc.Pages[i], outStream);
                }
                Console.WriteLine($"Page {i} saved to {outPath}");
            }
        }

        Console.WriteLine("All pages exported with the specified zoom.");
    }
}