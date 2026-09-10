using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class GraphHelper
{
    // Builds a predefined graph containing a rectangle, an ellipse and a line.
    // The method returns the Graph instance so it can be added to any page.
    public static Graph BuildSampleGraph(float width = 400f, float height = 200f)
    {
        // Create the graph container with the requested dimensions.
        Graph graph = new Graph(width, height);

        // ----- Rectangle shape -----
        // Use Aspose.Pdf.Drawing.Rectangle (a Shape), not Aspose.Pdf.Rectangle.
        var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
        rect.GraphInfo = new GraphInfo
        {
            FillColor = Aspose.Pdf.Color.LightGray,
            Color     = Aspose.Pdf.Color.Black,
            LineWidth = 1f
        };
        graph.Shapes.Add(rect);

        // ----- Ellipse -----
        var ellipse = new Ellipse(120f, 0f, 80f, 60f);
        ellipse.GraphInfo = new GraphInfo
        {
            FillColor = Aspose.Pdf.Color.Yellow,
            Color     = Aspose.Pdf.Color.Red,
            LineWidth = 1.5f
        };
        graph.Shapes.Add(ellipse);

        // ----- Line -----
        // The Line constructor expects a float array: { x1, y1, x2, y2 }.
        float[] linePoints = { 0f, 100f, 300f, 100f };
        var line = new Line(linePoints);
        line.GraphInfo = new GraphInfo
        {
            Color     = Aspose.Pdf.Color.Blue,
            LineWidth = 2f
        };
        graph.Shapes.Add(line);

        return graph;
    }
}

public class SampleUsage
{
    // Demonstrates adding the predefined graph to a newly created page.
    public static void AddGraphToPage()
    {
        using (Document doc = new Document())
        {
            // Add a blank page to the document.
            Page page = doc.Pages.Add();

            // Build the graph (you can customize width/height if needed).
            Graph graph = GraphHelper.BuildSampleGraph(400f, 200f);

            // Add the graph to the page's paragraph collection.
            page.Paragraphs.Add(graph);

            // Save the PDF.
            doc.Save("output.pdf");
        }
    }
}

public class Program
{
    // Entry point required for a console application.
    public static void Main()
    {
        SampleUsage.AddGraphToPage();
    }
}