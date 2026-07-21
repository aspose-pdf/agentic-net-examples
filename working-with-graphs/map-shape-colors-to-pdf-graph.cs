using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph constructor expects double values (use double literals)
            Graph graph = new Graph(500.0, 400.0);

            // Map shape identifiers to fill colors (use Aspose.Pdf.Drawing.Color)
            var shapeColors = new Dictionary<string, Color>
            {
                { "rect", Color.LightGray },
                { "ellipse", Color.Yellow },
                { "circle", Color.LightBlue }
            };

            // Rectangle shape (use Aspose.Pdf.Drawing.Rectangle, not Aspose.Pdf.Rectangle)
            if (shapeColors.TryGetValue("rect", out Color rectFill))
            {
                var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 300f, 200f, 150f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = rectFill,
                    Color = Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(rect);
            }

            // Ellipse shape
            if (shapeColors.TryGetValue("ellipse", out Color ellipseFill))
            {
                var ellipse = new Ellipse(250f, 250f, 150f, 100f);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = ellipseFill,
                    Color = Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(ellipse);
            }

            // Circle shape (ellipse with equal width/height)
            if (shapeColors.TryGetValue("circle", out Color circleFill))
            {
                var circle = new Ellipse(100f, 100f, 80f, 80f);
                circle.GraphInfo = new GraphInfo
                {
                    FillColor = circleFill,
                    Color = Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(circle);
            }

            // Add the graph to the page and save the document
            page.Paragraphs.Add(graph);
            doc.Save("shapes.pdf");
        }
    }
}
