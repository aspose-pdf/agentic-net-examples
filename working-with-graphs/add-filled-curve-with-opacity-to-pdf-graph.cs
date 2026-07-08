using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 300pt) using double literals as required by the API
            Graph graph = new Graph(400.0, 300.0);

            // Define a Bezier curve using a position array (float[]).
            // The array contains the coordinates of the start point,
            // three control points, and the end point: {x0, y0, x1, y1, x2, y2, x3, y3, x4, y4}
            float[] curvePoints = { 50f, 250f, 150f, 50f, 250f, 450f, 350f, 150f };
            Curve curve = new Curve(curvePoints);

            // Set visual properties via GraphInfo
            curve.GraphInfo = new GraphInfo
            {
                // Fill color with 50% opacity (alpha = 128). Use Color.FromArgb for transparency.
                FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255), // semi‑transparent blue
                // Border (stroke) thickness
                LineWidth = 2f,
                // Border (stroke) color
                Color = Aspose.Pdf.Color.Black
            };

            // Add the curve to the graph
            graph.Shapes.Add(curve);

            // Position the graph on the page (optional: set left/top)
            graph.Left = 50;
            graph.Top = 500;

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("filled_curve.pdf");
        }

        Console.WriteLine("PDF with filled curve created successfully.");
    }
}
