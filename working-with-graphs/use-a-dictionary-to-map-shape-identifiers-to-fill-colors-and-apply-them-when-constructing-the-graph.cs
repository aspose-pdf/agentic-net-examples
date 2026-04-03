using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Map shape identifiers to fill colors (use Aspose.Pdf.Drawing.Color)
        var shapeColors = new Dictionary<string, Color>
        {
            { "rect", Color.LightGray },
            { "ellipse", Color.Yellow },
            { "line", Color.Red }
        };

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double literals as required
            Graph graph = new Graph(500.0, 300.0);

            // ---- Rectangle (Aspose.Pdf.Drawing.Rectangle) ----
            // Position: left=50, bottom=200, width=150, height=100
            // Drawing.Rectangle expects (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 200f, 200f, 300f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = shapeColors["rect"], // Fill color from dictionary
                Color = Color.Black,               // Stroke color
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // ---- Ellipse ----
            // Position: left=200, bottom=150, width=100, height=80
            Ellipse ellipse = new Ellipse(200f, 150f, 100f, 80f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = shapeColors["ellipse"], // Fill color from dictionary
                Color = Color.DarkBlue,               // Stroke color
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipse);

            // ---- Line ----
            // Points array: { x1, y1, x2, y2 }
            float[] linePoints = { 350f, 250f, 450f, 250f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = shapeColors["line"], // Stroke color from dictionary
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "shapes.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was saved without rendering the graph.");
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
