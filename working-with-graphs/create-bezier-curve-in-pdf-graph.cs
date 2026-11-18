using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "bezier_curve.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the four control points (start, two control points, end)
            double startX = 100, startY = 400;   // Start point
            double cp1X   = 150, cp1Y   = 300;   // First control point
            double cp2X   = 250, cp2Y   = 500;   // Second control point
            double endX   = 300, endY   = 400;   // End point

            // Create a Path that will contain the cubic Bezier curve
            var bezierPath = new Aspose.Pdf.Drawing.Path();

            // Add the cubic Bezier curve shape (start, cp1, cp2, end)
            bezierPath.Shapes.Add(new Curve(new float[]
            {
                (float)startX, (float)startY,
                (float)cp1X,   (float)cp1Y,
                (float)cp2X,   (float)cp2Y,
                (float)endX,   (float)endY
            }));

            // Set the stroke color and line width of the curve (e.g., red)
            bezierPath.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0), // Red color
                LineWidth = 2f
            };

            // Create a Graph container and add the Path to it
            Graph graph = new Graph(500.0, 500.0); // Width and Height of the graph area
            graph.Shapes.Add(bezierPath);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bezier curve PDF saved to '{System.IO.Path.GetFullPath(outputPath)}'.");
    }
}
