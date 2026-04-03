using System;
using System.IO;
using System.Runtime.InteropServices;
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

            // Create a Graph container (width: 400, height: 400) – double constructor as required
            Graph graph = new Graph(400.0, 400.0);

            // Define the center and radius of the regular hexagon
            double centerX = 200;
            double centerY = 200;
            double radius = 100;
            // Pre‑compute the six vertices of the hexagon
            double[] angles = { 0, 60, 120, 180, 240, 300 };
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[6];
            for (int i = 0; i < 6; i++)
            {
                double rad = angles[i] * Math.PI / 180.0;
                double x = centerX + radius * Math.Cos(rad);
                double y = centerY + radius * Math.Sin(rad);
                vertices[i] = new Aspose.Pdf.Point(x, y);
            }

            // Create a Path shape that will contain the six line segments – fully qualify to avoid ambiguity
            Aspose.Pdf.Drawing.Path hexPath = new Aspose.Pdf.Drawing.Path();

            // Add a line for each edge of the hexagon
            for (int i = 0; i < 6; i++)
            {
                // Current vertex
                Aspose.Pdf.Point p1 = vertices[i];
                // Next vertex (wrap around to the first)
                Aspose.Pdf.Point p2 = vertices[(i + 1) % 6];

                // Line constructor expects a float array: {x1, y1, x2, y2}
                float[] lineCoords = {
                    (float)p1.X, (float)p1.Y,
                    (float)p2.X, (float)p2.Y
                };
                Line edge = new Line(lineCoords);

                // Set border (stroke) color and thickness via GraphInfo – LineWidth is a float
                edge.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,   // Border color
                    LineWidth = 2f                    // Border thickness (float literal)
                };

                // Add the line to the Path
                hexPath.Shapes.Add(edge);
            }

            // Add the completed Path to the Graph
            graph.Shapes.Add(hexPath);

            // Add the Graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "RegularHexagon.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("libgdiplus is required for PDF creation on this platform. Skipping save.");
            }
        }
    }
}
