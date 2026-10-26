using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Map shape identifiers to fill colors
        var shapeFillColors = new Dictionary<string, Color>
        {
            { "rect1", Color.FromRgb(0.9f, 0.2f, 0.2f) }, // light red
            { "ellipse1", Color.FromRgb(0.2f, 0.9f, 0.2f) }, // light green
            { "rect2", Color.FromRgb(0.2f, 0.2f, 0.9f) } // light blue
        };

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ---------- Rectangle 1 ----------
            // Identifier: "rect1"
            // Original coordinates: left=100, top=700, right=250, bottom=600
            // Width = right - left = 150, Height = top - bottom = 100
            var rect1 = new Aspose.Pdf.Drawing.Rectangle(100, 600, 150, 100);
            rect1.GraphInfo = new GraphInfo
            {
                FillColor = shapeFillColors["rect1"],
                Color = Color.Black, // border color
                LineWidth = 1
            };
            graph.Shapes.Add(rect1);

            // ---------- Ellipse ----------
            // Identifier: "ellipse1"
            // Original coordinates: left=300, top=700, right=450, bottom=600
            var ellipse = new Ellipse(300, 600, 150, 100);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = shapeFillColors["ellipse1"],
                Color = Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(ellipse);

            // ---------- Rectangle 2 ----------
            // Identifier: "rect2"
            // Original coordinates: left=500, top=700, right=650, bottom=600
            var rect2 = new Aspose.Pdf.Drawing.Rectangle(500, 600, 150, 100);
            rect2.GraphInfo = new GraphInfo
            {
                FillColor = shapeFillColors["rect2"],
                Color = Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect2);

            // Add the graph (which now contains all shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
    }
}
