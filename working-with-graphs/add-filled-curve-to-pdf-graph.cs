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
            // NOTE: Use the constructor that accepts double values (as required by newer Aspose.Pdf versions)
            Graph graph = new Graph(400.0, 300.0)
            {
                // Position the graph on the page
                Left = 50,
                Top  = 500
            };

            // Define a Bezier curve using a position array.
            // The array format: startX, startY, ctrl1X, ctrl1Y, ctrl2X, ctrl2Y, endX, endY
            float[] curvePoints = { 100f, 100f, 150f, 200f, 250f, 200f, 300f, 100f };
            Curve curve = new Curve(curvePoints);

            // Set visual properties via GraphInfo:
            // - FillColor defines the interior color.
            // - FillColorAlpha (if available) defines fill opacity. In the current Aspose.Pdf version this property is not present, so the fill will be fully opaque.
            // - Color defines the border (stroke) color.
            // - LineWidth defines the border thickness.
            curve.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.FromRgb(0.2, 0.6, 0.8), // light blue fill
                // FillColorAlpha = 0.5f, // <-- removed: property not available in this version
                Color     = Aspose.Pdf.Color.Black,               // black border
                LineWidth = 2f                                    // border thickness
            };

            // Add the curve shape to the graph
            graph.Shapes.Add(curve);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("filled_curve.pdf");
        }
    }
}
