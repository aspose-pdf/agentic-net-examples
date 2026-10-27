using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Lookup table: shape index -> border color
        var borderColors = new Dictionary<int, Aspose.Pdf.Color>
        {
            { 0, Aspose.Pdf.Color.FromRgb(1, 0, 0) }, // Red
            { 1, Aspose.Pdf.Color.FromRgb(0, 1, 0) }, // Green
            { 2, Aspose.Pdf.Color.FromRgb(0, 0, 1) }, // Blue
            { 3, Aspose.Pdf.Color.FromRgb(1, 1, 0) }  // Yellow
        };

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Parameters for drawing circles
            double startX = 100;      // X coordinate of first circle center
            double centerY = 700;     // Y coordinate (same for all circles)
            double radius = 40;       // Circle radius
            double spacing = 120;     // Horizontal distance between circles

            // Create a Graph container that will hold the drawing shapes
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Draw four circles, each with a border color from the lookup table
            for (int i = 0; i < 4; i++)
            {
                double cx = startX + i * spacing; // Current circle center X

                // Create an ellipse (circle) shape
                Ellipse circle = new Ellipse(cx - radius, centerY - radius, cx + radius, centerY + radius);

                // Determine border color; default to black if not found
                if (!borderColors.TryGetValue(i, out Aspose.Pdf.Color border))
                {
                    border = Aspose.Pdf.Color.Black;
                }

                // Set stroke (border) and fill properties via GraphInfo
                circle.GraphInfo = new GraphInfo
                {
                    LineWidth = 2,
                    Color = border,                     // Border color
                    FillColor = Aspose.Pdf.Color.Transparent // Transparent fill
                };

                // Add the shape to the graph container
                graph.Shapes.Add(circle);
            }

            // Add the graph (with all circles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF to disk
            doc.Save("graph.pdf");
        }
    }
}
