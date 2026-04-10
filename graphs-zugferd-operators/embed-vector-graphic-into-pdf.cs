using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_embedded.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt)
            Graph graph = new Graph(400.0, 200.0);

            // NOTE: GraphInfo does not expose a MarginInfo property in current SDK versions.
            // Positioning can be handled via the page layout or by adjusting the Graph's internal coordinates.

            // ----- Add a rectangle shape -----
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ----- Add a line shape -----
            float[] linePoints = { 0f, 0f, 300f, 100f };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(line);

            // Add the Graph (vector graphic) to the page's content stream
            page.Paragraphs.Add(graph);

            // Save the PDF – the graph is now embedded as a vector graphic object
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded vector graphic saved to '{outputPath}'.");
    }
}