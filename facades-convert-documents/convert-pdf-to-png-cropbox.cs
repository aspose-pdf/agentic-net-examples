using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for PngDevice and Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PagesAsPng";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPdf);

            // Iterate through each page (1‑based index)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Ensure the CropBox is set – it already defines the visible area.
                // No need to manually set PageSize or SourceRectangle on PngDevice
                // because those members do not exist in recent Aspose.Pdf versions.

                // Create a PNG device with the desired DPI
                PngDevice pngDevice = new PngDevice(new Resolution(300));
                // Optional: make background transparent
                pngDevice.TransparentBackground = true;

                string outPath = Path.Combine(outputDir, $"Page_{pageNum}.png");
                // Process the page and write directly to a file path
                pngDevice.Process(page, outPath);
                Console.WriteLine($"Saved page {pageNum} as PNG → {outPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
