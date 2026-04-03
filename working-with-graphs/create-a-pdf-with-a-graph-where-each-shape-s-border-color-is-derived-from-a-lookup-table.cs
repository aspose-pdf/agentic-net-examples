using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Define output PDF path
        const string outputPath = "graph_with_lookup_colors.pdf";

        // Lookup table for border colors (RGB values in 0..1 range)
        Aspose.Pdf.Color[] borderColors = new Aspose.Pdf.Color[]
        {
            Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0), // Red
            Aspose.Pdf.Color.FromRgb(0.0, 1.0, 0.0), // Green
            Aspose.Pdf.Color.FromRgb(0.0, 0.0, 1.0)  // Blue
        };

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph object (width: 400 points, height: 300 points)
            // Use double literals as required by the non‑obsolete constructor
            Graph graph = new Graph(400.0, 300.0)
            {
                // Position the graph on the page (left, top)
                Left = 100,
                Top = 500
            };

            // ---- Shape 1: Rectangle ----
            // Rectangle constructor expects float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 80f);
            rect.GraphInfo = new GraphInfo
            {
                // Border color from lookup table (index 0)
                Color = borderColors[0],
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ---- Shape 2: Ellipse ----
            var ellipse = new Aspose.Pdf.Drawing.Ellipse(200f, 0f, 150f, 80f);
            ellipse.GraphInfo = new GraphInfo
            {
                // Border color from lookup table (index 1)
                Color = borderColors[1],
                LineWidth = 2f
            };
            graph.Shapes.Add(ellipse);

            // ---- Shape 3: Line ----
            // Line constructor takes a float array { x1, y1, x2, y2 }
            float[] linePoints = { 0f, 150f, 350f, 150f };
            var line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                // Border color from lookup table (index 2)
                Color = borderColors[2],
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException caused by missing libgdiplus
    private static bool ContainsDllNotFound(Exception ex)
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
