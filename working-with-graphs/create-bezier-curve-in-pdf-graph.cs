using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "BezierGraph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the size of the graph (width, height)
            double graphWidth = 500;
            double graphHeight = 500;

            // Create a Graph object with the defined size
            Graph graph = new Graph(graphWidth, graphHeight);
            // Position the graph on the page (optional – defaults to (0,0))
            graph.Left = 0;
            graph.Top = 0;

            // Define the four control points of the Bezier curve
            // The Curve shape expects a flat float array: startX, startY, ctrl1X, ctrl1Y, ctrl2X, ctrl2Y, endX, endY
            float[] bezierPoints = new float[] { 50f, 250f, 150f, 400f, 350f, 100f, 450f, 250f };

            // Create the Bezier curve (as a Curve shape)
            Curve bezierCurve = new Curve(bezierPoints);
            // Set the stroke colour for the curve
            bezierCurve.GraphInfo.Color = Color.Blue;

            // Add the curve to the graph
            graph.Shapes.Add(bezierCurve);

            // Add the graph (which now contains the Bezier curve) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bezier curve PDF saved to '{outputPath}'.");
    }
}
