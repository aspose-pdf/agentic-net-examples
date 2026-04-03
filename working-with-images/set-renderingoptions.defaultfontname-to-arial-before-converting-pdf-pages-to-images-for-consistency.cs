using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create rendering options and set the default fallback font
            RenderingOptions renderOpts = new RenderingOptions();
            renderOpts.DefaultFontName = "Arial";

            // Define the resolution for the output images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);
            // Assign the rendering options to the device
            pngDevice.RenderingOptions = renderOpts;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNum}.png");
                // Save each page as a PNG image
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(doc.Pages[pageNum], outStream);
                }
                Console.WriteLine($"Saved page {pageNum} → {outPath}");
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}