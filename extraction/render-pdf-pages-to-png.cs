using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "PagesPng";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Concrete PNG device (does NOT implement IDisposable)
            // Set desired resolution and enable transparent background if needed
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = true
            };

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputFolder, $"Page_{pageNum}.png");
                // Render the current page to the PNG file
                pngDevice.Process(pdfDoc.Pages[pageNum], outPath);
            }
        }

        Console.WriteLine($"All pages have been rendered to PNG files in '{outputFolder}'.");
    }
}
