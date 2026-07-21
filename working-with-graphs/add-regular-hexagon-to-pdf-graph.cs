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

            // Create a Graph container (width, height) – use double overload as the float overload is obsolete
            Graph graph = new Graph(400.0, 400.0);

            // Parameters for a regular hexagon
            float radius = 100f;          // distance from center to vertices
            float centerX = 200f;         // X coordinate of the center
            float centerY = 200f;         // Y coordinate of the center

            // Compute the six vertices of the hexagon
            float[] vertices = new float[12]; // 6 points * (x,y)
            for (int i = 0; i < 6; i++)
            {
                double angle = Math.PI / 3 * i - Math.PI / 2; // start at top
                vertices[2 * i]     = (float)(centerX + radius * Math.Cos(angle));
                vertices[2 * i + 1] = (float)(centerY + radius * Math.Sin(angle));
            }

            // Create line shapes connecting consecutive vertices and close the polygon
            Shape[] lines = new Shape[6];
            for (int i = 0; i < 6; i++)
            {
                int next = (i + 1) % 6;
                float[] linePos = {
                    vertices[2 * i], vertices[2 * i + 1],
                    vertices[2 * next], vertices[2 * next + 1]
                };
                Line line = new Line(linePos);
                // Set border color and thickness via GraphInfo
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue, // border color
                    LineWidth = 2f                 // border thickness
                };
                lines[i] = line;
            }

            // Combine the lines into a single Path shape (fully qualified to avoid ambiguity with System.IO.Path)
            Aspose.Pdf.Drawing.Path hexagon = new Aspose.Pdf.Drawing.Path(lines);
            // Ensure the overall path also has the desired border styling
            hexagon.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };

            // Add the hexagon shape to the graph
            graph.Shapes.Add(hexagon);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("regular_hexagon.pdf");
        }

        Console.WriteLine("PDF with a regular hexagon saved as 'regular_hexagon.pdf'.");
    }
}
