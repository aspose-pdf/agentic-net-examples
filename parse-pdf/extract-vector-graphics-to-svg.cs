using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";
        // Directory where individual SVG files will be saved
        const string outputDir = "ExtractedSVG";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF document
            using (Document srcDoc = new Document(inputPath))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new temporary document that will contain only the current page
                    using (Document pageDoc = new Document())
                    {
                        // Add the current page into the temporary document (CopyTo is obsolete)
                        pageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Define the SVG save options (default saves each page as a separate SVG)
                        SvgSaveOptions svgOptions = new SvgSaveOptions();

                        // Build the output file name for this page
                        string outPath = Path.Combine(outputDir, $"Page_{i}.svg");

                        // Save the single‑page document as SVG
                        pageDoc.Save(outPath, svgOptions);

                        Console.WriteLine($"Saved SVG: {outPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
