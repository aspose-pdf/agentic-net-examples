using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a graph container with desired dimensions
            Graph graph = new Graph(500, 400);

            // Define four control points for the Bezier curve
            // (x0, y0), (x1, y1), (x2, y2), (x3, y3)
            float[] controlPoints = new float[]
            {
                100f, 300f, // first point
                150f, 350f, // second point
                250f, 250f, // third point
                300f, 300f  // fourth point
            };

            // Create the curve shape using the control points
            Curve bezier = new Curve(controlPoints);

            // Set stroke color and line width via GraphInfo
            bezier.GraphInfo = new GraphInfo
            {
                Color = Color.Blue, // stroke color
                LineWidth = 2
            };

            // Add the curve to the graph
            graph.Shapes.Add(bezier);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("BezierCurve.pdf");
        }
    }
}