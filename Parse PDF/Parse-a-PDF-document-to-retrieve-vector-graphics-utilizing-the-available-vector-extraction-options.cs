using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "VectorGraphics";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (using rule: document-disposal-with-using)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Check if the page contains vector graphics
                    if (page.HasVectorGraphics())
                    {
                        // Option 1: Save the whole page's vector graphics as an SVG file
                        string svgFilePath = Path.Combine(outputDir, $"Page_{pageIndex}.svg");
                        bool saved = page.TrySaveVectorGraphics(svgFilePath);
                        if (saved)
                        {
                            Console.WriteLine($"Vector graphics saved to: {svgFilePath}");
                        }

                        // Option 2: Extract individual SVG strings using SvgExtractor
                        // (useful if you need the SVG content in memory)
                        SvgExtractor extractor = new SvgExtractor();
                        var svgStrings = extractor.Extract(page); // returns List<string>

                        // Write each extracted SVG string to a separate file
                        for (int i = 0; i < svgStrings.Count; i++)
                        {
                            string individualSvgPath = Path.Combine(
                                outputDir,
                                $"Page_{pageIndex}_Graphic_{i + 1}.svg");

                            File.WriteAllText(individualSvgPath, svgStrings[i]);
                            Console.WriteLine($"Extracted SVG graphic saved to: {individualSvgPath}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Page {pageIndex} contains no vector graphics.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}