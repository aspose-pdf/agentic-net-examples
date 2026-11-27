using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "ExtractedVectors";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source PDF (wrapped in a using block for deterministic disposal)
            using (Document srcDoc = new Document(inputPdfPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
                {
                    // Create a temporary document that will contain only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add a copy of the current page to the temporary document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageIndex]);

                        // Prepare SVG save options (no extra options required for basic extraction)
                        SvgSaveOptions svgOptions = new SvgSaveOptions();

                        // Build the output file name (one SVG per page)
                        string outputSvgPath = Path.Combine(
                            outputFolder,
                            $"Page_{pageIndex}_vector.svg");

                        // Save the single‑page document as SVG
                        singlePageDoc.Save(outputSvgPath, svgOptions);

                        Console.WriteLine($"Extracted vectors from page {pageIndex} to '{outputSvgPath}'.");
                    }
                }
            }

            Console.WriteLine("Vector graphic extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}