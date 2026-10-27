using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_circle.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Circle parameters (coordinates are in points)
            double centerX = 200;   // X coordinate of the circle center
            double centerY = 400;   // Y coordinate of the circle center
            double radius  = 50;    // Radius of the circle

            // Create a Graph container that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create a filled ellipse (circle) shape.
            // Ellipse constructor: left, bottom, width, height
            Ellipse circle = new Ellipse(
                (float)(centerX - radius),   // left (lower‑left X)
                (float)(centerY - radius),   // bottom (lower‑left Y)
                (float)(radius * 2),         // width
                (float)(radius * 2)          // height
            );

            // Style the circle via its GraphInfo
            // Use normalized RGB values (0..1) or predefined colors to avoid range errors
            circle.GraphInfo.FillColor = Aspose.Pdf.Color.Blue; // solid blue fill
            circle.GraphInfo.Color = Aspose.Pdf.Color.Black;   // outline color
            circle.GraphInfo.LineWidth = 1;                     // outline width

            // Add the circle to the graph, then add the graph to the page
            graph.Shapes.Add(circle);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled circle saved to '{outputPath}'.");
    }
}
