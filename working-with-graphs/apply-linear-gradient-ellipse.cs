using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container. The constructor takes only width and height.
            // Position the graph on the page by setting Left and Top.
            var graph = new Graph(500, 300)   // width = 550-50, height = 800-500
            {
                Left = 50,
                Top = 500,
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black)
            };

            // Define an ellipse inside the graph.
            // Ellipse(left, bottom, width, height) – width/height are the size of the bounding rectangle.
            var ellipse = new Ellipse(100, 550, 400, 200);

            // Apply a linear gradient fill from blue (left) to green (right).
            // Gradient is expressed via a Color whose PatternColorSpace is a GradientAxialShading.
            ellipse.GraphInfo.FillColor = new Color
            {
                PatternColorSpace = new GradientAxialShading(Color.Blue, Color.Green)
                {
                    Start = new Point(100, 550),   // left‑bottom of the ellipse
                    End   = new Point(500, 750)    // right‑top of the ellipse
                }
            };

            // Optionally set a stroke color for the ellipse outline.
            ellipse.GraphInfo.Color = Color.Black;

            // Add the ellipse to the graph.
            graph.Shapes.Add(ellipse);

            // Add the graph to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
