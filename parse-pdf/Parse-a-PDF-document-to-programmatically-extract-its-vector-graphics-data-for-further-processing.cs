using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputRootDir = "VectorGraphics";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRootDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an SVG extractor (default options)
            SvgExtractor extractor = new SvgExtractor();

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Detect if the page contains vector graphics
                if (page.HasVectorGraphics())
                {
                    // Create a subdirectory for each page's SVG files
                    string pageOutputDir = Path.Combine(outputRootDir, $"Page_{i}");
                    Directory.CreateDirectory(pageOutputDir);

                    // Extract all vector graphics on the page to separate SVG files
                    // This method creates one SVG file per vector graphic element
                    extractor.Extract(page, pageOutputDir);

                    Console.WriteLine($"Extracted vector graphics from page {i} to '{pageOutputDir}'.");
                }
                else
                {
                    Console.WriteLine($"Page {i} contains no vector graphics.");
                }
            }
        }

        Console.WriteLine("Vector graphics extraction completed.");
    }
}