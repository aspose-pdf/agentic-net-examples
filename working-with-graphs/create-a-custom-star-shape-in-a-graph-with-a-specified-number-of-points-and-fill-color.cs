using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "star.pdf";

        // Parameters for the star
        int points = 5;                 // number of star points
        double outerRadius = 100;       // radius of outer vertices
        double innerRadius = 50;        // radius of inner vertices
        double centerX = 200;           // X coordinate of star centre
        double centerY = 300;           // Y coordinate of star centre

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values
            Graph graph = new Graph(400.0, 600.0);

            // Build the star shape as a Path
            Path starPath = CreateStarPath(points, centerX, centerY, outerRadius, innerRadius);

            // Set visual appearance (fill colour, outline colour, line width)
            starPath.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,   // fill colour
                Color = Aspose.Pdf.Color.Black,       // outline colour
                LineWidth = 1f                         // float literal as required
            };

            // Add the Path to the Graph and the Graph to the page
            graph.Shapes.Add(starPath);
            page.Paragraphs.Add(graph);

            // Guard Document.Save on platforms that lack GDI+ (e.g., macOS/Linux without libgdiplus)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping doc.Save() because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }

    // Generates a Path that draws a regular star with the given parameters
    static Path CreateStarPath(int pointCount, double cx, double cy, double outerR, double innerR)
    {
        if (pointCount < 2) throw new ArgumentException("pointCount must be >= 2");

        Path path = new Path();

        // Starting angle so the star points upwards
        double angle = -Math.PI / 2;
        double step = Math.PI / pointCount; // half‑step between outer and inner vertices

        // First vertex (outer)
        double radius = outerR;
        double startX = cx + radius * Math.Cos(angle);
        double startY = cy + radius * Math.Sin(angle);
        double prevX = startX, prevY = startY;

        // Generate alternating outer/inner vertices and connect them with lines
        for (int i = 1; i <= pointCount * 2; i++)
        {
            radius = (i % 2 == 0) ? outerR : innerR;
            angle += step;

            double x = cx + radius * Math.Cos(angle);
            double y = cy + radius * Math.Sin(angle);

            // Create a line from the previous vertex to the current one
            float[] linePos = { (float)prevX, (float)prevY, (float)x, (float)y };
            path.Shapes.Add(new Line(linePos));

            prevX = x;
            prevY = y;
        }

        // Close the star by connecting the last vertex back to the first
        float[] closePos = { (float)prevX, (float)prevY, (float)startX, (float)startY };
        path.Shapes.Add(new Line(closePos));

        return path;
    }
}
