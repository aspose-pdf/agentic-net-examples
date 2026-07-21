using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input and output file names (place input.pdf in the same folder as the executable)
        const string pdfFile = "input.pdf";
        const string pngFile = "page1.png";
        const string fallbackFont = "Arial";

        // Resolve full paths based on the executable directory
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string pdfPath = Path.Combine(baseDir, pdfFile);
        string pngPath = Path.Combine(baseDir, pngFile);

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure rendering options with a fallback font
            RenderingOptions renderOpts = new RenderingOptions
            {
                DefaultFontName = fallbackFont, // font used when original font is missing
                AnalyzeFonts    = true          // enable font analysis/substitution
            };

            // Create a PNG device (300 DPI resolution) and assign the rendering options
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution)
            {
                RenderingOptions = renderOpts
            };

            // Render the first page (pages are 1‑based) to a PNG file
            using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
            {
                pngDevice.Process(pdfDocument.Pages[1], pngStream);
            }
        }

        Console.WriteLine($"Page rendered to PNG with fallback font: {pngPath}");
    }
}
