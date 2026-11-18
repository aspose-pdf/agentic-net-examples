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

            // Create a graph with width=500 and height=400 using the double overload (non‑obsolete)
            Graph graph = new Graph(500.0, 400.0);

            // Create an unfilled arc: center at (250,200), radius 100, start angle 0°, sweep 180°
            // Arc constructor takes (posX, posY, radius, startAngle, sweepAngle) and expects float values
            Arc arc = new Arc(250f, 200f, 100f, 0f, 180f);

            // Set line width (float literal to avoid narrowing conversion warnings)
            arc.GraphInfo.LineWidth = 2f;

            // Set dash style (5 units on, 3 units off)
            arc.GraphInfo.DashArray = new int[] { 5, 3 };
            arc.GraphInfo.DashPhase = 0;

            // Ensure the arc has no fill (transparent)
            arc.GraphInfo.FillColor = Color.Transparent;

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page (Graph is a BaseParagraph)
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
