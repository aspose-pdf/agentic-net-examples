using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "graph.pdf";
        const string outputPath = "graph_with_rectangle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Document disposal must be wrapped in a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            Page page = doc.Pages[1];

            // Create a Graph that will hold drawing shapes. Use the page size so the graph covers the whole page.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create a rectangle shape with explicit dimensions.
            // Lower‑left corner (150,200), width 200pt, height 100pt.
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(150, 200, 200, 100);

            // Styling using GraphInfo (no System.Drawing.Color)
            rectShape.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.FromRgb(0.2, 0.6, 0.8),      // border color
                FillColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9), // fill color
                LineWidth = 2
            };

            // Add the rectangle to the graph, then add the graph to the page.
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Save the modified PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle added and saved to '{outputPath}'.");
    }
}
