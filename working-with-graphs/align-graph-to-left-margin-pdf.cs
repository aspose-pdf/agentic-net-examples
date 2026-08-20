using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define graph dimensions (width x height in points)
            float graphWidth = 400f;
            float graphHeight = 200f;

            // Create the graph
            Graph graph = new Graph(graphWidth, graphHeight);

            // Align the graph to the left margin with an offset (e.g., 50 points)
            graph.Left = 50f; // offset from the left edge of the page

            // Position the graph vertically (optional – here 50 points from the top)
            graph.Top = page.PageInfo.Height - graphHeight - 50f;

            // Add a simple rectangle shape to the graph as a visual example
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                0f, 0f, graphWidth, graphHeight);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Insert the graph into the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
