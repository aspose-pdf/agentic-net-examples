using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class GraphFactory
{
    /// <summary>
    /// Builds a predefined graph containing a rectangle, an ellipse and a line.
    /// The graph can be added to any PDF page via <c>page.Paragraphs.Add(graph)</c>.
    /// </summary>
    /// <param name="width">Width of the graph container (points).</param>
    /// <param name="height">Height of the graph container (points).</param>
    /// <returns>A fully configured <see cref="Aspose.Pdf.Drawing.Graph"/> instance.</returns>
    public static Graph CreatePredefinedGraph(double width, double height)
    {
        // Create the graph container with the requested dimensions.
        Graph graph = new Graph(width, height);

        // ---- Rectangle shape ----
        // Parameters: left, bottom, width, height (float values are accepted)
        var rect = new Aspose.Pdf.Drawing.Rectangle(10f, 10f, 100f, 60f);
        rect.GraphInfo = new GraphInfo
        {
            FillColor = Color.LightGray,
            Color = Color.Black,
            LineWidth = 1.5f
        };
        graph.Shapes.Add(rect);

        // ---- Ellipse shape ----
        var ellipse = new Ellipse(130f, 10f, 80f, 60f);
        ellipse.GraphInfo = new GraphInfo
        {
            FillColor = Color.Yellow,
            Color = Color.Red,
            LineWidth = 1.0f
        };
        graph.Shapes.Add(ellipse);

        // ---- Line shape ----
        // Constructor expects an array: { x1, y1, x2, y2 }
        var line = new Line(new float[] { 10f, 90f, 210f, 90f });
        line.GraphInfo = new GraphInfo
        {
            Color = Color.Blue,
            LineWidth = 2.0f
        };
        graph.Shapes.Add(line);

        // Return the ready‑to‑use graph.
        return graph;
    }
}

// Dummy entry point to satisfy the compiler when building an executable.
public class Program
{
    public static void Main()
    {
        // Intentionally left blank – the library method can be called from other code.
    }
}