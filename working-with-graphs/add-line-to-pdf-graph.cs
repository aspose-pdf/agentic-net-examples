using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a graph container with a specific size (width x height in points)
            Graph graph = new Graph(400, 200);

            // Define a line with exact coordinates: start (50,150), end (350,150)
            // Position array format: { x1, y1, x2, y2 }
            float[] linePosition = { 50f, 150f, 350f, 150f };
            Line line = new Line(linePosition);

            // Set visual attributes of the line via GraphInfo
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // line color
                LineWidth = 2f                    // line thickness
            };

            // Add the line shape to the graph's shape collection
            graph.Shapes.Add(line);

            // Position the graph on the page (optional offsets)
            graph.Left = 50;   // distance from the left edge of the page
            graph.Top  = 500;  // distance from the bottom edge of the page

            // Insert the graph into the page's content
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dimension‑specific line saved to '{outputPath}'.");
    }
}