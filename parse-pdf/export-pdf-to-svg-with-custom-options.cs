using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SvgOutput";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // ---------- Save the whole document as SVG with custom options ----------
            // Create SVG save options
            SvgSaveOptions svgSaveOpts = new SvgSaveOptions
            {
                // Scale to pixels (affects DPI‑like rendering)
                ScaleToPixels = true,
                // Cache glyphs for better performance
                CacheGlyphs = true
            };

            // Save the entire PDF as a single SVG file
            string wholeSvgPath = Path.Combine(outputDir, "document.svg");
            pdfDoc.Save(wholeSvgPath, svgSaveOpts);
            Console.WriteLine($"Document saved as SVG: {wholeSvgPath}");

            // ---------- Extract vector graphics per page with custom extraction options ----------
            // Configure extraction options
            SvgExtractionOptions extractionOpts = new SvgExtractionOptions
            {
                // Increase minimum stroke width to improve visibility at higher DPI
                MinStrokeWidth = 0.8,
                // Disable automatic grouping to keep individual paths (useful for CSS styling)
                AutoGrouping = false
            };

            // Create an extractor with the above options
            SvgExtractor extractor = new SvgExtractor(extractionOpts);

            // Iterate through pages and extract vector graphics
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                string pageDir = Path.Combine(outputDir, $"Page_{i}");
                Directory.CreateDirectory(pageDir);

                // Extract all vector graphics of the page to separate SVG files
                extractor.Extract(page, pageDir);
                Console.WriteLine($"Extracted SVG graphics for page {i} to folder: {pageDir}");
            }
        }
    }
}