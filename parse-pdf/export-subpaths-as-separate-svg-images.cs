using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Directory where each subpath will be saved as an individual SVG file
        const string outputRoot = "Subpaths";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Configure extraction to generate a separate SVG for every subpath
            SvgExtractionOptions extractionOptions = new SvgExtractionOptions
            {
                ExtractEverySubPathToSvg = true, // one SVG per subpath
                AutoGrouping = false             // disable automatic grouping
            };

            // Create the extractor with the above options
            SvgExtractor extractor = new SvgExtractor(extractionOptions);

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Optional: create a subfolder per page for better organization
                string pageOutputDir = Path.Combine(outputRoot, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageOutputDir);

                // Extract each subpath on the current page to separate SVG files
                // The method creates files with transparent backgrounds automatically
                extractor.Extract(page, pageOutputDir);
            }
        }

        Console.WriteLine("All subpaths have been exported as separate SVG images.");
    }
}