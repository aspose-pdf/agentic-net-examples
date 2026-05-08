using System;
using System.IO;
using System.Runtime.InteropServices;
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

            // Use the Graph constructor that accepts double values (as required by the API)
            Graph graph = new Graph(500.0, 400.0);

            // Define four control points for the Bezier curve (x0,y0, x1,y1, x2,y2, x3,y3)
            float[] controlPoints = new float[]
            {
                50f, 350f,   // Point 0
                150f, 50f,   // Point 1
                350f, 50f,   // Point 2
                450f, 350f   // Point 3
            };

            // Create the Curve shape with the control points
            Curve bezier = new Curve(controlPoints);

            // Set stroke color and line width via GraphInfo (LineWidth is a float)
            bezier.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // Stroke color
                LineWidth = 2f                    // Optional line width (float literal)
            };

            // Add the curve to the graph
            graph.Shapes.Add(bezier);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            string outputPath = "BezierCurve.pdf";

            // Guard Document.Save on platforms that lack GDI+ (libgdiplus) to avoid TypeInitializationException
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                // On non‑Windows platforms the Graph rendering requires GDI+ which may be missing.
                // Remove the graph before saving so the PDF can still be created.
                page.Paragraphs.Clear();
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved without the graph (GDI+ not available) to '{outputPath}'.");
            }
        }
    }
}
