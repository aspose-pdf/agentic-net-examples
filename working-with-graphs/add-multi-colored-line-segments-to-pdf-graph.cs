using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "graph_output.pdf";

        // Sample data: each tuple contains startX, startY, endX, endY, and the line color
        var segments = new (float startX, float startY, float endX, float endY, Color color)[]
        {
            (50, 700, 150, 650, Color.Red),
            (150, 650, 250, 720, Color.Green),
            (250, 720, 350, 680, Color.Blue),
            (350, 680, 450, 730, Color.Orange)
        };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – size can be adjusted as needed
            Graph graph = new Graph(500, 300);

            // Iterate over each segment and add a Line shape with its own color
            foreach (var seg in segments)
            {
                // Position array: {x1, y1, x2, y2}
                float[] pos = { seg.startX, seg.startY, seg.endX, seg.endY };
                Line line = new Line(pos);

                // Set visual properties via GraphInfo
                line.GraphInfo = new GraphInfo
                {
                    Color = seg.color,   // line color
                    LineWidth = 2        // optional line thickness
                };

                // Add the line to the graph
                graph.Shapes.Add(line);
            }

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
    }
}