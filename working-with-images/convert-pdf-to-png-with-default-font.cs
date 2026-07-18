using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice, RenderingOptions, Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PageImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set rendering options – substitute missing fonts with Times New Roman
            RenderingOptions renderOpts = new RenderingOptions
            {
                DefaultFontName = "Times New Roman"
            };

            // Create a PNG device (e.g., 300 DPI)
            PngDevice pngDevice = new PngDevice(new Resolution(300));
            // Apply the rendering options to the device
            pngDevice.RenderingOptions = renderOpts;

            // Iterate over pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");

                // Render the current page to PNG
                pngDevice.Process(pdfDoc.Pages[pageNumber], outputPath);
                Console.WriteLine($"Saved page {pageNumber} → {outputPath}");
            }
        }

        Console.WriteLine("All pages have been converted to images.");
    }
}
