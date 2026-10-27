using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold drawing objects
            // Use double values for width/height – the float overload is obsolete in recent versions
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create a straight line shape – constructor expects an array: [x1, y1, x2, y2]
            var line = new Line(new float[] { 100, 500, 400, 500 });

            // Configure visual appearance via GraphInfo
            // The current Aspose.Pdf version does not expose a LineCap property, so we set only the
            // colour and thickness that are universally supported.
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Black,   // line colour
                LineWidth = 2          // line thickness
                // Note: LineCap style is not available in this version of Aspose.Pdf.
            };

            // Add the line to the graph and the graph to the page
            graph.Shapes.Add(line);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with line saved to '{outputPath}'.");
    }
}
