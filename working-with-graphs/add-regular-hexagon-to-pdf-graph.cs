using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal.
        using (Document doc = new Document())
        {
            // Add a blank page to the document.
            Page page = doc.Pages.Add();

            // Define a graph (container for vector shapes) with desired size.
            // Width and height are in points (1/72 inch).
            Graph graph = new Graph(400, 300);

            // Parameters for a regular hexagon.
            double centerX = 200;   // X coordinate of the center.
            double centerY = 150;   // Y coordinate of the center.
            double radius  = 100;   // Distance from center to any vertex.
            int sides = 6;          // Number of sides for a regular polygon.

            // Pre‑compute the vertices of the hexagon.
            var vertices = new System.Drawing.PointF[sides];
            for (int i = 0; i < sides; i++)
            {
                double angle = Math.PI / 3 * i; // 60° increments.
                float x = (float)(centerX + radius * Math.Cos(angle));
                float y = (float)(centerY + radius * Math.Sin(angle));
                vertices[i] = new System.Drawing.PointF(x, y);
            }

            // Create line shapes for each edge of the polygon.
            for (int i = 0; i < sides; i++)
            {
                // Current vertex.
                var p1 = vertices[i];
                // Next vertex (wrap around to the first vertex).
                var p2 = vertices[(i + 1) % sides];

                // Line constructor expects a float array: { x1, y1, x2, y2 }.
                float[] linePos = { p1.X, p1.Y, p2.X, p2.Y };
                Line line = new Line(linePos);

                // Set border (stroke) color and thickness via GraphInfo.
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,   // Border color.
                    LineWidth = 2                     // Border thickness.
                };

                // Add the line to the graph.
                graph.Shapes.Add(line);
            }

            // Add the completed graph to the page's paragraph collection.
            page.Paragraphs.Add(graph);

            // Save the PDF document.
            doc.Save("RegularHexagon.pdf");
        }

        Console.WriteLine("PDF with a regular hexagon saved as 'RegularHexagon.pdf'.");
    }
}