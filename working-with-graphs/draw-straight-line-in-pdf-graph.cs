using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) that will hold the line
            // The Graph constructor accepts double values.
            Graph graph = new Graph(500.0, 200.0);

            // Define the start (x1, y1) and end (x2, y2) points of the line.
            // Line expects a float[] array, so we provide float literals.
            float[] linePoints = { 50f, 150f, 450f, 50f };
            Line line = new Line(linePoints);

            // Configure visual appearance of the line via GraphInfo.
            // Note: GraphInfo does not expose a LineCap property in the current API version,
            // so only color and thickness are set here.
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,   // line color
                LineWidth = 3f        // line thickness (float)
            };

            // Add the line shape to the graph
            graph.Shapes.Add(line);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save("line_graph.pdf");
        }
    }
}
