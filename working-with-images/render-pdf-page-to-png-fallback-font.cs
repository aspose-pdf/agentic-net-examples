using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class RenderPdfPageToPng
{
    static void Main()
    {
        // Input PDF file and the page to render (1‑based index)
        const string inputPdfPath = "input.pdf";
        const int pageNumber = 1;               // change as needed
        const string outputPngPath = "page1.png";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine($"Page number {pageNumber} is out of range. Document has {pdfDocument.Pages.Count} pages.");
                return;
            }

            // Create a resolution (e.g., 300 DPI) for the PNG output
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Configure rendering options – specify a fallback font name
            // This font will be used when the original PDF references a missing font.
            pngDevice.RenderingOptions.DefaultFontName = "Arial";
            // Optional: enable font analysis to improve substitution
            pngDevice.RenderingOptions.AnalyzeFonts = true;

            // Render the selected page to a PNG file
            using (FileStream pngStream = new FileStream(outputPngPath, FileMode.Create))
            {
                pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
            }

            Console.WriteLine($"Page {pageNumber} rendered to PNG: {outputPngPath}");
        }
    }
}