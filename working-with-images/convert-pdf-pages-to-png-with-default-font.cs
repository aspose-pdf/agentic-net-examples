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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Create a PNG device with a resolution of 300 DPI
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution);

                // Set the default font name to ensure consistent rendering
                var renderingOpts = new RenderingOptions();
                renderingOpts.DefaultFontName = "Arial";
                pngDevice.RenderingOptions = renderingOpts;

                // Build the output file name for the current page
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Render the page to a PNG file
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as {outPath}");
            }
        }
    }
}