using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

#pragma warning disable NU1903 // suppress known vulnerability warning for Microsoft.Bcl.Memory

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 300pt) using double literals as required
            Graph graph = new Graph(400.0, 300.0);

            // Define a Bezier curve using a float array.
            // The array contains the start point (x0, y0) followed by three control points (x1, y1, x2, y2, x3, y3).
            float[] curvePoints = {
                50f, 250f,   // start point
                150f, 50f,   // first control point
                250f, 450f,  // second control point
                350f, 250f   // end point
            };
            Curve curve = new Curve(curvePoints);

            // Set visual properties via GraphInfo
            curve.GraphInfo = new GraphInfo
            {
                // Fill color (light blue) with 50% opacity. Use Color.FromArgb(alpha, red, green, blue).
                FillColor = Color.FromArgb(128, 0, 0, 255), // alpha 128 ≈ 0.5 opacity
                // Stroke (border) color
                Color = Color.Black,
                // Border thickness (float literal required)
                LineWidth = 2.0f // float literal as GraphInfo.LineWidth is of type float
            };

            // Add the curve to the graph
            graph.Shapes.Add(curve);

            // Position the graph on the page (optional)
            graph.Left = 50.0;   // distance from left edge (double)
            graph.Top  = 500.0;  // distance from bottom edge (double)

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "filled_curve.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without graphical content.");
                }
            }
        }

        Console.WriteLine("PDF with filled curve created successfully.");
    }

    // Helper to detect missing native GDI+ library in nested exceptions
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
