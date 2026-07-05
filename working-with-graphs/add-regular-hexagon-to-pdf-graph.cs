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
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 400pt) using double constructor
            Graph graph = new Graph(400.0, 400.0);

            // Define a regular hexagon (6 sides) centered at (200,200) with radius 100
            double centerX = 200;
            double centerY = 200;
            double radius = 100;
            int sides = 6;
            double angleStep = 2 * Math.PI / sides;

            // Create a Path shape to hold the hexagon lines (fully qualified to avoid ambiguity)
            Aspose.Pdf.Drawing.Path hexPath = new Aspose.Pdf.Drawing.Path();

            // Build the hexagon by connecting consecutive vertices
            for (int i = 0; i < sides; i++)
            {
                // Current vertex
                double x1 = centerX + radius * Math.Cos(i * angleStep);
                double y1 = centerY + radius * Math.Sin(i * angleStep);

                // Next vertex (wrap around)
                double x2 = centerX + radius * Math.Cos(((i + 1) % sides) * angleStep);
                double y2 = centerY + radius * Math.Sin(((i + 1) % sides) * angleStep);

                // Create a line for this side
                float[] linePoints = {
                    (float)x1, (float)y1,
                    (float)x2, (float)y2
                };
                Line line = new Line(linePoints);

                // Add the line to the path
                hexPath.Shapes.Add(line);
            }

            // Set border color and thickness for the entire hexagon
            hexPath.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // Border color
                LineWidth = 2f                    // Border thickness (float)
            };

            // Add the hexagon path to the graph
            graph.Shapes.Add(hexPath);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("regular_hexagon.pdf");
        }

        Console.WriteLine("PDF with a regular hexagon saved as 'regular_hexagon.pdf'.");
    }
}