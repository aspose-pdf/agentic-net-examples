using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitSvg";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (lifecycle: load)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing (global rule)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new document containing only the current page (creation)
                    using (Document singlePageDoc = new Document())
                    {
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Prepare SVG save options (must use SaveOptions subclass)
                        SvgSaveOptions saveOpts = new SvgSaveOptions();

                        // Save the single‑page document as SVG (lifecycle: save)
                        string outPath = Path.Combine(outputDir, $"Page_{i}.svg");
                        singlePageDoc.Save(outPath, saveOpts);

                        Console.WriteLine($"Saved page {i} as SVG → {outPath}");
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