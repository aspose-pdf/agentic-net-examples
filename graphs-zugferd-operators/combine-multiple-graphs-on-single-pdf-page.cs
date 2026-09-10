using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "combined_graphs.pdf";

        // Create a new PDF document and add a single blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Size of each graph in points
            double graphWidth = 250;
            double graphHeight = 250;

            // Positions for a 2x2 grid (origin is bottom‑left)
            // Top‑left
            AddGraph(page, 0, page.PageInfo.Height - graphHeight, graphWidth, graphHeight, "Graph 1");
            // Top‑right
            AddGraph(page, graphWidth, page.PageInfo.Height - graphHeight, graphWidth, graphHeight, "Graph 2");
            // Bottom‑left
            AddGraph(page, 0, page.PageInfo.Height - 2 * graphHeight, graphWidth, graphHeight, "Graph 3");
            // Bottom‑right
            AddGraph(page, graphWidth, page.PageInfo.Height - 2 * graphHeight, graphWidth, graphHeight, "Graph 4");

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }

    static void AddGraph(Page page, double left, double top, double width, double height, string title)
    {
        // Create a Graph container with the desired size
        Graph graph = new Graph(width, height)
        {
            Left = left,
            Top = top,
            // Title property expects a TextFragment, not a raw string
            Title = new TextFragment(title)
        };

        // Add a light‑gray rectangle as a background shape
        var rect = new Aspose.Pdf.Drawing.Rectangle(
            10f,
            10f,
            (float)(width - 20),
            (float)(height - 20));
        rect.GraphInfo = new GraphInfo
        {
            FillColor = Color.LightGray,
            Color = Color.Black,
            LineWidth = 1f
        };
        graph.Shapes.Add(rect);

        // Add a red diagonal line across the rectangle
        float[] linePoints =
        {
            10f,
            10f,
            (float)(width - 10),
            (float)(height - 10)
        };
        Line line = new Line(linePoints);
        line.GraphInfo = new GraphInfo
        {
            Color = Color.Red,
            LineWidth = 2f
        };
        graph.Shapes.Add(line);

        // Place the graph on the page
        page.Paragraphs.Add(graph);
    }
}
