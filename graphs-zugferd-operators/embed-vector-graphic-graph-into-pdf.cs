using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a new page to host the vector graphic
            Page page = doc.Pages.Add();

            // Create a Graph container that spans the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the points of the simple line graph
            var points = new (float x, float y)[]
            {
                (50, 750),   // Point 1
                (150, 650),  // Point 2
                (250, 700),  // Point 3
                (350, 600),  // Point 4
                (450, 720)   // Point 5
            };

            // Add a line segment between each consecutive pair of points
            for (int i = 0; i < points.Length - 1; i++)
            {
                Line line = new Line(new float[]
                {
                    points[i].x, points[i].y,
                    points[i + 1].x, points[i + 1].y
                })
                {
                    GraphInfo = new GraphInfo
                    {
                        Color = Color.Black,
                        LineWidth = 1
                    }
                };

                graph.Shapes.Add(line);
            }

            // Embed the vector graphic into the page
            page.Paragraphs.Add(graph);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded vector graphic saved to '{outputPath}'.");
    }
}
