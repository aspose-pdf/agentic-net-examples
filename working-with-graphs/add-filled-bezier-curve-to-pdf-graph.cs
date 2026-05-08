using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "filled_curve.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points)
            // Use the constructor that accepts double values (the old float constructor is obsolete).
            Graph graph = new Graph(400.0, 200.0);

            // Define a Bezier curve using a position array (float[]).
            // The array contains the coordinates of the start point,
            // two control points, and the end point: {x0, y0, x1, y1, x2, y2, x3, y3}
            float[] curvePoints = { 50f, 150f, 150f, 250f, 250f, 50f, 350f, 150f };
            Curve curve = new Curve(curvePoints);

            // Set visual properties via GraphInfo:
            // - FillColor defines the interior color (use alpha channel for opacity).
            // - Color defines the border (stroke) color.
            // - LineWidth defines the border thickness.
            // Opacity: 40% transparent => 60% visible => alpha = 0.6 * 255 ≈ 153.
            // Original RGB (0.2, 0.6, 0.8) => (51, 153, 204).
            curve.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(153, 51, 153, 204), // 60% visible light‑blue fill
                Color = Color.Black,                         // Black border
                LineWidth = 3f                               // Border thickness = 3 points
            };

            // Add the curve to the graph's shape collection
            graph.Shapes.Add(curve);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      $"The PDF was not saved to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine($"Program finished. PDF path: {outputPath}");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
