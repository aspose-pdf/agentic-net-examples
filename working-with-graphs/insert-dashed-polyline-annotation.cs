using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "polyline_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define a graph that covers the whole page (width, height)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the vertices of the polyline
            Point[] vertices = new Point[]
            {
                new Point(100, 700),
                new Point(200, 600),
                new Point(300, 650),
                new Point(400, 550)
            };

            // Create a line for each consecutive pair of vertices
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                float[] lineCoords = new float[]
                {
                    (float)vertices[i].X, (float)vertices[i].Y,
                    (float)vertices[i + 1].X, (float)vertices[i + 1].Y
                };

                Line line = new Line(lineCoords);
                line.GraphInfo = new GraphInfo
                {
                    Color = Color.Black,
                    LineWidth = 2,
                    DashArray = new int[] { 5, 3 } // 5 units dash, 3 units gap
                };

                graph.Shapes.Add(line);
            }

            // Add the graph (which now contains the polyline) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with polyline saved to '{outputPath}'.");
    }
}
