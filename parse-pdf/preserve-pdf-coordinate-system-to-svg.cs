using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SvgOutput";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // 1. Save vector graphics of each page individually.
            //    Page.TrySaveVectorGraphics preserves the original
            //    coordinate system (PDF user space) in the generated SVG.
            // ------------------------------------------------------------
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                string svgPath = Path.Combine(outputDir, $"Page_{i}.svg");

                // Returns true if vector graphics were present and saved
                bool saved = page.TrySaveVectorGraphics(svgPath);

                Console.WriteLine(saved
                    ? $"Page {i} vector graphics saved to: {svgPath}"
                    : $"Page {i} contains no vector graphics.");
            }

            // ------------------------------------------------------------
            // 2. Save the entire document as a single SVG.
            //    Use SvgSaveOptions and set ScaleToPixels = false to keep
            //    the original PDF coordinate system (points) rather than
            //    converting to pixel units.
            // ------------------------------------------------------------
            string fullSvgPath = Path.Combine(outputDir, "Document.svg");
            SvgSaveOptions svgOptions = new SvgSaveOptions
            {
                // Preserve PDF user space (1 point = 1/72 inch)
                ScaleToPixels = false
            };

            doc.Save(fullSvgPath, svgOptions);
            Console.WriteLine($"Full document SVG saved to: {fullSvgPath}");
        }
    }
}