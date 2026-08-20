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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Define the resolution for the rasterized images (e.g., 300 DPI)
            var resolution = new Resolution(300);

            // Create a PNG device – this is the core API for page‑to‑image conversion
            var pngDevice = new PngDevice(resolution)
            {
                // Set rendering options with the desired default font
                RenderingOptions = new RenderingOptions
                {
                    DefaultFontName = "Times New Roman"
                }
            };

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNum}.png");

                // Render the current page to a PNG file
                using (var outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Saved page {pageNum} → {outPath}");
            }
        }

        Console.WriteLine("All pages have been converted to images.");
    }
}
