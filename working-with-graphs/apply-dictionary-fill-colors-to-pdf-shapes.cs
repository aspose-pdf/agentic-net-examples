using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Define a dictionary that maps shape identifiers to fill colors
        var shapeColors = new Dictionary<string, Color>
        {
            { "Rect1", Color.LightBlue },
            { "Ellipse1", Color.LightGreen },
            { "Line1", Color.LightCoral } // line will use Color property (stroke)
        };

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size is arbitrary, it will be placed on the page)
            Graph graph = new Graph(500, 400);

            // ----- Rectangle -----
            // Constructor: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 250, 150, 100);
            // Apply fill color from the dictionary
            rect.GraphInfo = new GraphInfo
            {
                FillColor = shapeColors["Rect1"],
                Color = Color.Black,      // stroke color
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // ----- Ellipse -----
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(250, 250, 150, 100);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = shapeColors["Ellipse1"],
                Color = Color.DarkGray,
                LineWidth = 2
            };
            graph.Shapes.Add(ellipse);

            // ----- Line -----
            // Line constructor expects a float array: { x1, y1, x2, y2 }
            float[] linePoints = { 100f, 100f, 400f, 100f };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = shapeColors["Line1"], // stroke color (no fill for line)
                LineWidth = 3
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document (lifecycle rule: use doc.Save)
            doc.Save("shapes_output.pdf");
        }

        Console.WriteLine("PDF with colored shapes created: shapes_output.pdf");
    }
}