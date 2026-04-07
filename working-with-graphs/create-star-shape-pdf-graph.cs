using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class StarShapeExample
{
    static void Main()
    {
        // Parameters for the star
        int points = 5;                     // Number of star points
        double outerRadius = 200;           // Outer radius
        double innerRadius = 80;            // Inner radius
        double centerX = 250;               // Center X coordinate
        double centerY = 250;               // Center Y coordinate
        Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.Yellow; // Fill color

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Graph rendering (requires GDI+). Guard it on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a Graph container (size 500x500 points) – use double ctor
                Graph graph = new Graph(500.0, 500.0);

                // Create a Path shape that will hold the star lines – fully qualified
                Aspose.Pdf.Drawing.Path starPath = new Aspose.Pdf.Drawing.Path();

                // Compute the vertices of the star (alternating outer/inner points)
                int totalVertices = points * 2;
                double angleStep = Math.PI / points; // 180° / points in radians

                // Store the computed points
                var vertices = new (float x, float y)[totalVertices];
                for (int i = 0; i < totalVertices; i++)
                {
                    double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                    double angle = i * angleStep - Math.PI / 2; // start at top (‑90°)

                    float x = (float)(centerX + radius * Math.Cos(angle));
                    float y = (float)(centerY + radius * Math.Sin(angle));
                    vertices[i] = (x, y);
                }

                // Create lines between consecutive vertices and close the shape
                for (int i = 0; i < totalVertices; i++)
                {
                    var start = vertices[i];
                    var end = vertices[(i + 1) % totalVertices]; // wrap around

                    // Line constructor expects a float array: {x1, y1, x2, y2}
                    float[] linePos = { start.x, start.y, end.x, end.y };
                    Line line = new Line(linePos);

                    // Add the line to the Path
                    starPath.Shapes.Add(line);
                }

                // Set visual properties via GraphInfo
                starPath.GraphInfo = new GraphInfo
                {
                    FillColor = fillColor,               // Fill the star
                    Color = Aspose.Pdf.Color.Black,      // Outline color
                    LineWidth = 1.5f                     // Outline thickness
                };

                // Add the completed Path to the Graph
                graph.Shapes.Add(starPath);

                // Add the Graph to the page's paragraphs
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Graph rendering requires GDI+ (libgdiplus) which is unavailable on this platform. Star shape will be omitted.");
            }

            // Save the PDF (Document.Save works on all platforms when no GDI+ dependent objects are present)
            doc.Save("StarShape.pdf");
        }

        Console.WriteLine("PDF created: StarShape.pdf");
    }
}
