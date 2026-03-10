using System;
using System.Runtime.InteropServices;
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
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // On non‑Windows platforms Graph rendering requires libgdiplus.
            // If the native library is not present we skip the Graph to avoid a
            // System.TypeInitializationException caused by GDI+ initialization.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on this OS; saving PDF without the curve.");
                doc.Save("filled_curve.pdf");
                return;
            }

            // Create a Graph container to hold vector shapes.
            // Width and height define the coordinate space for the graph (double literals are required).
            Graph graph = new Graph(400.0, 200.0);

            // Define the points for a cubic Bezier curve (8 values required):
            // startX, startY, controlX1, controlY1, controlX2, controlY2, endX, endY
            float[] curvePoints = { 100f, 100f, 150f, 200f, 250f, 0f, 200f, 100f };
            Curve curve = new Curve(curvePoints);

            // Set visual properties via GraphInfo (fill, stroke, line width)
            curve.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,   // Fill color
                Color = Color.DarkBlue,        // Stroke color
                LineWidth = 2                  // Stroke thickness
            };

            // Add the curve to the graph and the graph to the page
            graph.Shapes.Add(curve);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("filled_curve.pdf");
        }

        Console.WriteLine("PDF with filled curve saved as 'filled_curve.pdf'.");
    }
}