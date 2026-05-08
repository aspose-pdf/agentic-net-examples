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
        double outerRadius = 100;       // Outer radius in points
        double innerRadius = outerRadius / 2.5; // Inner radius
        double centerX = 200;           // Center X coordinate
        double centerY = 300;           // Center Y coordinate
        Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.Yellow; // Fill color

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container (size large enough to hold the star)
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(400.0, 400.0);

            // Compute the star vertices (alternating outer and inner points)
            List<float> vertices = new List<float>();
            double angleStep = Math.PI / points; // 180° / points in radians

            for (int i = 0; i < points * 2; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = i * angleStep - Math.PI / 2; // start at top

                float x = (float)(centerX + radius * Math.Cos(angle));
                float y = (float)(centerY + radius * Math.Sin(angle));

                vertices.Add(x);
                vertices.Add(y);
            }

            // Build a closed Path from the vertices using Line segments
            // Fully qualify the drawing Path to avoid conflict with System.IO.Path
            Aspose.Pdf.Drawing.Path starPath = new Aspose.Pdf.Drawing.Path();

            // Set visual properties (fill and stroke)
            starPath.GraphInfo = new GraphInfo
            {
                FillColor = fillColor,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f // float literal as required by GraphInfo
            };

            // Add lines between consecutive vertices and close the shape
            for (int i = 0; i < vertices.Count; i += 2)
            {
                // Current point
                float x1 = vertices[i];
                float y1 = vertices[i + 1];

                // Next point (wrap around to first)
                int nextIndex = (i + 2) % vertices.Count;
                float x2 = vertices[nextIndex];
                float y2 = vertices[nextIndex + 1];

                // Create a line segment
                float[] linePos = { x1, y1, x2, y2 };
                Line line = new Line(linePos);
                // Inherit the same GraphInfo (optional, can be omitted)
                line.GraphInfo = starPath.GraphInfo;

                // Add the line to the path
                starPath.Shapes.Add(line);
            }

            // Add the completed star path to the graph
            graph.Shapes.Add(starPath);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "StarShape.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Star shape PDF created: {outputPath}");
            }
            else
            {
                Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. " +
                                  "The PDF will be saved without rendering the Graph.");
                // Save without the graph – remove the graph from the page first
                page.Paragraphs.Remove(graph);
                doc.Save(outputPath);
                Console.WriteLine($"PDF (without star) saved: {outputPath}");
            }
        }
    }
}
