using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400, height: 400)
            Graph graph = new Graph(400, 400);

            // Define hexagon vertices (regular polygon with 6 sides)
            // Center at (200,200), radius 100
            double centerX = 200;
            double centerY = 200;
            double radius = 100;
            double angleStep = Math.PI / 3; // 60 degrees

            // Create a Path shape to hold the hexagon edges
            Aspose.Pdf.Drawing.Path hexPath = new Aspose.Pdf.Drawing.Path();

            for (int i = 0; i < 6; i++)
            {
                // Current vertex
                double x1 = centerX + radius * Math.Cos(i * angleStep);
                double y1 = centerY + radius * Math.Sin(i * angleStep);

                // Next vertex (wrap around)
                double x2 = centerX + radius * Math.Cos(((i + 1) % 6) * angleStep);
                double y2 = centerY + radius * Math.Sin(((i + 1) % 6) * angleStep);

                // Create a line between the two vertices
                float[] linePoints = {
                    (float)x1, (float)y1,
                    (float)x2, (float)y2
                };
                Line line = new Line(linePoints);

                // Add the line to the Path
                hexPath.Shapes.Add(line);
            }

            // Set border color and thickness for the hexagon
            hexPath.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // Border color
                LineWidth = 2                     // Border thickness
            };

            // Add the Path (hexagon) to the Graph
            graph.Shapes.Add(hexPath);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("RegularHexagon.pdf");
        }

        Console.WriteLine("PDF with a regular hexagon saved as 'RegularHexagon.pdf'.");
    }
}
