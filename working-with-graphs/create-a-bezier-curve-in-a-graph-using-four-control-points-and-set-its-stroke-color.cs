using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "bezier_curve.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define a Graph container (size: 500x400 points) – use double constructor as required by newer API
            Graph graph = new Graph(500.0, 400.0);

            // Four control points (x1,y1, x2,y2, x3,y3, x4,y4)
            // The Curve class expects an array of floats (four points)
            float[] controlPoints = {
                50f, 350f,   // Point 1
                150f, 50f,   // Point 2
                350f, 50f,   // Point 3
                450f, 350f   // Point 4
            };

            // Create the Bezier curve shape
            Curve bezier = new Curve(controlPoints);

            // Set visual properties via GraphInfo (stroke color and line width)
            bezier.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,          // Stroke color
                LineWidth = 2f               // Thickness of the curve line (float literal)
            };

            // Add the curve to the graph
            graph.Shapes.Add(bezier);

            // Optional: set fill color for the graph container (transparent here)
            graph.GraphInfo = new GraphInfo
            {
                FillColor = Color.Transparent
            };

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Bezier curve PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Bezier curve PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus present)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was created but cannot be saved with graphical content.");
                    // Optionally, you could save without the graph or inform the user.
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native library.
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
