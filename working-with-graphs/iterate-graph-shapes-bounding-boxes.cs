using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

#pragma warning disable NU1903 // Suppress known vulnerability warning for Microsoft.Bcl.Memory

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500pt, height: 400pt)
            Graph graph = new Graph(500.0, 400.0)
            {
                // Position the graph at the bottom‑left corner of the page
                Left = 0,
                Top  = 0
            };

            // ---- Add some shapes to the graph ----

            // Rectangle shape
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 300f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // Ellipse shape
            var ellipseShape = new Aspose.Pdf.Drawing.Ellipse(300f, 250f, 150f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ---- Iterate through all shapes in the graph and log their bounding boxes ----
            Console.WriteLine("Shape bounding boxes in the graph:");
            foreach (Shape shape in graph.Shapes)
            {
                // Determine the concrete shape type to extract coordinates
                if (shape is Aspose.Pdf.Drawing.Rectangle r)
                {
                    // Rectangle: Left, Bottom, Width, Height
                    Console.WriteLine($"Rectangle - LLX:{r.Left}, LLY:{r.Bottom}, URX:{r.Left + r.Width}, URY:{r.Bottom + r.Height}");
                }
                else if (shape is Aspose.Pdf.Drawing.Ellipse e)
                {
                    // Ellipse uses the same coordinate properties as Rectangle
                    Console.WriteLine($"Ellipse   - LLX:{e.Left}, LLY:{e.Bottom}, URX:{e.Left + e.Width}, URY:{e.Bottom + e.Height}");
                }
                else if (shape is Aspose.Pdf.Drawing.Path p)
                {
                    // Path does not expose direct bounds; indicate its presence
                    Console.WriteLine("Path shape - bounding box not directly accessible");
                }
                else
                {
                    // Generic fallback for other shape types
                    Console.WriteLine($"Shape of type {shape.GetType().Name} - bounding box not supported in this example");
                }
            }

            // Save the PDF (optional) – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "GraphWithShapes.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Saved on non‑Windows platform – ensure libgdiplus is installed.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
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
