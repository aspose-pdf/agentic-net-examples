using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF containing vector graphics.
        const string inputPdfPath = "input.pdf";

        // Output SVG file that will contain all extracted graphics.
        const string outputSvgPath = "combined_vectors.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an SvgExtractor with default options.
            SvgExtractor extractor = new SvgExtractor();

            // StringBuilder to accumulate SVG content from all pages.
            StringBuilder combinedSvg = new StringBuilder();

            // Iterate over all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Extract SVG strings for the current page.
                // Each string is a complete <svg> element representing a vector graphic.
                var pageSvgs = extractor.Extract(page);

                // Append each SVG to the combined output.
                foreach (string svgContent in pageSvgs)
                {
                    combinedSvg.AppendLine(svgContent);
                }
            }

            // Write the combined SVG content to the output file.
            File.WriteAllText(outputSvgPath, combinedSvg.ToString());

            Console.WriteLine($"Vector graphics extracted and saved to '{outputSvgPath}'.");
        }
    }
}