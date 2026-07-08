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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define graph size (width and height in points)
            double graphWidth = 400;
            double graphHeight = 400;

            // Create a Graph container
            Graph graph = new Graph(graphWidth, graphHeight);

            // Position the graph on the page (optional: set Left/Top)
            graph.Left = 100;   // X position on the page
            graph.Top = 500;    // Y position on the page

            // Create a star shape Path
            int starPoints = 5;               // Number of star points
            double outerRadius = 150;         // Outer radius
            double innerRadius = 60;          // Inner radius
            double centerX = graphWidth / 2;  // Center of the graph
            double centerY = graphHeight / 2;

            Aspose.Pdf.Drawing.Path starPath = CreateStarPath(starPoints, outerRadius, innerRadius, centerX, centerY);

            // Set visual appearance (fill and stroke)
            starPath.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 2
            };

            // Add the star Path to the graph
            graph.Shapes.Add(starPath);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with star shape saved to '{outputPath}'.");
    }

    // Generates a Path representing a star with the specified parameters
    private static Aspose.Pdf.Drawing.Path CreateStarPath(int points, double outerRadius, double innerRadius, double centerX, double centerY)
    {
        // Total vertices = 2 * points (alternating outer/inner)
        int vertexCount = points * 2;
        double[] verticesX = new double[vertexCount];
        double[] verticesY = new double[vertexCount];

        double angleStep = Math.PI / points; // 180° / points in radians
        double startAngle = -Math.PI / 2;    // Start at the top

        for (int i = 0; i < vertexCount; i++)
        {
            double radius = (i % 2 == 0) ? outerRadius : innerRadius;
            double angle = startAngle + i * angleStep;
            verticesX[i] = centerX + radius * Math.Cos(angle);
            verticesY[i] = centerY + radius * Math.Sin(angle);
        }

        // Create a Path and add line segments between consecutive vertices
        Aspose.Pdf.Drawing.Path path = new Aspose.Pdf.Drawing.Path();

        for (int i = 0; i < vertexCount; i++)
        {
            int next = (i + 1) % vertexCount;
            float[] linePos = {
                (float)verticesX[i], (float)verticesY[i],
                (float)verticesX[next], (float)verticesY[next]
            };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePos);
            // Inherit the Path's GraphInfo (fill/stroke) – no need to set here
            path.Shapes.Add(line);
        }

        return path;
    }
}