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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 300 points)
            // Use the constructor that accepts double values (the old float constructor is obsolete)
            Graph graph = new Graph(400.0, 300.0);

            // Position the graph on the page (optional: set Left/Top if needed)
            graph.Left = 50;   // X coordinate
            graph.Top  = 500;  // Y coordinate

            // Define a Bezier curve using an array of coordinates:
            // The array format is { x1, y1, x2, y2, x3, y3, x4, y4, ... }
            // For a single cubic Bezier segment we need 4 points (start + 3 control points)
            float[] curvePoints = { 0, 0,   // start point (relative to graph origin)
                                    100, 200, // first control point
                                    200, -100, // second control point
                                    300, 0 }; // end point

            // Create the Curve shape
            Curve curve = new Curve(curvePoints);

            // Set visual properties via GraphInfo
            // Transparency is not a property of GraphInfo. Use an ARGB color where the alpha channel
            // defines the fill opacity (0 = fully transparent, 255 = fully opaque).
            // Desired opacity: 60% (alpha = 0.6 * 255 ≈ 153)
            // Original light‑blue RGB (0.2, 0.6, 0.8) => (51, 153, 204)
            curve.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.FromArgb(153, 51, 153, 204), // 60% opaque light blue
                Color     = Aspose.Pdf.Color.Black,                     // border color
                LineWidth = 2f                                            // border thickness (float)
            };

            // Add the curve to the graph's shape collection
            graph.Shapes.Add(curve);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("filled_curve.pdf");
        }

        Console.WriteLine("PDF with filled curve saved as 'filled_curve.pdf'.");
    }
}
