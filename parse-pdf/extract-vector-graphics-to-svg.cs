using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where individual SVG files will be saved
        const string outputRoot = "ExtractedSvg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document (lifecycle: using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Skip pages that do not contain vector graphics
                if (!page.HasVectorGraphics())
                    continue;

                // Create a sub‑directory for each page's SVG files
                string pageOutputDir = Path.Combine(outputRoot, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageOutputDir);

                // SvgExtractor extracts each vector graphic as an individual SVG file
                SvgExtractor extractor = new SvgExtractor();

                // Extract all vector graphics from the current page into the directory
                extractor.Extract(page, pageOutputDir);
            }
        }

        Console.WriteLine($"Vector graphics extraction completed. SVG files are located in '{outputRoot}'.");
    }
}