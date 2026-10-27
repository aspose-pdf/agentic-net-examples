using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "arc_graph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a graph with the required size (width, height).
            Graph graph = new Graph(200.0, 200.0);

            // Configure default appearance for shapes added to the graph.
            graph.GraphInfo.LineWidth = 2.0f;               // 2 points stroke width
            graph.GraphInfo.DashArray = new int[] { 5, 3 }; // 5 pt dash, 3 pt gap
            graph.GraphInfo.DashPhase = 0;                  // start at beginning of pattern
            graph.GraphInfo.FillColor = Color.Empty;        // transparent fill (no fill)
            graph.GraphInfo.Color = Color.Black;            // stroke color

            // Create an unfilled half‑circle arc (0‑180 degrees).
            // Arc constructor signature: Arc(double left, double bottom, double width, double startAngle, double sweepAngle)
            Arc arc = new Arc(0, 0, 200, 0, 180);
            // Apply the same styling to the arc (optional – it inherits from the graph if not set).
            arc.GraphInfo.LineWidth = graph.GraphInfo.LineWidth;
            arc.GraphInfo.DashArray = graph.GraphInfo.DashArray;
            arc.GraphInfo.DashPhase = graph.GraphInfo.DashPhase;
            arc.GraphInfo.FillColor = Color.Empty;
            arc.GraphInfo.Color = Color.Black;

            graph.Shapes.Add(arc);

            // Attach the graph to the page. Positioning is handled by the page layout.
            page.Paragraphs.Add(graph);

            // Save the document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}