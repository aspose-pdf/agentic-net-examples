using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "star_shape.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define star parameters
            int points = 5;                     // Number of star points
            double centerX = 300;               // Center X coordinate
            double centerY = 400;               // Center Y coordinate
            double outerRadius = 150;           // Outer radius
            double innerRadius = outerRadius / 2.5; // Inner radius (adjust as needed)

            // Compute the vertices of the star (alternating outer/inner points)
            int vertexCount = points * 2;
            double angleStep = Math.PI / points; // 180° / points in radians
            double[] xs = new double[vertexCount];
            double[] ys = new double[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = i * angleStep - Math.PI / 2; // start at top (‑90°)
                xs[i] = centerX + radius * Math.Cos(angle);
                ys[i] = centerY + radius * Math.Sin(angle);
            }

            // Create a Path shape and add line segments between consecutive vertices
            Aspose.Pdf.Drawing.Path starPath = new Aspose.Pdf.Drawing.Path();

            // Set visual appearance (fill color and optional stroke)
            starPath.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,   // Fill color of the star
                Color = Aspose.Pdf.Color.Black,        // Stroke color
                LineWidth = 1
            };

            // Add line segments to the Path
            for (int i = 0; i < vertexCount; i++)
            {
                int next = (i + 1) % vertexCount; // wrap around to first vertex
                float[] lineCoords = new float[]
                {
                    (float)xs[i], (float)ys[i],
                    (float)xs[next], (float)ys[next]
                };
                starPath.Shapes.Add(new Line(lineCoords));
            }

            // Create a Graph container sized to the page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Add the star Path to the Graph
            graph.Shapes.Add(starPath);

            // Add the Graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Star shape PDF saved to '{outputPath}'.");
    }
}
