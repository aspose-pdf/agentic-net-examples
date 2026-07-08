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

            // Create a Graph container (canvas) with a defined size
            Graph graph = new Graph(400, 300); // width, height of the canvas

            // Define ellipse parameters
            double left = 100;               // X coordinate of the left side
            double bottom = 150;             // Y coordinate of the bottom side
            double horizontalRadius = 80;    // horizontal radius
            double verticalRadius = 50;      // vertical radius
            double width = horizontalRadius * 2;   // total width
            double height = verticalRadius * 2;    // total height

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Set stroke (outline) color via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red   // stroke color
                // FillColor can be omitted for no fill
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