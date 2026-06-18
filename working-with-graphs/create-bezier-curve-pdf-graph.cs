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

            // Create a Graph container (double literals for width/height)
            Graph graph = new Graph(500.0, 400.0);

            // Define four control points for the Bezier curve (float literals)
            float[] controlPoints = new float[]
            {
                50f, 150f,   // Point 1
                150f, 250f,  // Point 2
                250f, 50f,   // Point 3
                350f, 150f   // Point 4
            };

            // Create the Curve shape and set its stroke color and line width
            Curve bezier = new Curve(controlPoints)
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Blue,   // Stroke color
                    LineWidth = 2f        // Float literal for line width
                }
            };

            // Add the curve to the graph
            graph.Shapes.Add(bezier);

            // Add the graph to the page's paragraphs
            page.Paragraphs.Add(graph);

            string outputPath = "BezierCurve.pdf";

            // Guard Document.Save for platforms that may lack libgdiplus (macOS/Linux)
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF saved without graph rendering.");
                }
            }
        }

        Console.WriteLine("Bezier curve PDF creation attempted.");
    }

    // Helper to detect a nested DllNotFoundException (libgdiplus missing)
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

#pragma warning restore NU1903