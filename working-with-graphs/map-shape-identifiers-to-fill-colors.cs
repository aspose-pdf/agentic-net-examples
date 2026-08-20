using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Map shape identifiers to fill colors (Aspose.Pdf.Color)
        var shapeColors = new Dictionary<string, Color>
        {
            { "Rect1", Color.LightBlue },
            { "Ellipse1", Color.LightGreen },
            { "Rect2", Color.LightCoral }
        };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Graph container – use double parameters as the obsolete float ctor is deprecated
            Graph graph = new Graph(500.0, 400.0);

            // ---------- Rectangle 1 ----------
            // Identifier: "Rect1"
            var rect1 = new Aspose.Pdf.Drawing.Rectangle(50f, 300f, 150f, 200f);
            if (shapeColors.TryGetValue("Rect1", out Color rect1Color))
            {
                rect1.GraphInfo = new GraphInfo { FillColor = rect1Color };
            }
            graph.Shapes.Add(rect1);

            // ---------- Ellipse ----------
            // Identifier: "Ellipse1"
            var ellipse = new Ellipse(200f, 250f, 100f, 150f);
            if (shapeColors.TryGetValue("Ellipse1", out Color ellipseColor))
            {
                ellipse.GraphInfo = new GraphInfo { FillColor = ellipseColor };
            }
            graph.Shapes.Add(ellipse);

            // ---------- Rectangle 2 ----------
            // Identifier: "Rect2"
            var rect2 = new Aspose.Pdf.Drawing.Rectangle(350f, 100f, 120f, 180f);
            if (shapeColors.TryGetValue("Rect2", out Color rect2Color))
            {
                rect2.GraphInfo = new GraphInfo { FillColor = rect2Color };
            }
            graph.Shapes.Add(rect2);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("shapes_mapped_colors.pdf");
        }

        Console.WriteLine("PDF created: shapes_mapped_colors.pdf");
    }
}
