using System;
using System.IO;
using Aspose.Pdf;

class ExtractVectorGraphics
{
    static void Main()
    {
        // Input PDF and the page number to extract (1‑based indexing)
        const string inputPdfPath = "input.pdf";
        const int pageNumber = 2;                     // change as needed

        // Folder where the SVG files will be written
        const string outputFolder = "ExtractedSvgs";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document srcDoc = new Document(inputPdfPath))
            {
                // Validate the requested page number
                if (pageNumber < 1 || pageNumber > srcDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page number {pageNumber} is out of range. Document has {srcDoc.Pages.Count} pages.");
                    return;
                }

                // Create a temporary document that contains only the desired page.
                using (Document singlePageDoc = new Document())
                {
                    // Add the selected page to the new document.
                    singlePageDoc.Pages.Add(srcDoc.Pages[pageNumber]);

                    // Configure SVG save options.
                    SvgSaveOptions svgOptions = new SvgSaveOptions();
                    // Enable CSS style embedding (recommended for clean SVG output)
                    svgOptions.ScaleToPixels = true;

                    // Build the output file name – only one SVG will be produced because the document has a single page.
                    string svgPath = Path.Combine(outputFolder, $"Page_{pageNumber}.svg");

                    // Save the document as SVG.
                    singlePageDoc.Save(svgPath, svgOptions);
                }
            }

            Console.WriteLine($"Vector graphics from page {pageNumber} have been saved as an SVG file in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
