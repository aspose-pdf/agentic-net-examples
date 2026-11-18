using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice, Resolution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page1.png";
        const int pageNumber = 1; // 1‑based index per Aspose.Pdf

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate requested page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Configure rendering options with a fallback font
            RenderingOptions renderOpts = new RenderingOptions();
            // The fallback font is set via DefaultFontName (no FontFallbackSettings type exists)
            renderOpts.DefaultFontName = "Arial";

            // Create a PNG device (resolution 300 DPI) and assign the rendering options
            PngDevice pngDevice = new PngDevice(new Resolution(300));
            pngDevice.RenderingOptions = renderOpts;

            // Render the selected page to a memory stream and write to file
            using (MemoryStream ms = new MemoryStream())
            {
                pngDevice.Process(doc.Pages[pageNumber], ms);
                File.WriteAllBytes(outputPath, ms.ToArray());
            }
        }

        Console.WriteLine($"Page {pageNumber} rendered to PNG at '{outputPath}'.");
    }
}
