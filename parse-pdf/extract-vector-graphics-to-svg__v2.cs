using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SvgPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (core API only)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Build the SVG file name for this page
                string svgPath = Path.Combine(outputFolder, $"page_{pageNum}.svg");

                // Try to save vector graphics preserving the original coordinate system.
                // This method returns false if the page contains no vector path operators.
                bool saved = page.TrySaveVectorGraphics(svgPath);

                if (saved)
                {
                    Console.WriteLine($"Page {pageNum} vector graphics saved to: {svgPath}");
                }
                else
                {
                    // If no vector graphics are present, optionally extract using SvgExtractor.
                    // This demonstrates the alternative approach with explicit options.
                    var extractor = new SvgExtractor();
                    extractor.Extract(page, svgPath);
                    Console.WriteLine($"Page {pageNum} has no direct vector paths; full page SVG saved to: {svgPath}");
                }
            }
        }

        Console.WriteLine("Vector graphics extraction completed.");
    }
}