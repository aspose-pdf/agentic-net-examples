using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold vector shapes
            // Width and height define the drawing area on the page
            Graph graph = new Graph(500, 400);

            // Define horizontal and vertical radii for the ellipse
            double horizontalRadius = 100; // radius along the X‑axis
            double verticalRadius   = 50;  // radius along the Y‑axis

            // Calculate the bounding rectangle for the ellipse
            // Position the ellipse so its centre is at (250, 200) on the page
            double centerX = 250;
            double centerY = 200;
            double left   = centerX - horizontalRadius;
            double bottom = centerY - verticalRadius;
            double width  = horizontalRadius * 2;
            double height = verticalRadius * 2;

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Set the stroke (outline) color via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red   // stroke color
                // FillColor can be set here as well if a fill is desired
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse PDF saved to '{outputPath}'.");
    }
}