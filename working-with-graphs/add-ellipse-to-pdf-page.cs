using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_graph.pdf";

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph object with width and height, then position it on the page
            Graph graph = new Graph(400.0, 400.0); // width, height
            graph.Left = 50;   // X position on the page
            graph.Top = 500;   // Y position on the page
            page.Paragraphs.Add(graph);

            // Define an ellipse:
            // - Centered at (200, 200) within the graph's coordinate space
            // - Horizontal radius = 150 points, vertical radius = 80 points
            // - Stroke (outline) color set to blue
            // - No fill (transparent)
            double centerX = 200.0;
            double centerY = 200.0;
            double radiusX = 150.0;
            double radiusY = 80.0;
            // Ellipse constructor: left, bottom, width, height
            Ellipse ellipse = new Ellipse(
                centerX - radiusX,               // left
                centerY - radiusY,               // bottom
                radiusX * 2,                     // width
                radiusY * 2);                    // height

            // Styling – stroke color and fill color
            ellipse.GraphInfo.Color = Aspose.Pdf.Color.Blue;          // stroke
            ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.Transparent; // fill (transparent)

            // Add the ellipse to the graph's shape collection
            graph.Shapes.Add(ellipse);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
