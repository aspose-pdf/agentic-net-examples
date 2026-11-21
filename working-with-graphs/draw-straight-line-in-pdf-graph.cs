using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph dimensions (width x height)
            double graphWidth = 500;
            double graphHeight = 500;

            // Create a Graph container and position it on the page.
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = 50,   // X‑offset from the left edge of the page
                Top = 250    // Y‑offset from the bottom edge of the page
            };

            // Start and end points for the straight line (float values are required by the Line ctor)
            float startX = 100f;
            float startY = 500f;
            float endX   = 500f;
            float endY   = 300f;

            // Create the line shape. GraphInfo provides colour and thickness.
            // NOTE: GraphInfo does not expose a line‑cap property. To obtain a rounded cap
            // we draw small circles at the line ends (see startCap/endCap below).
            Line line = new Line(new float[] { startX, startY, endX, endY })
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Black,   // Stroke colour
                    LineWidth = 3          // Thickness in points
                }
            };

            // Simulate round caps by adding circles at the start and end points.
            var startCap = new Circle(startX, startY, line.GraphInfo.LineWidth / 2);
            startCap.GraphInfo.FillColor = Color.Black;
            startCap.GraphInfo.Color = Color.Black;

            var endCap = new Circle(endX, endY, line.GraphInfo.LineWidth / 2);
            endCap.GraphInfo.FillColor = Color.Black;
            endCap.GraphInfo.Color = Color.Black;

            // Add shapes to the graph in the desired order.
            graph.Shapes.Add(startCap);
            graph.Shapes.Add(line);
            graph.Shapes.Add(endCap);

            // Add the graph (which now contains the line) to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with line saved to '{outputPath}'.");
    }
}
