using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TableBorders";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            for (int pageNum = 2; pageNum <= pdfDoc.Pages.Count; pageNum += 2) // even‑numbered pages
            {
                Page page = pdfDoc.Pages[pageNum];

                // Use constructor to set resolution (Resolution is read‑only property)
                var pngDevice = new PngDevice(new Resolution(300))
                {
                    // Optional: enable transparent background if desired
                    TransparentBackground = true
                };

                string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}_Tables.png");

                // Save the rendered page as PNG (non‑PDF format, explicit device usage)
                pngDevice.Process(page, outputPath);

                Console.WriteLine($"Saved table borders from page {pageNum} to '{outputPath}'.");
            }
        }
    }
}
