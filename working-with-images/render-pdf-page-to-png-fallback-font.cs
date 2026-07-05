using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class RenderPdfPageToPng
{
    static void Main()
    {
        // Directory that contains the source PDF and where the PNG will be saved.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Output PNG file for the first page.
        string pngPath = Path.Combine(dataDir, "page1.png");

        // Ensure the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create rendering options and specify a fallback font.
            RenderingOptions renderOptions = new RenderingOptions
            {
                // Font name to use when the original font is missing.
                DefaultFontName = "Arial",
                // Enable font analysis/substitution.
                AnalyzeFonts = true
            };

            // Define the resolution of the output PNG (e.g., 300 DPI).
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the desired resolution.
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Apply the rendering options that include the fallback font.
                RenderingOptions = renderOptions
            };

            // Convert the first page (pages are 1‑based) to PNG and save to file.
            pngDevice.Process(pdfDocument.Pages[1], pngPath);
        }

        Console.WriteLine($"Page 1 rendered to PNG: {pngPath}");
    }
}