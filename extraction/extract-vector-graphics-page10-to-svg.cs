using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // Operator types (PathConstructionOperator, PathPaintOperator, etc.)

class ExtractVectorGraphics
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page10_filtered.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load source PDF inside a using block (lifecycle rule)
        using (Document srcDoc = new Document(inputPath))
        {
            // Ensure page 10 exists (pages are 1‑based)
            if (srcDoc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The document has fewer than 10 pages.");
                return;
            }

            // Get page 10
            Page srcPage = srcDoc.Pages[10];

            // Create a new PDF document that will hold only the filtered vector graphics
            using (Document targetDoc = new Document())
            {
                // Add a blank page to the target document
                Page targetPage = targetDoc.Pages.Add();

                // Iterate over all operators on the source page
                foreach (Operator op in srcPage.Contents)
                {
                    // Filter: keep only vector‑graphics related operators.
                    // The concrete operator types (PathConstructionOperator, PathPaintOperator) are not
                    // part of the public API in some Aspose.Pdf versions, so we identify them by name.
                    string typeName = op.GetType().Name;
                    if (typeName.Contains("PathConstruction") || typeName.Contains("PathPaint"))
                    {
                        // Add the operator to the target page's content stream.
                        // Operators can be reused; cloning is not required for this example.
                        targetPage.Contents.Add(op);
                    }
                }

                // Save the filtered page as SVG. Must pass an explicit SvgSaveOptions
                // because Document.Save(string) without options always writes PDF.
                SvgSaveOptions svgOpts = new SvgSaveOptions();
                targetDoc.Save(outputPath, svgOpts);
            }
        }

        Console.WriteLine($"Filtered vector graphics from page 10 saved to '{outputPath}'.");
    }
}
