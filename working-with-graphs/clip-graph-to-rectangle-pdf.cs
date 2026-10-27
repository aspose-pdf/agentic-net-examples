using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "clipped_graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container that spans the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // NOTE: Graph does NOT expose a Clip property. To emulate clipping we can
            // add a background rectangle that defines the visible area. All subsequent
            // shapes are drawn on top of this rectangle; anything outside the rectangle
            // will simply be covered by the page background.
            Aspose.Pdf.Drawing.Rectangle clipRect = new Aspose.Pdf.Drawing.Rectangle(100, 400, 400, 700);
            clipRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.White,
                Color = Color.White // no stroke – acts as a mask
            };
            graph.Shapes.Add(clipRect);

            // -------------------------------------------------
            // Draw a large ellipse that extends beyond the clip
            // -------------------------------------------------
            // Center at (250,550) with horizontal and vertical radii of 200
            Ellipse ellipse = new Ellipse(250, 550, 200, 200);
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Color.LightBlue,
                FillColor = Color.LightBlue
            };
            graph.Shapes.Add(ellipse);

            // -------------------------------------------------
            // Draw a rectangle that partially overlaps the clip
            // -------------------------------------------------
            // Lower‑left (50,450) upper‑right (500,600)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 450, 500, 600);
            rectShape.GraphInfo = new GraphInfo
            {
                Color = Color.LightCoral,
                FillColor = Color.LightCoral
            };
            graph.Shapes.Add(rectShape);

            // Add the graph object to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF – no PreSave() required for graphics
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clipping region saved to '{outputPath}'.");
    }
}
