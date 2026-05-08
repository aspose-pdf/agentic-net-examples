using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class GraphFactory
{
    /// <summary>
    /// Builds a predefined graph containing a rectangle, an ellipse and a line.
    /// The graph can be added to any PDF page via <c>page.Paragraphs.Add(graph)</c>.
    /// </summary>
    /// <param name="width">The width of the graph container (points).</param>
    /// <param name="height">The height of the graph container (points).</param>
    /// <returns>An <see cref="Aspose.Pdf.Drawing.Graph"/> instance ready to be used.</returns>
    public static Graph CreateSampleGraph(double width, double height)
    {
        // Create the graph container with the requested dimensions.
        Graph graph = new Graph(width, height);

        // -------------------------------------------------
        // Rectangle shape (positioned at (10,10) with size 100x50)
        // -------------------------------------------------
        var rect = new Aspose.Pdf.Drawing.Rectangle(
            (float)10, (float)10, (float)100, (float)50);
        rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray; // background fill
        rect.GraphInfo.Color = Aspose.Pdf.Color.Black;        // stroke color
        rect.GraphInfo.LineWidth = 1.5f;
        graph.Shapes.Add(rect);

        // -------------------------------------------------
        // Ellipse shape (positioned at (130,10) with size 80x50)
        // -------------------------------------------------
        var ellipse = new Aspose.Pdf.Drawing.Ellipse(
            (float)130, (float)10, (float)80, (float)50);
        ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.Yellow;
        ellipse.GraphInfo.Color = Aspose.Pdf.Color.Red;
        ellipse.GraphInfo.LineWidth = 1.0f;
        graph.Shapes.Add(ellipse);

        // -------------------------------------------------
        // Line shape (from point (10,80) to point (210,80))
        // -------------------------------------------------
        float[] linePoints = { 10f, 80f, 210f, 80f };
        var line = new Aspose.Pdf.Drawing.Line(linePoints);
        line.GraphInfo.Color = Aspose.Pdf.Color.Blue;
        line.GraphInfo.LineWidth = 2.0f;
        graph.Shapes.Add(line);

        // The graph is now fully configured and can be returned.
        return graph;
    }
}

// Simple entry point to satisfy the compiler. In a real library this would be omitted
// or moved to a separate console project.
class Program
{
    static void Main()
    {
        // Example usage: create a graph and add it to a new PDF page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Graph sample = GraphFactory.CreateSampleGraph(250, 120);
            page.Paragraphs.Add(sample);

            string outputPath = "sample_graph.pdf";

            // Guard Document.Save on non‑Windows platforms where libgdiplus (GDI+) may be missing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping Document.Save() on non‑Windows platform because GDI+ (libgdiplus) is not available.");
            }
        }
        Console.WriteLine("Graph creation routine completed.");
    }
}
