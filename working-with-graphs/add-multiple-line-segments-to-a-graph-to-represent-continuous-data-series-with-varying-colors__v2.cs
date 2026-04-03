using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "lines_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use the full page size for the graph
            double graphWidth = page.PageInfo.Width;
            double graphHeight = page.PageInfo.Height;

            // Create a graph canvas
            Graph graph = new Graph(graphWidth, graphHeight);

            // Define line segments with varying colors
            var segments = new[]
            {
                new { X1 = 50f,  Y1 = 700f, X2 = 200f, Y2 = 700f, Color = Color.Blue },
                new { X1 = 200f, Y1 = 700f, X2 = 350f, Y2 = 600f, Color = Color.Red },
                new { X1 = 350f, Y1 = 600f, X2 = 500f, Y2 = 650f, Color = Color.Green },
                new { X1 = 500f, Y1 = 650f, X2 = 650f, Y2 = 500f, Color = Color.Orange }
            };

            // Add each line segment to the graph
            foreach (var seg in segments)
            {
                // Position array: {x1, y1, x2, y2}
                float[] pos = { seg.X1, seg.Y1, seg.X2, seg.Y2 };
                Line line = new Line(pos);
                line.GraphInfo = new GraphInfo
                {
                    Color = seg.Color,
                    LineWidth = 2f
                };
                graph.Shapes.Add(line);
            }

            // Attach the graph to the page
            page.Paragraphs.Add(graph);

            // Save the document, handling possible GDI+ issues on non‑Windows platforms
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
                    Console.WriteLine("libgdiplus not available; graph rendering skipped. PDF saved without graph.");
                }
            }
        }
    }

    // Helper to detect nested DllNotFoundException
    static bool ContainsDllNotFound(Exception ex)
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