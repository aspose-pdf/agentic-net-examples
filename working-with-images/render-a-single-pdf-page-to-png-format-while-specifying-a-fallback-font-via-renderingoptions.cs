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

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Create rendering options and specify a fallback font
            RenderingOptions renderOpts = new RenderingOptions
            {
                // Font used when the original font is missing or cannot display a character
                DefaultFontName = "Arial",
                // Enable font analysis/substitution (optional but often useful)
                AnalyzeFonts = true
            };

            // Create a PNG device with desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Apply the rendering options to the device
                RenderingOptions = renderOpts
            };

            // Render the first page to a PNG file
            using (FileStream pngStream = new FileStream(outputPngPath, FileMode.Create))
            {
                pngDevice.Process(pdfDocument.Pages[1], pngStream);
            }

            Console.WriteLine($"Page 1 rendered to PNG: {outputPngPath}");
        }
    }
}