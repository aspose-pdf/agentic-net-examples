using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class StarShapeExample
{
    static void Main()
    {
        // Parameters for the star
        int points = 5;                 // Number of star points
        double outerRadius = 100;       // Radius of outer vertices
        double innerRadius = 50;        // Radius of inner vertices
        double centerX = 200;           // X‑coordinate of star centre
        double centerY = 200;           // Y‑coordinate of star centre
        Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.Yellow; // Fill colour

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width and height large enough to hold the star)
            // Use the double‑based constructor (the float overload is obsolete)
            Graph graph = new Graph(400.0, 400.0);

            // Compute star vertices (alternating outer/inner points)
            List<double> vertices = new List<double>();
            double angleStep = Math.PI / points; // 180° / points
            for (int i = 0; i < points * 2; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = i * angleStep - Math.PI / 2; // start at top (‑90°)
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                vertices.Add(x);
                vertices.Add(y);
            }

            // Build a closed Path from line segments connecting the vertices
            Aspose.Pdf.Drawing.Path starPath = new Aspose.Pdf.Drawing.Path();
            // Set fill colour via GraphInfo
            starPath.GraphInfo = new GraphInfo
            {
                FillColor = fillColor,
                Color = Aspose.Pdf.Color.Black, // outline colour
                LineWidth = 1
            };

            // Add line segments between consecutive vertices
            for (int i = 0; i < vertices.Count; i += 2)
            {
                // Current point
                float x1 = (float)vertices[i];
                float y1 = (float)vertices[i + 1];
                // Next point (wrap around to first)
                int nextIndex = (i + 2) % vertices.Count;
                float x2 = (float)vertices[nextIndex];
                float y2 = (float)vertices[nextIndex + 1];

                // Create a line shape and add it to the path
                float[] linePos = { x1, y1, x2, y2 };
                Line line = new Line(linePos);
                starPath.Shapes.Add(line);
            }

            // Add the completed path to the graph
            graph.Shapes.Add(starPath);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("StarShape.pdf");
        }
    }
}
