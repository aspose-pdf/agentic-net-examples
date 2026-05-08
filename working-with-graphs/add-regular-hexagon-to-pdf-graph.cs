using System;
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

            // Create a Graph container (width: 400, height: 400) using the double constructor
            Graph graph = new Graph(400.0, 400.0);

            // Define the six vertices of a regular hexagon (center at 200,200, radius 100)
            double centerX = 200;
            double centerY = 200;
            double radius = 100;
            double angleStep = Math.PI / 3; // 60 degrees

            // Store points in an array for easy line creation
            Point[] vertices = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = i * angleStep - Math.PI / 2; // start at top
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                vertices[i] = new Point(x, y);
            }

            // Add lines between consecutive vertices and close the polygon
            for (int i = 0; i < 6; i++)
            {
                Point start = vertices[i];
                Point end = vertices[(i + 1) % 6]; // wrap around to first vertex

                // Line constructor expects a float array: {x1, y1, x2, y2}
                float[] linePos = {
                    (float)start.X, (float)start.Y,
                    (float)end.X,   (float)end.Y
                };
                Line line = new Line(linePos)
                {
                    GraphInfo = new GraphInfo
                    {
                        Color = Aspose.Pdf.Color.Blue,   // border color
                        LineWidth = 2.0f                 // border thickness
                    }
                };

                // Add the line to the graph
                graph.Shapes.Add(line);
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            string outputPath = "RegularHexagon.pdf";

            // Guard Document.Save on platforms that may lack GDI+ (libgdiplus)
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Non‑Windows platform – ensure libgdiplus is installed if rendering issues appear.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was saved without rendering the graph.");
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
