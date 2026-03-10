using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // Path to source PDF
        const string outputDir    = "ExtractedSvg";            // Directory for SVG files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle: create/load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (global rule)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Check if the page contains vector graphics
                if (page.HasVectorGraphics())
                {
                    // Build a unique SVG file name for this page
                    string svgPath = Path.Combine(outputDir, $"page_{pageNum}.svg");

                    // Try to save vector graphics as SVG (returns true on success)
                    bool saved = page.TrySaveVectorGraphics(svgPath);
                    if (saved)
                    {
                        Console.WriteLine($"Page {pageNum}: vector graphics saved to '{svgPath}'.");
                    }
                    else
                    {
                        Console.WriteLine($"Page {pageNum}: no vector graphics were saved.");
                    }
                }
                else
                {
                    Console.WriteLine($"Page {pageNum}: does not contain vector graphics.");
                }
            }
        }

        // Document is automatically disposed here (document‑disposal‑with‑using rule)
        Console.WriteLine("Extraction completed.");
    }
}