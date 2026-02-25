using System;
using System.IO;
using Aspose.Pdf;               // Document, Page, SvgSaveOptions are all in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPagesSvg";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF inside a using block (deterministic disposal)
        using (Document srcDoc = new Document(inputPdf))
        {
            // NOTE: Aspose.Pdf uses 1‑based page indexing
            for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
            {
                // Create a new empty PDF document for the single page
                using (Document singlePageDoc = new Document())
                {
                    // Add the current page from the source document
                    singlePageDoc.Pages.Add(srcDoc.Pages[pageIndex]);

                    // Build the output SVG file path (e.g., Page_1.svg)
                    string svgPath = Path.Combine(outputDir, $"Page_{pageIndex}.svg");

                    // Save as SVG – must pass SvgSaveOptions explicitly (otherwise a PDF is written)
                    SvgSaveOptions svgOptions = new SvgSaveOptions();
                    singlePageDoc.Save(svgPath, svgOptions);

                    Console.WriteLine($"Saved page {pageIndex} as SVG → {svgPath}");
                }
            }
        }

        Console.WriteLine("PDF split into SVG pages completed.");
    }
}