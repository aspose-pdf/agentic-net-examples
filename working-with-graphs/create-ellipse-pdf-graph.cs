using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a graph container (canvas) for drawing shapes
            Graph graph = new Graph(400, 300); // width, height of the graph area

            // Define horizontal and vertical radii
            double horizontalRadius = 100; // radius along the X‑axis
            double verticalRadius   = 50;  // radius along the Y‑axis

            // Calculate bounding box parameters for the ellipse
            double left   = 100;                     // X‑coordinate of lower‑left corner
            double bottom = 200;                     // Y‑coordinate of lower‑left corner
            double width  = horizontalRadius * 2;    // total width = 2 × horizontal radius
            double height = verticalRadius * 2;      // total height = 2 × vertical radius

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Set the stroke (border) color via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red   // stroke color
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