using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

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
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Build the SVG file name for the current page
                string svgPath = Path.Combine(outputFolder, $"page_{pageIndex}.svg");

                // Try to save vector graphics of the page as SVG.
                // This method preserves the original PDF coordinate system,
                // ensuring layout accuracy in the resulting SVG.
                bool success = page.TrySaveVectorGraphics(svgPath);

                if (success)
                {
                    Console.WriteLine($"Page {pageIndex} vector graphics saved to: {svgPath}");
                }
                else
                {
                    Console.WriteLine($"Page {pageIndex} contains no vector graphics to export.");
                }
            }
        }
    }
}