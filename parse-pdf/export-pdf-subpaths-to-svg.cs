using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file containing vector graphics.
        const string inputPdf = "input.pdf";

        // Directory where each subpath will be saved as an individual SVG file.
        const string outputDir = "Subpaths";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure extraction options to generate a separate SVG for every subpath.
                SvgExtractionOptions options = new SvgExtractionOptions
                {
                    ExtractEverySubPathToSvg = true
                };

                // Create the extractor with the configured options.
                SvgExtractor extractor = new SvgExtractor(options);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Extract each subpath on the current page to its own SVG file.
                    // The method creates files named like "page_{pageNum}_subpath_{index}.svg".
                    extractor.Extract(page, outputDir);
                }
            }

            Console.WriteLine($"All subpaths have been exported to SVG files in '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}