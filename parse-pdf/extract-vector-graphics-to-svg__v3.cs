using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing vector graphics.
        const string pdfPath = "input.pdf";

        // Directory where SVG files will be written.
        const string outputDir = "VectorSvg";

        // Verify the source file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (using the standard Document constructor).
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Build the SVG file name for the current page.
                string svgPath = Path.Combine(outputDir, $"page_{pageIndex}.svg");

                // TrySaveVectorGraphics writes the vector content to an SVG file.
                // This method preserves the original PDF coordinate system,
                // ensuring layout accuracy in the resulting SVG.
                bool saved = page.TrySaveVectorGraphics(svgPath);

                if (saved)
                {
                    Console.WriteLine($"Page {pageIndex}: vector graphics saved to '{svgPath}'.");
                }
                else
                {
                    Console.WriteLine($"Page {pageIndex}: no vector graphics found.");
                }
            }
        }
    }
}