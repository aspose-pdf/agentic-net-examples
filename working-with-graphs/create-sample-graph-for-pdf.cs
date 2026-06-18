using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class GraphFactory
{
    /// <summary>
    /// Creates a predefined graph containing a rectangle, an ellipse and a line.
    /// The graph can be added to any page via <c>page.Paragraphs.Add(graph)</c>.
    /// </summary>
    /// <param name="width">Width of the graph in points.</param>
    /// <param name="height">Height of the graph in points.</param>
    /// <returns>A configured <see cref="Aspose.Pdf.Drawing.Graph"/> instance.</returns>
    public static Graph CreateSampleGraph(double width = 400, double height = 200)
    {
        // Initialize the graph with the desired dimensions.
        Graph graph = new Graph(width, height);

        // ---------- Rectangle ----------
        // Parameters: left, bottom, width, height
        Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 150, 100);
        rect.GraphInfo = new GraphInfo
        {
            FillColor = Color.LightGray,
            Color = Color.Black,
            LineWidth = 2
        };
        graph.Shapes.Add(rect);

        // ---------- Ellipse ----------
        // Parameters: left, bottom, width, height
        Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(200, 0, 150, 100);
        ellipse.GraphInfo = new GraphInfo
        {
            FillColor = Color.Yellow,
            Color = Color.Red,
            LineWidth = 1.5f
        };
        graph.Shapes.Add(ellipse);

        // ---------- Line ----------
        // Constructor expects a float array: { x1, y1, x2, y2 }
        float[] linePoints = { 0, 150, 350, 150 };
        Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
        line.GraphInfo = new GraphInfo
        {
            Color = Color.Blue,
            LineWidth = 3
        };
        graph.Shapes.Add(line);

        // Return the fully built graph.
        return graph;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The method is intentionally left minimal; it simply demonstrates that the
        // library compiles and can be invoked without performing any I/O.
        Graph sample = GraphFactory.CreateSampleGraph();
        // No further action required for the build.
    }
}
