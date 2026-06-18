using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PNG file path (single page)
        const string outputPngPath = "page1.png";
        // Name of the fallback font to use when a font is missing
        const string fallbackFontName = "Arial";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Create a resolution (DPI) for the output image
            Resolution resolution = new Resolution(300); // 300 DPI for high quality

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Configure rendering options with a fallback font
            RenderingOptions renderOpts = new RenderingOptions
            {
                DefaultFontName = fallbackFontName
            };
            pngDevice.RenderingOptions = renderOpts;

            // Render the first page (page numbers are 1‑based)
            Page page = pdfDocument.Pages[1];

            // Save the rendered page to a PNG file
            using (FileStream pngStream = new FileStream(outputPngPath, FileMode.Create))
            {
                pngDevice.Process(page, pngStream);
            }

            Console.WriteLine($"Page 1 rendered to PNG: {outputPngPath}");
        }
    }
}