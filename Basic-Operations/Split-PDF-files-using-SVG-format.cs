using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // Required for text-related types (if needed)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Source PDF
        const string outputFolder  = "SplitSvgPages";     // Folder to hold SVG files

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source PDF (using statement ensures deterministic disposal)
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Iterate over pages – Aspose.Pdf uses 1‑based indexing
                for (int pageIndex = 1; pageIndex <= sourceDoc.Pages.Count; pageIndex++)
                {
                    // Create a new temporary document that will contain only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageIndex]);

                        // Prepare SVG save options (required for non‑PDF output)
                        SvgSaveOptions svgOptions = new SvgSaveOptions();

                        // Build the output file name (e.g., Page_1.svg, Page_2.svg, …)
                        string svgFilePath = Path.Combine(outputFolder, $"Page_{pageIndex}.svg");

                        // Save the single‑page document as SVG
                        singlePageDoc.Save(svgFilePath, svgOptions);

                        Console.WriteLine($"Saved page {pageIndex} as SVG → {svgFilePath}");
                    }
                }
            }

            Console.WriteLine("PDF split into SVG pages completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}