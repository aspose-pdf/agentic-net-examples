using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

public static class GraphHelper
{
    // Creates a predefined graph containing a rectangle, an ellipse, and a line.
    // The returned Graph can be added to any page via: page.Paragraphs.Add(graph);
    public static Graph CreateSampleGraph(double width = 400, double height = 200)
    {
        // Initialize the graph with the specified dimensions.
        Graph graph = new Graph(width, height);

        // Add a light‑gray rectangle with a black border.
        var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
        rect.GraphInfo = new GraphInfo
        {
            FillColor = Color.LightGray,
            Color = Color.Black,
            LineWidth = 2f
        };
        graph.Shapes.Add(rect);

        // Add a yellow ellipse with a red border.
        var ellipse = new Ellipse(220f, 0f, 150f, 100f);
        ellipse.GraphInfo = new GraphInfo
        {
            FillColor = Color.Yellow,
            Color = Color.Red,
            LineWidth = 1.5f
        };
        graph.Shapes.Add(ellipse);

        // Add a blue line.
        float[] linePoints = { 0f, 150f, 350f, 150f };
        var line = new Line(linePoints);
        line.GraphInfo = new GraphInfo
        {
            Color = Color.Blue,
            LineWidth = 3f
        };
        graph.Shapes.Add(line);

        // Optional title for the graph – must be a TextFragment, not a string.
        graph.Title = new TextFragment("Sample Graph");

        return graph;
    }
}

public class Program
{
    public static void Main()
    {
        // Create a new PDF document.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Build the sample graph and add it to the page.
        Graph sampleGraph = GraphHelper.CreateSampleGraph();
        page.Paragraphs.Add(sampleGraph);

        // Save the document.
        doc.Save("SampleGraph.pdf");
    }
}
