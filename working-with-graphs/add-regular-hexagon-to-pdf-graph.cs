using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a graph container (width: 400, height: 400 points)
            Graph graph = new Graph(400, 400);

            // Define a regular hexagon (6 sides) centered at (200,200) with radius 100
            const int sides = 6;
            const double radius = 100;
            const double centerX = 200;
            const double centerY = 200;

            // Create a Path shape that will hold the polygon lines
            Path hexPath = new Path();

            // Set border color and thickness for the entire path
            hexPath.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // Border color
                LineWidth = 2                     // Border thickness
            };

            // Generate the vertices of the hexagon
            double[] xs = new double[sides];
            double[] ys = new double[sides];
            for (int i = 0; i < sides; i++)
            {
                double angle = 2 * Math.PI * i / sides;
                xs[i] = centerX + radius * Math.Cos(angle);
                ys[i] = centerY + radius * Math.Sin(angle);
            }

            // Add lines between consecutive vertices (and close the shape)
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                float[] linePoints = {
                    (float)xs[i], (float)ys[i],
                    (float)xs[next], (float)ys[next]
                };
                Line line = new Line(linePoints);
                // The line inherits the GraphInfo from the Path, but we can set it explicitly if needed
                line.GraphInfo = hexPath.GraphInfo;
                hexPath.Shapes.Add(line);
            }

            // Add the completed path to the graph
            graph.Shapes.Add(hexPath);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("RegularHexagon.pdf");
        }
    }
}