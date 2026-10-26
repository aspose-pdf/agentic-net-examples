using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF
        const string pngPath = "page1.png";      // output PNG
        const int    pageNumber = 1;               // page to render (1‑based)
        const string fallbackFont = "fallback.ttf"; // fallback font name or file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block – ensures deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Verify the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Configure rendering options with a fallback font
            RenderingOptions renderOpts = new RenderingOptions
            {
                // In Aspose.Pdf the fallback/default font is set via DefaultFontName
                DefaultFontName = fallbackFont
            };

            // Define the resolution for the PNG (e.g., 300 DPI) and create the device
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = true,
                RenderingOptions = renderOpts
            };

            // Render the selected page directly to a file stream
            using (FileStream outStream = new FileStream(pngPath, FileMode.Create, FileAccess.Write))
            {
                pngDevice.Process(doc.Pages[pageNumber], outStream);
            }
        }

        Console.WriteLine($"Page {pageNumber} rendered to PNG: {pngPath}");
    }
}
