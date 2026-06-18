using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class StarShapeExample
{
    static void Main()
    {
        // Parameters for the star
        int points = 5;                 // Number of star points
        double outerRadius = 100;       // Outer radius
        double innerRadius = 50;        // Inner radius
        double centerX = 200;           // Center X coordinate
        double centerY = 300;           // Center Y coordinate
        Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.Yellow; // Fill color

        // Create a new PDF document (lifecycle rule: use using)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width and height define the drawing area)
            // Use the double constructor as the float overload is obsolete
            Graph graph = new Graph(400.0, 400.0);

            // Compute the star vertices (alternating outer and inner points)
            List<Aspose.Pdf.Point> vertices = new List<Aspose.Pdf.Point>();
            double angleStep = Math.PI / points; // 180° / points in radians

            for (int i = 0; i < points * 2; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = i * angleStep - Math.PI / 2; // start at top (‑90°)

                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);

                vertices.Add(new Aspose.Pdf.Point((float)x, (float)y));
            }

            // Create a Path shape and add line segments between consecutive vertices
            // Fully qualify the Path type to avoid ambiguity with System.IO.Path
            Aspose.Pdf.Drawing.Path starPath = new Aspose.Pdf.Drawing.Path();

            // Set visual appearance via GraphInfo (fill and stroke)
            starPath.GraphInfo = new GraphInfo
            {
                FillColor = fillColor,               // Fill color
                Color = Aspose.Pdf.Color.Black,      // Stroke color
                LineWidth = 1f                        // Stroke width (float literal)
            };

            // Add lines to form the star shape
            for (int i = 0; i < vertices.Count; i++)
            {
                // Current point
                Aspose.Pdf.Point p1 = vertices[i];
                // Next point (wrap around to first)
                Aspose.Pdf.Point p2 = vertices[(i + 1) % vertices.Count];

                // Line constructor expects a float array: { x1, y1, x2, y2 }
                float[] linePos = { (float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y };
                Line line = new Line(linePos);

                // Add the line to the Path's shape collection
                starPath.Shapes.Add(line);
            }

            // Add the completed Path to the Graph
            graph.Shapes.Add(starPath);

            // Add the Graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "StarShape.pdf";
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

        Console.WriteLine("Star shape PDF created: StarShape.pdf");
    }

    // Helper to detect a nested DllNotFoundException (missing libgdiplus)
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
