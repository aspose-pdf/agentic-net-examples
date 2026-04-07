using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class StarShapeExample
{
    static void Main()
    {
        // Parameters for the star
        int starPoints = 5;                 // Number of star points
        double outerRadius = 200;           // Outer radius of the star
        double innerRadius = 80;            // Inner radius of the star
        double centerX = 250;               // Center X coordinate
        double centerY = 250;               // Center Y coordinate
        Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.Yellow; // Fill color

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size 500x500 points)
            Graph graph = new Graph(500, 500);

            // Create a Path that will hold the star shape
            Path starPath = new Path();

            // Compute the vertices of the star (alternating outer and inner points)
            int vertexCount = starPoints * 2;
            double angleStep = Math.PI / starPoints; // 180° / starPoints in radians
            float[] verticesX = new float[vertexCount];
            float[] verticesY = new float[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = i * angleStep - Math.PI / 2; // start at the top
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                verticesX[i] = (float)x;
                verticesY[i] = (float)y;
            }

            // Add line segments between consecutive vertices and close the shape
            for (int i = 0; i < vertexCount; i++)
            {
                int next = (i + 1) % vertexCount;
                float[] lineCoords = {
                    verticesX[i], verticesY[i],
                    verticesX[next], verticesY[next]
                };
                Line line = new Line(lineCoords);
                // Optional: set line color and width
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1
                };
                starPath.Shapes.Add(line);
            }

            // Set fill color (and optional stroke color) for the whole path
            starPath.GraphInfo = new GraphInfo
            {
                FillColor = fillColor,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };

            // Add the completed path to the graph
            graph.Shapes.Add(starPath);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("StarShape.pdf");
        }

        Console.WriteLine("Star shape PDF created: StarShape.pdf");
    }
}