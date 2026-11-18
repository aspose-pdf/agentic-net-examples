using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "polygon_graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Graph constructor takes only width and height. Position it with Left/Top properties.
            Graph graph = new Graph(500, 500)
            {
                Left = 0,
                Top = 0,
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };
            page.Paragraphs.Add(graph);

            // Define the vertices of the polygon – the first point is repeated at the end to close the shape.
            float[] polygonPoints = new float[]
            {
                100, 100,   // point 1
                400, 100,   // point 2
                300, 400,   // point 3
                200, 400,   // point 4
                100, 100    // close the polygon
            };

            // In Aspose.Pdf.Drawing a closed Line with a FillColor works as a polygon.
            Line polygon = new Line(polygonPoints);

            // Fill the polygon. Pattern fills are not directly supported in the current API, so we use a solid color.
            // If a pattern is required, it can be simulated by drawing additional lines inside the polygon.
            polygon.GraphInfo.FillColor = Color.LightGray;

            // Outline (stroke) settings – color, width and dash pattern.
            polygon.GraphInfo.Color = Color.DarkBlue;
            polygon.GraphInfo.LineWidth = 2f;
            polygon.GraphInfo.DashArray = new int[] { 3, 1 }; // 3 units on, 1 unit off

            // Add the polygon to the graph.
            graph.Shapes.Add(polygon);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with polygon saved to '{outputPath}'.");
    }
}
