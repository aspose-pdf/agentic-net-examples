using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "Subpaths";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate that the document has at least 13 pages (Aspose.Pdf uses 1‑based indexing)
            if (pdfDoc.Pages.Count < 13)
            {
                Console.Error.WriteLine("The document does not contain page 13.");
                return;
            }

            // Retrieve page 13
            Page page13 = pdfDoc.Pages[13];

            // Configure the SVG extractor to generate a separate SVG file for each sub‑path
            var options = new SvgExtractionOptions { ExtractEverySubPathToSvg = true };
            var extractor = new SvgExtractor(options);

            // Base file name for the generated SVG files (the extractor will append an index and .svg)
            string baseFileName = Path.Combine(outputDir, "subpath");

            // Perform the extraction. The extractor creates multiple files with sequential naming.
            extractor.Extract(page13, baseFileName);
        }

        Console.WriteLine($"Sub‑path SVG files have been saved to '{outputDir}'.");
    }
}
