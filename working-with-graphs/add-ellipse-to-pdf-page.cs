using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Center and radii of the ellipse
            double centerX = 300;   // horizontal center
            double centerY = 400;   // vertical center
            double radiusX = 100;   // horizontal radius
            double radiusY = 60;    // vertical radius

            // Calculate the bounding rectangle parameters required by the Ellipse constructor
            double left   = centerX - radiusX;   // left coordinate
            double bottom = centerY - radiusY;   // bottom coordinate
            double width  = radiusX * 2;         // total width (diameter)
            double height = radiusY * 2;         // total height (diameter)

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);
            // Stroke (outline) color
            ellipse.GraphInfo.Color = Color.Blue;
            // Fill color – transparent (no fill)
            ellipse.GraphInfo.FillColor = Color.Transparent;

            // Create a Graph container that spans the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse PDF saved to '{outputPath}'.");
    }
}
