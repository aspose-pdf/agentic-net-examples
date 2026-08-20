using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SvgOutput";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Build the SVG file name for this page
                string svgFilePath = Path.Combine(outputFolder, $"page_{pageIndex}.svg");

                // Try to save vector graphics of the page as SVG.
                // This method preserves the original coordinate system.
                bool hasVectorGraphics = page.TrySaveVectorGraphics(svgFilePath);

                if (hasVectorGraphics)
                {
                    Console.WriteLine($"Page {pageIndex}: SVG saved to '{svgFilePath}'.");
                }
                else
                {
                    Console.WriteLine($"Page {pageIndex}: No vector graphics to export.");
                }
            }
        }
    }
}