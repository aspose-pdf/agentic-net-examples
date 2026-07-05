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

            // Create a Graph container (width, height) – Graph expects double values
            Graph graph = new Graph(500.0, 200.0);

            // Define start (x1, y1) and end (x2, y2) points for the line – Line expects a float[]
            float[] linePoints = { 50f, 150f, 450f, 50f };
            Line line = new Line(linePoints);

            // Configure visual appearance of the line via GraphInfo
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // line color
                LineWidth = 3f                  // line thickness (float literal)
                // Note: LineCap is not supported in GraphInfo for the current Aspose.Pdf version.
            };

            // Add the line shape to the graph
            graph.Shapes.Add(line);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("line_graph.pdf");
        }
    }
}
