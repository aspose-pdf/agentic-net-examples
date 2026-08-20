using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) – use double overload
            Graph graph = new Graph(400.0, 200.0);

            // Define the points for a cubic Bézier curve.
            // The array format is: { startX, startY, ctrl1X, ctrl1Y, ctrl2X, ctrl2Y, endX, endY }
            float[] curvePoints = { 50f, 150f, 150f, 250f, 250f, 150f, 350f, 200f };
            Curve curve = new Curve(curvePoints);

            // Set visual properties via GraphInfo:
            // - FillColor with alpha channel defines fill opacity.
            // - Color defines the stroke (border) color.
            // - LineWidth defines the border thickness.
            curve.GraphInfo = new GraphInfo
            {
                // 50 % opacity light blue (R=0.2, G=0.5, B=0.8)
                FillColor = Color.FromArgb(128, 51, 128, 204),
                Color = Color.Black,
                LineWidth = 2
            };

            // Add the curve to the graph's shape collection
            graph.Shapes.Add(curve);

            // Place the graph on the page
            page.Paragraphs.Add(graph);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("filled_curve.pdf");
        }

        Console.WriteLine("PDF with filled curve saved as 'filled_curve.pdf'.");
    }
}