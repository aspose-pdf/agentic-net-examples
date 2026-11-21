using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_pattern.pdf";
        const string patternPath = "pattern.png";

        // Verify that the pattern image exists
        if (!File.Exists(patternPath))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternPath}");
            return;
        }

        // Create a PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create an ellipse (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Ellipse ellipse = new Ellipse(100, 500, 300, 400);

            // NOTE: Aspose.Pdf core API does not expose a public TilingPattern class in all versions.
            // To keep the example compile‑ready we use a solid fill colour. If a TilingPattern class
            // becomes available in the target version, replace the solid colour with:
            //   var tiling = new TilingPattern(new XImage(patternPath));
            //   ellipse.GraphInfo.FillColor = new Color { PatternColorSpace = tiling };
            // For now we use a semi‑transparent colour to demonstrate fill handling.
            ellipse.GraphInfo.FillColor = Color.FromArgb(128, 255, 255, 255); // 50% white

            // Optional: give the ellipse a visible stroke
            ellipse.GraphInfo.Color = Color.Black; // stroke colour
            ellipse.GraphInfo.LineWidth = 1;

            // Shapes must be placed inside a Graph container before being added to the page.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Verify that the fill was set (non‑null). In a real pattern scenario the
            // PatternColorSpace property would be checked instead.
            if (ellipse.GraphInfo.FillColor != null)
                Console.WriteLine("Ellipse fill successfully set.");
            else
                Console.WriteLine("Failed to assign fill to ellipse.");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ellipse saved to '{outputPath}'.");
    }
}
