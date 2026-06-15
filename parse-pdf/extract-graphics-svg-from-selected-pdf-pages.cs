using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Directory where extracted SVG files will be saved
        const string outputDir = "ExtractedSvgs";
        // Page numbers to process (1‑based indexing)
        int[] pagesToExtract = { 1, 3, 5 };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            foreach (int pageNumber in pagesToExtract)
            {
                // Validate page number
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Skipping invalid page number: {pageNumber}");
                    continue;
                }

                Page page = doc.Pages[pageNumber];

                // Collect graphic elements from the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page); // Correct way to search graphics on a page

                    // If no graphics were found, continue to next page
                    if (absorber.Elements.Count == 0)
                    {
                        Console.WriteLine($"No graphics found on page {pageNumber}.");
                        continue;
                    }

                    // Use SvgExtractor to convert the collected graphics to SVG
                    SvgExtractor svgExtractor = new SvgExtractor();

                    // Convert all collected graphic elements of the page into a single SVG string
                    string svgContent = svgExtractor.Extract(absorber.Elements, page);

                    // Build output file path (e.g., ExtractedSvgs/page_1.svg)
                    string outputFile = Path.Combine(outputDir, $"page_{pageNumber}.svg");

                    // Write the SVG content to the file
                    File.WriteAllText(outputFile, svgContent);
                    Console.WriteLine($"Extracted SVG for page {pageNumber} -> {outputFile}");
                }
            }
        }

        Console.WriteLine("Graphics extraction completed.");
    }
}